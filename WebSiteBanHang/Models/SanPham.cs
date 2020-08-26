namespace WebSiteBanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            CTSanPhams = new HashSet<CTSanPham>();
            SanPhamDonHangs = new HashSet<SanPhamDonHang>();
            ThongSoSanPhams = new HashSet<ThongSoSanPham>();
        }

        public int SanPhamID { get; set; }

        [Display(Name = "Tên danh mục sản phẩm")]
        public int? LoaiSanPhamID { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Bạn phải nhập đầy đủ tên sản phẩm")]
        [StringLength(100)]
        public string TenSanPham { get; set; }
        [Display(Name = "Thông tin sản phẩm")]
        [StringLength(200)]
        public string ThongTinSanPham { get; set; }

        [Display(Name = "Ghi chú")]
        [StringLength(200)]
        public string GhiChu { get; set; }

        public DateTime? NgayTao { get; set; }

        [Display(Name = "Ảnh sản phẩm")]
        [StringLength(200)]
        public string AnhSanPham { get; set; }

        [Display(Name = "Mã sản phẩm")]
        [StringLength(10)]
        public string MaSanPham { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        public DateTime? TopHot { get; set; }

        [Display(Name = "Giá sản phẩm")]
        public double? GiaSanPham { get; set; }

        [Display(Name = "Thêm hình ảnh")]
        [Column(TypeName = "xml")]
        public string ThemHinhAnh { get; set; }
        [Display(Name = "Sản phẩm bán chạy")]
        public bool? Hot { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTSanPham> CTSanPhams { get; set; }

        public virtual PhanLoaiSanPham PhanLoaiSanPham { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPhamDonHang> SanPhamDonHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongSoSanPham> ThongSoSanPhams { get; set; }
    }
}
