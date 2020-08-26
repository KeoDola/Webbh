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
    public class NhanVienController : BaseController
    {
        // GET: Admin/NhanVien
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new NhanVienDAO();
            var model = dao.ListAllpaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var nhanVien = new NhanVienDAO().ViewDetail(id);
            SetViewBag(nhanVien.UserID);
            return View(nhanVien);
        }

        [HttpPost]
        public ActionResult Edit(NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                var dao = new NhanVienDAO();

                var result = dao.Update(nhanVien);
                if (result)
                {
                    SetAlert("Sửa danh mục sản phẩm thành công", "success");
                    return RedirectToAction("Index", "NhanVien");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật danh mục sản phẩm không thành công");
                }
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult Create(NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                var currentCulture = Session[CommonConstant.CurrentCulture];
                var id = new NhanVienDAO().Insert(nhanVien);
                if (id > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", StaticResources.Resources.InsertCategoryFailed);
                }
            }
            return View(nhanVien);
        }
        [HttpDelete]
        //[HasCredential(RoleID = "DELETE_USER")]
        public ActionResult Delete(int id)
        {
            new NhanVienDAO().Delete(id);

            return RedirectToAction("Index");
        }
        [HttpPost]
        //[HasCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new NhanVienDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        public void SetViewBag(int? selectedId = null)
        {
            var dao = new UserDAO();
            ViewBag.UserID = new SelectList(dao.ListAll(), "UserID", "UserName", selectedId);
        }
    }
}