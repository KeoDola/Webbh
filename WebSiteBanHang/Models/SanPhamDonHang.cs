namespace WebSiteBanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPhamDonHang")]
    public partial class SanPhamDonHang
    {
        public int SanPhamDonHangID { get; set; }
        [Display(Name = "Mã sản phẩm")]

        public int? SanPhamID { get; set; }
        [Display(Name = "Mã đơn hàng")]

        public int? DonHangID { get; set; }
        [Display(Name = "Ghi chú")]

        [StringLength(300)]
        public string GhiChu { get; set; }
        [Display(Name = "Số lượng")]

        public int? SoLuong { get; set; }

        public virtual DonHang DonHang { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
