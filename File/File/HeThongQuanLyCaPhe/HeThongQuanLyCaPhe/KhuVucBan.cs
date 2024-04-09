namespace HeThongQuanLyCaPhe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhuVucBan")]
    public partial class KhuVucBan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhuVucBan()
        {
            BanNgois = new HashSet<BanNgoi>();
        }

        [Key]
        [StringLength(20)]
        public string MaKhu { get; set; }

        [StringLength(20)]
        public string TenKhu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BanNgoi> BanNgois { get; set; }
    }
}
