namespace HeThongQuanLyCaPhe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPhieuXuatKho")]
    public partial class ChiTietPhieuXuatKho
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string MaPhieuXuatKho { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string MaNguyenLieu { get; set; }

        public int? SoLuong { get; set; }

        public virtual NguyenLieu NguyenLieu { get; set; }

        public virtual PhieuXuatKho PhieuXuatKho { get; set; }
    }
}
