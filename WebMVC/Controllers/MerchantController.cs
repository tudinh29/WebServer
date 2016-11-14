using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebMVC.Common;
using WebMVC.EntityFramework;
using PagedList;
using Rotativa;
namespace WebMVC.Controllers
{
    public class MerchantController : BaseController
    {
        private MVCDbContext db = new MVCDbContext();
        // GET: Merchant
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            var model = Session[CommonConstants.USER_SESSION]; //khai báo 1 session bên common giống như bên Cart
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(string.Format("api/MERCHANT/FindMerchant?merchantCode={0}", temp.UserName)).Result;

            var merchant = new MERCHANT();
            if (response.IsSuccessStatusCode)
            {
                merchant = response.Content.ReadAsAsync<MERCHANT>().Result;
                //return RedirectToAction("Index", "Home");
            }

            return View(merchant);
        }

        public ActionResult Manager()
        {
            return View();
        }
       
    }
}