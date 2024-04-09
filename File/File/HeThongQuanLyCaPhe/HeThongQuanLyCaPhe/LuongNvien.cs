namespace HeThongQuanLyCaPhe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LuongNvien")]
    public partial class LuongNvien
    {
        [Key]
        [StringLength(20)]
        public string MaLuong { get; set; }

        [StringLength(20)]
        public string MaNhanVien { get; set; }

        public double? TongGioLam { get; set; }

        public double? TongTienLuong { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
