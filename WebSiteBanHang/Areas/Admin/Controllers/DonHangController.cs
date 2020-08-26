using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.DAO;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    public class DonHangController : BaseController
    {
        private ApplicationDbContext db;
        // GET: Admin/DonHang
        [HttpGet]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10, DateTime? tuNgayNhan = null, DateTime? denNgayNhan = null, DateTime? tuNgayXuat = null, DateTime? denNgayXuat = null)
        {
            var dao = new DonHangDAO();
            if (tuNgayNhan == null && denNgayNhan == null && tuNgayXuat == null && denNgayXuat == null)
            {
                DateTime today = DateTime.Now;
                tuNgayNhan = new DateTime(today.Year, today.Month, 1);
                denNgayNhan = tuNgayNhan.Value.AddMonths(1).AddDays(-1);
            }
            var model = dao.ListAllpaging(searchString, page, pageSize, tuNgayNhan, denNgayNhan, tuNgayXuat, denNgayXuat);
            ViewBag.TongTien = 0;
            foreach (var item in model)
            {
                ViewBag.TongTien += item.GiaTriDonHang;
            }
            ViewBag.SearchString = searchString;
            ViewBag.TuNgayNhan = tuNgayNhan;
            ViewBag.DenNgayNhan = denNgayNhan;
            ViewBag.TuNgayXuat = tuNgayXuat;
            ViewBag.DenNgayXuat = denNgayXuat;
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var donHang = DonHangDAO.Instance.ViewDetail(id);
            return View(donHang);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new DonHangDAO().Delete(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                var dao = new DonHangDAO();

                var result = dao.Update(donHang);
                if (result)
                {
                    SetAlert("Sửa đơn hàng thành công", "success");
                    return RedirectToAction("Index", "DonHang");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật đơn hàng không thành công");
                }
            }
            return View("Index");
        }
        [HttpPost]
        //[HasCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new DonHangDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        public ActionResult ShowModalSanPhamDonHang(int donHangID)
        {
            db = new ApplicationDbContext();
            var dao = new DonHangDAO();
            DonHang donHang = dao.ViewDetail(donHangID);
            var lstSanPhamDonHang = db.SanPhamDonHangs.Where(x => x.DonHangID == donHang.DonHangID).OrderByDescending(x => x.SanPhamDonHangID);
            ViewBag.GhiChu = donHang.GhiChu;
            ViewBag.DonHangID = donHangID;
            ViewBag.GiaTriDonHang = donHang.GiaTriDonHang;
            return View(lstSanPhamDonHang);
        }
    }
}