namespace HeThongQuanLyCaPhe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChamCong")]
    public partial class ChamCong
    {
        [Key]
        [StringLength(20)]
        public string MaChamCong { get; set; }

        [StringLength(20)]
        public string MaNhanVien { get; set; }

        [StringLength(10)]
        public string MaCaLamViec { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayLamViec { get; set; }

        public TimeSpan? GioBatDau { get; set; }

        public TimeSpan? GioKetThuc { get; set; }

        public double? TongGioLam { get; set; }

        public virtual CaLamViec CaLamViec { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
