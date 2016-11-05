using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMVC.Models
{
    public class LoginModel
    {
        //[Required(ErrorMessage = "Nhập user name")]
        public string UserName { set; get; }
        //[Required(ErrorMessage = "Nhập password")]
        public string Password { set; get; }
    }
}