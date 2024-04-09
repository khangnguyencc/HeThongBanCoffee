namespace HeThongQuanLyCaPhe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrangThaiSanPham")]
    public partial class TrangThaiSanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TrangThaiSanPham()
        {
            ChiTietTrangThaiSanPhams = new HashSet<ChiTietTrangThaiSanPham>();
        }

        [Key]
        [StringLength(20)]
        public string MaTrangThaiThe { get; set; }

        [StringLength(20)]
        public string MaNhanVien { get; set; }

        public DateTime? ThoiGianNhap { get; set; }

        public bool? TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietTrangThaiSanPham> ChiTietTrangThaiSanPhams { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
