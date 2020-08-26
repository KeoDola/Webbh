namespace WebSiteBanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonHang")]
    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            SanPhamDonHangs = new HashSet<SanPhamDonHang>();
        }

        public int DonHangID { get; set; }
        [Display(Name = "Mã nhân viên")]
        public int? NhanVienID { get; set; }
        [Display(Name = "Mã khách hàng")]
        public int? KhachHangID { get; set; }

        public int? DiaChiNhanHangID { get; set; }
        [Display(Name = "Ngày xuất")]
        [Column(TypeName = "date")]
        public DateTime? NgayXuat { get; set; }
        [Display(Name = "Ngày nhận")]
        [Column(TypeName = "date")]
        public DateTime? NgayNhan { get; set; }
        [Display(Name = "Ghi chú")]
        [StringLength(200)]
        public string GhiChu { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
        [Display(Name = "Giá trị đơn hàng")]
        public double? GiaTriDonHang { get; set; }
        [Display(Name = "Địa chỉ nhận hàng chi tiết")]
        [StringLength(500)]
        public string DiaChiNhanHangChiTiet { get; set; }

        public virtual DiaChiNhanHang DiaChiNhanHang { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPhamDonHang> SanPhamDonHangs { get; set; }
    }
}
