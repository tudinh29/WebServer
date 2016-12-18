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
			@ViewBag.searchString = searchString;
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
        public ActionResult ViewDetailDay(string MerchantCode)
        {
            //Tâm code
            return View();
        }

        public ActionResult Month(string MerchantType, string RegionType, List<string> MerchantTypeValue, List<string> RegionTypeValue, string searchString, int page = 1, int size = 10)
        {
            List<MERCHANT_SUMMARY_MONTHLY> list = new List<MERCHANT_SUMMARY_MONTHLY>();
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
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:21212/");
            //Filter
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
            //End Filter
            CheckBoxValue(ref MerchantType, ref MerchantTypeValue);
            ViewBag.tempMerchantType = MerchantType;

            CheckBoxValue(ref RegionType, ref RegionTypeValue);
            ViewBag.tempRegionType = RegionType;
            //End Filter
            if (MerchantTypeValue != null || RegionTypeValue != null)
            {
                //Có thì lọc thôi
                string name = "";
                string table = "MERCHANT_SUMMARY_MONTHLY";
                if (temp.UserType == "A")
                {
                    name = temp.UserName;
                }
                string findQuery = FindFilterYearly(table, name, MerchantTypeValue, RegionTypeValue, page - 1, size);
                string countQuery = CountFilterYearly(table, name, MerchantTypeValue, RegionTypeValue);
                HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindFilterMonthly?query={0}", findQuery)).Result;

                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_MONTHLY>>().Result;
                    HttpResponseMessage response2 = client.GetAsync(string.Format("api/Statistical/CountFilter?query={0}", countQuery)).Result;
                    if (response2.IsSuccessStatusCode)
                    {
                        totalRetrival = response2.Content.ReadAsAsync<int>().Result;
                    }
                    totalPage = (int)Math.Ceiling((double)totalRetrival / size);
                    ViewBag.Total = totalRetrival;
                    ViewBag.Page = page;
                    ViewBag.TotalPage = totalPage;
                    ViewBag.MaxPage = maxPage;
                    ViewBag.First = 1;
                    ViewBag.Last = totalPage;
                }
                ViewBag.MerchantTypeValue = MerchantTypeValue;
                ViewBag.RegionTypeValue = RegionTypeValue;
                return View(list.ToList());
            }
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (temp.UserType == "T")
            {
                if (String.IsNullOrEmpty(searchString))
                {
                    HttpResponseMessage response1 = client.GetAsync(string.Format("api/Statistical/CountMerchantSummaryMonthly")).Result;

                    if (response1.IsSuccessStatusCode)
                    {
                        totalRetrival = response1.Content.ReadAsAsync<int>().Result;
                    }

                    HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindAllMerchantSummaryMonthly?pageIndex={0}&pageSize={1}", page - 1, size)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_MONTHLY>>().Result;
                        totalPage = (int)Math.Ceiling((double)totalRetrival / size);
                        ViewBag.Total = totalRetrival;
                        ViewBag.Page = page;
                        ViewBag.TotalPage = totalPage;
                        ViewBag.MaxPage = maxPage;
                        ViewBag.First = 1;
                        ViewBag.Last = totalPage;
                    }
                    return View(list.ToList());
                }
                else
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindMerchantSummaryMonthlyElement?searchString={0}&pageIndex={1}&pageSize={2}", searchString, page - 1, size)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_MONTHLY>>().Result;
                        HttpResponseMessage response2 = client.GetAsync(string.Format("api/Statistical/CountMerchantSummaryMonthlyElement?searchString={0}", searchString)).Result;
                        if (response2.IsSuccessStatusCode)
                        {
                            totalRetrival = response2.Content.ReadAsAsync<int>().Result;
                        }
                        totalPage = (int)Math.Ceiling((double)totalRetrival / size);
                        ViewBag.Total = totalRetrival;
                        ViewBag.Page = page;
                        ViewBag.TotalPage = totalPage;
                        ViewBag.MaxPage = maxPage;
                        ViewBag.First = 1;
                        ViewBag.Last = totalPage;
                    }
                    @ViewBag.searchString = searchString;
                    return View(list.ToList());
                }
            }
            else
            {
                if (temp.UserType == "A")
                {
                    //agent
                    if (String.IsNullOrEmpty(searchString))
                    {
                        HttpResponseMessage response1 = client.GetAsync(string.Format("api/Statistical/CountMerchantSummaryMonthly_Agent?AgentCode={0}", temp.UserName)).Result;

                        if (response1.IsSuccessStatusCode)
                        {
                            totalRetrival = response1.Content.ReadAsAsync<int>().Result;
                        }

                        HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindAllMerchantSummaryMonthly_Agent?pageIndex={0}&pageSize={1}&AgentCode={2}", page - 1, size, temp.UserName)).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_MONTHLY>>().Result;
                            totalPage = (int)Math.Ceiling((double)totalRetrival / size);
                            ViewBag.Total = totalRetrival;
                            ViewBag.Page = page;
                            ViewBag.TotalPage = totalPage;
                            ViewBag.MaxPage = maxPage;
                            ViewBag.First = 1;
                            ViewBag.Last = totalPage;
                        }
                        return View(list.ToList());
                    }
                    else
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindMerchantSummaryMonthlyElement_Agent?searchString={0}&pageIndex={1}&pageSize={2}&AgentCode={3}", searchString, page - 1, size, temp.UserName)).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_MONTHLY>>().Result;
                            HttpResponseMessage response2 = client.GetAsync(string.Format("api/Statistical/CountMerchantSummaryMonthlyElement_Agent?searchString={0}&AgentCode={1}", searchString, temp.UserName)).Result;
                            if (response2.IsSuccessStatusCode)
                            {
                                totalRetrival = response2.Content.ReadAsAsync<int>().Result;
                            }
                            totalPage = (int)Math.Ceiling((double)totalRetrival / size);
                            ViewBag.Total = totalRetrival;
                            ViewBag.Page = page;
                            ViewBag.TotalPage = totalPage;
                            ViewBag.MaxPage = maxPage;
                            ViewBag.First = 1;
                            ViewBag.Last = totalPage;
                        }
                        @ViewBag.searchString = searchString;
                        return View(list.ToList());
                    }
                }
                else
                {
                    //merchant
                    if (String.IsNullOrEmpty(searchString))
                    {
                        HttpResponseMessage response1 = client.GetAsync(string.Format("api/Statistical/CountMerchantSummaryMonthly_Merchant?MerchantCode={0}", temp.UserName)).Result;

                        if (response1.IsSuccessStatusCode)
                        {
                            totalRetrival = response1.Content.ReadAsAsync<int>().Result;
                        }

                        HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindAllMerchantSummaryMonthly_Merchant?pageIndex={0}&pageSize={1}&MerchantCode={2}", page - 1, size, temp.UserName)).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_MONTHLY>>().Result;
                            totalPage = (int)Math.Ceiling((double)totalRetrival / size);
                            ViewBag.Total = totalRetrival;
                            ViewBag.Page = page;
                            ViewBag.TotalPage = totalPage;
                            ViewBag.MaxPage = maxPage;
                            ViewBag.First = 1;
                            ViewBag.Last = totalPage;
                        }
                        return View(list.ToList());
                    }
                    else
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindMerchantSummaryMonthlyElement_Merchant?searchString={0}&pageIndex={1}&pageSize={2}&MerchantCode={3}", searchString, page - 1, size, temp.UserName)).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_MONTHLY>>().Result;
                            HttpResponseMessage response2 = client.GetAsync(string.Format("api/Statistical/CountMerchantSummaryMonthlyElement_MerchantCode?searchString={0}&MerchantCode={1}", searchString, temp.UserName)).Result;
                            if (response2.IsSuccessStatusCode)
                            {
                                totalRetrival = response2.Content.ReadAsAsync<int>().Result;
                            }
                            totalPage = (int)Math.Ceiling((double)totalRetrival / size);
                            ViewBag.Total = totalRetrival;
                            ViewBag.Page = page;
                            ViewBag.TotalPage = totalPage;
                            ViewBag.MaxPage = maxPage;
                            ViewBag.First = 1;
                            ViewBag.Last = totalPage;
                        }
                        @ViewBag.searchString = searchString;
                        return View(list.ToList());
                    }
                }
            }
        }
        public ActionResult ViewDetailMonth(string ReportMonth, string ReportYear, string MerchantCode)
        {
            MERCHANT_SUMMARY_MONTHLY list = new MERCHANT_SUMMARY_MONTHLY();
            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindMerchantSummaryMonthly?ReportMonth={0}&ReportYear={1}&MerchantCode={2}", ReportMonth, ReportYear, MerchantCode)).Result;
            if (response.IsSuccessStatusCode)
            {
                list = response.Content.ReadAsAsync<MERCHANT_SUMMARY_MONTHLY>().Result;
            }
            @ViewBag.ReportMonth = ReportMonth;
            @ViewBag.ReportYear = ReportYear;
            @ViewBag.MerchantCode = MerchantCode;
            return View(list);
        }
        public ActionResult Quarter(string MerchantType, string RegionType, List<string> MerchantTypeValue, List<string> RegionTypeValue, string searchString, int page = 1, int size = 10)
        {
            List<MERCHANT_SUMMARY_QUARTERLY> list = new List<MERCHANT_SUMMARY_QUARTERLY>();
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
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:21212/");
            //Filter
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
            //End Filter
            CheckBoxValue(ref MerchantType, ref MerchantTypeValue);
            ViewBag.tempMerchantType = MerchantType;

            CheckBoxValue(ref RegionType, ref RegionTypeValue);
            ViewBag.tempRegionType = RegionType;
            //End Filter
            if (MerchantTypeValue != null || RegionTypeValue != null)
            {
                //Có thì lọc thôi
                string name = "";
                string table = "MERCHANT_SUMMARY_QUARTERLY";
                if (temp.UserType == "A")
                {
                    name = temp.UserName;
                }
                string findQuery = FindFilterYearly(table, name, MerchantTypeValue, RegionTypeValue, page - 1, size);
                string countQuery = CountFilterYearly(table, name, MerchantTypeValue, RegionTypeValue);
                HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindFilterQuarterly?query={0}", findQuery)).Result;

                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_QUARTERLY>>().Result;
                    HttpResponseMessage response2 = client.GetAsync(string.Format("api/Statistical/CountFilter?query={0}", countQuery)).Result;
                    if (response2.IsSuccessStatusCode)
                    {
                        totalRetrival = response2.Content.ReadAsAsync<int>().Result;
                    }
                    totalPage = (int)Math.Ceiling((double)totalRetrival / size);
                    ViewBag.Total = totalRetrival;
                    ViewBag.Page = page;
                    ViewBag.TotalPage = totalPage;
                    ViewBag.MaxPage = maxPage;
                    ViewBag.First = 1;
                    ViewBag.Last = totalPage;
                }
                ViewBag.MerchantTypeValue = MerchantTypeValue;
                ViewBag.RegionTypeValue = RegionTypeValue;
                return View(list.ToList());
            }
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (temp.UserType == "T")
            {
                if (String.IsNullOrEmpty(searchString))
                {
                    HttpResponseMessage response1 = client.GetAsync(string.Format("api/Statistical/CountMerchantSummaryQuarterly")).Result;

                    if (response1.IsSuccessStatusCode)
                    {
                        totalRetrival = response1.Content.ReadAsAsync<int>().Result;
                    }

                    HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindAllMerchantSummaryQuarterly?pageIndex={0}&pageSize={1}", page - 1, size)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_QUARTERLY>>().Result;
                        totalPage = (int)Math.Ceiling((double)totalRetrival / size);
                        ViewBag.Total = totalRetrival;
                        ViewBag.Page = page;
                        ViewBag.TotalPage = totalPage;
                        ViewBag.MaxPage = maxPage;
                        ViewBag.First = 1;
                        ViewBag.Last = totalPage;
                    }
                    return View(list.ToList());
                }
                else
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindMerchantSummaryQuarterlyElement?searchString={0}&pageIndex={1}&pageSize={2}", searchString, page - 1, size)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_QUARTERLY>>().Result;
                        HttpResponseMessage response2 = client.GetAsync(string.Format("api/Statistical/CountMerchantSummaryQuarterlyElement?searchString={0}", searchString)).Result;
                        if (response2.IsSuccessStatusCode)
                        {
                            totalRetrival = response2.Content.ReadAsAsync<int>().Result;
                        }
                        totalPage = (int)Math.Ceiling((double)totalRetrival / size);
                        ViewBag.Total = totalRetrival;
                        ViewBag.Page = page;
                        ViewBag.TotalPage = totalPage;
                        ViewBag.MaxPage = maxPage;
                        ViewBag.First = 1;
                        ViewBag.Last = totalPage;
                    }
                    @ViewBag.searchString = searchString;
                    return View(list.ToList());
                }
            }
            else
            {
                if (temp.UserType == "A")
                {
                    //agent
                    if (String.IsNullOrEmpty(searchString))
                    {
                        HttpResponseMessage response1 = client.GetAsync(string.Format("api/Statistical/CountMerchantSummaryQuarterly_Agent?AgentCode={0}", temp.UserName)).Result;

                        if (response1.IsSuccessStatusCode)
                        {
                            totalRetrival = response1.Content.ReadAsAsync<int>().Result;
                        }

                        HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindAllMerchantSummaryQuarterly_Agent?pageIndex={0}&pageSize={1}&AgentCode={2}", page - 1, size, temp.UserName)).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_QUARTERLY>>().Result;
                            totalPage = (int)Math.Ceiling((double)totalRetrival / size);
                            ViewBag.Total = totalRetrival;
                            ViewBag.Page = page;
                            ViewBag.TotalPage = totalPage;
                            ViewBag.MaxPage = maxPage;
                            ViewBag.First = 1;
                            ViewBag.Last = totalPage;
                        }
                        return View(list.ToList());
                    }
                    else
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindMerchantSummaryQuarterlyElement_Agent?searchString={0}&pageIndex={1}&pageSize={2}&AgentCode={3}", searchString, page - 1, size, temp.UserName)).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_QUARTERLY>>().Result;
                            HttpResponseMessage response2 = client.GetAsync(string.Format("api/Statistical/CountMerchantSummaryQuarterlyElement_Agent?searchString={0}&AgentCode={1}", searchString, temp.UserName)).Result;
                            if (response2.IsSuccessStatusCode)
                            {
                                totalRetrival = response2.Content.ReadAsAsync<int>().Result;
                            }
                            totalPage = (int)Math.Ceiling((double)totalRetrival / size);
                            ViewBag.Total = totalRetrival;
                            ViewBag.Page = page;
                            ViewBag.TotalPage = totalPage;
                            ViewBag.MaxPage = maxPage;
                            ViewBag.First = 1;
                            ViewBag.Last = totalPage;
                        }
                        @ViewBag.searchString = searchString;
                        return View(list.ToList());
                    }
                }
                else
                {
                    //merchant
                    if (String.IsNullOrEmpty(searchString))
                    {
                        HttpResponseMessage response1 = client.GetAsync(string.Format("api/Statistical/CountMerchantSummaryQuarterly_Merchant?MerchantCode={0}", temp.UserName)).Result;

                        if (response1.IsSuccessStatusCode)
                        {
                            totalRetrival = response1.Content.ReadAsAsync<int>().Result;
                        }

                        HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindAllMerchantSummaryQuarterly_Merchant?pageIndex={0}&pageSize={1}&MerchantCode={2}", page - 1, size, temp.UserName)).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_QUARTERLY>>().Result;
                            totalPage = (int)Math.Ceiling((double)totalRetrival / size);
                            ViewBag.Total = totalRetrival;
                            ViewBag.Page = page;
                            ViewBag.TotalPage = totalPage;
                            ViewBag.MaxPage = maxPage;
                            ViewBag.First = 1;
                            ViewBag.Last = totalPage;
                        }
                        return View(list.ToList());
                    }
                    else
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindMerchantSummaryQuarterlyElement_Merchant?searchString={0}&pageIndex={1}&pageSize={2}&MerchantCode={3}", searchString, page - 1, size, temp.UserName)).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_QUARTERLY>>().Result;
                            HttpResponseMessage response2 = client.GetAsync(string.Format("api/Statistical/CountMerchantSummaryQuarterlyElement_MerchantCode?searchString={0}&MerchantCode={1}", searchString, temp.UserName)).Result;
                            if (response2.IsSuccessStatusCode)
                            {
                                totalRetrival = response2.Content.ReadAsAsync<int>().Result;
                            }
                            totalPage = (int)Math.Ceiling((double)totalRetrival / size);
                            ViewBag.Total = totalRetrival;
                            ViewBag.Page = page;
                            ViewBag.TotalPage = totalPage;
                            ViewBag.MaxPage = maxPage;
                            ViewBag.First = 1;
                            ViewBag.Last = totalPage;
                        }
                        @ViewBag.searchString = searchString;
                        return View(list.ToList());
                    }
                }
            }
        }
        public ActionResult ViewDetailQuarter(string ReportQuarter, string ReportYear, string MerchantCode)
        {
            MERCHANT_SUMMARY_QUARTERLY list = new MERCHANT_SUMMARY_QUARTERLY();
            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindMerchantSummaryQuarterly?ReportQuarter={0}&ReportYear={1}&MerchantCode={2}", ReportQuarter, ReportYear, MerchantCode)).Result;
            if (response.IsSuccessStatusCode)
            {
                list = response.Content.ReadAsAsync<MERCHANT_SUMMARY_QUARTERLY>().Result;
            }
            @ViewBag.ReportQuarter = ReportQuarter;
            @ViewBag.ReportYear = ReportYear;
            @ViewBag.MerchantCode = MerchantCode;
            return View(list);
        }
        public ActionResult Year(string MerchantType, string RegionType, List<string> MerchantTypeValue, List<string> RegionTypeValue, string searchString, int page = 1, int size = 10)
        {
            List<MERCHANT_SUMMARY_YEARLY> list = new List<MERCHANT_SUMMARY_YEARLY>();
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
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:21212/");
            //Filter
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

            CheckBoxValue(ref MerchantType, ref MerchantTypeValue);
            ViewBag.tempMerchantType = MerchantType;

            CheckBoxValue(ref RegionType, ref RegionTypeValue);
            ViewBag.tempRegionType = RegionType;
            //End Filter
            if (MerchantTypeValue != null || RegionTypeValue != null)
            {
                //Có thì lọc thôi
                string name = "";
                string table = "MERCHANT_SUMMARY_YEARLY";
                if (temp.UserType == "A")
                {
                    name = temp.UserName;
                }
                string findQuery = FindFilterYearly(table, name, MerchantTypeValue, RegionTypeValue, page - 1, size);
                string countQuery = CountFilterYearly(table, name, MerchantTypeValue, RegionTypeValue);
                HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindFilterYearly?query={0}", findQuery)).Result;

                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_YEARLY>>().Result;
                    HttpResponseMessage response2 = client.GetAsync(string.Format("api/Statistical/CountFilter?query={0}", countQuery)).Result;
                    if (response2.IsSuccessStatusCode)
                    {
                        totalRetrival = response2.Content.ReadAsAsync<int>().Result;
                    }
                    totalPage = (int)Math.Ceiling((double)totalRetrival / size);
                    ViewBag.Total = totalRetrival;
                    ViewBag.Page = page;
                    ViewBag.TotalPage = totalPage;
                    ViewBag.MaxPage = maxPage;
                    ViewBag.First = 1;
                    ViewBag.Last = totalPage;
                }
                ViewBag.MerchantTypeValue = MerchantTypeValue;
                ViewBag.RegionTypeValue = RegionTypeValue;
                return View(list.ToList());
            }
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (temp.UserType == "T")
            {
                if (String.IsNullOrEmpty(searchString))
                {
                    HttpResponseMessage response1 = client.GetAsync(string.Format("api/Statistical/CountMerchantSummaryYearly")).Result;

                    if (response1.IsSuccessStatusCode)
                    {
                        totalRetrival = response1.Content.ReadAsAsync<int>().Result;
                    }

                    HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindAllMerchantSummaryYearly?pageIndex={0}&pageSize={1}", page - 1, size)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_YEARLY>>().Result;
                        totalPage = (int)Math.Ceiling((double)totalRetrival / size);
                        ViewBag.Total = totalRetrival;
                        ViewBag.Page = page;
                        ViewBag.TotalPage = totalPage;
                        ViewBag.MaxPage = maxPage;
                        ViewBag.First = 1;
                        ViewBag.Last = totalPage;
                    }
                    return View(list.ToList());
                }
                else
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindMerchantSummaryYearlyElement?searchString={0}&pageIndex={1}&pageSize={2}", searchString, page - 1, size)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_YEARLY>>().Result;
                        HttpResponseMessage response2 = client.GetAsync(string.Format("api/Statistical/CountMerchantSummaryYearlyElement?searchString={0}", searchString)).Result;
                        if (response2.IsSuccessStatusCode)
                        {
                            totalRetrival = response2.Content.ReadAsAsync<int>().Result;
                        }
                        totalPage = (int)Math.Ceiling((double)totalRetrival / size);
                        ViewBag.Total = totalRetrival;
                        ViewBag.Page = page;
                        ViewBag.TotalPage = totalPage;
                        ViewBag.MaxPage = maxPage;
                        ViewBag.First = 1;
                        ViewBag.Last = totalPage;
                    }
                    @ViewBag.searchString = searchString;
                    return View(list.ToList());
                }
            }
            else
            {
                if (temp.UserType == "A")
                {
                    //agent
                    if (String.IsNullOrEmpty(searchString))
                    {
                        HttpResponseMessage response1 = client.GetAsync(string.Format("api/Statistical/CountMerchantSummaryYearly_Agent?AgentCode={0}", temp.UserName)).Result;

                        if (response1.IsSuccessStatusCode)
                        {
                            totalRetrival = response1.Content.ReadAsAsync<int>().Result;
                        }

                        HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindAllMerchantSummaryYearly_Agent?pageIndex={0}&pageSize={1}&AgentCode={2}", page - 1, size, temp.UserName)).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_YEARLY>>().Result;
                            totalPage = (int)Math.Ceiling((double)totalRetrival / size);
                            ViewBag.Total = totalRetrival;
                            ViewBag.Page = page;
                            ViewBag.TotalPage = totalPage;
                            ViewBag.MaxPage = maxPage;
                            ViewBag.First = 1;
                            ViewBag.Last = totalPage;
                        }
                        return View(list.ToList());
                    }
                    else
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindMerchantSummaryYearlyElement_Agent?searchString={0}&pageIndex={1}&pageSize={2}&AgentCode={3}", searchString, page - 1, size, temp.UserName)).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_YEARLY>>().Result;
                            HttpResponseMessage response2 = client.GetAsync(string.Format("api/Statistical/CountMerchantSummaryYearlyElement_Agent?searchString={0}&AgentCode={1}", searchString, temp.UserName)).Result;
                            if (response2.IsSuccessStatusCode)
                            {
                                totalRetrival = response2.Content.ReadAsAsync<int>().Result;
                            }
                            totalPage = (int)Math.Ceiling((double)totalRetrival / size);
                            ViewBag.Total = totalRetrival;
                            ViewBag.Page = page;
                            ViewBag.TotalPage = totalPage;
                            ViewBag.MaxPage = maxPage;
                            ViewBag.First = 1;
                            ViewBag.Last = totalPage;
                        }
                        @ViewBag.searchString = searchString;
                        return View(list.ToList());
                    }
                }
                else
                {
                    //merchant
                    if (String.IsNullOrEmpty(searchString))
                    {
                        HttpResponseMessage response1 = client.GetAsync(string.Format("api/Statistical/CountMerchantSummaryYearly_Merchant?MerchantCode={0}", temp.UserName)).Result;

                        if (response1.IsSuccessStatusCode)
                        {
                            totalRetrival = response1.Content.ReadAsAsync<int>().Result;
                        }

                        HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindAllMerchantSummaryYearly_Merchant?pageIndex={0}&pageSize={1}&MerchantCode={2}", page - 1, size, temp.UserName)).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_YEARLY>>().Result;
                            totalPage = (int)Math.Ceiling((double)totalRetrival / size);
                            ViewBag.Total = totalRetrival;
                            ViewBag.Page = page;
                            ViewBag.TotalPage = totalPage;
                            ViewBag.MaxPage = maxPage;
                            ViewBag.First = 1;
                            ViewBag.Last = totalPage;
                        }
                        return View(list.ToList());
                    }
                    else
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindMerchantSummaryYearlyElement_Merchant?searchString={0}&pageIndex={1}&pageSize={2}&MerchantCode={3}", searchString, page - 1, size, temp.UserName)).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_YEARLY>>().Result;
                            HttpResponseMessage response2 = client.GetAsync(string.Format("api/Statistical/CountMerchantSummaryYearlyElement_MerchantCode?searchString={0}&MerchantCode={1}", searchString, temp.UserName)).Result;
                            if (response2.IsSuccessStatusCode)
                            {
                                totalRetrival = response2.Content.ReadAsAsync<int>().Result;
                            }
                            totalPage = (int)Math.Ceiling((double)totalRetrival / size);
                            ViewBag.Total = totalRetrival;
                            ViewBag.Page = page;
                            ViewBag.TotalPage = totalPage;
                            ViewBag.MaxPage = maxPage;
                            ViewBag.First = 1;
                            ViewBag.Last = totalPage;
                        }
                        @ViewBag.searchString = searchString;
                        return View(list.ToList());
                    }
                }
            }
        }
        public ActionResult ViewDetailYear(string ReportYear, string MerchantCode)
        {
            MERCHANT_SUMMARY_YEARLY list = new MERCHANT_SUMMARY_YEARLY();
            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindMerchantSummaryYearly?ReportYear={0}&MerchantCode={1}", ReportYear, MerchantCode)).Result;
            if (response.IsSuccessStatusCode)
            {
                list = response.Content.ReadAsAsync<MERCHANT_SUMMARY_YEARLY>().Result;
            }
            @ViewBag.ReportYear = ReportYear;
            @ViewBag.MerchantCode = MerchantCode;
            return View(list);
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

        public string CountFilterYearly(string table, string AgentCode, List<string> MerchantTypeValue, List<string> RegionTypeValue)
        {
            string query = "select count(*) from " + table + " M where ";
            string ConditionMerchant = "";
            string ConditionRegion = "";
            if (MerchantTypeValue != null)
            {
                //Lọc theo merchant type trước
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

                if (RegionTypeValue != null)
                {
                    //Nếu có thêm region thì lọc cả 2
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
                    query = query + " and " + ConditionRegion;
                }
                
            }
            else
            {
                //Lọc theo Region
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
                query = query + ConditionRegion;
                
            }

            if (!String.IsNullOrEmpty(AgentCode))
            {
                query = query + " and M.AgentCode = " + "'" + AgentCode + "'";
            }

            return query;
        }

        public string FindFilterYearly(string table, string AgentCode, List<string> MerchantTypeValue, List<string> RegionTypeValue, int page = 1, int size = 10)
        {
            string query = "select * from " + table + " M where ";
            string ConditionMerchant = "";
            string ConditionRegion = "";
            if (MerchantTypeValue != null)
            {
                //Lọc theo merchant type trước
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

                if (RegionTypeValue != null)
                {
                    //Nếu có thêm region thì lọc cả 2
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
                    query = query + " and " + ConditionRegion;
                }
            }
            else
            {
                //Lọc theo Region
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
                query = query + ConditionRegion;
            }

            if (!String.IsNullOrEmpty(AgentCode))
            {
                query = query + " and M.AgentCode = " + "'" + AgentCode + "'";
            }

            query = query + " order by ReportYear Offset " + page.ToString() + "*" + size.ToString() + " row fetch next " + size.ToString() + " row only";
            return query;
        }
    }
}