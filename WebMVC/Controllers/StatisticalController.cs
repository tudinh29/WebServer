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
using ClosedXML.Excel;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;
using CsvHelper;
using WebAPI.Controllers;
using PagedList;
using System.Web.Mvc.Html;
using Newtonsoft.Json;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class StatisticalController : BaseController
    {
        // GET: Statistical
        public ActionResult Index(string MerchantType, string RegionType, List<string> MerchantTypeValue, List<string> RegionTypeValue, string searchString, int page = 1, int size = 10)
        {
            CheckBoxValue(ref MerchantType, ref MerchantTypeValue);
            ViewBag.tempMerchantType = MerchantType;

            CheckBoxValue(ref RegionType, ref RegionTypeValue);
            ViewBag.tempRegionType = RegionType;

         
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View();

            int totalPage = 0;
            int maxPage = 4;
            int totalRetrival = 0;
            List<MERCHANT_SUMMARY_DAILY> ListSummary = new List<MERCHANT_SUMMARY_DAILY>();

            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage responseMerchantType = client.GetAsync(string.Format("api/Merchant_Type/SelectAllMerchantType")).Result;
            HttpResponseMessage responseRegion = client.GetAsync(string.Format("api/REGION/FindAllRegion")).Result;
            if (responseMerchantType.IsSuccessStatusCode && responseRegion.IsSuccessStatusCode)
            {
                List<MERCHANT_TYPE> listMerchantType = responseMerchantType.Content.ReadAsAsync<List<MERCHANT_TYPE>>().Result;
                List<REGION> listRegion = responseRegion.Content.ReadAsAsync<List<REGION>>().Result;
                ViewBag.MerchantType = new SelectList(listMerchantType, "MerchantType", "Description");
                ViewBag.RegionType = new SelectList(listRegion, "RegionCode", "RegionName");
            }

            if (temp.UserType == "T")   //Master
            {
                if (String.IsNullOrEmpty(searchString))
                {
                    
                    if (MerchantTypeValue == null && RegionTypeValue == null)
                    {
                        HttpResponseMessage responseCount = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/CountMerchantSummaryDaily")).Result;
                        HttpResponseMessage response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/GetMerchantSummaryDefault_ForQuery?pageIndex={0}&pageSize={1}", page, size)).Result;
                        if (response.IsSuccessStatusCode && responseCount.IsSuccessStatusCode)
                        {
                            totalRetrival = responseCount.Content.ReadAsAsync<int>().Result;
                            ListSummary = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_DAILY>>().Result;
                        }
                    }
                    else
                    {
                        string queryFind = queryFilterStatistical(MerchantTypeValue, RegionTypeValue, "*");
                        queryFind = queryFind + " order by M.ReportDate Offset " + (page - 1) * size + " row fetch next " + size + " row only";
                        string queryCount = queryFilterStatistical(MerchantTypeValue, RegionTypeValue, "Count(*)");
                        HttpResponseMessage responseFilter = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/FindFilter?query={0}", queryFind)).Result;
                        HttpResponseMessage responseFilterCount = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/FindCountFilter?query={0}", queryCount)).Result;

                        if (responseFilter.IsSuccessStatusCode && responseFilterCount.IsSuccessStatusCode)
                        {
                            ListSummary = responseFilter.Content.ReadAsAsync<List<MERCHANT_SUMMARY_DAILY>>().Result;
                            totalRetrival = responseFilterCount.Content.ReadAsAsync<int>().Result;
                        }
                        ViewBag.MerchantTypeValue = MerchantTypeValue;
                        ViewBag.RegionTypeValue = RegionTypeValue;
                    }
                }
                else
                {
                    HttpResponseMessage responseCount = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/FindCountMerchantSummaryElement_ForQuery?searchString={0}", searchString)).Result;
                    HttpResponseMessage response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/FindMerchantSummaryElement_ForQuery?searchString={0}&pageIndex={1}&pageSize={2}", searchString, page, size)).Result;
                    if (response.IsSuccessStatusCode && responseCount.IsSuccessStatusCode)
                    {
                        totalRetrival = responseCount.Content.ReadAsAsync<int>().Result;
                        ListSummary = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_DAILY>>().Result;
                    }
                    @ViewBag.searchString = searchString;                            
                }
            }
            else if (temp.UserType == "A")
            {
                if (String.IsNullOrEmpty(searchString))
                {
                    if (MerchantTypeValue == null && RegionTypeValue == null)
                    {
                        HttpResponseMessage responseCount = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/GetCountMerchantSummaryForAgentDefault_ForQuery?AgentCode={0}", temp.UserName)).Result;
                        HttpResponseMessage response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/GetMerchantSummaryForAgentDefault_ForQuery?AgentCode={0}&pageIndex={1}&pageSize={2}",temp.UserName ,page, size)).Result;
                        if (response.IsSuccessStatusCode && responseCount.IsSuccessStatusCode)
                        {
                            totalRetrival = responseCount.Content.ReadAsAsync<int>().Result;
                            ListSummary = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_DAILY>>().Result;
                        }              
                    }
                    else
                    {
                        string queryFind = queryFilterStatistical(MerchantTypeValue, RegionTypeValue, "*");
                        queryFind = queryFind + "and M.AgentCode = '" + temp.UserName +"' order by M.ReportDate Offset " + (page - 1) * size + " row fetch next " + size + " row only";
                        string queryCount = queryFilterStatistical(MerchantTypeValue, RegionTypeValue, "Count(*)");
                        queryCount = queryCount + "and M.AgentCode = '" + temp.UserName + "'";

                        HttpResponseMessage responseFilter = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/FindFilter?query={0}", queryFind)).Result;
                        HttpResponseMessage responseFilterCount = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/FindCountFilter?query={0}", queryCount)).Result;

                        if (responseFilter.IsSuccessStatusCode && responseFilterCount.IsSuccessStatusCode)
                        {
                            ListSummary = responseFilter.Content.ReadAsAsync<List<MERCHANT_SUMMARY_DAILY>>().Result;
                            totalRetrival = responseFilterCount.Content.ReadAsAsync<int>().Result;
                        }
                        ViewBag.MerchantTypeValue = MerchantTypeValue;
                        ViewBag.RegionTypeValue = RegionTypeValue;         
                    }                 
                }
                else
                {
                    HttpResponseMessage responseCount = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/FindCountMerchantSummaryForAgentElement_ForQuery?searchString={0}&AgentCode={1}", searchString, temp.UserName)).Result;
                    HttpResponseMessage response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/FindMerchantSummaryForAgentElement_ForQuery?searchString={0}&AgentCode={1}&pageIndex={2}&pageSize={3}", searchString, temp.UserName, page, size)).Result;
                    if (response.IsSuccessStatusCode && responseCount.IsSuccessStatusCode)
                    {
                        totalRetrival = responseCount.Content.ReadAsAsync<int>().Result;
                        ListSummary = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_DAILY>>().Result;

                    }
                    @ViewBag.searchString = searchString;
                }
            }
            else return View();

            totalPage = (int)Math.Ceiling((double)totalRetrival / size);
            ViewBag.Total = totalRetrival;
            ViewBag.Page = page;
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            return View(ListSummary); 
            
        }
        public ActionResult ViewDetailDay(string MerchantCode)
        {
            //Tâm code
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View();
            List<MerchantSummaryDailyTiny> list = new List<MerchantSummaryDailyTiny>();
            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response;
            if (temp.UserType == "A")
            {

                response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/GetMerchantSummaryForAgentDefaultMerchantCode?AgentCode={0}&&MerchantCode={1}&&ReportDate={2}", temp.UserName, Request.QueryString["MerchantCode"], Request.QueryString["ReportDate"])).Result;
                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<List<Models.MerchantSummaryDailyTiny>>().Result;
                }
            }
            if (temp.UserType == "M")
            {
                response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/GetMerchantSummaryForMerchantCode?MerchantCode={0}", temp.UserName)).Result;
                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<List<Models.MerchantSummaryDailyTiny>>().Result;
                }
            }
            return View(list);
        }

        public ActionResult Month(string searchString, int page = 1, int size = 10)
        {
            // Diễm Code
            return View();
        }
        public ActionResult ViewDetailMonth(string MerchantCode)
        {
            return View();
        }
        public ActionResult Quarter(string searchString, int page = 1, int size = 10)
        {
            return View();
        }
        public ActionResult ViewDetailQuarter(string MerchantCode)
        {
            return View();
        }
        public ActionResult Year(string searchString, int page = 1, int size = 10)
        {
            return View();
        }
        public ActionResult ViewDetailYear(string MerchantCode)
        {
            return View();
        }

























        public ActionResult ExportPDF(string searchString)
        {
            //HttpClient client = new AccessAPI().Access();
            string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Page: [page] of [toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            List<Models.MerchantSummaryDailyTiny> lists = new List<Models.MerchantSummaryDailyTiny>();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View();

            if (temp.UserType == "T")   //Master
            {
                if (String.IsNullOrEmpty(searchString))
                {
                    lists = getAllSumDaily();
                    @ViewBag.searchString = searchString;
                }
                else
                {
                    HttpClient client = new AccessAPI().Access();
                    HttpResponseMessage response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/FindMerchantSummaryElement?searchString={0}", searchString)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lists = response.Content.ReadAsAsync<List<Models.MerchantSummaryDailyTiny>>().Result;
                    }
                    @ViewBag.searchString = searchString;
                }
            }
            else if (temp.UserType == "A")
            {
                if (String.IsNullOrEmpty(searchString))
                {
                    HttpClient client = new AccessAPI().Access();
                    HttpResponseMessage response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/GetMerchantSummaryForAgentDefault?AgentCode={0}", temp.UserName)).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        lists = response.Content.ReadAsAsync<List<Models.MerchantSummaryDailyTiny>>().Result;
                    }
                    @ViewBag.searchString = searchString;
                }
                else
                {
                    HttpClient client = new AccessAPI().Access();
                    HttpResponseMessage response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/FindMerchantSummaryForAgentElement?AgentCode={0}&&searchString={1}", temp.UserName, searchString)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lists = response.Content.ReadAsAsync<List<Models.MerchantSummaryDailyTiny>>().Result;
                    }
                    @ViewBag.searchString = searchString;
                }
            }
            else return View();
            return new Rotativa.PartialViewAsPdf("MerchantStatistical", lists)
            {   //MerchantSumaryDailyStatistical
                FileName = "MerchantStatistical.pdf",
                CustomSwitches = footer
            };
        }

        private List<Models.MerchantSummaryDailyTiny> getAllSumDaily()
        {
            var merchantSummary = new List<Models.MerchantSummaryDailyTiny>();

            string domain = "";
            string url = domain + "/api/MERCHANT_SUMMARY_DAILY/GetMerchantSummaryDefault";

            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                merchantSummary = response.Content.ReadAsAsync<List<Models.MerchantSummaryDailyTiny>>().Result;
            }

            return merchantSummary;
        }

        public ActionResult ExportExcel(string searchString)
        {
            List<Models.MerchantSummaryDailyTiny> lists = new List<Models.MerchantSummaryDailyTiny>();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View();

            if (temp.UserType == "T")   //Master
            {
                if (String.IsNullOrEmpty(searchString))
                {
                    lists = getAllSumDaily();
                    @ViewBag.searchString = searchString;
                }
                else
                {
                    HttpClient client = new AccessAPI().Access();
                    HttpResponseMessage response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/FindMerchantSummaryElement?searchString={0}", searchString)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lists = response.Content.ReadAsAsync<List<Models.MerchantSummaryDailyTiny>>().Result;
                    }
                    @ViewBag.searchString = searchString;
                }
            }
            else if (temp.UserType == "A")
            {
                if (String.IsNullOrEmpty(searchString))
                {
                    HttpClient client = new AccessAPI().Access();
                    HttpResponseMessage response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/GetMerchantSummaryForAgentDefault?AgentCode={0}", temp.UserName)).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        lists = response.Content.ReadAsAsync<List<Models.MerchantSummaryDailyTiny>>().Result;
                    }
                    @ViewBag.searchString = searchString;
                }
                else
                {
                    HttpClient client = new AccessAPI().Access();
                    HttpResponseMessage response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/FindMerchantSummaryForAgentElement?AgentCode={0}&&searchString={1}", temp.UserName, searchString)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lists = response.Content.ReadAsAsync<List<Models.MerchantSummaryDailyTiny>>().Result;
                    }
                    @ViewBag.searchString = searchString;
                }
            }
            else return View();

            var gv = new GridView();
            gv.DataSource = lists;
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=MERCHANT_SUMMARY_DAILY.xls");
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

        public ActionResult ExportCSV(string searchString)
        {
            List<Models.MerchantSummaryDailyTiny> lists = new List<Models.MerchantSummaryDailyTiny>();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View();

            if (temp.UserType == "T")   //Master
            {
                if (String.IsNullOrEmpty(searchString))
                {
                    lists = getAllSumDaily();
                }
                else
                {
                    HttpClient client = new AccessAPI().Access();
                    HttpResponseMessage response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/FindMerchantSummaryElement?searchString={0}", searchString)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lists = response.Content.ReadAsAsync<List<Models.MerchantSummaryDailyTiny>>().Result;
                    }
                    @ViewBag.searchString = searchString;
                }
            }
            else if (temp.UserType == "A")
            {
                if (String.IsNullOrEmpty(searchString))
                {
                    HttpClient client = new AccessAPI().Access();
                    HttpResponseMessage response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/GetMerchantSummaryForAgentDefault?AgentCode={0}", temp.UserName)).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        lists = response.Content.ReadAsAsync<List<Models.MerchantSummaryDailyTiny>>().Result;
                    }
                    @ViewBag.searchString = searchString;
                }
                else
                {
                    HttpClient client = new AccessAPI().Access();
                    HttpResponseMessage response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/FindMerchantSummaryForAgentElement?AgentCode={0}&&searchString={1}", temp.UserName, searchString)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lists = response.Content.ReadAsAsync<List<Models.MerchantSummaryDailyTiny>>().Result;
                    }
                    @ViewBag.searchString = searchString;
                }
            }
            else return View();

            StringWriter sw = new StringWriter();
            sw.WriteLine("Merchant Code,Sale Amount,Sale Count,Return Amount,Return Count,Net Amount,Transaction Count,Key Amount,Report Date");
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=MERCHANT_LIST.csv");
            Response.ContentType = "text/csv";
            //var csv = new CsvWriter(sw);
            foreach (var item in lists)
            {
                sw.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", item.MerchantCode, item.SaleAmount, item.SaleCount, item.ReturnAmount, item.ReturnCount, item.NetAmount, item.TransactionCount, item.KeyedAmount, item.ReportDate.ToString()
                    ));
            }
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
        public string queryFilterStatistical(List<string> MerchantTypeValue, List<string> RegionTypeValue, string condition)
        {
            string query = "select " + condition + " from MERCHANT_SUMMARY_DAILY M where ";
            string ConditionMerchant = "";
            string ConditionRegion = "";
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
            return query;
        }
    }
}