using BotDetect.Web.Mvc;
using CaptchaMvc.HtmlHelpers;
using Facebook;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using WebSiteBanHang.Common;
using WebSiteBanHang.DAO;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class UsersController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
        }
        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });


            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information, like email, first name, middle name etc
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string userName = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;

                var user = new User();
                if(email == null)
                {
                    email = "facebook@gmail.com";
                }
                user.Password = "123";
                user.PasswordSalt = Encryptor.MD5Hash(user.Password);
                user.Email = email;
                user.UserName = email;
                user.Status = true;
                user.Name = firstname + " " + middlename + " " + lastname;
                user.CreatedDate = DateTime.Now;
                var resultInsert = new UserDAO().InsertForFacebook(user);
                if (resultInsert > 0)
                {
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.UserID;
                    Session.Add(CommonConstant.USER_SESSION, userSession);
                }
            }
            return Redirect("/");
        }
        public ActionResult Logout()
        {
            Session[CommonConstant.USER_SESSION] = null;
            return Redirect("/");
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetByID(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.UserID;
                    Session.Add(CommonConstant.USER_SESSION, userSession);
                    return Redirect("/");
                }
                else if (result == 0)
                    ModelState.AddModelError("", "Tài khoản không tồn tại!");
                else if (result == -1)
                    ModelState.AddModelError("", "Tài khoản đang bị khóa!");
                else if (result == -2)
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                else
                    ModelState.AddModelError("", "Đăng nhập không đúng.");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (!this.IsCaptchaValid(""))
                {
                    ViewBag.error = "Bạn phải nhập mã sau ";
                    return View("Register");
                }
                var dao = new UserDAO();
                if (dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new User();
                    user.UserName = model.UserName;
                    user.Name = model.Name;
                    user.Password = model.Password;
                    user.PasswordSalt = Encryptor.MD5Hash(model.Password);
                    user.Phone = model.Phone;
                    user.Email = model.Email;
                    user.Address = model.Address;
                    user.DiaChiChiTiet = model.DiaChiChiTiet;
                    user.CreatedDate = DateTime.Now;
                    user.Status = true;
                    if (!string.IsNullOrEmpty(model.ProvinceID))
                    {
                        user.ThanhPhoID = int.Parse(model.ProvinceID);
                    }
                    if (!string.IsNullOrEmpty(model.DistrictID))
                    {
                        user.QuanHuyenID = int.Parse(model.DistrictID);
                    }
                    var result = dao.Insert(user);
                    if (result > 0)
                    {
                        ViewBag.Success = "Đăng ký thành công";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công.");
                    }
                }
            }
            return View(model);
        }
        //public JsonResult LoadProvince()
        //{
        //    var xmlDoc = XDocument.Load(Server.MapPath(@"~/assets/client/data/Provinces_Data.xml"));

        //    var xElements = xmlDoc.Element("Root").Elements("Item").Where(x => x.Attribute("type").Value == "province");
        //    var list = new List<ProvinceModel>();
        //    ProvinceModel province = null;
        //    foreach (var item in xElements)
        //    {
        //        province = new ProvinceModel();
        //        province.ID = int.Parse(item.Attribute("id").Value);
        //        province.Name = item.Attribute("value").Value;
        //        list.Add(province);

        //    }
        //    return Json(new
        //    {
        //        data = list,
        //        status = true
        //    });
        //}
        //public JsonResult LoadDistrict(int provinceID)
        //{
        //    var xmlDoc = XDocument.Load(Server.MapPath(@"~/assets/client/data/Provinces_Data.xml"));

        //    var xElement = xmlDoc.Element("Root").Elements("Item")
        //        .Single(x => x.Attribute("type").Value == "province" && int.Parse(x.Attribute("id").Value) == provinceID);

        //    var list = new List<DistrictModel>();
        //    DistrictModel district = null;
        //    foreach (var item in xElement.Elements("Item").Where(x => x.Attribute("type").Value == "district"))
        //    {
        //        district = new DistrictModel();
        //        district.ID = int.Parse(item.Attribute("id").Value);
        //        district.Name = item.Attribute("value").Value;
        //        //district.Th = int.Parse(xElement.Attribute("id").Value);
        //        list.Add(district);

        //    }
        //    return Json(new
        //    {
        //        data = list,
        //        status = true
        //    });
        //}
        //public JsonResult LoadWards(int provinceID)
        //{
        //    var xmlDoc = XDocument.Load(Server.MapPath(@"~/assets/client/data/Provinces_Data.xml"));

        //    var xElement = xmlDoc.Element("Root").Elements("Item")
        //        .Single(x => x.Attribute("type").Value == "province" && int.Parse(x.Attribute("id").Value) == provinceID);

        //    var list = new List<XaPhuong>();
        //    XaPhuong wards = null;
        //    foreach (var item in xElement.Elements("Item").Where(x => x.Attribute("type").Value == "district"))
        //    {
        //        wards = new XaPhuong();
        //        wards.XaPhuongID = int.Parse(item.Attribute("id").Value);
        //        wards.TenXaPhuong = item.Attribute("value").Value;
        //        //district.ProvinceID = int.Parse(xElement.Attribute("id").Value);
        //        list.Add(wards);

        //    }
        //    return Json(new
        //    {
        //        data = list,
        //        status = true
        //    });
        //}
    }
}