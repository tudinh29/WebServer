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
using Newtonsoft.Json.Linq;
using System.Text;

namespace WebMVC.Controllers
{
    public class ReportController : BaseController
    {
        // GET: Report
        public ActionResult Index()
        {
            string reportType = "Day";
            string reportStartDate = "20161101";// DateTime.Now.ToString("yyyyMM") + "01";
            string reportEndDate = "20161130";//DateTime.Now.ToString("yyyyMMdd");
            string reportStartMonth = String.Empty;
            string reportEndMonth = String.Empty;
            string reportStartYear = String.Empty;
            string reportEndYear = String.Empty;
            string reportStartQuarter = String.Empty;
            string reportEndQuarter = String.Empty;

            string reportDataDayFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportDataGenerality?startDate={0}&endDate={1}";
            string reportDateDayForLineFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportDateForLineChartGenerality?startDate={0}&endDate={1}";

            string reportDataMonthFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportDataMonthly?startMonth={0}&startYear={1}&endMonth={2}&endYear={3}";
            string reportDateMonthForLineFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportDateForLineChartMonthly?startMonth={0}&startYear={1}&endMonth={2}&endYear={3}";

            string reportDataQuarterFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportDataQuarterly?startQuarter={0}&startYear={1}&endQuarter={2}&endYear={3}";
            string reportDateQuarterForLineFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportDateForLineChartQuarterly?startQuarter={0}&startYear={1}&endQuarter={2}&endYear={3}";

            string reportDataYearFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportDataYearly?startYear={0}&endYear={1}";
            string reportDateYearForLineFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportDateForLineChartYearly?startYear={0}&endYear={1}";

            string reportDataAPI = String.Format(reportDataDayFormat, reportStartDate, reportEndDate);
            string reportDateForLineAPI = String.Format(reportDateDayForLineFormat, reportStartDate, reportEndDate);

            if (HttpContext.Request.HttpMethod == "POST")
            {
                reportType = Request["reportType"];
                reportStartDate = Request["reportStartDate"];
                reportEndDate = Request["reportEndDate"];
                reportStartMonth = Request["reportStartMonth"];
                reportEndMonth = Request["reportEndMonth"];
                reportStartYear = Request["reportStartYear"];
                reportEndYear = Request["reportEndYear"];
                reportStartQuarter = Request["reportStartQuarter"];
                reportEndQuarter = Request["reportEndQuarter"];

                switch (reportType.ToLower())
                {
                    case "day":
                        reportDataAPI = String.Format(reportDataDayFormat, reportStartDate, reportEndDate);
                        reportDateForLineAPI = String.Format(reportDateDayForLineFormat, reportStartDate, reportEndDate);
                        break;
                    case "month":
                        reportDataAPI = String.Format(reportDataMonthFormat, reportStartMonth, reportStartYear, reportEndMonth, reportEndYear);
                        reportDateForLineAPI = String.Format(reportDateMonthForLineFormat, reportStartMonth, reportStartYear, reportEndMonth, reportEndYear);
                        break;
                    case "quarter":
                        reportDataAPI = String.Format(reportDataQuarterFormat, reportStartQuarter, reportStartYear, reportEndQuarter, reportEndYear);
                        reportDateForLineAPI = String.Format(reportDateQuarterForLineFormat, reportStartQuarter, reportStartYear, reportEndQuarter, reportEndYear);
                        break;
                    case "year":
                        reportDataAPI = String.Format(reportDataYearFormat, reportStartYear, reportEndYear);
                        reportDateForLineAPI = String.Format(reportDateYearForLineFormat, reportStartYear, reportEndYear);
                        break;
                    default:
                        break;
                }
            }
            
            List<MERCHANT_SUMMARY> list = new List<MERCHANT_SUMMARY>();

            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(reportDataAPI).Result;

            if (response.IsSuccessStatusCode)
            {
                list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY>>().Result;
            }
            else return View("Index");

            response = client.GetAsync(reportDateForLineAPI).Result;
            List<Models.Statistic> lineChartData = new List<Models.Statistic>();
            if (response.IsSuccessStatusCode)
            {
                lineChartData = response.Content.ReadAsAsync<List<Models.Statistic>>().Result;
            }
            else return View("Index");

            HttpResponseMessage responseMerchantType = client.GetAsync(string.Format("api/Merchant_Type/SelectAllMerchantType")).Result;
            HttpResponseMessage responseCity = client.GetAsync(string.Format("api/Region/FindAllRegion")).Result;
            if (responseCity.IsSuccessStatusCode && responseMerchantType.IsSuccessStatusCode)
            {
                List<MERCHANT_TYPE> lookupType = responseMerchantType.Content.ReadAsAsync<List<MERCHANT_TYPE>>().Result;
                List<REGION> lookupRegion = responseCity.Content.ReadAsAsync<List<REGION>>().Result;

                var listMerchantType = getListMerchantType(list);
                var listRegion = getListRegion(list);
                var cardTypeReport = getCardTypeReport(list);
                foreach (var item in listMerchantType)
                {
                    item.MerchantTypeName = lookupType.FirstOrDefault(a => a.MerchantType == item.MerchantType).Description.ToString();
                }
                foreach (var item in listRegion)
                {
                    item.RegionName = lookupRegion.FirstOrDefault(a => a.RegionCode == item.RegionCode).RegionName.ToString();
                }
                ViewBag.listRegion = listRegion;
                ViewBag.listMerchantType = listMerchantType;
                ViewBag.listSummary = list;
                ViewBag.cardTypeReport = cardTypeReport;
                ViewBag.lineChartData = lineChartData;
                return View();
            }
            else return View("Index");


        }

