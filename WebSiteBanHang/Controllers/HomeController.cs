using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebBanHang.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult HamLayDulieu(string hoten, string diachi)
        {
            return View();
        }
        public ActionResult Index()
        {
            var db = new ApplicationDbContext();
            var dsSanPhamMoi = db.SanPhams.OrderByDescending(x => x.NgayTao).Take(12).ToList();
            ViewData["dsSanPhamMoi"] = dsSanPhamMoi;
            var dsSanPhamBanChay = db.SanPhams.Where(x => x.Hot == true).Take(8).ToList();
            ViewData["dsSanPhamBanChay"] = dsSanPhamBanChay;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}