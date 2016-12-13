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

namespace WebMVC.Controllers
{
    public class StatisticalController : BaseController
    {
        // GET: Statistical
        public ActionResult Index(string searchString,int page = 1, int size = 10)
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
                    var listStatic = getAllSumDaily().ToPagedList(page, size);
                    return View(listStatic);       
                }
                else
                {
                    HttpClient client = new AccessAPI().Access();
                    HttpResponseMessage response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/FindMerchantSummaryElement?searchString={0}", searchString)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lists = response.Content.ReadAsAsync<List<Models.MerchantSummaryDailyTiny>>().Result;
                    }
                    var listStatic = lists.ToPagedList(page, size);
                    @ViewBag.searchString = searchString;
                    return View(listStatic);
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
                    var listStatic = lists.ToPagedList(page, size);
                    @ViewBag.searchString = searchString;
                    return View(listStatic);       
                }
                else
                {
                    HttpClient client = new AccessAPI().Access();
                    HttpResponseMessage response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/FindMerchantSummaryForAgentElement?AgentCode={0}&&searchString={1}",temp.UserName,searchString)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        lists = response.Content.ReadAsAsync<List<Models.MerchantSummaryDailyTiny>>().Result;
                    }
                    var listStatic = lists.ToPagedList(page, size);
                    @ViewBag.searchString = searchString;
                    return View(listStatic);
                }
            }
            else return View();
        }
        public ActionResult ViewDetailDay(string MerchantCode)
        {
            //Tâm code
            return View();
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

        
    }
}