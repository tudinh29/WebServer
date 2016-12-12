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
using System.IO;
using CsvHelper;
using Rotativa;
using ClosedXML.Excel;
using System.Web.UI.WebControls;
using System.Web.UI;


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
        public ActionResult Agent(string searchString, int page = 1,int size = 10)
        {
            IList<AGENT> list = new List<AGENT>();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View("Index");
           
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:21212/");

            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (temp.UserType == "T")
            {
                HttpClient client = new AccessAPI().Access();
                @ViewBag.action = "Agent";
                if (String.IsNullOrEmpty(searchString))
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
                    HttpResponseMessage response = client.GetAsync(string.Format("api/Agent/FindAgentElement?searchString={0}", searchString)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        list = response.Content.ReadAsAsync<List<AGENT>>().Result;
                    }
                    @ViewBag.searchString = searchString;
                    var listAgent = list.ToPagedList(page, size);
                    return View(listAgent);
                }
            }
            else return View("Index");
            
        }
        public ActionResult ExportAgentPDF(string searchString)
        {
            string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Page: [page] of [toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            List<AGENT> list = new List<AGENT>();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View("Index");

            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:21212/");

            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (temp.UserType == "T")
            {
                HttpClient client = new AccessAPI().Access();
                @ViewBag.action = "Agent";
                if (String.IsNullOrEmpty(searchString))
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/Agent/FindAllAgent")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        list = response.Content.ReadAsAsync<List<AGENT>>().Result;
                    }
                }
                else
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/Agent/FindAgentElement?searchString={0}", searchString)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        list = response.Content.ReadAsAsync<List<AGENT>>().Result;
                    }
                    @ViewBag.searchString = searchString;
                }
            }
            else return View("Index");

            return new Rotativa.PartialViewAsPdf("AgentPDF", list)
            {   
                FileName = "AgentManagement.pdf",
                CustomSwitches = footer
            };
        }
        public ActionResult ExportMerchantPDF(string searchString)
        {
            string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Page: [page] of [toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
             List<MERCHANT> list = new List<MERCHANT>();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View("Index");
            HttpClient client = new AccessAPI().Access();
            if (temp.UserType == "T")   //Master
            {
                if (String.IsNullOrEmpty(searchString))
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindAllMerchant")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
                    }
                }
                else
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindMerchantElement?searchString={0}", searchString)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
                    }
                    @ViewBag.searchString = searchString;
                }
            }
            else if (temp.UserType == "A")
            {
                if (String.IsNullOrEmpty(searchString))
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindMerchantByAgentCode?agentCode={0}", temp.UserName)).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
                    }
                }
                else
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindMerchantByAgentCodeAndElement?searchString={0}&agentCode={1}", searchString, temp.UserName)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
                    }
                    @ViewBag.searchString = searchString;
                    @ViewBag.agentCode = temp.UserName;
                }
            }
            else return View("Index");
            return new Rotativa.PartialViewAsPdf("MerchantPDF", list)
            {   //MerchantSumaryDailyStatistical
                FileName = "MerchantManagement.pdf",
                CustomSwitches = footer
            };
        }
        //[HttpGet]
        //public ActionResult FindAgentElement(string searchString, int? page, int size = 10)
        //{

        //    List<AGENT> list = new List<AGENT>();
        //    var model = Session[CommonConstants.USER_SESSION];
        //    var temp = new USER_INFORMATION();
        //    if (model != null)
        //    {
        //        temp = (USER_INFORMATION)model;
        //    }
        //    else return View("Index");
        //    //HttpClient client = new HttpClient();
        //    //client.BaseAddress = new Uri("http://localhost:21212/");
        //    int pageNumber = (page ?? 1);
        //    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    if (temp.UserType == "T")
        //    {
        //        HttpClient client = new AccessAPI().Access();
        //        if (searchString == "")
        //        {
        //            return RedirectToAction("Agent");
        //        }

        //        HttpResponseMessage response = client.GetAsync(string.Format("api/Agent/FindAgentElement?searchString={0}", searchString)).Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            list = response.Content.ReadAsAsync<List<AGENT>>().Result;
        //        }
        //        @ViewBag.searchString = searchString;
        //        var listAgent = list.ToPagedList(pageNumber, size);
        //        return View("Agent", listAgent);
        //    }
        //    else return View("Index");
        //}

        [HttpGet]
        public ActionResult ViewDetail_Agent(string agentCode)
        {
            AGENT agent = new AGENT();
            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(string.Format("api/Agent/FindAgent?agentCode={0}", agentCode)).Result;

            if (response.IsSuccessStatusCode)
            {
                agent = response.Content.ReadAsAsync<AGENT>().Result;
            }
            loadDataIntoViewAddNewAgent(agent);
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
        public ActionResult Merchant(string MerchantType, string RegionType, string Active, List<string> MerchantTypeValue, List<string> RegionTypeValue, List<string> ActiveTypeValue, string searchString, int page = 1, int size = 10)
        {
            CheckBoxValue(ref MerchantType, ref MerchantTypeValue);
            ViewBag.tempMerchantType = MerchantType;

            CheckBoxValue(ref RegionType, ref RegionTypeValue);
            ViewBag.tempRegionType = RegionType;

            CheckBoxValue(ref Active, ref ActiveTypeValue);
            ViewBag.tempActive = Active;           

            List<MERCHANT> list = new List<MERCHANT>();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View("Index");
            HttpClient client = new AccessAPI().Access();
            @ViewBag.action = "Merchant";
            HttpResponseMessage responseMerchantType = client.GetAsync(string.Format("api/Merchant_Type/SelectAllMerchantType")).Result;
            HttpResponseMessage responseRegion = client.GetAsync(string.Format("api/REGION/FindAllRegion")).Result;
            if (responseMerchantType.IsSuccessStatusCode && responseRegion.IsSuccessStatusCode)
            {
                List<MERCHANT_TYPE> listMerchantType = responseMerchantType.Content.ReadAsAsync<List<MERCHANT_TYPE>>().Result;
                List<REGION> listRegion = responseRegion.Content.ReadAsAsync<List<REGION>>().Result;
                ViewBag.MerchantType = new SelectList(listMerchantType, "MerchantType", "Description");
                ViewBag.RegionType = new SelectList(listRegion, "RegionCode", "RegionName");
            }

            if (temp.UserType == "T")
            {
                if (String.IsNullOrEmpty(searchString))
                {
                    if(MerchantTypeValue == null &&  RegionTypeValue == null && ActiveTypeValue == null)
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
                        //string ViewBa = HttpUtility.UrlDecode(MerchantTypeValue[0]);
                        string abc = Request.QueryString["MerchantTypeValue"];
                        string query = queryFilter(MerchantTypeValue, RegionTypeValue, ActiveTypeValue);
                        //string url = HttpContext.Current.Request.Url.PathAndQuery;
                        //ViewBag.Url = Request.Url.Query;
                        List<MERCHANT> listMerchant = new List<MERCHANT>();
                        HttpResponseMessage responseFilter = client.GetAsync(string.Format("api/MERCHANT/FindFilter?query={0}", query)).Result;

                        if (responseFilter.IsSuccessStatusCode)
                        {
                            listMerchant = responseFilter.Content.ReadAsAsync<List<MERCHANT>>().Result;
                        }
                        //MerchantTypeValue=CL&MerchantTypeValue=DN
                        //string ViewBa = HttpUtility.UrlDecode(MerchantTypeValue[0]); 
                        var listMerchant_1 = listMerchant.ToPagedList(page, size);
                        ViewBag.MerchantTypeValue = MerchantTypeValue;
                        ViewBag.RegionTypeValue = RegionTypeValue;
                        ViewBag.ActiveTypeValue = ActiveTypeValue;
                        ViewBag.query = "CLMerchantTypeValueDN";//Server.UrlEncode("Dog&Cat"); ;

                        //MerchantTypeValue=CL&MerchantTypeValue=DN
                        return View(listMerchant_1);
                    }
                    
                }
                else
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindMerchantElement?searchString={0}", searchString)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
                    }
                    @ViewBag.searchString = searchString;
                    var listMerchant = list.ToPagedList(page, size);
                    return View(listMerchant);
                } 
            }
            else
            {
                if (temp.UserType == "A")
                {
                    if (String.IsNullOrEmpty(searchString))
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindMerchantByAgentCode?agentCode={0}", temp.UserName)).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
                        }
                        var listMerchant = list.ToPagedList(page, size);
                        return View(listMerchant);
                    }
                    else
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindMerchantByAgentCodeAndElement?searchString={0}&agentCode={1}", searchString, temp.UserName)).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
                        }
                        @ViewBag.searchString = searchString;
                        @ViewBag.agentCode = temp.UserName;
                        var listMerchant = list.ToPagedList(page, size);
                        return View(listMerchant);
                    }
                }
                else return View("Index");
            }
        }

        //[HttpGet]
        //public ActionResult FindMerchantElement(string searchString, int page = 1, int size = 10)
        //{
        //    List<MERCHANT> list = new List<MERCHANT>();
        //    var model = Session[CommonConstants.USER_SESSION];
        //    var temp = new USER_INFORMATION();
        //    if (model != null)
        //    {
        //        temp = (USER_INFORMATION)model;
        //    }
        //    else return View("Index");
        //    //client.BaseAddress = new Uri("http://localhost:21212/");

        //    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    HttpClient client = new AccessAPI().Access();

        //    if (temp.UserType == "T")
        //    {
        //        if (searchString == "")
        //        {
        //            return RedirectToAction("Merchant");
        //        }
        //        else
        //        {
        //            HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindMerchantElement?searchString={0}", searchString)).Result;

        //            if (response.IsSuccessStatusCode)
        //            {
        //                list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
        //            }
        //            @ViewBag.searchString = searchString;
        //        }
                
        //        var listMerchant = list.ToPagedList(page, size);
        //        return View("Merchant", listMerchant);
        //    }
        //    else
        //    {
        //        if (temp.UserType == "A")
        //        {
        //            if (searchString == "")
        //            {
        //                return RedirectToAction("Merchant");
        //            }
        //            else
        //            {
        //                HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindMerchantByAgentCodeAndElement?searchString={0}&agentCode={1}", searchString, temp.UserName)).Result;

        //                if (response.IsSuccessStatusCode)
        //                {
        //                    list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
        //                }
        //                @ViewBag.searchString = searchString;
        //                @ViewBag.agentCode = temp.UserName;
        //            }

        //            var listMerchant = list.ToPagedList(page, size);
        //            return View("Merchant", listMerchant);
        //        }
        //        else return View("Index");
        //    }
            
        //}

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
                List<AGENT> listAgent = responseAgent.Content.ReadAsAsync<List<AGENT>>().Result;
                List<STATUS> listStatus = new List<STATUS>();
                listStatus.Add(new STATUS() { ID = "A", Description = "ACTIVE" });
                listStatus.Add(new STATUS() { ID = "I", Description = "INACTIVE" });
                foreach (var Agent in listAgent )
                {
                    Agent.AgentName = Agent.AgentCode.ToString() + " - " + Agent.AgentName.ToString();
                }
                
                List<PROCESSOR> listProcessor = responseProcessor.Content.ReadAsAsync<List<PROCESSOR>>().Result;

                if (merchant != null)
                {
                    ViewBag.MerchantType = new SelectList(listMerchantType, "MerchantType", "Description", merchant.MerchantType);
                    ViewBag.BackEndProcessor = new SelectList(listProcessor, "ID", "ProcessorName", merchant.BackEndProcessor);
                    ViewBag.CityCode = new SelectList(listCity, "CityCode", "CityName", merchant.CityCode);
                    ViewBag.AgentCode = new SelectList(listAgent, "AgentCode", "AgentName", merchant.AgentCode);
                    ViewBag.Status = new SelectList(listStatus, "ID", "Description", merchant.Status);
                }
                else
                {
                    ViewBag.MerchantType = new SelectList(listMerchantType, "MerchantType", "Description");
                    ViewBag.BackEndProcessor = new SelectList(listProcessor, "ID", "ProcessorName");
                    ViewBag.CityCode = new SelectList(listCity, "CityCode", "CityName");
                    ViewBag.AgentCode = new SelectList(listAgent, "AgentCode", "AgentName");
                    ViewBag.Status = new SelectList(listStatus, "ID", "Description");

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
        [HttpPost]
        public ActionResult ViewDetail_Agent(AGENT agent)
        {
            var check = new bool();
            string id = agent.AgentCode;
            if (ModelState.IsValid)
            {
                HttpClient client = new AccessAPI().Access();
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("api/AGENT/UpdateAgent?id={0}", id), agent).Result;
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                    check = response.Content.ReadAsAsync<bool>().Result;

                if (check == true)
                    return RedirectToAction("Agent");
            }
            loadDataIntoViewAddNewAgent();
            return View(agent);
        }

        [HttpGet]
        public ActionResult AddNewAgent()
        {
            loadDataIntoViewAddNewAgent();
            return View();
        }

        [HttpPost]
        public ActionResult AddNewAgent(AGENT agent)
        {
            //cái macode agent may chuyen no vè ma chua hay de ten luon alo alo
            //string jsonMerchant = JsonConvert.SerializeObject(merchant);
            var check = new bool();
            //debug tipe di
            if (ModelState.IsValid)
            {
                HttpClient client = new AccessAPI().Access();
                HttpResponseMessage response = client.PostAsJsonAsync("api/AGENT/AddNewAgent", agent).Result;
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                    check = response.Content.ReadAsAsync<bool>().Result;

                if (check == true)
                    return RedirectToAction("Agent");
            }
            loadDataIntoViewAddNewAgent();
            return View(agent);
        }

        public void loadDataIntoViewAddNewAgent(AGENT agent = null)
        {
            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage responseCity = client.GetAsync(string.Format("api/City/SelectAllCity")).Result;
            List<STATUS> listStatus = new List<STATUS>();
            listStatus.Add(new STATUS() { ID = "A", Description = "ACTIVE" });
            listStatus.Add(new STATUS() { ID = "I", Description = "INACTIVE" });
            if (responseCity.IsSuccessStatusCode)
            {
                List<CITY> listCity = responseCity.Content.ReadAsAsync<List<CITY>>().Result;
                if (agent != null)
                {
                    ViewBag.CityCode = new SelectList(listCity, "CityCode", "CityName", agent.CityCode);
                    ViewBag.Status = new SelectList(listStatus, "ID", "Description",agent.AgentStatus);

                }
                else
                {
                    ViewBag.CityCode = new SelectList(listCity, "CityCode", "CityName");
                    ViewBag.Status = new SelectList(listStatus, "ID", "Description");
                }                
            }

        }

        [HttpGet]
        public ActionResult ViewListMerchant(string agentName, string agentCode, string regionCode, int page = 1, int size = 10)
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
            HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindMerchantByAgentCode?agentCode={0}", agentCode)).Result;
            if (response.IsSuccessStatusCode)
            {
                list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
            }
            @ViewBag.action = "ViewListMerchant";
            @ViewBag.agentCode = agentCode;
            @ViewBag.agentName = agentName;
            @ViewBag.regionCode = regionCode;
            var listMerchant = list.ToPagedList(page, size);
            loadDataIntoViewViewListMerchant(agentCode, regionCode);
            return View(listMerchant);
        }

        public void loadDataIntoViewViewListMerchant (string agentCode, string regionCode)
        {
            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindMerchantAvailable?agentCode={0}&regionCode={1}", agentCode, regionCode)).Result;
            if (response.IsSuccessStatusCode)
            {
                List<MERCHANT> listMerchant = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
                foreach (var merchant in listMerchant )
                {
                    merchant.MerchantName = merchant.MerchantCode.ToString() + " - " + merchant.MerchantName.ToString();
                }
                ViewBag.MerchantCode = new SelectList(listMerchant, "MerchantCode", "MerchantName");
            } 
             
        }

        [HttpPost]
        public ActionResult UpdateAgentOfMerchant(string merchantCode, string agentCode, string regionCode, int page = 1, int size = 10)
        {
            var check = new bool();
            MERCHANT merchant = new MERCHANT() { AgentCode = agentCode };
            if (ModelState.IsValid)
            {
                HttpClient client = new AccessAPI().Access();
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("api/MERCHANT/UpdateAgentOfMerchant?merchantCode={0}", merchantCode), merchant).Result;
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                    check = response.Content.ReadAsAsync<bool>().Result;
            }
            //if (check == true)
            //{
                
            //}
            List<MERCHANT> list = new List<MERCHANT>();
            HttpClient client1 = new AccessAPI().Access();
            HttpResponseMessage response1 = client1.GetAsync(string.Format("api/Merchant/FindMerchantByAgentCode?agentCode={0}", agentCode)).Result;
            if (response1.IsSuccessStatusCode)
            {
                list = response1.Content.ReadAsAsync<List<MERCHANT>>().Result;
            }
            @ViewBag.action = "ViewListMerchant";
            @ViewBag.agentCode = agentCode;
            @ViewBag.regionCode = regionCode;
            var listMerchant = list.ToPagedList(page, size);
            loadDataIntoViewViewListMerchant(agentCode, regionCode);
            return View("ViewListMerchant", listMerchant);
        }

        public ActionResult MerChantExportCSV(string searchString)
        {
            List<MERCHANT> list = new List<MERCHANT>();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View("Index");
            //client.BaseAddress = new Uri("http://localhost:21212/");

            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpClient client = new AccessAPI().Access();

            if (temp.UserType == "T")
            {
                if (searchString == "" || searchString == null)
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindAllMerchant")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
                    }
                }
                else
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindMerchantElement?searchString={0}", searchString)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
                    }
                    @ViewBag.searchString = searchString;
                }
            }
            else
            {
                if (temp.UserType == "A")
                {
                    if (searchString == "" || searchString == null)
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindMerchantByAgentCode?agentCode={0}", temp.UserName)).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
                        }
                    }
                    else
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindMerchantByAgentCodeAndElement?searchString={0}&agentCode={1}", searchString, temp.UserName)).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
                        }
                        @ViewBag.searchString = searchString;
                        @ViewBag.agentCode = temp.UserName;
                    }
                }
                else return View("Index");
            }

            StringWriter sw = new StringWriter();
            sw.WriteLine("Merchant Code,Merchant Name,Merchant Type,Status,Owner,Address,City,Last Active Date,Closed Date");
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=MERCHANT_LIST.csv");
            Response.ContentType = "text/csv";
            //var csv = new CsvWriter(sw);
            foreach (var item in list)
            {
                sw.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", item.MerchantCode, item.MerchantName, item.MerchantType, item.Status, item.Owner, item.Address1, item.CITY, item.LastActiveDate.ToString(), item.CloseDate.ToString()
                    ));
            }
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("Index");
        }

        public ActionResult AgentExportCSV(string searchString)
        {
            List<AGENT> list = new List<AGENT>();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View("Index");

            if (temp.UserType == "T")
            {
                HttpClient client = new AccessAPI().Access();
                if (searchString == "" || searchString == null)
                {
                    HttpResponseMessage response1 = client.GetAsync(string.Format("api/Agent/FindAllAgent")).Result;

                    if (response1.IsSuccessStatusCode)
                    {
                        list = response1.Content.ReadAsAsync<List<AGENT>>().Result;
                    }
                }

                HttpResponseMessage response = client.GetAsync(string.Format("api/Agent/FindAgentElement?searchString={0}", searchString)).Result;

                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<List<AGENT>>().Result;
                }
                @ViewBag.searchString = searchString;
            }
            else return View("Index");

            StringWriter sw = new StringWriter();
            sw.WriteLine("Agent Code,Agent Name,Status,Owner,Address,City,Last Active Date,Closed Date");
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=AGENT_LIST.csv");
            Response.ContentType = "text/csv";
            //var csv = new CsvWriter(sw);
            foreach (var item in list)
            {
                sw.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7}", item.AgentCode, item.AgentName, item.AgentStatus, item.Owner, item.Address1, item.CITY, item.LastActiveDate.ToString(), item.CloseDate.ToString()
                    ));
            }
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("Index");
        }

        public ActionResult MerChantExportExcel(string searchString)
        {
            List<MERCHANT> list = new List<MERCHANT>();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View("Index");
            //client.BaseAddress = new Uri("http://localhost:21212/");

            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpClient client = new AccessAPI().Access();

            if (temp.UserType == "T")
            {
                if (searchString == "" || searchString == null)
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindAllMerchant")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
                    }
                }
                else
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindMerchantElement?searchString={0}", searchString)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
                    }
                    @ViewBag.searchString = searchString;
                }
            }
            else
            {
                if (temp.UserType == "A")
                {
                    if (searchString == "" || searchString == null)
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindMerchantByAgentCode?agentCode={0}", temp.UserName)).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
                        }
                    }
                    else
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindMerchantByAgentCodeAndElement?searchString={0}&agentCode={1}", searchString, temp.UserName)).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
                        }
                        @ViewBag.searchString = searchString;
                        @ViewBag.agentCode = temp.UserName;
                    }
                }
                else return View("Index");
            }

            var gv = new GridView();
            gv.DataSource = list.ToList();
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=MERCHANT_LIST.xls");
            Response.ContentType = "appliation/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter tw = new HtmlTextWriter(sw);

            gv.RenderControl(tw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("Index");
        }

        public ActionResult AgentExportExcel(string searchString)
        {
            List<AGENT> list = new List<AGENT>();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View("Index");

            if (temp.UserType == "T")
            {
                HttpClient client = new AccessAPI().Access();
                if (searchString == "" || searchString == null)
                {
                    HttpResponseMessage response1 = client.GetAsync(string.Format("api/Agent/FindAllAgent")).Result;

                    if (response1.IsSuccessStatusCode)
                    {
                        list = response1.Content.ReadAsAsync<List<AGENT>>().Result;
                    }
                }

                HttpResponseMessage response = client.GetAsync(string.Format("api/Agent/FindAgentElement?searchString={0}", searchString)).Result;

                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<List<AGENT>>().Result;
                }
                @ViewBag.searchString = searchString;
            }
            else return View("Index");

            var gv = new GridView();
            gv.DataSource = list.ToList();
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=AGENT_LIST.xls");
            Response.ContentType = "appliation/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter tw = new HtmlTextWriter(sw);

            gv.RenderControl(tw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("Index");
        }

        private void CheckBoxValue(ref string tempCheck, ref List<string> TypeValue)
        {
            if (TypeValue == null && String.IsNullOrEmpty(tempCheck) == false)
                TypeValue = tempCheck.Split(',').ToList();
            if (String.IsNullOrEmpty(tempCheck) && TypeValue != null)
                tempCheck = String.Join(",", TypeValue);
        }
        public string queryFilter(List<string> MerchantTypeValue, List<string> RegionTypeValue, List<string> ActiveTypeValue)
        {
            string query = "select * from MERCHANT M where ";
            string ConditionMerchant = "";
            string ConditionRegion = "";
            string ConditionActive = "";
            if (MerchantTypeValue != null)
            {
                ConditionMerchant = ConditionMerchant + "(";
                for (int i = 0; i < MerchantTypeValue.Count; i++)
                {
                    ConditionMerchant = ConditionMerchant + "M.MerchantType = " + "'" + MerchantTypeValue[i] + "'";
                    if (i < MerchantTypeValue.Count - 1)
                    {
                        ConditionMerchant = ConditionMerchant + " or ";
                    }
                }
                ConditionMerchant = ConditionMerchant + ")";
                query = query + ConditionMerchant;
            }

            if (RegionTypeValue != null)
            {
                ConditionRegion = ConditionRegion + "(";
                for (int i = 0; i < RegionTypeValue.Count; i++)
                {
                    ConditionRegion = ConditionRegion + "M.RegionCode = " + "'" + RegionTypeValue[i] + "'";
                    if (i < RegionTypeValue.Count - 1)
                    {
                        ConditionRegion = ConditionRegion + " or ";
                    }
                }
                ConditionRegion = ConditionRegion + ")";

                if (MerchantTypeValue == null)
                {
                    query = query + ConditionRegion;
                }
                else
                {
                    query = query + " and " + ConditionRegion;
                }
            }

            if (ActiveTypeValue != null)
            {
                ConditionActive = ConditionActive + "(";
                for (int i = 0; i < ActiveTypeValue.Count; i++)
                {
                    ConditionActive = ConditionActive + "M.Status = " + "'" + ActiveTypeValue[i] + "'";
                    if (i < ActiveTypeValue.Count - 1)
                    {
                        ConditionActive = ConditionActive + " or ";
                    }
                }
                ConditionActive = ConditionActive + ")";

                if (RegionTypeValue == null && MerchantTypeValue == null)
                {
                    query = query + ConditionActive;
                }
                else
                {
                    query = query + " and " + ConditionActive;
                }
            }
            return query;
        }
    }
}

