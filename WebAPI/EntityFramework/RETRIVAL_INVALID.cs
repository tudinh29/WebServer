namespace WebAPI.EntityFramework
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("RETRIVAL_INVALID")]
    public partial class RETRIVAL_INVALID
    {
        [Key]
        [StringLength(10)]
        public string RetrivalCode { get; set; }

        [Required]
        [StringLength(20)]
        public string AccountNumber { get; set; }

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
        
        public string ErrorMessage { get; set; }

        public virtual CARD CARD { get; set; }

        public virtual MERCHANT MERCHANT { get; set; }
    }
}