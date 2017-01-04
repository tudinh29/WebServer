using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMVC.EntityFramework
{
    public class TRANSACTION_DETAIL_INVALID
    {
        public string TransactionCode { get; set; }


        public DateTime ReportDate { get; set; }


        public string MerchantCode { get; set; }


        public string TerminalNumber { get; set; }

        public int FileSource { get; set; }

        public long BatchNumber { get; set; }


        public DateTime ExpirationDate { get; set; }


        public string CardtypeCode { get; set; }


        public decimal TransactionAmount { get; set; }


        public DateTime TransactionDate { get; set; }

        public TimeSpan TransactionTime { get; set; }

        public bool KeyedEntry { get; set; }


        public string AuthorizationNumber { get; set; }

        public TimeSpan ReportTime { get; set; }


        public string Description { get; set; }


        public Byte[] AccountNumber { get; set; }


        public string FirstTwelveAccountNumber { get; set; }


        public string TransactionTypeCode { get; set; }


        public string RegionCode { get; set; }


        public string MerchantType { get; set; }


        public string AgentCode { get; set; }


        public string ErrorMessage { get; set; }
    }
}