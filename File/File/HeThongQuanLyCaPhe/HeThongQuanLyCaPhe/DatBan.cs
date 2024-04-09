namespace HeThongQuanLyCaPhe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DatBan")]
    public partial class DatBan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DatBan()
        {
            ChiTietPhieuDatBans = new HashSet<ChiTietPhieuDatBan>();
        }

        [Key]
        [StringLength(20)]
        public string MaDatBan { get; set; }

        [StringLength(20)]
        public string MaNhanVien { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDatBan { get; set; }

        [StringLength(20)]
        public string MaKhachHang { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ThoiGianNhanBan { get; set; }

        public int? SoLuong { get; set; }

        [Column(TypeName = "text")]
        public string GhiChu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuDatBan> ChiTietPhieuDatBans { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
