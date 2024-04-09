namespace HeThongQuanLyCaPhe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuNhapKho")]
    public partial class PhieuNhapKho
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuNhapKho()
        {
            ChiTietPhieuNhapKhoes = new HashSet<ChiTietPhieuNhapKho>();
        }

        [Key]
        [StringLength(20)]
        public string MaPhieuNhapKho { get; set; }

        [StringLength(20)]
        public string MaKhoHang { get; set; }

        [StringLength(20)]
        public string MaNhaCungCap { get; set; }

        public DateTime? NgayNhap { get; set; }

        public double? TongTien { get; set; }

        [StringLength(50)]
        public string MaNhanVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuNhapKho> ChiTietPhieuNhapKhoes { get; set; }

        public virtual KhoHang KhoHang { get; set; }

        public virtual NhaCungCap NhaCungCap { get; set; }
    }
}
