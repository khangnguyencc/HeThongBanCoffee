namespace HeThongQuanLyCaPhe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LichSuQuanLyKho")]
    public partial class LichSuQuanLyKho
    {
        [Key]
        public int STT { get; set; }

        [StringLength(20)]
        public string MaKhoHang { get; set; }

        [StringLength(20)]
        public string MaNhanVien { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayBatDau { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayKetThuc { get; set; }

        public bool? TrangThai { get; set; }

        public virtual KhoHang KhoHang { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
