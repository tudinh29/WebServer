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
            List<Models.MerchantSummaryDailyTiny> lists = new List<Models.MerchantSummaryDailyTiny>();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View();

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
                        var listStatic = getAllSumDaily().ToPagedList(page, size);
                        return View(listStatic);
                    }
                    else
                    {
                        string query = queryFilterStatistical(MerchantTypeValue, RegionTypeValue);
                        List<MerchantSummaryDailyTiny> listStatistical = new List<MerchantSummaryDailyTiny>();
                        HttpResponseMessage responseFilter = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/FindFilter?query={0}", query)).Result;

                        if (responseFilter.IsSuccessStatusCode)
                        {
                            listStatistical = responseFilter.Content.ReadAsAsync<List<MerchantSummaryDailyTiny>>().Result;
                        }
                        var listStatistical_1 = listStatistical.ToPagedList(page, size);
                        ViewBag.MerchantTypeValue = MerchantTypeValue;
                        ViewBag.RegionTypeValue = RegionTypeValue;
                        return View(listStatistical_1);
                    }
                }
                else
                {
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
                    if (MerchantTypeValue == null && RegionTypeValue == null)
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/GetMerchantSummaryForAgentDefault?AgentCode={0}", temp.UserName)).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            lists = response.Content.ReadAsAsync<List<Models.MerchantSummaryDailyTiny>>().Result;
                        }
                        var listStatic = lists.ToPagedList(page, size);
                        return View(listStatic);   
                    }
                    else
                    {
                        string query = queryFilterStatistical(MerchantTypeValue, RegionTypeValue);
                        query = query + "and AgentCode = '" + temp.UserName + "'";
                        List<MerchantSummaryDailyTiny> listStatistical = new List<MerchantSummaryDailyTiny>();
                        HttpResponseMessage responseFilter = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/FindFilter?query={0}", query)).Result;

                        if (responseFilter.IsSuccessStatusCode)
                        {
                            listStatistical = responseFilter.Content.ReadAsAsync<List<MerchantSummaryDailyTiny>>().Result;
                        }
                        var listStatistical_1 = listStatistical.ToPagedList(page, size);
                        ViewBag.MerchantTypeValue = MerchantTypeValue;
                        ViewBag.RegionTypeValue = RegionTypeValue;
                        return View(listStatistical_1);
                    }
                       
                }
                else
                {
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

        public ActionResult ExportPDF()
        {
            HttpClient client = new AccessAPI().Access();
            string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Page: [page] of [toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            HttpResponseMessage response = client.GetAsync(string.Format("api/Merchant/FindAllMerchant")).Result;
            var list = new List<MERCHANT>();
            if (response.IsSuccessStatusCode)
            {
                list = response.Content.ReadAsAsync<List<MERCHANT>>().Result;
            }
            return new Rotativa.PartialViewAsPdf("MerchantStatistical", list)
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
        public string queryFilterStatistical(List<string> MerchantTypeValue, List<string> RegionTypeValue)
        {
            string query = "select * from MERCHANT_SUMMARY_DAILY M where ";
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