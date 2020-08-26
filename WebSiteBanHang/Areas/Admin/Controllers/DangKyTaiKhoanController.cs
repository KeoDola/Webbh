using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Common;
using WebSiteBanHang.DAO;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    public class DangKyTaiKhoanController : Controller
    {
        // GET: Admin/DangKyTaiKhoan
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAccount()
        {
            var dao = new UserDAO();
            string name = Request.Form["Name"];
            string userName = Request.Form["Username"];
            string password = Request.Form["Password"];
            string password1 = Request.Form["Password1"];
            if(password != password1)
            {
                ModelState.AddModelError("", "Đăng ký tài khoản không thành công");
                return RedirectToAction("DangKy", "DangKyTaiKhoan");
            }
            string email = Request.Form["Email"];
            int phone1 = Convert.ToInt32(Request.Form["Phone1"]);
            string address = Request.Form["Address"];
            User user = new User();
            user.Name = name;
            user.UserName = userName;
            user.Password = password;
            user.Email = email;
            user.Phone = phone1;
            user.Address = address;
            var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
            user.PasswordSalt = encryptedMd5Pas;
            if (user.GroupID == null)
            {
                user.GroupID = 3;
            }
            user.Status = true;
            long id = dao.Insert(user);
            if (id > 0)
            {
                SetAlert("Đăng ký tài khoản thành công", "success");
                return RedirectToAction("DangKy", "DangKyTaiKhoan");
            }
            else
            {
                ModelState.AddModelError("", "Đăng ký tài khoản không thành công");
            }
            return View("DangKy");
        }
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}