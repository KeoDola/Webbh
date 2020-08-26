using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Common;
using WebSiteBanHang.DAO;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class ThanhToanController : Controller
    {
        private ApplicationDbContext db;
        // GET: ThanhToans
        public ActionResult Index()
        {
            var sessionGioHang = Session["GioHang"];
            if (sessionGioHang == null)
                sessionGioHang = new GioHang();
            return View(sessionGioHang);
        }
        [HttpPost]
        public ActionResult HoanThanhThanhToan()
        {
            db = new ApplicationDbContext();
            KhachHang kh = new KhachHang();
            DonHang dh = new DonHang();
            SanPhamDonHang spdh = new SanPhamDonHang();
            var sessionGioHang = Session["GioHang"] as GioHang;
            //foreach ra mot loat cac san pham id 
            var User = Session[Common.CommonConstant.USER_SESSION] as UserLogin;
            kh.HoTen = Request.Form["hoTen"];
            kh.SoDienThoai = Convert.ToInt32(Request.Form["soDienThoai"]);
            kh.Email = Request.Form["email"];
            kh.DiaChi = Request.Form["diaChi"];
            if (Request.Form["gioiTinh"] != null && (Request.Form["gioiTinh"] == "true" | Request.Form["gioiTinh"] == "false"))
            {
                kh.GioiTinh = Convert.ToBoolean(Request.Form["gioiTinh"]);
            }
            else
            {
                kh.GioiTinh = true;
            }
            dh.NgayNhan = Convert.ToDateTime(Request.Form["ngayNhan"]);
            dh.DiaChiNhanHangChiTiet = Request.Form["diaChiChiTiet"];
            dh.GhiChu = Request.Form["ghiChu"];
            if (User == null)
            {
                //Lay thong tin khach hang tu User roi cap nhat lai thong tin khach hang
                //update khach hang, cam khach hang id vao bang don hang 
                //
                //
                int khachHangID = KhachHangDAO.Instance.insertKhachHang(kh);
                //
                dh.KhachHangID = khachHangID;
                dh.Status = false;
                dh.GiaTriDonHang = sessionGioHang.TongTien;
                //
                int donHangID = DonHangDAO.Instance.Insert(dh);
                //
                spdh.DonHangID = donHangID;
                foreach (var item in sessionGioHang.Gio)
                {
                    spdh.SanPhamID = item.SanPham.SanPhamID;
                    spdh.SoLuong = item.SoLuong;
                    SanPhamDonHangDAO.Instance.insertSanPhamDonHang(spdh);
                }
            }
            else
            {
                var KhachHang = db.KhachHangs.SingleOrDefault(x => x.UserID == User.UserID);
                if (KhachHang != null)
                {
                    KhachHang.HoTen = kh.HoTen;
                    KhachHang.SoDienThoai = kh.SoDienThoai;
                    KhachHang.Email = kh.Email;
                    KhachHang.DiaChi = kh.DiaChi;
                    KhachHang.GioiTinh = kh.GioiTinh;
                    KhachHangDAO.Instance.Update(KhachHang);
                    dh.KhachHangID = KhachHang.KhachHangID;
                }
                else
                {
                    kh.UserID = Convert.ToInt32(User.UserID);
                    int khachHangID = KhachHangDAO.Instance.insertKhachHang(kh);
                    dh.KhachHangID = khachHangID;
                }
                dh.Status = true;
                dh.GiaTriDonHang = sessionGioHang.TongTien;
                int donHangID = DonHangDAO.Instance.Insert(dh);
                spdh.DonHangID = donHangID;
                foreach (var item in sessionGioHang.Gio)
                {
                    spdh.SanPhamID = item.SanPham.SanPhamID;
                    spdh.SoLuong = item.SoLuong;
                    SanPhamDonHangDAO.Instance.insertSanPhamDonHang(spdh);
                }
            }

            ViewBag.DonHangID = dh.DonHangID;
            string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/neworder.html"));

            content = content.Replace("{{CustomerName}}", kh.HoTen);
            content = content.Replace("{{Phone}}", kh.SoDienThoai.ToString());
            content = content.Replace("{{Email}}", kh.Email);
            content = content.Replace("{{Address}}", kh.DiaChi);
            content = content.Replace("{{Total}}", dh.GiaTriDonHang.ToString());
            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

            new Mailhelper().SendMail(kh.Email, "Đơn hàng mới từ website", content);
            new Mailhelper().SendMail(toEmail, "Đơn hàng mới từ website", content);
            return View();
        }
    }
}