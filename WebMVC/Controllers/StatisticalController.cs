﻿using System;
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

            int option = 0;
            string queryFind = "";
            string queryCount = "";

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
                        queryFind = queryFilterStatistical(MerchantTypeValue, RegionTypeValue, "*");
                        queryFind = queryFind + " order by M.ReportDate Offset " + (page - 1) * size + " row fetch next " + size + " row only";
                        queryCount = queryFilterStatistical(MerchantTypeValue, RegionTypeValue, "Count(*)");
                        option = 1;
                    }
                }
                else
                {
                    if (MerchantTypeValue != null || RegionTypeValue != null)
                    {
                        string conditionSearch = ConditionSearch(searchString);
                        queryFind = queryFilterStatistical(MerchantTypeValue, RegionTypeValue, "*");
                        queryFind = queryFind + conditionSearch + " order by M.ReportDate Offset " + (page - 1) * size + " row fetch next " + size + " row only";
                        queryCount = queryFilterStatistical(MerchantTypeValue, RegionTypeValue, "Count(*)") + conditionSearch;
                        option = 1;
                        @ViewBag.searchString = searchString;
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
            }
            else if (temp.UserType == "A")
            {
                if (String.IsNullOrEmpty(searchString))
                {
                    if (MerchantTypeValue == null && RegionTypeValue == null)
                    {
                        HttpResponseMessage responseCount = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/GetCountMerchantSummaryForAgentDefault_ForQuery?AgentCode={0}", temp.UserName)).Result;
                        HttpResponseMessage response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/GetMerchantSummaryForAgentDefault_ForQuery?AgentCode={0}&pageIndex={1}&pageSize={2}", temp.UserName, page, size)).Result;
                        if (response.IsSuccessStatusCode && responseCount.IsSuccessStatusCode)
                        {
                            totalRetrival = responseCount.Content.ReadAsAsync<int>().Result;
                            ListSummary = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_DAILY>>().Result;
                        }
                    }
                    else
                    {
                        queryFind = queryFilterStatistical(MerchantTypeValue, RegionTypeValue, "*");
                        queryFind = queryFind + "and M.AgentCode = '" + temp.UserName + "' order by M.ReportDate Offset " + (page - 1) * size + " row fetch next " + size + " row only";
                        queryCount = queryFilterStatistical(MerchantTypeValue, RegionTypeValue, "Count(*)");
                        queryCount = queryCount + "and M.AgentCode = '" + temp.UserName + "'";
                        option = 1;
                    }
                }
                else
                {
                    if (MerchantTypeValue != null || RegionTypeValue != null)
                    {
                        string conditionSearch = ConditionSearch(searchString);
                        queryFind = queryFilterStatistical(MerchantTypeValue, RegionTypeValue, "*");
                        queryFind = queryFind + "and M.AgentCode = '" + temp.UserName + "' " + conditionSearch + " order by M.ReportDate Offset " + (page - 1) * size + " row fetch next " + size + " row only";
                        queryCount = queryFilterStatistical(MerchantTypeValue, RegionTypeValue, "Count(*)");
                        queryCount = queryCount + "and M.AgentCode = '" + temp.UserName + "'" + conditionSearch;
                        option = 1;
                        @ViewBag.searchString = searchString;
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
            }
            else if (temp.UserType == "M")
            {
                if (String.IsNullOrEmpty(searchString))
                {
                    if (MerchantTypeValue == null && RegionTypeValue == null)
                    {
                        HttpResponseMessage responseCount = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/GetCountMerchantSummaryForMerchantDefault_ForQuery?MerchantCode={0}", temp.UserName)).Result;
                        HttpResponseMessage response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/GetMerchantSummaryForMerchantDefault_ForQuery?MerchantCode={0}&pageIndex={1}&pageSize={2}", temp.UserName, page, size)).Result;
                        if (response.IsSuccessStatusCode && responseCount.IsSuccessStatusCode)
                        {
                            totalRetrival = responseCount.Content.ReadAsAsync<int>().Result;
                            ListSummary = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_DAILY>>().Result;
                        }
                    }
                    else
                    {
                        queryFind = queryFilterStatistical(MerchantTypeValue, RegionTypeValue, "*");
                        queryFind = queryFind + "and M.MerchantCode = '" + temp.UserName + "' order by M.ReportDate Offset " + (page - 1) * size + " row fetch next " + size + " row only";
                        queryCount = queryFilterStatistical(MerchantTypeValue, RegionTypeValue, "Count(*)");
                        queryCount = queryCount + "and M.MerchantCode = '" + temp.UserName + "'";
                        option = 1;
                    }
                }
                else
                {
                    if (MerchantTypeValue != null || RegionTypeValue != null)
                    {
                        string conditionSearch = ConditionSearch(searchString);
                        queryFind = queryFilterStatistical(MerchantTypeValue, RegionTypeValue, "*");
                        queryFind = queryFind + "and M.MerchantCode = '" + temp.UserName + "' " + conditionSearch + " order by M.ReportDate Offset " + (page - 1) * size + " row fetch next " + size + " row only";
                        queryCount = queryFilterStatistical(MerchantTypeValue, RegionTypeValue, "Count(*)");
                        queryCount = queryCount + "and M.MerchantCode = '" + temp.UserName + "'" + conditionSearch;
                        option = 1;
                        @ViewBag.searchString = searchString;
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
            }
            else return View();

            if (option != 0)
            {
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

            totalPage = (int)Math.Ceiling((double)totalRetrival / size);
            ViewBag.Total = totalRetrival;
            ViewBag.Page = page;
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            return View(ListSummary);

        }
        public ActionResult ViewDetailDay(string MerchantCode, string ReportDate)
        {
            //Tâm code
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View();
            MERCHANT_SUMMARY_DAILY list = new MERCHANT_SUMMARY_DAILY();
            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response;
            if (temp.UserType == "T")
            {
                response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/GetMerchantSummaryForMaster?MerchantCode={0}&ReportDate={1}", MerchantCode, ReportDate)).Result;

                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<MERCHANT_SUMMARY_DAILY>().Result;
                    HttpResponseMessage responseMerchantType = client.GetAsync(string.Format("api/MERCHANT_TYPE/GetMerchantTypeName?MerchantType={0}", list.MerchantType)).Result;
                    HttpResponseMessage responseRegion = client.GetAsync(string.Format("api/REGION/GetRegionName?RegionCode={0}", list.RegionCode)).Result;
                    if (responseMerchantType.IsSuccessStatusCode && responseRegion.IsSuccessStatusCode)
                    {
                        MERCHANT_TYPE MerchantType = responseMerchantType.Content.ReadAsAsync<MERCHANT_TYPE>().Result;
                        REGION Region = responseRegion.Content.ReadAsAsync<REGION>().Result;

                        ViewBag.MerchantTypeName = MerchantType.Description;
                        ViewBag.RegionName = Region.RegionName;
                    }

                }
            }
            if (temp.UserType == "A")
            {

                response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/GetMerchantSummaryForAgentDefaultMerchantCode?AgentCode={0}&&MerchantCode={1}&&ReportDate={2}", temp.UserName, MerchantCode, ReportDate)).Result;
                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<MERCHANT_SUMMARY_DAILY>().Result;
                    HttpResponseMessage responseMerchantType = client.GetAsync(string.Format("api/MERCHANT_TYPE/GetMerchantTypeName?MerchantType={0}", list.MerchantType)).Result;
                    HttpResponseMessage responseRegion = client.GetAsync(string.Format("api/REGION/GetRegionName?RegionCode={0}", list.RegionCode)).Result;
                    if (responseMerchantType.IsSuccessStatusCode && responseRegion.IsSuccessStatusCode)
                    {
                        MERCHANT_TYPE MerchantType = responseMerchantType.Content.ReadAsAsync<MERCHANT_TYPE>().Result;
                        REGION Region = responseRegion.Content.ReadAsAsync<REGION>().Result;

                        ViewBag.MerchantTypeName = MerchantType.Description;
                        ViewBag.RegionName = Region.RegionName;
                    }
                }
            }
            if (temp.UserType == "M")
            {
                response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/GetMerchantSummaryForMaster?MerchantCode={0}&ReportDate={1}", MerchantCode, ReportDate)).Result;
                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<MERCHANT_SUMMARY_DAILY>().Result;
                    HttpResponseMessage responseMerchantType = client.GetAsync(string.Format("api/MERCHANT_TYPE/GetMerchantTypeName?MerchantType={0}", list.MerchantType)).Result;
                    HttpResponseMessage responseRegion = client.GetAsync(string.Format("api/REGION/GetRegionName?RegionCode={0}", list.RegionCode)).Result;
                    if (responseMerchantType.IsSuccessStatusCode && responseRegion.IsSuccessStatusCode)
                    {
                        MERCHANT_TYPE MerchantType = responseMerchantType.Content.ReadAsAsync<MERCHANT_TYPE>().Result;
                        REGION Region = responseRegion.Content.ReadAsAsync<REGION>().Result;

                        ViewBag.MerchantTypeName = MerchantType.Description;
                        ViewBag.RegionName = Region.RegionName;
                    }
                }
            }

            return View(list);
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
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (temp.UserType == "T")
            {
                if (MerchantTypeValue != null || RegionTypeValue != null)
                {
                    string name = "";
                    string table = "MERCHANT_SUMMARY_MONTHLY";

                    string findQuery = FindFilterQuery(table, name, MerchantTypeValue, RegionTypeValue, searchString, page - 1, size);
                    string countQuery = CountFilterQuery(table, name, MerchantTypeValue, RegionTypeValue, searchString);
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
                    }
                    ViewBag.MerchantTypeValue = MerchantTypeValue;
                    ViewBag.RegionTypeValue = RegionTypeValue;
                    ViewBag.searchString = searchString;
                }
                else
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
                        }
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
                        }
                        ViewBag.searchString = searchString;
                    }
                }
            }
            else
            {
                if (temp.UserType == "A")
                {
                    //agent
                    if (MerchantTypeValue != null || RegionTypeValue != null)
                    {
                        string name = temp.UserName;
                        string table = "MERCHANT_SUMMARY_MONTHLY";

                        string findQuery = FindFilterQuery(table, name, MerchantTypeValue, RegionTypeValue, searchString, page - 1, size);
                        string countQuery = CountFilterQuery(table, name, MerchantTypeValue, RegionTypeValue, searchString);
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
                        }
                        ViewBag.MerchantTypeValue = MerchantTypeValue;
                        ViewBag.RegionTypeValue = RegionTypeValue;
                        ViewBag.searchString = searchString;
                    }
                    else
                    {
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
                            }
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
                            }
                            ViewBag.searchString = searchString;
                        }
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
                        }
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

                        }
                        @ViewBag.searchString = searchString;
                    }
                }
            }
            totalPage = (int)Math.Ceiling((double)totalRetrival / size);
            ViewBag.Total = totalRetrival;
            ViewBag.Page = page;
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            return View(list.ToList());
        }
        public ActionResult ViewDetailMonth(string ReportMonth, string ReportYear, string MerchantCode)
        {
            HttpClient client = new AccessAPI().Access();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View("Error");

            if (temp.UserType == "M")
            {
                if (MerchantCode != temp.UserName)
                    return View("Error");
            }

            if (temp.UserType == "A")
            {
                string query = "select count(1) from MERCHANT_SUMMARY_MONTHLY M where M.MerchantCode = '" + MerchantCode + "' and M.AgentCode = '" + temp.UserName + "'";
                HttpResponseMessage response2 = client.GetAsync(string.Format("api/Statistical/CountFilter?query={0}", query)).Result;
                if (response2.IsSuccessStatusCode)
                {
                    if (response2.Content.ReadAsAsync<int>().Result == 0)
                        return View("Error");
                }
            }

            MERCHANT_SUMMARY_MONTHLY list = new MERCHANT_SUMMARY_MONTHLY();
            HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindMerchantSummaryMonthly?ReportMonth={0}&ReportYear={1}&MerchantCode={2}", ReportMonth, ReportYear, MerchantCode)).Result;
            if (response.IsSuccessStatusCode)
            {
                list = response.Content.ReadAsAsync<MERCHANT_SUMMARY_MONTHLY>().Result;
                HttpResponseMessage responseMerchantType = client.GetAsync(string.Format("api/MERCHANT_TYPE/GetMerchantTypeName?MerchantType={0}", list.MerchantType)).Result;
                HttpResponseMessage responseRegion = client.GetAsync(string.Format("api/REGION/GetRegionName?RegionCode={0}", list.RegionCode)).Result;
                if (responseMerchantType.IsSuccessStatusCode && responseRegion.IsSuccessStatusCode)
                {
                    MERCHANT_TYPE MerchantType = responseMerchantType.Content.ReadAsAsync<MERCHANT_TYPE>().Result;
                    REGION Region = responseRegion.Content.ReadAsAsync<REGION>().Result;

                    ViewBag.MerchantTypeName = MerchantType.Description;
                    ViewBag.RegionName = Region.RegionName;
                }
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

            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (temp.UserType == "T")
            {
                if (MerchantTypeValue != null || RegionTypeValue != null)
                {
                    string name = "";
                    string table = "MERCHANT_SUMMARY_QUARTERLY";

                    string findQuery = FindFilterQuery(table, name, MerchantTypeValue, RegionTypeValue, searchString, page - 1, size);
                    string countQuery = CountFilterQuery(table, name, MerchantTypeValue, RegionTypeValue, searchString);
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
                    }
                    ViewBag.MerchantTypeValue = MerchantTypeValue;
                    ViewBag.RegionTypeValue = RegionTypeValue;
                    ViewBag.searchString = searchString;
                }
                else
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
                        }
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
                        }
                        ViewBag.searchString = searchString;
                    }
                }
            }
            else
            {
                if (temp.UserType == "A")
                {
                    //agent
                    if (MerchantTypeValue != null || RegionTypeValue != null)
                    {
                        string name = temp.UserName;
                        string table = "MERCHANT_SUMMARY_QUARTERLY";

                        string findQuery = FindFilterQuery(table, name, MerchantTypeValue, RegionTypeValue, searchString, page - 1, size);
                        string countQuery = CountFilterQuery(table, name, MerchantTypeValue, RegionTypeValue, searchString);
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
                        }
                        ViewBag.MerchantTypeValue = MerchantTypeValue;
                        ViewBag.RegionTypeValue = RegionTypeValue;
                        ViewBag.searchString = searchString;
                    }
                    else
                    {
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
                            }
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
                            }
                            ViewBag.searchString = searchString;
                        }
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
                        }
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
                        }
                        @ViewBag.searchString = searchString;
                    }
                }
            }
            totalPage = (int)Math.Ceiling((double)totalRetrival / size);
            ViewBag.Total = totalRetrival;
            ViewBag.Page = page;
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            return View(list.ToList());
        }
        public ActionResult ViewDetailQuarter(string ReportQuarter, string ReportYear, string MerchantCode)
        {
            HttpClient client = new AccessAPI().Access();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View("Error");

            if (temp.UserType == "M")
            {
                if (MerchantCode != temp.UserName)
                    return View("Error");
            }

            if (temp.UserType == "A")
            {
                string query = "select count(1) from MERCHANT_SUMMARY_QUARTERLY M where M.MerchantCode = '" + MerchantCode + "' and M.AgentCode = '" + temp.UserName + "'";
                HttpResponseMessage response2 = client.GetAsync(string.Format("api/Statistical/CountFilter?query={0}", query)).Result;
                if (response2.IsSuccessStatusCode)
                {
                    if (response2.Content.ReadAsAsync<int>().Result == 0)
                        return View("Error");
                }
            }

            MERCHANT_SUMMARY_QUARTERLY list = new MERCHANT_SUMMARY_QUARTERLY();
            HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindMerchantSummaryQuarterly?ReportQuarter={0}&ReportYear={1}&MerchantCode={2}", ReportQuarter, ReportYear, MerchantCode)).Result;
            if (response.IsSuccessStatusCode)
            {
                list = response.Content.ReadAsAsync<MERCHANT_SUMMARY_QUARTERLY>().Result;
                HttpResponseMessage responseMerchantType = client.GetAsync(string.Format("api/MERCHANT_TYPE/GetMerchantTypeName?MerchantType={0}", list.MerchantType)).Result;
                HttpResponseMessage responseRegion = client.GetAsync(string.Format("api/REGION/GetRegionName?RegionCode={0}", list.RegionCode)).Result;
                if (responseMerchantType.IsSuccessStatusCode && responseRegion.IsSuccessStatusCode)
                {
                    MERCHANT_TYPE MerchantType = responseMerchantType.Content.ReadAsAsync<MERCHANT_TYPE>().Result;
                    REGION Region = responseRegion.Content.ReadAsAsync<REGION>().Result;

                    ViewBag.MerchantTypeName = MerchantType.Description;
                    ViewBag.RegionName = Region.RegionName;
                }
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

            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (temp.UserType == "T")
            {
                if (MerchantTypeValue != null || RegionTypeValue != null)
                {
                    string name = "";
                    string table = "MERCHANT_SUMMARY_YEARLY";

                    string findQuery = FindFilterQuery(table, name, MerchantTypeValue, RegionTypeValue, searchString, page - 1, size);
                    string countQuery = CountFilterQuery(table, name, MerchantTypeValue, RegionTypeValue, searchString);
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
                    }
                    ViewBag.MerchantTypeValue = MerchantTypeValue;
                    ViewBag.RegionTypeValue = RegionTypeValue;
                    ViewBag.searchString = searchString;
                }
                else
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
                        }
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
                        }
                        ViewBag.searchString = searchString;
                    }
                }
            }
            else
            {
                if (temp.UserType == "A")
                {
                    //agent
                    if (MerchantTypeValue != null || RegionTypeValue != null)
                    {
                        string name = temp.UserName;
                        string table = "MERCHANT_SUMMARY_YEARLY";

                        string findQuery = FindFilterQuery(table, name, MerchantTypeValue, RegionTypeValue, searchString, page - 1, size);
                        string countQuery = CountFilterQuery(table, name, MerchantTypeValue, RegionTypeValue, searchString);
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
                        }
                        ViewBag.MerchantTypeValue = MerchantTypeValue;
                        ViewBag.RegionTypeValue = RegionTypeValue;
                        ViewBag.searchString = searchString;
                    }
                    else
                    {
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
                            }
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
                            }
                            ViewBag.searchString = searchString;
                        }
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
                        }
                        @ViewBag.searchString = searchString;
                    }
                }
            }
            totalPage = (int)Math.Ceiling((double)totalRetrival / size);
            ViewBag.Total = totalRetrival;
            ViewBag.Page = page;
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            return View(list.ToList());
        }
        public ActionResult ViewDetailYear(string ReportYear, string MerchantCode)
        {
            HttpClient client = new AccessAPI().Access();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View("Error");

            if (temp.UserType == "M")
            {
                if (MerchantCode != temp.UserName)
                    return View("Error");
            }

            if (temp.UserType == "A")
            {
                string query = "select count(1) from MERCHANT_SUMMARY_YEARLY M where M.MerchantCode = '" + MerchantCode + "' and M.AgentCode = '" + temp.UserName + "'";
                HttpResponseMessage response2 = client.GetAsync(string.Format("api/Statistical/CountFilter?query={0}", query)).Result;
                if (response2.IsSuccessStatusCode)
                {
                    if (response2.Content.ReadAsAsync<int>().Result == 0)
                        return View("Error");
                }
            }

            MERCHANT_SUMMARY_YEARLY list = new MERCHANT_SUMMARY_YEARLY();
            HttpResponseMessage response = client.GetAsync(string.Format("api/Statistical/FindMerchantSummaryYearly?ReportYear={0}&MerchantCode={1}", ReportYear, MerchantCode)).Result;
            if (response.IsSuccessStatusCode)
            {
                list = response.Content.ReadAsAsync<MERCHANT_SUMMARY_YEARLY>().Result;
                HttpResponseMessage responseMerchantType = client.GetAsync(string.Format("api/MERCHANT_TYPE/GetMerchantTypeName?MerchantType={0}", list.MerchantType)).Result;
                HttpResponseMessage responseRegion = client.GetAsync(string.Format("api/REGION/GetRegionName?RegionCode={0}", list.RegionCode)).Result;
                if (responseMerchantType.IsSuccessStatusCode && responseRegion.IsSuccessStatusCode)
                {
                    MERCHANT_TYPE MerchantType = responseMerchantType.Content.ReadAsAsync<MERCHANT_TYPE>().Result;
                    REGION Region = responseRegion.Content.ReadAsAsync<REGION>().Result;

                    ViewBag.MerchantTypeName = MerchantType.Description;
                    ViewBag.RegionName = Region.RegionName;
                }
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

        private string ConditionSearch(string searchString)
        {
            string conditionSearch = " and(M.MerchantCode like '%" + searchString + "%' OR M.ReportDate like '%" + searchString + "%' OR M.SaleAmount like '%" + searchString
                + "%' OR M.SaleCount like '%" + searchString + "%' OR M.ReturnAmount like '%" + searchString + "%' OR M.ReturnCount like '%" + searchString
                + "%' OR M.NetAmount like '%" + searchString + "%' OR M.TransactionCount like '%" + searchString + "%' OR M.KeyedAmount like '%" + searchString + "%' )";
            return conditionSearch;
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

        public string CountFilterQuery(string table, string AgentCode, List<string> MerchantTypeValue, List<string> RegionTypeValue, string searchString)
        {
            bool flat = true;
            string query = "select count(*) from " + table + " M where ";
                // Điều kiện merchant type
            if (MerchantTypeValue != null)
            {
<<<<<<< HEAD
                query = query + MerchantTypeCondition(MerchantTypeValue);
                flat = false;
=======
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

>>>>>>> origin/KetHopSearchFilter_Merchant_StatistialDay
            }
                // Điều kiện region
            if (RegionTypeValue != null)
            {
<<<<<<< HEAD
                if (flat == false)
                    query = query + " AND ";
                else
                    flat = false;
                query = query + RegionCondition(RegionTypeValue);
=======
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

>>>>>>> origin/KetHopSearchFilter_Merchant_StatistialDay
            }
                // Điều kiện search
            if (!String.IsNullOrEmpty(searchString))
            {
                if (flat == false)
                    query = query + " AND ";
                query = query + SearchCondition(table, searchString);
            }
            
                // dùng cho agent
            if (!String.IsNullOrEmpty(AgentCode))
                query = query + " AND AgentCode = '" + AgentCode + "'";
            
            return query;
        }

        public string FindFilterQuery(string table, string AgentCode, List<string> MerchantTypeValue, List<string> RegionTypeValue, string searchString, int page = 1, int size = 10)
        {
            bool flat = true;
            string query = "select * from " + table + " M where ";
            // Điều kiện merchant type
            if (MerchantTypeValue != null)
            {
                query = query + MerchantTypeCondition(MerchantTypeValue);
                flat = false;
            }
            // Điều kiện region
            if (RegionTypeValue != null)
            {
                if (flat == false)
                    query = query + " AND ";
                else
                    flat = false;
                query = query + RegionCondition(RegionTypeValue);
            }
            // Điều kiện search
            if (!String.IsNullOrEmpty(searchString))
            {
                if (flat == false)
                    query = query + " AND ";
                query = query + SearchCondition(table, searchString);
            }

            // dùng cho Agent
            if (!String.IsNullOrEmpty(AgentCode))
                query = query + " AND AgentCode = '" + AgentCode + "'";

            query = query + " order by ReportYear Offset " + page.ToString() + "*" + size.ToString() + " row fetch next " + size.ToString() + " row only";
            return query;
        }

        public string RegionCondition(List<string> RegionTypeValue)
        {
            string ConditionRegion = "(";
            for (int i = 0; i < RegionTypeValue.Count; i++)
            {
                ConditionRegion = ConditionRegion + "M.RegionCode = " + "'" + RegionTypeValue[i] + "'";
                if (i < RegionTypeValue.Count - 1)
                {
                    ConditionRegion = ConditionRegion + " or ";
                }
            }
            ConditionRegion = ConditionRegion + ")";
            return ConditionRegion;
        }

        public string MerchantTypeCondition(List<string> MerchantTypeValue)
        {
            string ConditionMerchant = "(";
            for (int i = 0; i < MerchantTypeValue.Count; i++)
            {
                ConditionMerchant = ConditionMerchant + "M.MerchantType = " + "'" + MerchantTypeValue[i] + "'";
                if (i < MerchantTypeValue.Count - 1)
                {
                    ConditionMerchant = ConditionMerchant + " or ";
                }
            }
            ConditionMerchant = ConditionMerchant + ")";
            return ConditionMerchant;
        }

        public string SearchCondition(string table, string searchString)
        {
            string SearchCondition = "(";
            double num;
            bool isNum = Double.TryParse(searchString, out num);
            if (isNum)
            {
                double maxmoney = num + 100;
                double minmoney = num - 100;
                double maxcount = num + 100;
                double mincount = num - 100;
                SearchCondition = SearchCondition + "M.SaleAmount between " + minmoney.ToString() + " and " + maxmoney.ToString()
                    + " OR M.NetAmount between " + minmoney.ToString() + " and " + maxmoney.ToString()
                    + " OR M.ReturnAmount between " + minmoney.ToString() + " and " + maxmoney.ToString()
                    + " OR M.SaleCount between " + mincount.ToString() + " and " + maxcount.ToString()
                    + " OR M.ReturnCount between " + mincount.ToString() + " and " + maxcount.ToString()
                    + " OR M.TransactionCount between " + mincount.ToString() + " and " + maxcount.ToString()
                    + " OR M.ReportYear = " + num.ToString();
                if (table == "MERCHANT_SUMMARY_MONTHLY")
                {
                    SearchCondition = SearchCondition + " OR M.ReportMonth = " + num.ToString();
                }
                else if (table == "MERCHANT_SUMMARY_QUARTERLY")
                {
                    SearchCondition = SearchCondition + " OR M.ReportQuarter = " + num.ToString();
                }
                SearchCondition = SearchCondition + ")";
            }
            else
            {
                SearchCondition = SearchCondition + "M.MerchantCode = '" + searchString + "'"
                    + " OR M.MerchantType like '%" + searchString + "%'"
                    + " OR M.RegionCode like '%" + searchString + "%'"
                    + " OR M.AgentCode like '%" + searchString + "%')";
            }

            return SearchCondition;
        }
    }
}