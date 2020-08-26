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

    public class UserController : BaseController
    {
        //// GET: Admin/User
        //[HasCredential(Roles = "VIEW_USER")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var sessionUser = Session[Common.CommonConstant.USER_SESSION] as UserLogin;
            var dao = new UserDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            if(sessionUser.GroupID == 2)
            {
                SetAlert("Bạn không có quyền truy cập vào trang này", "warning");
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View(model);
            }
        }
        [HttpGet]
        //[HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            var sessionUser = Session[Common.CommonConstant.USER_SESSION] as UserLogin;
            SetViewBag();
            if (sessionUser.GroupID == 2)
            {
                SetAlert("Bạn không có quyền truy cập vào trang này", "warning");
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View();
            }
            
        }
        [HttpGet]
        //[HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(int id)
        {
            var sessionUser = Session[Common.CommonConstant.USER_SESSION] as UserLogin;
            var user = new UserDAO().ViewDetail(id);
            SetViewBag(user.GroupID);
            if (sessionUser.GroupID == 2)
            {
                SetAlert("Bạn không có quyền truy cập vào trang này", "warning");
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View(user);
            }
        }

        [HttpPost]
        //[HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();

                var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                user.PasswordSalt = encryptedMd5Pas;
                if (user.GroupID == null)
                {
                    user.GroupID = 3;
                }
                if (user.Status != true && user.Status != false)
                {
                    user.Status = true;
                }
                long id = dao.Insert(user);
                if (id > 0)
                {
                    SetAlert("Thêm user thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm user không thành công");
                }
            }
            return View("Index");
        }

        [HttpPost]
        //[HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(User user)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            User us = db.Users.Find(user.UserID);
            user.Password = us.Password;
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                    user.PasswordSalt = encryptedMd5Pas;
                }

                var result = dao.Update(user);
                if (result)
                {
                    SetAlert("Sửa user thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật user không thành công");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        //[HasCredential(RoleID = "DELETE_USER")]
        public ActionResult Delete(int id)
        {
            new UserDAO().Delete(id);

            return RedirectToAction("Index");
        }
        [HttpPost]
        //[HasCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        public void SetViewBag(int? selectedId = null)
        {
            var dao = new UserGroupDAO();
            ViewBag.GroupID = new SelectList(dao.ListAll(), "UserGroupID", "GroupName", selectedId);
        }
    }
}