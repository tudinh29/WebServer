using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WebMVC.Common;
using WebMVC.EntityFramework;
using WebMVC.Models;
namespace WebMVC.Controllers
{
    public class AgentController : BaseController
    {
        private MVCDbContext db = new MVCDbContext();
        // GET: Agent
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
            var user = (USER_INFORMATION)Session[CommonConstants.USER_SESSION];
            var pass = Session[CommonConstants.HASH_PASSWORD];
            StringContent content = new StringContent("username=" + user.UserName.ToString() + "&password=" + pass + "&grant_type=password");
            HttpResponseMessage res = client.PostAsync(string.Format("api/security/token"), content).Result;
            TokenModel token = res.Content.ReadAsAsync<TokenModel>().Result;
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token.access_token);
            HttpResponseMessage response = client.GetAsync(string.Format("api/AGENT/FindAgent?agentCode={0}", temp.UserName)).Result;

            var agent = new AGENT();
            if (response.IsSuccessStatusCode)
            {
                agent = response.Content.ReadAsAsync<AGENT>().Result;
                //return RedirectToAction("Index", "Home");
            }

            return View(agent);
        }
    }
}