﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebAPI.Models
{
    [KnownTypeAttribute(typeof(Statistic))]
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
        public decimal ReturnAmount { get; set; }
        public string RegionCode { get; set; }
        public string MerchantType { get; set; }
        public string AgentCode { get; set; }
    }
}