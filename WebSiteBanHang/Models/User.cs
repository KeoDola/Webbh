namespace WebSiteBanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            KhachHangs = new HashSet<KhachHang>();
            NhanViens = new HashSet<NhanVien>();
        }

        public int UserID { get; set; }
        [Display(Name = "Tên tài khoản")]
        [StringLength(50)]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu")]

        [StringLength(32)]
        public string Password { get; set; }
        [Display(Name = "Tên người dùng")]
        [StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "Địa chỉ")]
        [StringLength(50)]
        public string Address { get; set; }
        [Display(Name = "Địa chỉ chi tiết")]

        [StringLength(300)]
        public string DiaChiChiTiet { get; set; }
        [Display(Name = "Địa chỉ Email")]
        [StringLength(50)]
        public string Email { get; set; }
        [Display(Name = "Số điện thoại")]
        public int? Phone { get; set; }

        public int? ThanhPhoID { get; set; }

        public int? QuanHuyenID { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        public int? XaPhuongID { get; set; }

        public int? GroupID { get; set; }

        [StringLength(200)]
        public string PasswordSalt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhachHang> KhachHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhanVien> NhanViens { get; set; }

        public virtual QuanHuyen QuanHuyen { get; set; }

        public virtual ThanhPho ThanhPho { get; set; }

        public virtual UserGroup UserGroup { get; set; }

        public virtual XaPhuong XaPhuong { get; set; }
    }
}
