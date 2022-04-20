namespace webtruyen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TACGIA")]
    public partial class TACGIA
    {
        [Key]
        public int MATG { get; set; }

        [StringLength(200)]
        public string TEN_TG { get; set; }

        [StringLength(200)]
        public string BIDANH { get; set; }

        [StringLength(500)]
        public string MOTA { get; set; }
    }
}
