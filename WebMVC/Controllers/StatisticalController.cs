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
    public class StatisticalController : BaseController
    {
        // GET: Statistical
        public ActionResult Index()
        {
            List<MERCHANT> list = new List<MERCHANT>();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View();

            ViewBag.marchantSummary = getAllSumDaily();

            if (temp.UserType != "T")   //Master
            {
                //Code here
                return View();
            }
            else   //Agent
            {
                //Code here
                return View();
            }
           
        }

        public ActionResult Detail()
        {
            getStatistic();
            // Read summaryTable
            //Test
            ViewBag.MerchantType = "Nhà hàng";
            ViewBag.Count = 5;
            return View();
        }

        private List<Models.MerchantSummaryDailyTiny> getAllSumDaily()
        {
            var marchantSummary = new List<Models.MerchantSummaryDailyTiny>();

            string domain = Request.Url.ToString().Replace(Request.Url.PathAndQuery, "");
            string url = domain + "/api/MERCHANT_SUMMARY_DAILY/GetAllStatistic";

            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                marchantSummary = response.Content.ReadAsAsync<List<Models.MerchantSummaryDailyTiny>>().Result;
            }

            return marchantSummary;
        }

        public decimal getStatisticResult(string url)
        {
            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<decimal>().Result;
            }
            else
            {
                return 0;
            }
        }

        public List<Models.Statistic> getMarchantStat(string url)
        {
            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<List<Models.Statistic>>().Result;
            }
            else
            {
                return null;
            }
        }

        public void getStatistic()
        {
            string domain = Request.Url.ToString().Replace(Request.Url.PathAndQuery, "");
            string apiUrl = domain + "/api/MERCHANT_SUMMARY_DAILY/GetTotalStatictisDaily?q={0}";
            string apiMarchantTypeStatUrl = domain + "/api/MERCHANT_SUMMARY_DAILY/GetMerchantTypeStatistic";
            string apiMarchantRegionStatUrl = domain + "/api/MERCHANT_SUMMARY_DAILY/GetMerchantRegionStatistic";
            string apiMarchantDailyRevenueStatUrl = domain + "/api/MERCHANT_SUMMARY_DAILY/GetMerchantDailyRevenueStatistic";
            string apiCardTypeStatUrl = domain + "/api/MERCHANT_SUMMARY_DAILY/GetCardTypeStatistic";

            ViewBag.Revenue = getStatisticResult(String.Format(apiUrl, "revenue"));
            ViewBag.Sale = getStatisticResult(String.Format(apiUrl, "sale"));
            ViewBag.Return = getStatisticResult(String.Format(apiUrl, "return"));
            ViewBag.Transaction = getStatisticResult(String.Format(apiUrl, "transaction"));

            ViewBag.MerchantTypeStatistic = getMarchantStat(apiMarchantTypeStatUrl);
            ViewBag.MerchantRegionStatistic = getMarchantStat(apiMarchantRegionStatUrl);
            ViewBag.MerchantDailyRevenueStatistic = getMarchantStat(apiMarchantDailyRevenueStatUrl);
            ViewBag.CardTypeStatistic = getMarchantStat(apiCardTypeStatUrl);
        }
    }
}