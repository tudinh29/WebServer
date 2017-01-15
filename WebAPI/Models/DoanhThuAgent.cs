using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class DoanhThuAgent
    {
        public string AgentCode { get; set; }
        public decimal DoanhThu { get; set; }
        public decimal TrungBinh { get; set; }
        public decimal TangTruong { get; set; }
    }
}