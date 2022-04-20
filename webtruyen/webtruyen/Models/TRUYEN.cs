namespace webtruyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TRUYEN")]
    public partial class TRUYEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TRUYEN()
        {
            CHUONGTRUYENs = new HashSet<CHUONGTRUYEN>();
        }

        [Key]
        public int MATRUYEN { get; set; }

        [StringLength(200)]
        public string TENTRUYEN { get; set; }

        public int? MATG { get; set; }

        public int? MATHELOAI { get; set; }

        public long? LUOTXEM { get; set; }

        [StringLength(100)]
        public string NGUON { get; set; }

        [StringLength(100)]
        public string HINHANH { get; set; }

        [Column(TypeName = "ntext")]
        public string MOTA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHUONGTRUYEN> CHUONGTRUYENs { get; set; }

        public virtual THELOAI THELOAI { get; set; }

        public virtual TACGIA TACGIA { get; set; }
       


    }
}
