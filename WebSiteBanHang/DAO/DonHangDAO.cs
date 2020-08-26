using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.DAO
{
    public class DonHangDAO
    {
        ApplicationDbContext db = null;
        private static DonHangDAO instance;
        static object key = new object();
        public static DonHangDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)//bất đồng bộ , chiếm dụng tài nguyên....
                    {
                        instance = new DonHangDAO();
                    }
                }
                return instance;
            }
        }
        public DonHangDAO()
        {
            db = new ApplicationDbContext();
        }
        //public CTSanPham ViewDetailBill(int donHangID)
        //{
        //    var ctDonHang = db.CTDo.SingleOrDefault(x => x.SanPhamID == donHangID);
        //    return ctSanPham;
        //}
        public int Insert(DonHang entity)
        {
            entity.Status = false;
            db.DonHangs.Add(entity);
            db.SaveChanges();
            return entity.DonHangID;
        }
        public List<DonHang> ListNewBill(int top)
        {
            return db.DonHangs.OrderByDescending(x => x.NgayNhan).Take(top).ToList();
        }
        //public List<string> ListName(string keyword)
        //{
        //    return db.DonHangs.Where(x => x..Contains(keyword)).Select(x => x.TenSanPham).ToList();
        //}
        public IEnumerable<DonHang> ListAllpaging(string searchString, int page, int pageSize, DateTime? tuNgayNhan = null, DateTime? denNgayNhan = null, DateTime? tungayXuat = null, DateTime? denngayXuat = null)
        {
            IQueryable<DonHang> model = db.DonHangs;
            if (tuNgayNhan != null)
            {
                model = model.Where(x => x.NgayNhan >= tuNgayNhan);
            }
            if (denNgayNhan != null)
                model = model.Where(x => x.NgayNhan <= denNgayNhan);
            if (tungayXuat != null)
                model = model.Where(x => x.NgayXuat >= tungayXuat);
            if (tungayXuat != null)
                model = model.Where(x => x.NgayXuat >= denngayXuat);
            //Laays thang gan nhat
            //if (tuNgayNhan == null && denNgayNhan == null && tungayXuat == null && denngayXuat == null)
            //{

            //    DateTime today = DateTime.Now;
            //    tuNgayNhan = new DateTime(today.Year, today.Month, 1);
            //    denNgayNhan = tuNgayNhan.Value.AddMonths(1).AddDays(-1);
            //    model = model.Where(x => x.NgayNhan >= tuNgayNhan && x.NgayNhan <= denNgayNhan);
            //}
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.NhanVien.HoTen.Contains(searchString) || x.KhachHang.HoTen.Contains(searchString));

            }

            return model.OrderByDescending(x => x.DonHangID).ToPagedList(page, pageSize);
        }


        public bool Update(DonHang entity)
        {
            try
            {
                var donHang = db.DonHangs.Find(entity.DonHangID);
                donHang.NgayXuat = entity.NgayXuat;
                donHang.GhiChu = entity.GhiChu;
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
                var donHang = db.DonHangs.Find(id);
                var sanPhamDonHang = db.SanPhamDonHangs.Where(x => x.DonHangID == donHang.DonHangID).ToList();
                db.SanPhamDonHangs.RemoveRange(sanPhamDonHang);
                db.DonHangs.Remove(donHang);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public DonHang ViewDetail(int donHangID)
        {
            return db.DonHangs.Find(donHangID);
        }
        public bool ChangeStatus(long id)
        {
            var dh = db.DonHangs.Find(id);
            dh.Status = !dh.Status;
            db.SaveChanges();
            return dh.Status;
        }
    }
}
