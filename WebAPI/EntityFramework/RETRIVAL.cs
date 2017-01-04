namespace WebAPI.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RETRIVAL")]
    public partial class RETRIVAL
    {
        [Key]
        [StringLength(10)]
        public string RetrivalCode { get; set; }

        [Required]
        public Byte[] AccountNumber { get; set; }

        [Required]
        [StringLength(10)]
        public string MerchantCode { get; set; }

        [StringLength(20)]
        public string TransactionCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TransactionDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ReportDate { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amout { get; set; }

        public virtual CARD CARD { get; set; }

        public virtual MERCHANT MERCHANT { get; set; }
    }
}
