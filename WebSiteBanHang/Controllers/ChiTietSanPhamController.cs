using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;
using System.Data.Entity;


namespace WebBanHang.Controllers
{
    public class ChiTietSanPhamController : Controller
    {
        // GET: ChiTietSanPham
        public ActionResult Index(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var sanPham = db.SanPhams.Include(x => x.CTSanPhams).Where(x => x.SanPhamID == id).FirstOrDefault();
                var ctSanPham = db.CTSanPhams.SingleOrDefault(x => x.SanPhamID == id);
                var dsSanPhamLienQuan = new List<SanPham>();
                if (sanPham.LoaiSanPhamID != null)
                {
                    dsSanPhamLienQuan = db.SanPhams.Where(x => x.LoaiSanPhamID == sanPham.LoaiSanPhamID).Take(8).ToList();

                }
                if (sanPham == null)
                    return View("Error");
                ViewData["sanPham"] = sanPham;
                ViewData["ctSanPham"] = ctSanPham;
                ViewData["dsSanPhamLienQuan"] = dsSanPhamLienQuan;
                return View();
            }
        }
    }
}