        private List<MERCHANT_SUMMARY> getListMerchantType(List<MERCHANT_SUMMARY> list)
        {
            var res = list.GroupBy(a => a.MerchantType).Select(x => new MERCHANT_SUMMARY
            {
                MerchantType = x.Key,
                SaleAmount = x.Sum(y => y.SaleAmount),
                SaleCount = x.Sum(y => y.SaleCount),
                ReturnAmount = x.Sum(y => y.ReturnAmount),
                ReturnCount = x.Sum(y => y.ReturnCount),
                NetAmount = x.Sum(y => y.NetAmount),
                TransactionCount = x.Sum(y => y.TransactionCount),
                KeyedAmount = x.Sum(y => y.KeyedAmount),
                KeyedCount = x.Sum(y => y.KeyedCount),
                KeyedReturnAmount = x.Sum(y => y.KeyedReturnAmount),
                KeyedReturnCount = x.Sum(y => y.KeyedReturnCount),
                KeyedNetAmount = x.Sum(y => y.KeyedNetAmount),
                KeyedTransactionCount = x.Sum(y => y.KeyedTransactionCount),
                ForeignCardAmount = x.Sum(y => y.ForeignCardAmount),
                ForeignCardCount = x.Sum(y => y.ForeignCardCount),
                ForeignCardReturnAmount = x.Sum(y => y.ForeignCardReturnAmount),
                ForeignCardReturnCount = x.Sum(y => y.ForeignCardReturnCount),
                ForeignCardNetAmount = x.Sum(y => y.ForeignCardNetAmount),
                ForeignCardTransactionCount = x.Sum(y => y.ForeignCardTransactionCount),
                DebitCardAmount = x.Sum(y => y.DebitCardAmount),
                DebitCardCount = x.Sum(y => y.DebitCardCount),
                DebitCardReturnAmount = x.Sum(y => y.DebitCardReturnAmount),
                DebitCardReturnCount = x.Sum(y => y.DebitCardReturnCount),
                DebitCardNetAmount = x.Sum(y => y.DebitCardNetAmount),
                DebitCardTransactionCount = x.Sum(y => y.DebitCardTransactionCount),
                VisaCardAmount = x.Sum(y => y.VisaCardAmount),
                VisaCardCount = x.Sum(y => y.VisaCardCount),
                VisaCardReturnAmount = x.Sum(y => y.VisaCardReturnAmount),
                VisaCardReturnCount = x.Sum(y => y.VisaCardReturnCount),
                VisaCardNetAmount = x.Sum(y => y.VisaCardNetAmount),
                VisaCardTransactionCount = x.Sum(y => y.VisaCardTransactionCount),
                DiscoverCardAmount = x.Sum(y => y.DiscoverCardAmount),
                DiscoverCardCount = x.Sum(y => y.DiscoverCardCount),
                DiscoverCardReturnAmount = x.Sum(y => y.DiscoverCardReturnAmount),
                DiscoverCardReturnCount = x.Sum(y => y.DiscoverCardReturnCount),
                DiscoverCardNetAmount = x.Sum(y => y.DiscoverCardNetAmount),
                DiscoverCardTransactionCount = x.Sum(y => y.DiscoverCardTransactionCount),
                MasterCardAmount = x.Sum(y => y.MasterCardAmount),
                MasterCardCount = x.Sum(y => y.MasterCardCount),
                MasterCardReturnAmount = x.Sum(y => y.MasterCardReturnAmount),
                MasterCardReturnCount = x.Sum(y => y.MasterCardReturnCount),
                MasterCardNetAmount = x.Sum(y => y.MasterCardNetAmount),
                MasterCardTransactionCount = x.Sum(y => y.MasterCardTransactionCount),
                AmericanExpressAmount = x.Sum(y => y.AmericanExpressAmount),
                AmericanExpressCount = x.Sum(y => y.AmericanExpressCount),
                AmericanExpressReturnAmount = x.Sum(y => y.AmericanExpressReturnAmount),
                AmericanExpressReturnCount = x.Sum(y => y.AmericanExpressReturnCount),
                AmericanExpressNetAmount = x.Sum(y => y.AmericanExpressNetAmount),
                AmericanExpressTransactionCount = x.Sum(y => y.AmericanExpressTransactionCount),
                OtherCardAmount = x.Sum(y => y.OtherCardAmount),
                OtherCardCount = x.Sum(y => y.OtherCardCount),
                OtherCardReturnAmount = x.Sum(y => y.OtherCardReturnAmount),
                OtherCardReturnCount = x.Sum(y => y.OtherCardReturnCount),
                OtherCardNetAmount = x.Sum(y => y.OtherCardNetAmount),
                OtherCardTransactionCount = x.Sum(y => y.OtherCardTransactionCount),
            }).ToList();
            return res;
        }


