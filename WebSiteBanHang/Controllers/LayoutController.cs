using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebBanHang.Controllers
{
    public class LayoutController : Controller
    {
        public ActionResult Header()
        {
            var db = new ApplicationDbContext();
            var danhMucSanPhamDoNam = db.PhanLoaiSanPhams.Where(x => x.DanhMucSanPham.LaDoNam == true && (x.DanhMucSanPham.LaDoNu == false || x.DanhMucSanPham.LaDoNu ==null)).ToList();
            ViewData["danhMucSanPhamDoNam"] = danhMucSanPhamDoNam;
            var danhMucSanPhamDoNu = db.PhanLoaiSanPhams.Where(x => x.DanhMucSanPham.LaDoNu == true && (x.DanhMucSanPham.LaDoNam ==false || x.DanhMucSanPham.LaDoNam == null)).ToList();
            ViewData["danhMucSanPhamDoNu"] = danhMucSanPhamDoNu;
            GioHang sessionGioHang = Session["GioHang"] as GioHang;
            if (sessionGioHang == null)
                ViewBag.SoLuong = 0;
            else
                ViewBag.SoLuong = sessionGioHang.TongSoLuong;
            return View();
        }

        public ActionResult Footer()
        {
            return View();
        }
    }
}