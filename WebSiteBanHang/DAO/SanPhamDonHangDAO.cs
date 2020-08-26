using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.DAO
{
    public class SanPhamDonHangDAO
    {
        ApplicationDbContext db = null;
        private static SanPhamDonHangDAO instance;
        static object key = new object();
        public static SanPhamDonHangDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)//bất đồng bộ , chiếm dụng tài nguyên....
                    {
                        instance = new SanPhamDonHangDAO();
                    }
                }
                return instance;
            }
        }
        public SanPhamDonHangDAO() { db = new ApplicationDbContext(); }
        public int insertSanPhamDonHang(SanPhamDonHang spdh)
        {
            try
            {
                db.SanPhamDonHangs.Add(spdh);
                db.SaveChanges();
                return spdh.SanPhamDonHangID;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public bool Update(SanPhamDonHang entity)
        {
            try
            {
                var spdHang = db.SanPhamDonHangs.Find(entity.SanPhamDonHangID);
                spdHang.DonHangID = entity.DonHangID;
                spdHang.SanPhamID = entity.SanPhamID;
                spdHang.SoLuong = entity.SoLuong;
                spdHang.GhiChu = entity.GhiChu;
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
                var spdHang = db.SanPhamDonHangs.Find(id);
                db.SanPhamDonHangs.Remove(spdHang);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SanPhamDonHang ViewDetail(int spdHangID)
        {
            return db.SanPhamDonHangs.Find(spdHangID);
        }
        public IEnumerable<SanPhamDonHang> ListAllpaging(string searchString, int page, int pageSize)
        {
            IQueryable<SanPhamDonHang> model = db.SanPhamDonHangs;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.GhiChu.Contains(searchString));
            }
            return model.OrderByDescending(x => x.SanPhamDonHangID).ToPagedList(page, pageSize);
        }
    }
}