        private List<MERCHANT_SUMMARY> getListRegion(List<MERCHANT_SUMMARY> list)
        {
            var res = list.GroupBy(a => a.RegionCode).Select(x => new MERCHANT_SUMMARY
            {
                RegionCode = x.Key,
                SaleAmount = x.Sum(y => y.SaleAmount),
                SaleCount = x.Sum(y => y.SaleCount),
                ReturnAmount = x.Sum(y => y.ReturnAmount),
                ReturnCount = x.Sum(y => y.ReturnCount),
                NetAmount = x.Sum(y => y.NetAmount),
                TransactionCount = x.Sum(y => y.TransactionCount),
                KeyedAmount = x.Sum(y => y.KeyedAmount),
                KeyedCount = x.Sum(y => y.KeyedCount),
                KeyedReturnAmount = x.Sum(y => y.KeyedReturnAmount),
                KeyedReturnCount = x.Sum(y => y.KeyedReturnCount),
                KeyedNetAmount = x.Sum(y => y.KeyedNetAmount),
                KeyedTransactionCount = x.Sum(y => y.KeyedTransactionCount),
                ForeignCardAmount = x.Sum(y => y.ForeignCardAmount),
                ForeignCardCount = x.Sum(y => y.ForeignCardCount),
                ForeignCardReturnAmount = x.Sum(y => y.ForeignCardReturnAmount),
                ForeignCardReturnCount = x.Sum(y => y.ForeignCardReturnCount),
                ForeignCardNetAmount = x.Sum(y => y.ForeignCardNetAmount),
                ForeignCardTransactionCount = x.Sum(y => y.ForeignCardTransactionCount),
                DebitCardAmount = x.Sum(y => y.DebitCardAmount),
                DebitCardCount = x.Sum(y => y.DebitCardCount),
                DebitCardReturnAmount = x.Sum(y => y.DebitCardReturnAmount),
                DebitCardReturnCount = x.Sum(y => y.DebitCardReturnCount),
                DebitCardNetAmount = x.Sum(y => y.DebitCardNetAmount),
                DebitCardTransactionCount = x.Sum(y => y.DebitCardTransactionCount),
                VisaCardAmount = x.Sum(y => y.VisaCardAmount),
                VisaCardCount = x.Sum(y => y.VisaCardCount),
                VisaCardReturnAmount = x.Sum(y => y.VisaCardReturnAmount),
                VisaCardReturnCount = x.Sum(y => y.VisaCardReturnCount),
                VisaCardNetAmount = x.Sum(y => y.VisaCardNetAmount),
                VisaCardTransactionCount = x.Sum(y => y.VisaCardTransactionCount),
                DiscoverCardAmount = x.Sum(y => y.DiscoverCardAmount),
                DiscoverCardCount = x.Sum(y => y.DiscoverCardCount),
                DiscoverCardReturnAmount = x.Sum(y => y.DiscoverCardReturnAmount),
                DiscoverCardReturnCount = x.Sum(y => y.DiscoverCardReturnCount),
                DiscoverCardNetAmount = x.Sum(y => y.DiscoverCardNetAmount),
                DiscoverCardTransactionCount = x.Sum(y => y.DiscoverCardTransactionCount),
                MasterCardAmount = x.Sum(y => y.MasterCardAmount),
                MasterCardCount = x.Sum(y => y.MasterCardCount),
                MasterCardReturnAmount = x.Sum(y => y.MasterCardReturnAmount),
                MasterCardReturnCount = x.Sum(y => y.MasterCardReturnCount),
                MasterCardNetAmount = x.Sum(y => y.MasterCardNetAmount),
                MasterCardTransactionCount = x.Sum(y => y.MasterCardTransactionCount),
                AmericanExpressAmount = x.Sum(y => y.AmericanExpressAmount),
                AmericanExpressCount = x.Sum(y => y.AmericanExpressCount),
                AmericanExpressReturnAmount = x.Sum(y => y.AmericanExpressReturnAmount),
                AmericanExpressReturnCount = x.Sum(y => y.AmericanExpressReturnCount),
                AmericanExpressNetAmount = x.Sum(y => y.AmericanExpressNetAmount),
                AmericanExpressTransactionCount = x.Sum(y => y.AmericanExpressTransactionCount),
                OtherCardAmount = x.Sum(y => y.OtherCardAmount),
                OtherCardCount = x.Sum(y => y.OtherCardCount),
                OtherCardReturnAmount = x.Sum(y => y.OtherCardReturnAmount),
                OtherCardReturnCount = x.Sum(y => y.OtherCardReturnCount),
                OtherCardNetAmount = x.Sum(y => y.OtherCardNetAmount),
                OtherCardTransactionCount = x.Sum(y => y.OtherCardTransactionCount),
            }).ToList();
            return res;
        }


