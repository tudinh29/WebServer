using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMVC.Models
{
    public class Mail
    {
        public string MailMaster { get; set; }
        public string MailMerchant { get; set; }
        public string Password { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string MerchantInfor { get; set; }
    }
}