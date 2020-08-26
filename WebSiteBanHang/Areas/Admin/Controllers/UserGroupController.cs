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
    public class UserGroupController : BaseController
    {
        // GET: Admin/UserGroupGroup
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var sessionUser = Session[Common.CommonConstant.USER_SESSION] as UserLogin;
            var dao = new UserGroupDAO();
            var model = dao.ListAllpaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            if (sessionUser.GroupID == 2)
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
            var userGroup = new UserGroupDAO().ViewDetail(id);
            if (sessionUser.GroupID == 2)
            {
                SetAlert("Bạn không có quyền truy cập vào trang này", "warning");
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View(userGroup);
            }
        }

        [HttpPost]
        //[HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(UserGroup userGroup)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserGroupDAO();
                long id = dao.Insert(userGroup);
                if (id > 0)
                {
                    SetAlert("Thêm userGroup thành công", "success");
                    return RedirectToAction("Index", "UserGroup");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm userGroup không thành công");
                }
            }
            return View("Index");
        }

        [HttpPost]
        //[HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(UserGroup userGroup)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserGroupDAO();
                var result = dao.Update(userGroup);
                if (result)
                {
                    SetAlert("Sửa userGroup thành công", "success");
                    return RedirectToAction("Index", "UserGroup");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật userGroup không thành công");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        //[HasCredential(RoleID = "DELETE_USER")]
        public ActionResult Delete(int id)
        {
            new UserGroupDAO().Delete(id);

            return RedirectToAction("Index");
        }
    }
}