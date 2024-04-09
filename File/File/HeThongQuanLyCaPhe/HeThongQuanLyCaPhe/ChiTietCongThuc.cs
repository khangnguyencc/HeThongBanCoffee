namespace HeThongQuanLyCaPhe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietCongThuc")]
    public partial class ChiTietCongThuc
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string MaNguyenLieu { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string MaCongThucSanPham { get; set; }

        [StringLength(20)]
        public string DungTich { get; set; }

        public virtual CongThuc CongThuc { get; set; }

        public virtual NguyenLieu NguyenLieu { get; set; }
    }
}
