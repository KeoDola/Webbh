namespace WebSiteBanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CTSanPham")]
    public partial class CTSanPham
    {
        public int CTSanPhamID { get; set; }
        [Display(Name = "Mã sản phẩm")]
        public int? SanPhamID { get; set; }
        [Display(Name = "Ghi chú")]
        [StringLength(200)]
        public string GhiChu { get; set; }
        [Display(Name = "Số lượng")]
        public int? SoLuong { get; set; }
        [Display(Name = "Ảnh sản phẩm")]
        [StringLength(200)]
        public string AnhSanPham { get; set; }
        [Display(Name = "Chi tiết")]
        [StringLength(300)]
        public string ChiTiet { get; set; }
        [Display(Name = "Giảm giá")]
        public int? GiamGia { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