        private List<MERCHANT_SUMMARY> getCardTypeReport(List<MERCHANT_SUMMARY> list)
        {
            var res = list.GroupBy(a => a.ReportDate).Select(x => new MERCHANT_SUMMARY
            {
                ReportDate = x.Key,
                SaleAmount = x.Sum(y => y.SaleAmount),
                SaleCount = x.Sum(y => y.SaleCount),
                ReturnAmount = x.Sum(y => y.ReturnAmount),
                ReturnCount = x.Sum(y => y.ReturnCount),
                NetAmount = x.Sum(y => y.NetAmount),
                TransactionCount = x.Sum(y => y.TransactionCount),
                KeyedAmount = x.Sum(y => y.KeyedAmount),
                KeyedCount = x.Sum(y => y.KeyedCount),
                KeyedReturnAmount = x.Sum(y => y.KeyedReturnAmount),
                KeyedReturnCount = x.Sum(y => y.KeyedReturnCount),
                KeyedNetAmount = x.Sum(y => y.KeyedNetAmount),
                KeyedTransactionCount = x.Sum(y => y.KeyedTransactionCount),
                ForeignCardAmount = x.Sum(y => y.ForeignCardAmount),
                ForeignCardCount = x.Sum(y => y.ForeignCardCount),
                ForeignCardReturnAmount = x.Sum(y => y.ForeignCardReturnAmount),
                ForeignCardReturnCount = x.Sum(y => y.ForeignCardReturnCount),
                ForeignCardNetAmount = x.Sum(y => y.ForeignCardNetAmount),
                ForeignCardTransactionCount = x.Sum(y => y.ForeignCardTransactionCount),
                DebitCardAmount = x.Sum(y => y.DebitCardAmount),
                DebitCardCount = x.Sum(y => y.DebitCardCount),
                DebitCardReturnAmount = x.Sum(y => y.DebitCardReturnAmount),
                DebitCardReturnCount = x.Sum(y => y.DebitCardReturnCount),
                DebitCardNetAmount = x.Sum(y => y.DebitCardNetAmount),
                DebitCardTransactionCount = x.Sum(y => y.DebitCardTransactionCount),
                VisaCardAmount = x.Sum(y => y.VisaCardAmount),
                VisaCardCount = x.Sum(y => y.VisaCardCount),
                VisaCardReturnAmount = x.Sum(y => y.VisaCardReturnAmount),
                VisaCardReturnCount = x.Sum(y => y.VisaCardReturnCount),
                VisaCardNetAmount = x.Sum(y => y.VisaCardNetAmount),
                VisaCardTransactionCount = x.Sum(y => y.VisaCardTransactionCount),
                DiscoverCardAmount = x.Sum(y => y.DiscoverCardAmount),
                DiscoverCardCount = x.Sum(y => y.DiscoverCardCount),
                DiscoverCardReturnAmount = x.Sum(y => y.DiscoverCardReturnAmount),
                DiscoverCardReturnCount = x.Sum(y => y.DiscoverCardReturnCount),
                DiscoverCardNetAmount = x.Sum(y => y.DiscoverCardNetAmount),
                DiscoverCardTransactionCount = x.Sum(y => y.DiscoverCardTransactionCount),
                MasterCardAmount = x.Sum(y => y.MasterCardAmount),
                MasterCardCount = x.Sum(y => y.MasterCardCount),
                MasterCardReturnAmount = x.Sum(y => y.MasterCardReturnAmount),
                MasterCardReturnCount = x.Sum(y => y.MasterCardReturnCount),
                MasterCardNetAmount = x.Sum(y => y.MasterCardNetAmount),
                MasterCardTransactionCount = x.Sum(y => y.MasterCardTransactionCount),
                AmericanExpressAmount = x.Sum(y => y.AmericanExpressAmount),
                AmericanExpressCount = x.Sum(y => y.AmericanExpressCount),
                AmericanExpressReturnAmount = x.Sum(y => y.AmericanExpressReturnAmount),
                AmericanExpressReturnCount = x.Sum(y => y.AmericanExpressReturnCount),
                AmericanExpressNetAmount = x.Sum(y => y.AmericanExpressNetAmount),
                AmericanExpressTransactionCount = x.Sum(y => y.AmericanExpressTransactionCount),
                OtherCardAmount = x.Sum(y => y.OtherCardAmount),
                OtherCardCount = x.Sum(y => y.OtherCardCount),
                OtherCardReturnAmount = x.Sum(y => y.OtherCardReturnAmount),
                OtherCardReturnCount = x.Sum(y => y.OtherCardReturnCount),
                OtherCardNetAmount = x.Sum(y => y.OtherCardNetAmount),
                OtherCardTransactionCount = x.Sum(y => y.OtherCardTransactionCount),
            }).ToList();
            return res;
        }
    }
}