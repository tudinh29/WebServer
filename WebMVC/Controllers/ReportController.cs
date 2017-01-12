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
        public ActionResult Index(string UserCode)
        {
            return viewReport(UserCode);
        }

        public ActionResult PrintPreview(string UserCode)
        {
            return viewReport(UserCode, "PrintPreview");
        }

        private ActionResult viewReport(string UserCode, string defaultView = "Index")
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

            string userCode = UserCode;
            ViewBag.UserCode = userCode;


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

            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View();
            if (!String.IsNullOrEmpty(userCode))
            {
                reportDataAPI += ("&code=" + userCode);
                reportDateForLineAPI += ("&code=" + userCode);
            }
            else
            {
                if (temp.UserType != "T")
                {
                    reportDataAPI += ("&code=" + temp.UserName);
                    reportDateForLineAPI += ("&code=" + temp.UserName);
                }
            }


            List<MERCHANT_SUMMARY> list = new List<MERCHANT_SUMMARY>();

            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(reportDataAPI).Result;

            if (response.IsSuccessStatusCode)
            {
                list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY>>().Result;
            }
            else return View(defaultView);

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

                foreach (var item in lookupRegion)
                {
                    var region = listRegion.Find(x => x.RegionCode == item.RegionCode);
                    if (region == null)
                    {
                        listRegion.Add(new MERCHANT_SUMMARY()
                        {
                            RegionName = item.RegionName,
                            NetAmount = 0,
                            TransactionCount = 0,
                            SaleAmount = 0,
                            SaleCount = 0,
                            ReturnCount = 0
                        });
                    }
                    else
                    {
                        region.RegionName = item.RegionName;
                    }
                }

                foreach (var item in lookupType)
                {
                    var type = listMerchantType.Find(x => x.MerchantType == item.MerchantType);
                    if (type == null)
                    {
                        listMerchantType.Add(new MERCHANT_SUMMARY()
                        {
                            MerchantTypeName = item.Description,
                            NetAmount = 0,
                            TransactionCount = 0,
                            SaleAmount = 0,
                            SaleCount = 0,
                            ReturnCount = 0
                        });
                    }
                    else
                    {
                        type.MerchantTypeName = item.Description;
                    }
                }

                ViewBag.SummaryReport = handleNullVal(getCardTypeReport(list).FirstOrDefault());
                ViewBag.Regions = listRegion;
                ViewBag.MerchantTypes = listMerchantType;

                ViewBag.listRegion = listRegion;
                ViewBag.listMerchantType = listMerchantType;
                ViewBag.listSummary = list;
                ViewBag.cardTypeReport = cardTypeReport;
                ViewBag.lineChartData = lineChartData;
                getDataForCharts();

                return View();
            }
            else return View(defaultView);
        }

        private MERCHANT_SUMMARY handleNullVal(MERCHANT_SUMMARY val)
        {
            if (val == null)
            {
                return new MERCHANT_SUMMARY()
                {
                    NetAmount = 0,
                    TransactionCount = 0,
                    SaleAmount = 0,
                    SaleCount = 0,
                    ReturnCount = 0
                };
            } else
            {
                return val;
            }
        }

        private void getDataForCharts()
        {
            var DonutData = String.Empty;
            var BarData = String.Empty;
            var LineData = String.Empty;
            var CardTypeData = String.Empty;

            foreach (MERCHANT_SUMMARY item in ViewBag.MerchantTypes)
            {
                if (item.MerchantType == null)
                {
                    item.MerchantType = "Khác";
                }
                DonutData += "{label: \"{0}\", value: {1} },".Replace("{0}", item.MerchantTypeName).Replace("{1}", item.SaleAmount.ToString());
            }
            if (DonutData.Length > 1)
            {
                DonutData = HttpUtility.HtmlDecode(DonutData.Substring(0, DonutData.Length - 1));
            }

            foreach (var item in ViewBag.lineChartData)
            {
                if (item.Name == null)
                {
                    item.Name = "Khác";
                }
                LineData += "{d: \"{0}\", sales: {1},returns:{2},count: {3} },".Replace("{0}", item.Name).Replace("{1}", item.Value.ToString()).Replace("{2}", item.ReturnAmount.ToString()).Replace("{3}", item.TransactionCount.ToString() == "" ? "0" : item.TransactionCount.ToString());
            }
            if (LineData.Length > 1)
            {
                LineData = LineData.Substring(0, LineData.Length - 1);
            }

            foreach (var item in ViewBag.Regions)
            {
                if (item.RegionCode == null)
                {
                    item.RegionCode = "Khác";
                }
                BarData += "{region: \"{0}\", sale: {1} },".Replace("{0}", item.RegionName).Replace("{1}", item.SaleAmount.ToString());
            }
            if (BarData.Length > 1)
            {
                BarData = BarData.Substring(0, BarData.Length - 1);
            }


            foreach (MERCHANT_SUMMARY item in ViewBag.cardTypeReport)
            {
                CardTypeData += "{label: \"{0}\", data: {1} },".Replace("{0}", "Foreign Card").Replace("{1}", item.ForeignCardAmount.ToString());
                CardTypeData += "{label: \"{0}\", data: {1} },".Replace("{0}", "Debit Card").Replace("{1}", item.DebitCardAmount.ToString());
                CardTypeData += "{label: \"{0}\", data: {1} },".Replace("{0}", "Visa Card").Replace("{1}", item.VisaCardAmount.ToString());
                CardTypeData += "{label: \"{0}\", data: {1} },".Replace("{0}", "Discover Card").Replace("{1}", item.DiscoverCardAmount.ToString());
                CardTypeData += "{label: \"{0}\", data: {1} },".Replace("{0}", "Master Card").Replace("{1}", item.MasterCardAmount.ToString());
                CardTypeData += "{label: \"{0}\", data: {1} },".Replace("{0}", "American Express Card").Replace("{1}", item.AmericanExpressAmount.ToString());
                CardTypeData += "{label: \"{0}\", data: {1} }".Replace("{0}", "Other Card").Replace("{1}", item.OtherCardAmount.ToString());
            }


            ViewBag.DonutData = DonutData;
            ViewBag.BarData = BarData;
            ViewBag.LineData = LineData;
            ViewBag.CardTypeData = CardTypeData;
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