namespace WebAPI.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MERCHANT_SUMMARY
    {
        [Key]
        [Column(Order = 0, TypeName = "string")]
        public String ReportDate { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MerchantCode { get; set; }

        [Column(TypeName = "money")]
        public decimal? SaleAmount { get; set; }

        public Int64? SaleCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? ReturnAmount { get; set; }

        public Int64? ReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? NetAmount { get; set; }

        public Int64? TransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? KeyedAmount { get; set; }

        public Int64? KeyedCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? KeyedReturnAmount { get; set; }

        public Int64? KeyedReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? KeyedNetAmount { get; set; }

        public Int64? KeyedTransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? ForeignCardAmount { get; set; }

        public Int64? ForeignCardCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? ForeignCardReturnAmount { get; set; }

        public Int64? ForeignCardReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? ForeignCardNetAmount { get; set; }

        public Int64? ForeignCardTransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? DebitCardAmount { get; set; }

        public Int64? DebitCardCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? DebitCardReturnAmount { get; set; }

        public Int64? DebitCardReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? DebitCardNetAmount { get; set; }

        public Int64? DebitCardTransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? VisaCardAmount { get; set; }

        public Int64? VisaCardCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? VisaCardReturnAmount { get; set; }

        public Int64? VisaCardReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? VisaCardNetAmount { get; set; }

        public Int64? VisaCardTransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? DiscoverCardAmount { get; set; }

        public Int64? DiscoverCardCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? DiscoverCardReturnAmount { get; set; }

        public Int64? DiscoverCardReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? DiscoverCardNetAmount { get; set; }

        public Int64? DiscoverCardTransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? MasterCardAmount { get; set; }

        public Int64? MasterCardCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? MasterCardReturnAmount { get; set; }

        public Int64? MasterCardReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? MasterCardNetAmount { get; set; }

        public Int64? MasterCardTransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? AmericanExpressAmount { get; set; }

        public Int64? AmericanExpressCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? AmericanExpressReturnAmount { get; set; }

        public Int64? AmericanExpressReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? AmericanExpressNetAmount { get; set; }

        public Int64? AmericanExpressTransactionCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? OtherCardAmount { get; set; }

        public Int64? OtherCardCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? OtherCardReturnAmount { get; set; }

        public Int64? OtherCardReturnCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? OtherCardNetAmount { get; set; }

        public Int64? OtherCardTransactionCount { get; set; }

        [StringLength(10)]
        public string RegionCode { get; set; }

        [StringLength(10)]
        public string MerchantType { get; set; }

        [StringLength(10)]
        public string AgentCode { get; set; }
    }
}
