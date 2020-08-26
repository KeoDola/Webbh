using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Models;
using WebSiteBanHang.ViewModel;

namespace WebSiteBanHang.DAO
{
    public class ProductDAO
    {
        ApplicationDbContext db = null;
        private static ProductDAO instance;
        static object key = new object();
        public static ProductDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)//bất đồng bộ , chiếm dụng tài nguyên....
                    {
                        instance = new ProductDAO();
                    }
                }
                return instance;
            }
        }
        public ProductDAO()
        {
            db = new ApplicationDbContext();
        }

        public CTSanPham ViewDetailProduct(int sanPhamID)
        {
            var ctSanPham = db.CTSanPhams.SingleOrDefault(x => x.SanPhamID == sanPhamID);
            return ctSanPham;
        }
        public long Insert(SanPham entity)
        {
            entity.Status = true;
            db.SanPhams.Add(entity);
            db.SaveChanges();
            return entity.SanPhamID;
        }
        public List<SanPham> ListNewSanPham(int top)
        {
            return db.SanPhams.OrderByDescending(x => x.NgayTao).Take(top).ToList();
        }
        public List<string> ListName(string keyword)
        {
            return db.SanPhams.Where(x => x.TenSanPham.Contains(keyword)).Select(x => x.TenSanPham).ToList();
        }
        public IEnumerable<SanPham> ListAllpaging(string searchString,int phanLoaiSanPhamID, int page, int pageSize)
        {
            IQueryable<SanPham> model = db.SanPhams;
            if(phanLoaiSanPhamID != 0)
            {
                model = model.Where(x => x.LoaiSanPhamID == phanLoaiSanPhamID);
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenSanPham.Contains(searchString) || x.ThongTinSanPham.Contains(searchString) || x.MaSanPham.Contains(searchString)||x.GiaSanPham.ToString().Contains(searchString));
            }
            return model.OrderByDescending(x => x.NgayTao).ToPagedList(page, pageSize);
        }

        /// <summary>
        /// Get list product by category
        /// </summary>
        /// <param name="sanPhamID"></param>
        /// <returns></returns>
        public List<SanPham> ListByCategoryId(int? danhMucSanPhamID, ref int totalRecord, int pageIndex = 1, int pageSize = 4)
        {
            totalRecord = db.SanPhams.Where(x => x.LoaiSanPhamID == danhMucSanPhamID).Count();
            var model = db.SanPhams.Where(x => x.LoaiSanPhamID == danhMucSanPhamID).OrderByDescending(x => x.SanPhamID).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
        public List<SanPham> Search(int? danhMucSanPhamID, string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 4)
        {
            totalRecord = db.SanPhams.Where(x => x.TenSanPham.Contains(keyword) && x.LoaiSanPhamID == danhMucSanPhamID).Count();
            var model = db.SanPhams.Where(x => x.LoaiSanPhamID == danhMucSanPhamID && x.TenSanPham.Contains(keyword)).OrderByDescending(x => x.SanPhamID).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
        /// <summary>
        /// List feature product
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<SanPham> ListFeatureProduct(int top)
        {
            return db.SanPhams.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.NgayTao).Take(top).ToList();
        }
        public List<SanPham> ListRelatedProducts(int sanPhamID)
        {
            var sanPham = db.SanPhams.Find(sanPhamID);
            return db.SanPhams.Where(x => x.SanPhamID != sanPhamID && x.LoaiSanPhamID == sanPham.LoaiSanPhamID).ToList();
        }
        public void UpdateImages(long sanPhamID, string images)
        {
            var sanPham = db.SanPhams.Find(sanPhamID);
            sanPham.AnhSanPham = images;
            db.SaveChanges();
        }

        public bool Update(SanPham entity)
        {
            try
            {
                var sanPham = db.SanPhams.Find(entity.SanPhamID);
                sanPham.TenSanPham = entity.TenSanPham;
                sanPham.AnhSanPham = entity.AnhSanPham;
                sanPham.LoaiSanPhamID = entity.LoaiSanPhamID;
                sanPham.ThongTinSanPham = entity.ThongTinSanPham;
                sanPham.GiaSanPham = entity.GiaSanPham;
                sanPham.GhiChu = entity.GhiChu;
                sanPham.MaSanPham = entity.MaSanPham;
                sanPham.Hot = entity.Hot;
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
                var sanPham = db.SanPhams.Find(id);
                var lstCTSanPham = db.CTSanPhams.Where(x => x.SanPhamID == sanPham.SanPhamID).ToList();
                db.CTSanPhams.RemoveRange(lstCTSanPham);
                db.SanPhams.Remove(sanPham);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SanPham ViewDetail(int sanPhamID)
        {
            return db.SanPhams.Find(sanPhamID);
        }
        public bool ChangeStatus(long id)
        {
            var pr = db.SanPhams.Find(id);
            pr.Status = !pr.Status;
            db.SaveChanges();
            return pr.Status;
        }
        public List<PhanLoaiSanPham> getDanhMucSanPham()
        {
            var danhMucSanPham = db.PhanLoaiSanPhams.ToList();
            return danhMucSanPham;
        }
    }
}