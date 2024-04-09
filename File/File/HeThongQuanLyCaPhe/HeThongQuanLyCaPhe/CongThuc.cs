namespace HeThongQuanLyCaPhe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CongThuc")]
    public partial class CongThuc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CongThuc()
        {
            ChiTietCongThucs = new HashSet<ChiTietCongThuc>();
        }

        [Key]
        [StringLength(20)]
        public string MaCongThucSanPham { get; set; }

        [StringLength(20)]
        public string MaSanPham { get; set; }

        public virtual SanPham SanPham { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietCongThuc> ChiTietCongThucs { get; set; }
    }
}
