using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            //Lay so luong tin nhan chua doc
            var model = Session[CommonConstants.USER_SESSION];
            var user = new USER_INFORMATION();
            if (model != null)
            {
                user = (USER_INFORMATION)model;
            }
            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(string.Format("api/MESSAGETYPE/CountUnreadMessage?MaCode={0}&UserType={1}", user.UserName, user.UserType)).Result;
            if (response.IsSuccessStatusCode)
            {
                int number = response.Content.ReadAsAsync<int>().Result;
                Session.Add(CommonConstants.NUMBER_UNREAD_MESSAGE, number);
            }
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