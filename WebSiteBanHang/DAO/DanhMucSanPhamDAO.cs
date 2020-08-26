using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.DAO
{
    public class DanhMucSanPhamDAO
    {
        ApplicationDbContext db = null;
        private static DanhMucSanPhamDAO instance;
        static object key = new object();
        public static DanhMucSanPhamDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)//bất đồng bộ , chiếm dụng tài nguyên....
                    {
                        instance = new DanhMucSanPhamDAO();
                    }
                }
                return instance;
            }
        }
        public DanhMucSanPhamDAO()
        {
            db = new ApplicationDbContext();
        }
        public long Insert(DanhMucSanPham danhMucSanPham)
        {
            danhMucSanPham.Status = true;
            db.DanhMucSanPhams.Add(danhMucSanPham);
            db.SaveChanges();
            return danhMucSanPham.DanhMucSanPhamID;
        }

        public bool Update(DanhMucSanPham entity)
        {
            try
            {
                var danhMucSanPham = db.DanhMucSanPhams.Find(entity.DanhMucSanPhamID);
                danhMucSanPham.TenDanhMucSanPham = entity.TenDanhMucSanPham;
                danhMucSanPham.GhiChu = entity.GhiChu;
                danhMucSanPham.Status = entity.Status;
                danhMucSanPham.LaDoNam = entity.LaDoNam;
                danhMucSanPham.LaDoNu = entity.LaDoNu;
                danhMucSanPham.LaDoTreEm = entity.LaDoTreEm;
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
                var danhMucSanPham = db.DanhMucSanPhams.Find(id);
                var lstPhanLoaiSanPham = db.PhanLoaiSanPhams.Where(x => x.DanhMucSanPhamID == danhMucSanPham.DanhMucSanPhamID).ToList();
                db.PhanLoaiSanPhams.RemoveRange(lstPhanLoaiSanPham);
                db.DanhMucSanPhams.Remove(danhMucSanPham);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool ChangeStatus(int id)
        {
            var danhMucSanPham = db.DanhMucSanPhams.Find(id);
            danhMucSanPham.Status = !danhMucSanPham.Status;
            db.SaveChanges();
            return danhMucSanPham.Status;
        }

        public IEnumerable<DanhMucSanPham> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<DanhMucSanPham> model = db.DanhMucSanPhams;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenDanhMucSanPham.Contains(searchString) || x.GhiChu.Contains(searchString));

            }
            return model.OrderByDescending(x => x.DanhMucSanPhamID).ToPagedList(page, pageSize);
        }

        public List<DanhMucSanPham> ListAll()
        {
            return db.DanhMucSanPhams.Where(x => x.Status == true).ToList();
        }

        public DanhMucSanPham ViewDetail(long id)
        {
            return db.DanhMucSanPhams.Find(id);
        }
        public bool ChangeStatus(long id)
        {
            var dm = db.DanhMucSanPhams.Find(id);
            dm.Status = !dm.Status;
            db.SaveChanges();
            return dm.Status;
        }


        public List<DanhMucSanPham> ListAll(bool? choose = null)
        {
            if (choose == true)
            {
                return db.DanhMucSanPhams.Where(x => x.LaDoNam == true && x.LaDoNu == null && x.LaDoTreEm == null).ToList();
            }
            else if (choose == false)
            {
                return db.DanhMucSanPhams.Where(x => x.LaDoNam == null && x.LaDoNu == true && x.LaDoTreEm == null).ToList();
            }
            else
            {
                return db.DanhMucSanPhams.Where(x => x.LaDoNam == true && x.LaDoNu == true && x.LaDoTreEm == true).ToList();
            }
        }

    }
}