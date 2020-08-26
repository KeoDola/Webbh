using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.DAO
{
    public class NhanVienDAO
    {
        ApplicationDbContext db = null;
        private static NhanVienDAO instance;
        static object key = new object();
        public static NhanVienDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        instance = new NhanVienDAO();
                    }
                }
                return instance;
            }
        }
        public NhanVienDAO()
        {
            db = new ApplicationDbContext();
        }

        public long Insert(NhanVien entity)
        {
            db.NhanViens.Add(entity);
            db.SaveChanges();
            return entity.NhanVienID;
        }
        public bool Update(NhanVien entity)
        {
            try
            {
                var nhanVien = db.NhanViens.Find(entity.NhanVienID);
                nhanVien.UserID = entity.UserID;
                nhanVien.HoTen = entity.HoTen;
                nhanVien.NgaySinh = entity.NgaySinh;
                nhanVien.SoDienThoai1 = entity.SoDienThoai1;
                nhanVien.SoDienThoai2 = entity.SoDienThoai2;
                nhanVien.Email = entity.Email;
                nhanVien.DiaChi = entity.DiaChi;
                nhanVien.QueQuan = entity.QueQuan;
                nhanVien.GhiChu = entity.GhiChu;
                nhanVien.GioiTinh = entity.GioiTinh;
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
                var nhanVien = db.NhanViens.Find(id);
                db.NhanViens.Remove(nhanVien);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public NhanVien ViewDetail(int nhanVienID)
        {
            return db.NhanViens.Find(nhanVienID);
        }
        public IEnumerable<NhanVien> ListAllpaging(string searchString, int page, int pageSize)
        {
            IQueryable<NhanVien> model = db.NhanViens;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.HoTen.Contains(searchString) || x.QueQuan.Contains(searchString) || x.Email.Contains(searchString) || x.DiaChi.Contains(searchString));
            }
            return model.OrderByDescending(x => x.NhanVienID).ToPagedList(page, pageSize);
        }
        public bool ChangeStatus(long id)
        {
            var nhanVien = db.NhanViens.Find(id);
            nhanVien.GioiTinh = !nhanVien.GioiTinh;
            db.SaveChanges();
            return nhanVien.GioiTinh;
        }
    }
}