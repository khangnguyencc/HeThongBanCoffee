namespace HeThongQuanLyCaPhe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguyenLieu")]
    public partial class NguyenLieu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NguyenLieu()
        {
            ChiTietCongThucs = new HashSet<ChiTietCongThuc>();
            ChiTietPhieuNhapKhoes = new HashSet<ChiTietPhieuNhapKho>();
            ChiTietPhieuXuatKhoes = new HashSet<ChiTietPhieuXuatKho>();
        }

        [Key]
        [StringLength(20)]
        public string MaNguyenLieu { get; set; }

        [StringLength(20)]
        public string MaLoaiNguyenLieu { get; set; }

        [StringLength(100)]
        public string TenNguyenLieu { get; set; }

        [Column(TypeName = "date")]
        public DateTime? HanSuDung { get; set; }

        public int? SoLuong { get; set; }

        [StringLength(50)]
        public string DonVi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietCongThuc> ChiTietCongThucs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuNhapKho> ChiTietPhieuNhapKhoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuXuatKho> ChiTietPhieuXuatKhoes { get; set; }

        public virtual LoaiNguyenLieu LoaiNguyenLieu { get; set; }
    }
}
