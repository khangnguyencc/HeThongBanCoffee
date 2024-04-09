namespace HeThongQuanLyCaPhe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [Key]
        [Column("TaiKhoan")]
        [StringLength(50)]
        public string TaiKhoan1 { get; set; }

        [StringLength(100)]
        public string MatKhau { get; set; }

        [StringLength(10)]
        public string MaChucVu { get; set; }

        public bool? TrangThai { get; set; }

        [StringLength(20)]
        public string MaNhanVien { get; set; }

        public virtual ChucVu ChucVu { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
