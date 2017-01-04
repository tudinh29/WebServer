using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class DoanhThuMerchant
    {
        public string MerchantCode { get; set; }
        public decimal DoanhThu { get; set; }
        public decimal TrungBinh { get; set; }
        public string TangTruong { get; set; }
    }
}