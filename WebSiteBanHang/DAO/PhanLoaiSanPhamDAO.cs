using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.DAO
{
    public class PhanLoaiSanPhamDAO
    {
        ApplicationDbContext db = null;
        private static PhanLoaiSanPhamDAO instance;
        static object key = new object();
        public static PhanLoaiSanPhamDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)//bất đồng bộ , chiếm dụng tài nguyên....
                    {
                        instance = new PhanLoaiSanPhamDAO();
                    }
                }
                return instance;
            }
        }
        public PhanLoaiSanPhamDAO()
        {
            db = new ApplicationDbContext();
        }
        public long Insert(PhanLoaiSanPham phanLoaiSanPham)
        {
            db.PhanLoaiSanPhams.Add(phanLoaiSanPham);
            db.SaveChanges();
            return phanLoaiSanPham.PhanLoaiSanPhamID;
        }

        public bool Update(PhanLoaiSanPham entity)
        {
            try
            {
                var phanLoaiSanPham = db.PhanLoaiSanPhams.Find(entity.PhanLoaiSanPhamID);
                phanLoaiSanPham.TenPhanLoaiSanPham = entity.TenPhanLoaiSanPham;
                phanLoaiSanPham.DanhMucSanPhamID = entity.DanhMucSanPhamID;
                phanLoaiSanPham.GhiChu = entity.GhiChu;
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
                var phamLoaiSanPham = db.PhanLoaiSanPhams.Find(id);
                var lstSanPham = db.SanPhams.Where(x => x.LoaiSanPhamID == phamLoaiSanPham.PhanLoaiSanPhamID).ToList();
                db.SanPhams.RemoveRange(lstSanPham);
                db.PhanLoaiSanPhams.Remove(phamLoaiSanPham);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<PhanLoaiSanPham> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<PhanLoaiSanPham> model = db.PhanLoaiSanPhams;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenPhanLoaiSanPham.Contains(searchString) || x.GhiChu.Contains(searchString));

            }
            return model.OrderByDescending(x => x.PhanLoaiSanPhamID).ToPagedList(page, pageSize);
        }
        public List<PhanLoaiSanPham> ListAll(bool? choose=null)
        {
            if(choose == true)
            {
                return  db.PhanLoaiSanPhams.Where(x => x.DanhMucSanPham.LaDoNam == true && x.DanhMucSanPham.LaDoNu == null && x.DanhMucSanPham.LaDoTreEm == null).ToList();
            }
            else if(choose == false)
            {
                return db.PhanLoaiSanPhams.Where(x => x.DanhMucSanPham.LaDoNam == null && x.DanhMucSanPham.LaDoNu == true && x.DanhMucSanPham.LaDoTreEm == null).ToList();
            }
            else
            {
                return db.PhanLoaiSanPhams.Where(x => x.DanhMucSanPham.LaDoNam == true && x.DanhMucSanPham.LaDoNu == true && x.DanhMucSanPham.LaDoTreEm == true).ToList();
            }
        }

        public PhanLoaiSanPham ViewDetail(long id)
        {
            return db.PhanLoaiSanPhams.Find(id);
        }

    }
}