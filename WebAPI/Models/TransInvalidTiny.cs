﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class TransInvalidTiny
    {
        public string TransactionCode { get; set; }


        public DateTime ReportDate { get; set; }


        public string MerchantCode { get; set; }
        public string CardtypeCode { get; set; }


        public decimal TransactionAmount { get; set; }


        public DateTime TransactionDate { get; set; }

        public Byte[] AccountNumber { get; set; }

        public string TransactionTypeCode { get; set; }
        public string AgentCode { get; set; }


        public string ErrorMessage { get; set; }
    }
}