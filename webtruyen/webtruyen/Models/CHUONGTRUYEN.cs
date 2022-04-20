namespace webtruyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHUONGTRUYEN")]
    public partial class CHUONGTRUYEN
    {
        [Key]
        public int MACHUONG { get; set; }

        [StringLength(100)]
        public string TENCHUONG { get; set; }

        [StringLength(100)]
        public string TENPHU { get; set; }

        [Column(TypeName = "ntext")]
        public string NOIDUNG { get; set; }

        public long? LUOTXEM { get; set; }

        public int? MATRUYEN { get; set; }

        public virtual TRUYEN TRUYEN { get; set; }
    }
}
