namespace HeThongQuanLyCaPhe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPhieuNhapKho")]
    public partial class ChiTietPhieuNhapKho
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string MaPhieuNhapKho { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string MaNguyenLieu { get; set; }

        public int? SoLuong { get; set; }

        public double? DonGia { get; set; }

        [StringLength(50)]
        public string DonVi { get; set; }

        public double? ThanhTien { get; set; }

        public virtual NguyenLieu NguyenLieu { get; set; }

        public virtual PhieuNhapKho PhieuNhapKho { get; set; }
    }
}
