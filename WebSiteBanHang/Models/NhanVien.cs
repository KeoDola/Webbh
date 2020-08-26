namespace WebSiteBanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            DonHangs = new HashSet<DonHang>();
            NhapKhoes = new HashSet<NhapKho>();
        }

        public int NhanVienID { get; set; }
        [Display(Name = "Họ tên")]
        [StringLength(50)]
        public string HoTen { get; set; }
        [Display(Name = "Ngày sinh")]
        [Column(TypeName = "date")]
        public DateTime? NgaySinh { get; set; }
        [Display(Name = "Giới tính")]
        public bool GioiTinh { get; set; }
        [Display(Name = "Địa chỉ")]
        [StringLength(200)]
        public string DiaChi { get; set; }
        [Display(Name = "Quê quán")]
        [StringLength(200)]
        public string QueQuan { get; set; }
        [Display(Name = "Số điện thoại 1")]
        public int? SoDienThoai1 { get; set; }
        [Display(Name = "Số điện thoại 2")]
        public int? SoDienThoai2 { get; set; }
        [Display(Name = "Địa chỉ Email")]
        [StringLength(100)]
        public string Email { get; set; }
        [Display(Name = "Ghi chú")]
        [StringLength(200)]
        public string GhiChu { get; set; }
        [Display(Name = "Tên tài khoản")]
        public int? UserID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHang> DonHangs { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhapKho> NhapKhoes { get; set; }
    }
}
