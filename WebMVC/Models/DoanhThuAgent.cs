using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMVC.Models
{
    public class DoanhThuAgent
    {
        public string AgentCode { get; set; }
        public decimal DoanhThu { get; set; }
        public decimal TrungBinh { get; set; }
        public string TangTruong { get; set; }
    }
}