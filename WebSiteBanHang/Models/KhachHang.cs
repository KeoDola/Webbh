namespace WebSiteBanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            DiaChiNhanHangs = new HashSet<DiaChiNhanHang>();
            DonHangs = new HashSet<DonHang>();
        }
        [Display(Name = "Mã khách hàng")]
        public int KhachHangID { get; set; }
        [Display(Name = "Họ tên")]
        [StringLength(50)]
        public string HoTen { get; set; }
        [Display(Name = "Ngày sinh")]
        [Column(TypeName = "date")]
        public DateTime? NgaySinh { get; set; }
        [Display(Name = "Giới tính")]
        public bool GioiTinh { get; set; }
        [Display(Name = "Địa chỉ Email")]
        [StringLength(100)]
        public string Email { get; set; }
        [Display(Name = "Số điện thoại")]
        public int? SoDienThoai { get; set; }
        [Display(Name = "Ghi chú")]
        [StringLength(200)]
        public string GhiChu { get; set; }
        [Display(Name = "Địa chỉ")]
        [StringLength(300)]
        public string DiaChi { get; set; }
        [Display(Name = "Địa chỉ Email")]
        public int? UserID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiaChiNhanHang> DiaChiNhanHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHang> DonHangs { get; set; }

        public virtual User User { get; set; }
    }
}
