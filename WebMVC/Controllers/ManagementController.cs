using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebMVC.Common;
using WebMVC.EntityFramework;
using WebAPI.Controllers;
using PagedList;
using System.Web.Mvc.Html;
using Newtonsoft.Json;

namespace WebMVC.Controllers
{
    public class ManagementController : BaseController
    {
        // GET: Management
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Agent(string agentCode, string agentName, string cityCode, string address, string owner, int page = 1,int size = 10)
        {
            IList<AGENT> list = new List<AGENT>();
           
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:21212/");

            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpClient client = new AccessAPI().Access();
            if (agentCode == null)
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/Agent/FindAllAgent")).Result;

                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<List<AGENT>>().Result;
                }
                var listAgent = list.ToPagedList(page, size);
                return View(listAgent);
            }
            else
            {
                var agent = new AgentController();
                var listAgent = agent.ListAgents(agentCode, agentName, cityCode, address, owner, page, size);
                return View(listAgent);
            } 
        }

      
        public ActionResult ViewDetail_Agent(string agentCode)
        {
            AGENT agent = new AGENT();
            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(string.Format("api/Agent/FindAgent?agentCode={0}", agentCode)).Result;

            if (response.IsSuccessStatusCode)
            {
                agent = response.Content.ReadAsAsync<AGENT>().Result;
            }
            return View(agent);
        }

        [HttpPost]
        public ActionResult ChangeStatus(string id)
        {
            
            //var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = new HttpResponseMessage();
            if (id.Substring(0,2) == "AG")
            {
                HttpClient client = new AccessAPI().Access();
                StringContent content = new StringContent("");
                response = client.PostAsync(string.Format("api/Agent/ChangeStatus?agentCode={0}", id), content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var check = response.Content.ReadAsAsync<bool>().Result;
                }
                return RedirectToAction("Agent");
            }
            else 
            {
                HttpClient client = new AccessAPI().Access();
                StringContent content = new StringContent("");
                response = client.PostAsync(string.Format("api/Merchant/ChangeStatus?merchantCode={0}", id), content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var check = response.Content.ReadAsAsync<bool>().Result;
                }
                return RedirectToAction("Merchant");
            }
            
            
            
        }

        [HttpGet]
        public ActionResult ReloadAgents(int page = 1, int size = 10)
        {
            IList<AGENT> list = new List<AGENT>();
            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(string.Format("api/Agent/FindAllAgent")).Result;
            if (response.IsSuccessStatusCode)
            {
                list = response.Content.ReadAsAsync<List<AGENT>>().Result;
            }
            var listAgent = list.ToPagedList(page, size);
            return View("Agent", listAgent);
        }

        [HttpGet]
        public ActionResult Merchant(int page = 1, int size = 10)
        {
            List<MERCHANT> list = new List<MERCHANT>();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View("Index");
            HttpClient client = new AccessAPI().Access();
            if (temp.UserType != "A")
            { 
                HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindAllMerchant")).Result;
                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
                }
            }
            else
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindMerchantByAgentCode?agentCode={0}", temp.UserName)).Result;
                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
                }
            }
            var listMerchant = list.ToPagedList(page, size);
            return View(listMerchant);
        }

        public ActionResult ReloadMerchants(int page = 1, int size = 10)
        {
            List<MERCHANT> list = new List<MERCHANT>();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View("Index");
            HttpClient client = new AccessAPI().Access();
            if (temp.UserType != "A")
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindAllMerchant")).Result;
                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
                }
            }
            else
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindMerchantByAgentCode?agentCode={0}", temp.UserName)).Result;
                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
                }
            }
            var listMerchant = list.ToPagedList(page, size);
            return View("Merchant", listMerchant);
        }

        public ActionResult ViewDetail_Merchant(string merchantCode)
        {
            MERCHANT merchant = new MERCHANT();
            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindMerchant?merchantCode={0}", merchantCode)).Result;

            if (response.IsSuccessStatusCode)
            {
                merchant = response.Content.ReadAsAsync<MERCHANT>().Result;
            }
            return View(merchant);
        }

        

    }
}