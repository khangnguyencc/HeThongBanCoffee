namespace HeThongQuanLyCaPhe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BanNgoi")]
    public partial class BanNgoi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BanNgoi()
        {
            ChiTietPhieuDatBans = new HashSet<ChiTietPhieuDatBan>();
        }

        [Key]
        [StringLength(10)]
        public string MaBan { get; set; }

        [StringLength(20)]
        public string MaKhu { get; set; }

        [StringLength(20)]
        public string TenKhu { get; set; }

        [StringLength(10)]
        public string SoBan { get; set; }

        public virtual KhuVucBan KhuVucBan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuDatBan> ChiTietPhieuDatBans { get; set; }
    }
}
