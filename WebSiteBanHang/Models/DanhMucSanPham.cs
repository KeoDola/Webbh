namespace WebSiteBanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhMucSanPham")]
    public partial class DanhMucSanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DanhMucSanPham()
        {
            PhanLoaiSanPhams = new HashSet<PhanLoaiSanPham>();
        }

        public int DanhMucSanPhamID { get; set; }

        public int? DanhMucSanPhamPID { get; set; }
        [Display(Name ="Tên danh mục sản phẩm")]
        [Required(ErrorMessage ="Yêu cầu nhập đầy đủ")]
        [StringLength(100)]
        public string TenDanhMucSanPham { get; set; }
        [Display(Name = "Ghi chú")]
        [StringLength(200)]
        public string GhiChu { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [StringLength(50)]
        public string Language { get; set; }
        [Display(Name = "Danh mục sản phẩm nam")]
        public bool? LaDoNam { get; set; }
        [Display(Name = "Danh mục sản phẩm nữ")]    
        public bool? LaDoNu { get; set; }
        [Display(Name = "Danh mục sản phẩm trẻ em")]
        public bool? LaDoTreEm { get; set; }

        public virtual LoaiDanhMucSanPham LoaiDanhMucSanPham { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhanLoaiSanPham> PhanLoaiSanPhams { get; set; }
    }
}
