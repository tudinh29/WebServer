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
    public class CompareController : BaseController
    {
        //
        // GET: /Chart_Compare/
        public ActionResult Index()
        {
            string reportType = "month";
           
            string reportStartMonth = "11";
            string reportEndMonth = "12";
            string reportStartYear = "2016";
            string reportEndYear = "2016";
            string reportStartQuarter = String.Empty;
            string reportEndQuarter = String.Empty;

            string reportDataMonthFormat = String.Empty;
            string reportDateMonthForLineFormat = String.Empty;
            string reportDataQuarterFormat = String.Empty;
            string reportDateQuarterForLineFormat = String.Empty;
            string reportDataYearFormat = String.Empty;
            string reportDateYearForLineFormat = String.Empty;

            string reportDataAPI = String.Empty;
            string reportMonthForLineAPI = String.Empty;

            //string reportDataMonthFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportDataMonthly_Compare?startMonth={0}&startYear={1}";
            //string reportDateMonthForLineFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportData_Generality_Compare?startMonth={0}&startYear={1}&endMonth={2}&endYear={3}";


            //string reportDataQuarterFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportData_Generality_Quaterly_Compare?startQuarter={0}&startYear={1}&endQuarter={2}&endYear={3}";
            //string reportDateQuarterForLineFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportData_Generality_Quaterly_Compare?startQuarter={0}&startYear={1}&endQuarter={2}&endYear={3}";

            //string reportDataYearFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportData_Generality_Yearly_Compare?startYear={0}&endYear={1}";
            //string reportDateYearForLineFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportData_Generality_Yearly_Compare?startYear={0}&endYear={1}";



            //string reportDataAPI = String.Format(reportDataMonthFormat, reportStartMonth, reportStartYear);
            //string reportMonthForLineAPI = String.Format(reportDateMonthForLineFormat, reportStartMonth, reportStartYear, reportEndMonth, reportEndYear);

            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            HttpClient client = new AccessAPI().Access();
            if (temp.UserType == "T")
            {
                reportDataMonthFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportDataMonthly_Compare?startMonth={0}&startYear={1}";
                reportDateMonthForLineFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportData_Generality_Compare?startMonth={0}&startYear={1}&endMonth={2}&endYear={3}";

                reportDataQuarterFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportData_Generality_Quaterly_Compare?startQuarter={0}&startYear={1}&endQuarter={2}&endYear={3}";
                reportDateQuarterForLineFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportData_Generality_Quaterly_Compare?startQuarter={0}&startYear={1}&endQuarter={2}&endYear={3}";

                reportDataYearFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportData_Generality_Yearly_Compare?startYear={0}&endYear={1}";
                reportDateYearForLineFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportData_Generality_Yearly_Compare?startYear={0}&endYear={1}";

                reportDataAPI = String.Format(reportDataMonthFormat, reportStartMonth, reportStartYear);
                reportMonthForLineAPI = String.Format(reportDateMonthForLineFormat, reportStartMonth, reportStartYear, reportEndMonth, reportEndYear);
            }
            else
            {
                if (temp.UserType == "A")
                {
                    reportDataMonthFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportDataMonthly_Compare?startMonth={0}&startYear={1}";
                    reportDateMonthForLineFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportData_Generality_Compare_Agent?startMonth={0}&startYear={1}&endMonth={2}&endYear={3}&agentCode={4}";

                    reportDataQuarterFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportData_Generality_Quaterly_Compare_Agent?startQuarter={0}&startYear={1}&endQuarter={2}&endYear={3}&agentCode={4}";
                    reportDateQuarterForLineFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportData_Generality_Quaterly_Compare_Agent?startQuarter={0}&startYear={1}&endQuarter={2}&endYear={3}&agentCode={4}";

                    reportDataYearFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportData_Generality_Yearly_Compare_Agent?startYear={0}&endYear={1}&agentCode={2}";
                    reportDateYearForLineFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportData_Generality_Yearly_Compare_Agent?startYear={0}&endYear={1}&agentCode={2}";


                    reportDataAPI = String.Format(reportDataMonthFormat, reportStartMonth, reportStartYear);
                    reportMonthForLineAPI = String.Format(reportDateMonthForLineFormat, reportStartMonth, reportStartYear, reportEndMonth, reportEndYear, temp.UserName);
                }

            }
            




            if (HttpContext.Request.HttpMethod == "POST")
            {
                reportType = Request["reportType"];
                reportStartMonth = Request["reportStartMonth"];
                reportEndMonth = Request["reportEndMonth"];
                reportStartYear = Request["reportStartYear"];
                reportEndYear = Request["reportEndYear"];
                reportStartQuarter = Request["reportStartQuarter"];
                reportEndQuarter = Request["reportEndQuarter"];

                if (temp.UserType == "T")
                {
                    switch (reportType.ToLower())
                    {
                        //case "day":
                        //reportDataAPI = String.Format(reportDataDayFormat, reportStartDate, reportEndDate);
                        //reportDateForLineAPI = String.Format(reportDateDayForLineFormat, reportStartDate, reportEndDate);
                        //break;


                        case "month":
                            reportDataAPI = String.Format(reportDataMonthFormat, reportStartMonth, reportStartYear);
                            reportMonthForLineAPI = String.Format(reportDateMonthForLineFormat, reportStartMonth, reportStartYear, reportEndMonth, reportEndYear);
                            //reportMonthForLineAPI1 = String.Format(reportDateMonthForLineFormat, reportEndMonth, reportEndYear);
                            break;
                        case "quarter":
                            reportDataAPI = String.Format(reportDataQuarterFormat, reportStartQuarter, reportStartYear, reportEndQuarter, reportEndYear);
                            reportMonthForLineAPI = String.Format(reportDateQuarterForLineFormat, reportStartQuarter, reportStartYear, reportEndQuarter, reportEndYear);

                            break;
                        case "year":
                            reportDataAPI = String.Format(reportDataYearFormat, reportStartYear, reportEndYear);
                            reportMonthForLineAPI = String.Format(reportDateYearForLineFormat, reportStartYear, reportEndYear);

                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    
                        switch (reportType.ToLower())
                        {
                            //case "day":
                            //reportDataAPI = String.Format(reportDataDayFormat, reportStartDate, reportEndDate);
                            //reportDateForLineAPI = String.Format(reportDateDayForLineFormat, reportStartDate, reportEndDate);
                            //break;


                            case "month":
                                reportDataAPI = String.Format(reportDataMonthFormat, reportStartMonth, reportStartYear);
                                reportMonthForLineAPI = String.Format(reportDateMonthForLineFormat, reportStartMonth, reportStartYear, reportEndMonth, reportEndYear, temp.UserName);
                                //reportMonthForLineAPI1 = String.Format(reportDateMonthForLineFormat, reportEndMonth, reportEndYear);
                                break;
                            case "quarter":
                                reportDataAPI = String.Format(reportDataQuarterFormat, reportStartQuarter, reportStartYear, reportEndQuarter, reportEndYear, temp.UserName);
                                reportMonthForLineAPI = String.Format(reportDateQuarterForLineFormat, reportStartQuarter, reportStartYear, reportEndQuarter, reportEndYear, temp.UserName);

                                break;
                            case "year":
                                reportDataAPI = String.Format(reportDataYearFormat, reportStartYear, reportEndYear, temp.UserName);
                                reportMonthForLineAPI = String.Format(reportDateYearForLineFormat, reportStartYear, reportEndYear, temp.UserName);

                                break;
                            default:
                                break;
                        
                    }
                }
                
            }
            //HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(reportMonthForLineAPI).Result;
            //HttpResponseMessage response1 = client.GetAsync(reportMonthForLineAPI1).Result;
            List<Models.Statistic> lineChartData = new List<Models.Statistic>();
            //List<Models.Statistic> lineChartData1 = new List<Models.Statistic>();
            if (response.IsSuccessStatusCode)
            {
                lineChartData = response.Content.ReadAsAsync<List<Models.Statistic>>().Result;
                //lineChartData1 = response1.Content.ReadAsAsync<List<Models.Statistic>>().Result;
                
                ViewBag.lineChartData = lineChartData;
                @ViewBag.Month1 = reportStartMonth ;
                @ViewBag.Month2 = reportEndMonth;
                @ViewBag.Year1 = reportStartYear;
                @ViewBag.Year2 = reportEndYear;
                @ViewBag.Quarter1 = reportStartQuarter;
                @ViewBag.Quarter2 = reportEndQuarter;
                @ViewBag.startMonth = reportStartMonth + "/" + reportStartYear;
                @ViewBag.endMonth = reportEndMonth + "/" + reportEndYear;
                //ViewBag.lineChartData1 = lineChartData1;
                @ViewBag.reportType = reportType.ToLower();
            }
            else return View("Index");
            return View();
        }
	}
}