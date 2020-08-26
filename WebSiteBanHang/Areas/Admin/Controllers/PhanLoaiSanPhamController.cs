using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;
using WebSiteBanHang.Common;
using WebSiteBanHang.DAO;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    public class PhanLoaiSanPhamController : BaseController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/DanhMucSanPham
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new PhanLoaiSanPhamDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create(bool? choose)
        {
            SetViewBag(null, choose);
            return View();
        }

        [HttpGet]
        public ActionResult CreateFollowCategory()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var phanLoaiSanPham = new PhanLoaiSanPhamDAO().ViewDetail(id);
            bool? choose = null;
            if (phanLoaiSanPham != null)
            {
                bool? isFale, isFemale, isChildren;
                isFale = db.DanhMucSanPhams.SingleOrDefault(x => x.DanhMucSanPhamID == phanLoaiSanPham.DanhMucSanPhamID).LaDoNam;
                isFemale = db.DanhMucSanPhams.SingleOrDefault(x => x.DanhMucSanPhamID == phanLoaiSanPham.DanhMucSanPhamID).LaDoNu;
                isChildren = db.DanhMucSanPhams.SingleOrDefault(x => x.DanhMucSanPhamID == phanLoaiSanPham.DanhMucSanPhamID).LaDoTreEm;
                if (isFale == true && isFemale == null && isChildren == null)
                {
                    choose = true;
                }
                else if (isFale == null && isFemale == true && isChildren == null)
                {
                    choose = false;
                }
                else if (isFale == true && isFemale == true && isChildren == true)
                {
                    choose = null;
                }
            }
            SetViewBag(phanLoaiSanPham.PhanLoaiSanPhamID, choose);
            return View(phanLoaiSanPham);
        }

        [HttpPost]
        public ActionResult Edit(PhanLoaiSanPham phanLoaiSanPham)
        {
            if (ModelState.IsValid)
            {
                var dao = new PhanLoaiSanPhamDAO();

                var result = dao.Update(phanLoaiSanPham);
                if (result)
                {
                    SetAlert("Sửa danh mục sản phẩm thành công", "success");
                    return RedirectToAction("Index", "PhanLoaiSanPham");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật danh mục sản phẩm không thành công");
                }
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult Create(PhanLoaiSanPham phanLoaiSanPham)
        {
            if (ModelState.IsValid)
            {
                var currentCulture = Session[CommonConstant.CurrentCulture];
                var id = new PhanLoaiSanPhamDAO().Insert(phanLoaiSanPham);
                if (id > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", StaticResources.Resources.InsertCategoryFailed);
                }
            }
            return View(phanLoaiSanPham);
        }
        [HttpDelete]
        //[HasCredential(RoleID = "DELETE_USER")]
        public ActionResult Delete(int id)
        {
            new PhanLoaiSanPhamDAO().Delete(id);

            return RedirectToAction("Index");
        }
        public void SetViewBag(int? selectedId = null, bool? choose = null)
        {
            var dao = new DanhMucSanPhamDAO();
            ViewBag.DanhMucSanPhamID = new SelectList(dao.ListAll(choose), "DanhMucSanPhamID", "TenDanhMucSanPham", selectedId);
        }
    }
}