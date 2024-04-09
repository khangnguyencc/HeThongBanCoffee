namespace HeThongQuanLyCaPhe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiNguyenLieu")]
    public partial class LoaiNguyenLieu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiNguyenLieu()
        {
            NguyenLieux = new HashSet<NguyenLieu>();
        }

        [Key]
        [StringLength(20)]
        public string MaLoaiNguyenLieu { get; set; }

        [StringLength(20)]
        public string MaKhoHang { get; set; }

        [StringLength(100)]
        public string TenLoaiNguyenLieu { get; set; }

        public virtual KhoHang KhoHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguyenLieu> NguyenLieux { get; set; }
    }
}
