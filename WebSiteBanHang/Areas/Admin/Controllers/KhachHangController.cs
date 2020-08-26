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
    public class KhachHangController : BaseController
    {
        // GET: Admin/KhachHang
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new KhachHangDAO();
            var model = dao.ListAllpaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var khacHang = new KhachHangDAO().ViewDetail(id);
            return View(khacHang);
        }

        [HttpPost]
        public ActionResult Edit(KhachHang khacHang)
        {
            if (ModelState.IsValid)
            {
                var dao = new KhachHangDAO();

                var result = dao.Update(khacHang);
                if (result)
                {
                    SetAlert("Sửa thông tin khách hàng thành công", "success");
                    return RedirectToAction("Index", "KhachHang");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thông tin khách hàng không thành công");
                }
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult Create(KhachHang khacHang)
        {
            if (ModelState.IsValid)
            {
                var currentCulture = Session[CommonConstant.CurrentCulture];
                var id = new KhachHangDAO().insertKhachHang(khacHang);
                if (id != 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", StaticResources.Resources.InsertCategoryFailed);
                }
            }
            return View(khacHang);
        }
        [HttpDelete]
        //[HasCredential(RoleID = "DELETE_USER")]
        public ActionResult Delete(int id)
        {
            new KhachHangDAO().Delete(id);

            return RedirectToAction("Index");
        }
        [HttpPost]
        //[HasCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new KhachHangDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
