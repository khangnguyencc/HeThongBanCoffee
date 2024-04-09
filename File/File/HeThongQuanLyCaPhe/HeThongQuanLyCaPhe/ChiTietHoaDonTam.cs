namespace HeThongQuanLyCaPhe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietHoaDonTam")]
    public partial class ChiTietHoaDonTam
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string MaHoaDonTam { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string MaSanPham { get; set; }

        public double? DonGia { get; set; }

        public int? SoLuong { get; set; }

        public double? ThanhTien { get; set; }

        public virtual HoaDonTam HoaDonTam { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
