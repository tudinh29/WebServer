using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC.Common;
using WebMVC.EntityFramework;

namespace WebMVC.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult TitleHome()
        {

            var model = Session[CommonConstants.USER_SESSION]; //khai báo 1 session bên common giống như bên Cart
            var list = new USER_INFORMATION();
            if (model != null)
            {
                list = (USER_INFORMATION)model;
            }
            ViewBag.UserType = list;

            return PartialView(list);
        }


        
    }
}