namespace WebMVC.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TRANSACTION_DETAIL
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string TransactionCode { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime ReportDate { get; set; }

        [Required]
        [StringLength(10)]
        public string MerchantCode { get; set; }

        [Required]
        [StringLength(20)]
        public string TerminalNumber { get; set; }

        public int FileSource { get; set; }

        public long? BatchNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExpirationDate { get; set; }

        [StringLength(10)]
        public string CardtypeCode { get; set; }

        [Column(TypeName = "money")]
        public decimal? TransactionAmount { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TransactionDate { get; set; }

        public TimeSpan? TransactionTime { get; set; }

        public bool? KeyedEntry { get; set; }

        [StringLength(20)]
        public string AuthorizationNumber { get; set; }

        public TimeSpan? ReportTime { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [Required]
        public Byte[] AccountNumber { get; set; }

        [StringLength(12)]
        public string FirstTwelveAccountNumber { get; set; }

        [StringLength(10)]
        public string TransactionTypeCode { get; set; }

        [StringLength(10)]
        public string RegionCode { get; set; }

        [StringLength(10)]
        public string MerchantType { get; set; }

        [StringLength(10)]
        public string AgentCode { get; set; }

        public virtual CARD CARD { get; set; }

        public virtual MERCHANT MERCHANT { get; set; }

        public virtual TRANSACTION_TYPE TRANSACTION_TYPE { get; set; }
    }
}
