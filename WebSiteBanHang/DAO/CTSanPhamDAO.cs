using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.DAO
{
    public class CTSanPhamDAO
    {

        ApplicationDbContext db = null;
        private static CTSanPhamDAO instance;
        static object key = new object();
        public static CTSanPhamDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)//bất đồng bộ , chiếm dụng tài nguyên....
                    {
                        instance = new CTSanPhamDAO();
                    }
                }
                return instance;
            }
        }
        public CTSanPhamDAO()
        {
            db = new ApplicationDbContext();
        }
        public long Insert(CTSanPham entity)
        {
            db.CTSanPhams.Add(entity);
            db.SaveChanges();
            return entity.CTSanPhamID;
        }
        public bool Update(CTSanPham entity)
        {
            try
            {
                var ctSanPham = db.CTSanPhams.Find(entity.CTSanPhamID);
                ctSanPham.SoLuong = entity.SoLuong;
                ctSanPham.AnhSanPham = entity.AnhSanPham;
                ctSanPham.ChiTiet = entity.ChiTiet;
                ctSanPham.GhiChu = entity.GhiChu;
                ctSanPham.ChiTiet = entity.ChiTiet;
                ctSanPham.GiamGia = entity.GiamGia;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var cTSanPham = db.CTSanPhams.Find(id);
                db.CTSanPhams.Remove(cTSanPham);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public CTSanPham ViewDetail(int ctSanPhamID)
        {
            return db.CTSanPhams.Find(ctSanPhamID);
        }
    }
}