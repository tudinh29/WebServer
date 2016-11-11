using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WebMVC.Common;
using WebMVC.EntityFramework;
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
            HttpResponseMessage response = client.GetAsync(string.Format("api/AGENT/FindAgent?agentCode={0}", temp.UserName)).Result;

            var agent = new AGENT();
            if (response.IsSuccessStatusCode)
            {
                agent = response.Content.ReadAsAsync<AGENT>().Result;
                //return RedirectToAction("Index", "Home");
            }

            return View(agent);
        }

        public IEnumerable<AGENT> ListAgents(string agentCode, string agentName, int page, int pageSize)
        {
            IOrderedQueryable<AGENT> model = db.AGENT;
            if (!string.IsNullOrEmpty(agentCode))
            {
                model = model.Where(x => x.AgentCode.Contains(agentCode) || x.AgentName.Contains(agentName)).OrderByDescending(x => x.AgentCode);
            }
            return model.ToPagedList(page, pageSize);
        }
    }
}