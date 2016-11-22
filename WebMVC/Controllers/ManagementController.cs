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
                string AgentCode = agentCode;
                string AgentName = agentName;
                string CityCode = cityCode;
                string Address = address;
                string Owner = owner;

                ViewBag.agentCode = AgentCode;
                ViewBag.agentName = AgentName;
                ViewBag.cityCode = CityCode;
                ViewBag.address = Address;
                ViewBag.owner = Owner;

                if (agentCode == "")
                {
                    agentCode = "######";
                }
                if (agentName == "")
                {
                    agentName = "######";
                }
                if (cityCode == "")
                {
                    cityCode = "######";
                }
                if (address == "")
                {
                    address = "######";
                }
                if (owner == "")
                {
                    owner = "######";
                }
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
        public ActionResult Merchant(string merchantCode, string merchantName, string cityCode, string agentCode, string address, string merchantType, int page = 1, int size = 10)
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
                if (merchantCode == null)
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindAllMerchant")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
                    }
                    var listMerchant = list.ToPagedList(page, size);
                    return View(listMerchant);
                }
                else
                {
                     string MerchantCode = merchantCode;
                     string MerchantName = merchantName;
                     string CityCode = cityCode;
                     string AgentCode = agentCode;
                     string Address = address;
                     string MerchantType = merchantType;
 
                     ViewBag.merchantCode = MerchantCode;
                     ViewBag.merchantName = MerchantName;
                     ViewBag.cityCode = CityCode;
                     ViewBag.address = Address;
                     ViewBag.agentCode = AgentCode;
                     ViewBag.merchantType = MerchantType;

                    if (merchantCode == "")
                    {
                        merchantCode = "##";
                    }
                    if (merchantName == "")
                    {
                        merchantName = "##";
                    }
                    if (cityCode == "")
                    {
                        cityCode = "##";
                    }
                    if (address == "")
                    {
                        address = "##";
                    }
                    if (merchantType == "")
                    {
                        merchantType = "##";
                    }
                    if (agentCode == "")
                    {
                        agentCode = "##";

                    }
                    var merchant = new MerchantController();
                    var listMerchant = merchant.ListMerchant(merchantCode, merchantName, cityCode, address, agentCode, merchantType, page, size);
                    return View(listMerchant);
                }
            }
            else
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindMerchantByAgentCode?agentCode={0}", temp.UserName)).Result;
                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
                }
                var listMerchant = list.ToPagedList(page, size);
                return View(listMerchant);
            }
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

        public void loadDataIntoViewAddNewMerchant(MERCHANT merchant=null)
        {
            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage responseMerchantType = client.GetAsync(string.Format("api/Merchant_Type/SelectAllMerchantType")).Result;
            HttpResponseMessage responseCity = client.GetAsync(string.Format("api/City/SelectAllCity")).Result;
            HttpResponseMessage responseAgent = client.GetAsync(string.Format("api/Agent/FindAllAgent")).Result;
            HttpResponseMessage responseProcessor = client.GetAsync(string.Format("api/Processor/SelectAllProcessor")).Result;

            if (responseAgent.IsSuccessStatusCode && responseCity.IsSuccessStatusCode &&
                responseMerchantType.IsSuccessStatusCode && responseProcessor.IsSuccessStatusCode)
            {
                List<MERCHANT_TYPE> listMerchantType = responseMerchantType.Content.ReadAsAsync<List<MERCHANT_TYPE>>().Result;
                List<CITY> listCity = responseCity.Content.ReadAsAsync<List<CITY>>().Result;
                List<AGENT> listAgent = responseAgent.Content.ReadAsAsync<List<AGENT>>().Result; ;
                List<PROCESSOR> listProcessor = responseProcessor.Content.ReadAsAsync<List<PROCESSOR>>().Result;

                if (merchant != null)
                {
                    ViewBag.BackEndProcessor = new SelectList(listProcessor, "ID", "ProcessorName", merchant.BackEndProcessor);
                    ViewBag.CityCode = new SelectList(listCity, "CityCode", "CityName", merchant.CityCode);
                    ViewBag.MerchantType = new SelectList(listMerchantType, "MerchantType", "MerchantType", merchant.MerchantType);
                    ViewBag.AgentCode = new SelectList(listAgent, "AgentCode", "AgentName", merchant.AgentCode);
                }
                else
                {
                    ViewBag.BackEndProcessor = new SelectList(listProcessor, "ID", "ProcessorName");
                    ViewBag.CityCode = new SelectList(listCity, "CityCode", "CityName");
                    ViewBag.MerchantType = new SelectList(listMerchantType, "MerchantType", "MerchantType");
                    ViewBag.AgentCode = new SelectList(listAgent, "AgentCode", "AgentName");
                }
                


            }

        }
        [HttpGet]
        public ActionResult AddNewMerchant()
        {
            loadDataIntoViewAddNewMerchant();
            return View();
        }

        [HttpPost]
        public ActionResult AddNewMerchant(MERCHANT merchant)
        {
            //cái macode agent may chuyen no vè ma chua hay de ten luon alo alo
            //string jsonMerchant = JsonConvert.SerializeObject(merchant);
            var check = new bool();
            //debug tipe di
            if (ModelState.IsValid)
            {
                HttpClient client = new AccessAPI().Access();
                HttpResponseMessage response = client.PostAsJsonAsync("api/MERCHANT/AddNewMerchant", merchant).Result;
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                    check = response.Content.ReadAsAsync<bool>().Result;

                if (check == true)
                    return RedirectToAction("Merchant");
            }
            loadDataIntoViewAddNewMerchant();
            return View(merchant);
        }
        [HttpGet]
        public ActionResult ViewDetail_Merchant(string merchantCode)
        {
            MERCHANT merchant = new MERCHANT();
            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindMerchant?merchantCode={0}", merchantCode)).Result;

            if (response.IsSuccessStatusCode)
            {
                merchant = response.Content.ReadAsAsync<MERCHANT>().Result;
            }
            loadDataIntoViewAddNewMerchant(merchant);
            return View(merchant);
        }
        [HttpPost]
        public ActionResult ViewDetail_Merchant(MERCHANT merchant)
        {
            var check = new bool();
            string id = merchant.MerchantCode;
            if (ModelState.IsValid)
            {
                HttpClient client = new AccessAPI().Access();
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("api/MERCHANT/UpdateMerchant?id={0}", id), merchant).Result;
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                    check = response.Content.ReadAsAsync<bool>().Result;

                if (check == true)
                    return RedirectToAction("Merchant");
            }
            loadDataIntoViewAddNewMerchant();
            return View(merchant);
        }
    }
}