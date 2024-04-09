namespace HeThongQuanLyCaPhe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDonTam")]
    public partial class HoaDonTam
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDonTam()
        {
            ChiTietHoaDonTams = new HashSet<ChiTietHoaDonTam>();
        }

        [Key]
        [StringLength(20)]
        public string MaHoaDonTam { get; set; }

        [StringLength(20)]
        public string MaKhachHang { get; set; }

        [StringLength(20)]
        public string MaNhanVien { get; set; }

        [StringLength(20)]
        public string MaKhuyenMai { get; set; }

        [StringLength(20)]
        public string SoThe { get; set; }

        public DateTime? NgayBanHang { get; set; }

        [StringLength(50)]
        public string PhuongThucThanhToan { get; set; }

        public double? TongTien { get; set; }

        public double? TienKhach { get; set; }

        public double? TienThua { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDonTam> ChiTietHoaDonTams { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual KhuyenMai KhuyenMai { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
