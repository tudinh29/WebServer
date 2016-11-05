namespace WebAPI.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MERCHANT_SUMMARY_MONTHLY
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReportMonth { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReportYear { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string MerchantCode { get; set; }

        [Column(TypeName = "money")]
        public decimal? SaleAmount { get; set; }

        public int? SaleCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? ReturnAmount { get; set; }

        public int? ReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? NetAmount { get; set; }

        public int? TransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? KeyedAmount { get; set; }

        public int? KeyedCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? KeyedReturnAmount { get; set; }

        public int? KeyedReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? KeyedNetAmount { get; set; }

        public int? KeyedTransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? ForeignCardAmount { get; set; }

        public int? ForeignCardCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? ForeignCardReturnAmount { get; set; }

        public int? ForeignCardReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? ForeignCardNetAmount { get; set; }

        public int? ForeignCardTransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? DebitCardAmount { get; set; }

        public int? DebitCardCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? DebitCardReturnAmount { get; set; }

        public int? DebitCardReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? DebitCardNetAmount { get; set; }

        public int? DebitCardTransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? VisaCardAmount { get; set; }

        public int? VisaCardCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? VisaCardReturnAmount { get; set; }

        public int? VisaCardReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? VisaCardNetAmount { get; set; }

        public int? VisaCardTransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? DiscoverCardAmount { get; set; }

        public int? DiscoverCardCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? DiscoverCardReturnAmount { get; set; }

        public int? DiscoverCardReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? DiscoverCardNetAmount { get; set; }

        public int? DiscoverCardTransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? MasterCardAmount { get; set; }

        public int? MasterCardCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? MasterCardReturnAmount { get; set; }

        public int? MasterCardReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? MasterCardNetAmount { get; set; }

        public int? MasterCardTransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? AmericanExpressAmount { get; set; }

        public int? AmericanExpressCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? AmericanExpressReturnAmount { get; set; }

        public int? AmericanExpressReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? AmericanExpressNetAmount { get; set; }

        public int? AmericanExpressTransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? OtherCardAmount { get; set; }

        public int? OtherCardCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? OtherCardReturnAmount { get; set; }

        public int? OtherCardReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? OtherCardNetAmount { get; set; }

        public int? OtherCardTransactionCount { get; set; }

        [StringLength(10)]
        public string RegionCode { get; set; }

        [StringLength(10)]
        public string MerchantType { get; set; }

        [StringLength(10)]
        public string AgentCode { get; set; }
    }
}
