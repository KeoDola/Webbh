using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.DAO;
using WebSiteBanHang.Models;
namespace WebBanHang.Controllers
{
    public class DanhSachSanPhamController : Controller
    {
        private ApplicationDbContext db;
        // GET: DanhSachSanPham
        public ActionResult Index(int? id, int page = 1, int pageSize = 4)
        {
            using (var db = new ApplicationDbContext())
            {
                if (id.HasValue)
                {
                    var tenDanhMuc = db.PhanLoaiSanPhams.Where(x => x.PhanLoaiSanPhamID == id).Select(x => x.TenPhanLoaiSanPham).FirstOrDefault();
                    int totalRecord = 0;
                    var dsSanPham = new ProductDAO().ListByCategoryId(id, ref totalRecord, page, pageSize);

                    ViewBag.Total = totalRecord;
                    ViewBag.Page = page;

                    int maxPage = 5;
                    int totalPage = 0;

                    totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
                    ViewBag.TotalPage = totalPage +1;
                    ViewBag.MaxPage = maxPage;
                    ViewBag.First = 1;
                    ViewBag.Last = totalPage;
                    ViewBag.Next = page + 1;
                    ViewBag.Prev = page - 1;

                    //var dsSanPham = db.SanPhams.Where(x => x.LoaiSanPhamID == id).OrderByDescending(x => x.SanPhamID).Take(12).ToList();
                    ViewData["dsSanPham"] = dsSanPham;
                    ViewBag.tenDanhMuc = tenDanhMuc;
                    ViewBag.danhMucSanPhamID = id;
                }
                else
                {
                    var dsSanPham = db.SanPhams.OrderByDescending(x => x.SanPhamID).Take(12).ToList();
                    ViewData["dsSanPham"] = dsSanPham;
                    ViewBag.tenDanhMuc = "Sản phẩm";
                    ViewBag.danhMucSanPhamID = id;
                }
                return View();
            }
        }
        [HttpPost]
        public ActionResult Search(int page = 1, int pageSize = 4)
        {
            using (db = new ApplicationDbContext())
            {
                string keyword = Request.Form["searchString"];
                int id = Convert.ToInt32(Request.Form["id"]);
                var tenDanhMuc = db.PhanLoaiSanPhams.Where(x => x.PhanLoaiSanPhamID == id).Select(x => x.TenPhanLoaiSanPham).FirstOrDefault();
                int totalRecord = 0;
                var dsSanPham = new ProductDAO().Search(id, keyword, ref totalRecord, page, pageSize);

                ViewBag.Total = totalRecord;
                ViewBag.Page = page;
                ViewBag.Keyword = keyword;
                int maxPage = 5;
                int totalPage = 0;

                totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
                ViewBag.TotalPage = totalPage +1;
                ViewBag.MaxPage = maxPage;
                ViewBag.First = 1;
                ViewBag.Last = totalPage;
                ViewBag.Next = page + 1;
                ViewBag.Prev = page - 1;
                ViewData["dsSanPham"] = dsSanPham;
                ViewBag.tenDanhMuc = tenDanhMuc;
                ViewBag.danhMucSanPhamID = id;
                return View("Index");
            }
        }
    }
}