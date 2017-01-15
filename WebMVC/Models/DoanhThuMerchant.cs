using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMVC.Models
{
    public class DoanhThuMerchant
    {
        public string MerchantCode { get; set; }
        public decimal DoanhThu { get; set; }
        public decimal TrungBinh { get; set; }
        public decimal TangTruong { get; set; }
    }
}