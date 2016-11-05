namespace WebAPI.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MERCHANT_SUMMARY_YEARLY
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReportYear { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MerchantCode { get; set; }

        [Column(TypeName = "money")]
        public decimal? SaleAmount { get; set; }

        public long? SaleCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? ReturnAmount { get; set; }

        public long? ReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? NetAmount { get; set; }

        public long? TransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? KeyedAmount { get; set; }

        public long? KeyedCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? KeyedReturnAmount { get; set; }

        public long? KeyedReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? KeyedNetAmount { get; set; }

        public long? KeyedTransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? ForeignCardAmount { get; set; }

        public long? ForeignCardCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? ForeignCardReturnAmount { get; set; }

        public long? ForeignCardReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? ForeignCardNetAmount { get; set; }

        public long? ForeignCardTransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? DebitCardAmount { get; set; }

        public long? DebitCardCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? DebitCardReturnAmount { get; set; }

        public long? DebitCardReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? DebitCardNetAmount { get; set; }

        public long? DebitCardTransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? VisaCardAmount { get; set; }

        public long? VisaCardCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? VisaCardReturnAmount { get; set; }

        public long? VisaCardReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? VisaCardNetAmount { get; set; }

        public long? VisaCardTransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? DiscoverCardAmount { get; set; }

        public long? DiscoverCardCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? DiscoverCardReturnAmount { get; set; }

        public long? DiscoverCardReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? DiscoverCardNetAmount { get; set; }

        public long? DiscoverCardTransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? MasterCardAmount { get; set; }

        public long? MasterCardCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? MasterCardReturnAmount { get; set; }

        public long? MasterCardReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? MasterCardNetAmount { get; set; }

        public long? MasterCardTransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? AmericanExpressAmount { get; set; }

        public long? AmericanExpressCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? AmericanExpressReturnAmount { get; set; }

        public long? AmericanExpressReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? AmericanExpressNetAmount { get; set; }

        public long? AmericanExpressTransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? OtherCardAmount { get; set; }

        public long? OtherCardCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? OtherCardReturnAmount { get; set; }

        public long? OtherCardReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? OtherCardNetAmount { get; set; }

        public long? OtherCardTransactionCount { get; set; }

        [StringLength(10)]
        public string RegionCode { get; set; }

        [StringLength(10)]
        public string MerchantType { get; set; }

        [StringLength(10)]
        public string AgentCode { get; set; }
    }
}
