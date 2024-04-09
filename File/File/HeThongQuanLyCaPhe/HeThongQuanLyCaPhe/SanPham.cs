namespace HeThongQuanLyCaPhe
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
            CongThucs = new HashSet<CongThuc>();
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
            ChiTietHoaDonTams = new HashSet<ChiTietHoaDonTam>();
            ChiTietTrangThaiSanPhams = new HashSet<ChiTietTrangThaiSanPham>();
        }

        [Key]
        [StringLength(20)]
        public string MaSanPham { get; set; }

        [StringLength(20)]
        public string MaLoaiSanPham { get; set; }

        [StringLength(100)]
        public string TenSanPham { get; set; }

        public double? DonGia { get; set; }

        [StringLength(255)]
        public string HinhAnh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CongThuc> CongThucs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDonTam> ChiTietHoaDonTams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietTrangThaiSanPham> ChiTietTrangThaiSanPhams { get; set; }

        public virtual LoaiSanPham LoaiSanPham { get; set; }
    }
}
