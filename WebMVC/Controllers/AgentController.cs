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
    public class AgentController : BaseController
    {
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
            HttpResponseMessage response = client.GetAsync(string.Format("api/AGENT/FindAgent?agentCode={0}", temp.UserName)).Result;

            var agent = new AGENT();
            if (response.IsSuccessStatusCode)
            {
                agent = response.Content.ReadAsAsync<AGENT>().Result;
                //return RedirectToAction("Index", "Home");
            }

            return View(agent);
        }
  //may co bem mvc cho nào vạy

    }
}