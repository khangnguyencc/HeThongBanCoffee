namespace HeThongQuanLyCaPhe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPhieuDatBan")]
    public partial class ChiTietPhieuDatBan
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string MaDatBan { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MaBan { get; set; }

        [StringLength(20)]
        public string MaKhu { get; set; }

        [StringLength(20)]
        public string TenKhu { get; set; }

        [StringLength(10)]
        public string SoBan { get; set; }

        public virtual BanNgoi BanNgoi { get; set; }

        public virtual DatBan DatBan { get; set; }
    }
}
