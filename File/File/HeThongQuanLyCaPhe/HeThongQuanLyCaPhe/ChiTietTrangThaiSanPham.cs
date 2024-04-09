namespace HeThongQuanLyCaPhe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietTrangThaiSanPham")]
    public partial class ChiTietTrangThaiSanPham
    {
        [StringLength(255)]
        public string HinhAnh { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string MaTrangThaiThe { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string MaSanPham { get; set; }

        public int? SoLuong { get; set; }

        [Column(TypeName = "text")]
        public string Ghichu { get; set; }

        public virtual SanPham SanPham { get; set; }

        public virtual TrangThaiSanPham TrangThaiSanPham { get; set; }
    }
}
