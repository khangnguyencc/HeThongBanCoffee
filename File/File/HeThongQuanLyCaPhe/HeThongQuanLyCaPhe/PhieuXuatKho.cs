namespace HeThongQuanLyCaPhe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuXuatKho")]
    public partial class PhieuXuatKho
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuXuatKho()
        {
            ChiTietPhieuXuatKhoes = new HashSet<ChiTietPhieuXuatKho>();
        }

        [Key]
        [StringLength(20)]
        public string MaPhieuXuatKho { get; set; }

        [StringLength(20)]
        public string MaKhoHang { get; set; }

        [StringLength(20)]
        public string MaNhanVien { get; set; }

        public DateTime? NgayXuat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuXuatKho> ChiTietPhieuXuatKhoes { get; set; }

        public virtual KhoHang KhoHang { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        public virtual NhanVien NhanVien1 { get; set; }
    }
}
