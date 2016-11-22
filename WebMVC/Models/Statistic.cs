using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMVC.Models
{
    public class Statistic
    {
        [Required]
        [StringLength(10)]
        public string Name { get; set; }
        [Required]
        public decimal Value { get; set; }
    }

    public class MerchantSummaryDailyTiny
    {
        public DateTime ReportDate { get; set; }
        public string MerchantCode { get; set; }
        public decimal SaleAmount { get; set; }
        public int SaleCount { get; set; }
        public decimal ReturnAmount { get; set; }
        public int ReturnCount { get; set; }
        public decimal NetAmount { get; set; }
        public int TransactionCount { get; set; }
        public decimal KeyedAmount { get; set; }
    }
}