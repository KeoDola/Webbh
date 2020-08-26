using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.ViewModel
{
    public class SanPhamViewModel
    {
        public int SanPhamID { set; get; }
        public string AnhSanPham { set; get; }
        public string MaSanPham { set; get; }
        public string TenSanPham { set; get; }
        public double? GiaSanPham { set; get; }
        public string TenDanhMucSanPham { set; get; }
        public string ThongTinSanPham { set; get; }
        public DateTime? NgayTao { set; get; }
    }
}