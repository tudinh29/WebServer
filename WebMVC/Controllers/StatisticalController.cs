using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC.Common;
using WebMVC.EntityFramework;

namespace WebMVC.Controllers
{
    public class StatisticalController : BaseController
    {
        // GET: Statistical
        public ActionResult Index()
        {
            List<MERCHANT> list = new List<MERCHANT>();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View();
            if (temp.UserType != "T")   //Master
            {
                //Code here
                return View();
            }
            else   //Agent
            {
                //Code here
                return View();
            }
           
        }

        public ActionResult Detail(List<MERCHANT> listMerchant)
        {
            return View();
        }
    }
}