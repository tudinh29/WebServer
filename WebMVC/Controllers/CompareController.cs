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
                else
                {
                    reportDataMonthFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportDataMonthly_Compare?startMonth={0}&startYear={1}";
                    reportDateMonthForLineFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportData_Generality_Compare_Merchant?startMonth={0}&startYear={1}&endMonth={2}&endYear={3}&merchantCode={4}";

                    reportDataQuarterFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportData_Generality_Quaterly_Compare_Merchant?startQuarter={0}&startYear={1}&endQuarter={2}&endYear={3}&merchantCode={4}";
                    reportDateQuarterForLineFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportData_Generality_Quaterly_Compare_Merchant?startQuarter={0}&startYear={1}&endQuarter={2}&endYear={3}&merchantCode={4}";

                    reportDataYearFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportData_Generality_Yearly_Compare_Merchant?startYear={0}&endYear={1}&merchantCode={2}";
                    reportDateYearForLineFormat = "api/MERCHANT_SUMMARY_DAILY/GetReportData_Generality_Yearly_Compare_Merchant?startYear={0}&endYear={1}&merchantCode={2}";


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
                    if (temp.UserType == "A")
                    {
                        switch (reportType.ToLower())
                        {
                            


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
                    else
                    {
                        switch (reportType.ToLower())
                        {



                            case "month":
                                reportDataAPI = String.Format(reportDataMonthFormat, reportStartMonth, reportStartYear);
                                reportMonthForLineAPI = String.Format(reportDateMonthForLineFormat, reportStartMonth, reportStartYear, reportEndMonth, reportEndYear, temp.UserName);

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

            }

            HttpResponseMessage response = client.GetAsync(reportMonthForLineAPI).Result;

            List<Models.Statistic> lineChartData = new List<Models.Statistic>();

            if (response.IsSuccessStatusCode)
            {
                lineChartData = response.Content.ReadAsAsync<List<Models.Statistic>>().Result;


                ViewBag.lineChartData = lineChartData;
                @ViewBag.Month1 = reportStartMonth;
                @ViewBag.Month2 = reportEndMonth;
                @ViewBag.Year1 = reportStartYear;
                @ViewBag.Year2 = reportEndYear;
                @ViewBag.Quarter1 = reportStartQuarter;
                @ViewBag.Quarter2 = reportEndQuarter;
                @ViewBag.startMonth = reportStartMonth + "/" + reportStartYear;
                @ViewBag.endMonth = reportEndMonth + "/" + reportEndYear;

                @ViewBag.reportType = reportType.ToLower();
                if (reportType.ToLower() == "month")
                {
                    @ViewBag.ds = tinhTiLe_thang(lineChartData, (int.Parse)(reportStartMonth), (int.Parse)(reportStartYear), (int.Parse)(reportEndMonth), (int.Parse
                    )(reportEndYear));
                }
                else
                {
                    if (reportType.ToLower() == "year")
                    {

                        @ViewBag.ds = tinhTiLe_nam(lineChartData, (int.Parse)(reportStartYear), (int.Parse
                   )(reportEndYear));
                    }
                    else
                    {
                        @ViewBag.ds = tinhTiLe_quy(lineChartData, (int.Parse)(reportStartQuarter), (int.Parse)(reportStartYear), (int.Parse)(reportEndQuarter), (int.Parse
                    )(reportEndYear));
                    }
                }

            }
            else return View("Index");
            return View();
        }

        private List<Models.Statistic> tinhTiLe_thang(List<Models.Statistic> ds, int month1, int year1, int month2, int year2)
        {
            Models.Statistic d;
            List<Models.Statistic> tile = new List<Models.Statistic>();

            if (ds.Count > 0)
            {
                int ngay = 0;
                int thang = 0;
                string sname = String.Empty;
                decimal svalue = 0;
                decimal sreturn = 0;
                long scount = 0;
                int vt = 0;
                Models.Statistic thang1 = new Models.Statistic();
                Models.Statistic thang2 = new Models.Statistic();
                thang1.Name = month1 + "/" + year1;
                thang2.Name = month2 + "/" + year2;

                foreach (Models.Statistic item in ds)
                {
                    vt = item.Name.IndexOf('/');
                    if (sname == "")
                    {
                        ngay = (int.Parse)(item.Name.Substring(0, vt));
                        thang = (int.Parse)(item.Name.Substring(vt + 1, item.Name.Length - vt - 1));
                        sname = item.Name;
                        svalue = (decimal.Parse)(item.Value.ToString());
                        sreturn = (decimal.Parse)(item.ReturnAmount.ToString());
                        scount = (long.Parse)(item.TransactionCount.ToString());
                    }
                    else
                    {
                        if (ngay == (int.Parse)(item.Name.Substring(0, vt)))
                        {
                            d = new Models.Statistic();
                            d.Name = ngay.ToString() + "/" + month2.ToString() + " - " + ngay.ToString() + "/" + month1.ToString();
                            if (svalue != 0)
                            {
                                d.Value = Math.Round(((decimal.Parse)(item.Value.ToString()) - svalue) / svalue, 2) * 100;
                            }
                            else
                            {
                                d.Value = 100;
                            }
                            if (sreturn != 0)
                            {
                                d.ReturnAmount = Math.Round(((decimal.Parse)(item.ReturnAmount.ToString()) - sreturn) / sreturn, 2) * 100;
                            }
                            else
                            {
                                d.ReturnAmount = 100;
                            }
                            d.TransactionCount = (long.Parse)(item.TransactionCount.ToString()) - scount;
                            sname = String.Empty;
                            tile.Add(d);
                            thang1.Value += svalue;
                            thang1.ReturnAmount += sreturn;
                            thang1.TransactionCount += scount;
                            thang2.Value += (decimal.Parse)(item.Value.ToString());
                            thang2.ReturnAmount += (decimal.Parse)(item.ReturnAmount.ToString());
                            thang2.TransactionCount += (long.Parse)(item.TransactionCount.ToString());
                        }
                        else
                        {
                            d = new Models.Statistic();
                            if (thang == month1)
                            {
                                d.Name = ngay.ToString() + "/" + month2.ToString() + " - " + ngay.ToString() + "/" + month1.ToString();
                                if (svalue != 0)
                                {
                                    d.Value = -100;
                                }
                                else
                                {
                                    d.Value = 0;
                                }
                                if (sreturn != 0)
                                {
                                    d.ReturnAmount = -100;
                                }
                                else
                                {
                                    d.ReturnAmount = 0;
                                }

                                d.TransactionCount = scount;
                                tile.Add(d);
                                thang1.Value += svalue;
                                thang1.ReturnAmount += sreturn;
                                thang1.TransactionCount += scount;
                            }
                            else
                            {
                                d.Name = ngay.ToString() + "/" + month2.ToString() + " - " + ngay.ToString() + "/" + month1.ToString();
                                if (svalue != 0)
                                {
                                    d.Value = 100;
                                }
                                else
                                {
                                    d.Value = 0;
                                }
                                if (sreturn != 0)
                                {
                                    d.ReturnAmount = 100;
                                }
                                else
                                {
                                    d.ReturnAmount = 0;
                                }
                                
                                tile.Add(d);
                                thang2.Value += svalue;
                                thang2.ReturnAmount += sreturn;
                                thang2.TransactionCount += scount;
                            }
                            ngay = (int.Parse)(item.Name.Substring(0, vt));
                            thang = (int.Parse)(item.Name.Substring(vt + 1, item.Name.Length - vt - 1));
                            sname = item.Name;
                            svalue = (decimal.Parse)(item.Value.ToString());
                            sreturn = (decimal.Parse)(item.ReturnAmount.ToString());
                            scount = (long.Parse)(item.TransactionCount.ToString());
                        }
                    }
                }
                d = new Models.Statistic();
                if(sname != "")
                {
                    if (thang == month1)
                    {
                        d.Name = ngay.ToString() + "/" + month2.ToString() + " - " + ngay.ToString() + "/" + month1.ToString();
                        if (svalue != 0)
                        {
                            d.Value = -100;
                        }
                        else
                        {
                            d.Value = 0;
                        }
                        if (sreturn != 0)
                        {
                            d.ReturnAmount = -100;
                        }
                        else
                        {
                            d.ReturnAmount = 0;
                        }

                        d.TransactionCount = scount;
                        tile.Add(d);
                        thang1.Value += svalue;
                        thang1.ReturnAmount += sreturn;
                        thang1.TransactionCount += scount;
                    }
                    else
                    {
                        d.Name = ngay.ToString() + "/" + month2.ToString() + " - " + ngay.ToString() + "/" + month1.ToString();
                        if (svalue != 0)
                        {
                            d.Value = 100;
                        }
                        else
                        {
                            d.Value = 0;
                        }
                        if (sreturn != 0)
                        {
                            d.ReturnAmount = 100;
                        }
                        else
                        {
                            d.ReturnAmount = 0;
                        }
                        d.TransactionCount = -scount;
                        tile.Add(d);
                        thang2.Value += svalue;
                        thang2.ReturnAmount += sreturn;
                        thang2.TransactionCount += scount;
                    }

                }
                
                d = new Models.Statistic();
                d.Name = month2 + "/" + year2 + " - " + month1 + "/" + year1;
                if (thang1.Value != 0)
                {

                    d.Value = Math.Round((thang2.Value - thang1.Value) / thang1.Value, 2);


                }
                else
                {
                    if (thang2.Value != 0)
                    {
                        d.Value = 100;
                    }
                    else
                    {
                        d.Value = 0;
                    }
                }
                if (thang1.ReturnAmount != 0)
                {
                    d.ReturnAmount = Math.Round((thang2.ReturnAmount - thang1.ReturnAmount) / thang1.ReturnAmount, 2);
                }
                else
                {
                    if (thang2.ReturnAmount != 0)
                    {
                        d.ReturnAmount = 100;
                    }
                    else
                    {
                        d.ReturnAmount = 0;
                    }
                }
                tile.Add(d);
            }


            return tile;
        }

        private List<Models.Statistic> tinhTiLe_quy(List<Models.Statistic> ds, int quarter1, int year1, int quarter2, int year2)
        {
            Models.Statistic d;
            List<Models.Statistic> tile = new List<Models.Statistic>();
            if (ds.Count > 0)
            {
                Models.Statistic quy1 = new Models.Statistic();
                Models.Statistic quy2 = new Models.Statistic();
                quy1.Name = quarter1.ToString() + "/" + year1.ToString();
                quy2.Name = quarter2.ToString() + "/" + year2.ToString();
                

              
                    int thang = 0;
                    int nam = 0;
                    string sname = String.Empty;
                    decimal svalue = 0;
                    decimal sreturn = 0;
                    long scount = 0;
                    int vt = 0;
                    int t = 0;
                    int quy = 0;


                    foreach (Models.Statistic item in ds)
                    {
                        vt = item.Name.IndexOf('-');
                        int vt1 = 0;
                        vt1 = item.Name.IndexOf('/');

                        if (sname == "")
                        {
                            thang = (int.Parse)(item.Name.Substring(0, vt));
                            t = thang;
                            nam = (int.Parse)(item.Name.Substring(vt1 + 1, item.Name.Length - vt1 - 1));
                            quy = (int.Parse)(item.Name.Substring(vt + 1, vt1 - vt - 1));
                            if (quy == quarter1 && quarter1 > quarter2)
                            {
                                thang = thang + (quarter1 - quarter2) * 3;
                               
                            }
                            if (quy == quarter2 && quarter2 > quarter1)
                            {
                                thang = thang + (quarter2 - quarter1) * 3;
                               
                            }
                            sname = item.Name;
                            svalue = (decimal.Parse)(item.Value.ToString());
                            sreturn = (decimal.Parse)(item.ReturnAmount.ToString());
                            scount = (long.Parse)(item.TransactionCount.ToString());

                        }
                        else
                        {

                            if (t == (int.Parse)(item.Name.Substring(0, vt)))
                            {
                                d = new Models.Statistic();
                                d.Name = thang.ToString() + '/' + year2.ToString() + " - " + thang.ToString() + "/" + year1.ToString();
                                if (svalue != 0)
                                {
                                    d.Value = Math.Round(((decimal.Parse)(item.Value.ToString()) - svalue) / svalue, 2) * 100;
                                }
                                else
                                {
                                    d.Value = 100;
                                }
                                if (sreturn != 0)
                                {
                                    d.ReturnAmount = Math.Round(((decimal.Parse)(item.ReturnAmount.ToString()) - sreturn) / sreturn, 2) * 100;
                                }
                                else
                                {
                                    d.ReturnAmount = 100;
                                }
                                d.TransactionCount = (long.Parse)(item.TransactionCount.ToString()) - scount;
                                sname = String.Empty;
                                tile.Add(d);
                                quy1.Value += svalue;
                                quy1.ReturnAmount += sreturn;
                                quy1.TransactionCount += scount;
                                quy2.Value += (decimal.Parse)(item.Value.ToString());
                                quy2.ReturnAmount += (decimal.Parse)(item.ReturnAmount.ToString());
                                quy2.TransactionCount += (long.Parse)(item.TransactionCount.ToString());
                            }
                            else
                            {
                                d = new Models.Statistic();
                                if (quy == quarter1)
                                {
                                    d.Name = thang.ToString() + '/' + year2.ToString() + " - " + thang.ToString() + "/" + year1.ToString();
                                    if (svalue != 0)
                                    {
                                        d.Value = -100;
                                    }
                                    else
                                    {
                                        d.Value = 0;
                                    }
                                    if (sreturn != 0)
                                    {
                                        d.ReturnAmount = -100;
                                    }
                                    else
                                    {
                                        d.ReturnAmount = 0;
                                    }
                                    d.TransactionCount = scount;
                                    tile.Add(d);
                                    quy1.Value += svalue;
                                    quy1.ReturnAmount += sreturn;
                                    quy1.TransactionCount += scount;
                                }
                                else
                                {
                                    d.Name = thang.ToString() + '/' + year2.ToString() + " - " + thang.ToString() + "/" + year1.ToString();
                                    if (svalue != 0)
                                    {
                                        d.Value = 100;
                                    }
                                    else
                                    {
                                        d.Value = 0;
                                    }
                                    if (sreturn != 0)
                                    {
                                        d.ReturnAmount = 100;
                                    }
                                    else
                                    {
                                        d.ReturnAmount = 0;
                                    }
                                    d.TransactionCount = -scount;
                                    tile.Add(d);
                                    quy2.Value += svalue;
                                    quy2.ReturnAmount += sreturn;
                                    quy2.TransactionCount += scount;
                                }
                                thang = (int.Parse)(item.Name.Substring(0, vt));
                                t = thang;
                                nam = (int.Parse)(item.Name.Substring(vt1 + 1, item.Name.Length - vt1 - 1));
                                quy = (int.Parse)(item.Name.Substring(vt + 1, vt1));
                                if (quy == quarter1 && quarter1 > quarter2)
                                {
                                    thang = thang + (quarter1 - quarter2) * 3;
                                }
                                if (quy == quarter2 && quarter2 > quarter1)
                                {
                                    thang = thang + (quarter2 - quarter1) * 3;
                                }
                                sname = item.Name;
                                svalue = (decimal.Parse)(item.Value.ToString());
                                sreturn = (decimal.Parse)(item.ReturnAmount.ToString());
                                scount = (long.Parse)(item.TransactionCount.ToString());
                            }
                        }
                    }
                    d = new Models.Statistic();
                if(sname != "")
                {
                    if (quy == quarter1)
                    {
                        d.Name = thang.ToString() + '/' + year2.ToString() + " - " + thang.ToString() + "/" + year1.ToString();
                        if (svalue != 0)
                        {
                            d.Value = -100;
                        }
                        else
                        {
                            d.Value = 0;
                        }
                        if (sreturn != 0)
                        {
                            d.ReturnAmount = -100;
                        }
                        else
                        {
                            d.ReturnAmount = 0;
                        }
                        d.TransactionCount = scount;
                        tile.Add(d);
                        quy1.Value += svalue;
                        quy1.ReturnAmount += sreturn;
                    }
                    else
                    {
                        d.Name = thang.ToString() + '/' + year2.ToString() + " - " + thang.ToString() + "/" + year1.ToString();
                        if (svalue != 0)
                        {
                            d.Value = 100;
                        }
                        else
                        {
                            d.Value = 0;
                        }
                        if (sreturn != 0)
                        {
                            d.ReturnAmount = 100;
                        }
                        else
                        {
                            d.ReturnAmount = 0;
                        }
                        d.TransactionCount = -scount;
                        tile.Add(d);
                        quy2.Value += svalue;
                        quy2.ReturnAmount += sreturn;
                    }
                }
                   
                

                d = new Models.Statistic();
                d.Name = quarter2 + "/" + year2 + " - " + quarter1 + "/" + year1;
                if (quy1.Value != 0)
                {

                    d.Value = Math.Round((quy2.Value - quy1.Value) / quy1.Value, 2);


                }
                else
                {
                    if (quy2.Value != 0)
                    {
                        d.Value = 100;
                    }
                    else
                    {
                        d.Value = 0;
                    }
                }
                if (quy1.ReturnAmount != 0)
                {
                    d.ReturnAmount = Math.Round((quy2.ReturnAmount - quy1.ReturnAmount) / quy1.ReturnAmount, 2);
                }
                else
                {
                    if (quy2.ReturnAmount != 0)
                    {
                        d.ReturnAmount = 100;
                    }
                    else
                    {
                        d.ReturnAmount = 0;
                    }
                }
                tile.Add(d);
            }


            return tile;
        }

        private List<Models.Statistic> tinhTiLe_nam(List<Models.Statistic> ds, int year1, int year2)
        {
            Models.Statistic d;
            List<Models.Statistic> tile = new List<Models.Statistic>();
            if (ds.Count > 0)
            {
                if (ds.Count > 0)
                {
                    int nam = 0;
                    int thang = 0;
                    string sname = String.Empty;
                    decimal svalue = 0;
                    decimal sreturn = 0;
                    long scount = 0;
                    int vt = 0;
                    Models.Statistic nam1 = new Models.Statistic();
                    Models.Statistic nam2 = new Models.Statistic();
                    nam1.Name = year1.ToString();
                    nam2.Name = year2.ToString();

                    foreach (Models.Statistic item in ds)
                    {
                        vt = item.Name.IndexOf('/');
                        if (sname == "")
                        {
                            thang = (int.Parse)(item.Name.Substring(0, vt));
                            nam = (int.Parse)(item.Name.Substring(vt + 1, item.Name.Length - vt - 1));
                            sname = item.Name;
                            svalue = (decimal.Parse)(item.Value.ToString());
                            sreturn = (decimal.Parse)(item.ReturnAmount.ToString());
                            scount = (long.Parse)(item.TransactionCount.ToString());
                        }
                        else
                        {
                            if (thang == (int.Parse)(item.Name.Substring(0, vt)))
                            {
                                d = new Models.Statistic();
                                d.Name = thang.ToString() + "/" + year2.ToString() + " - " + thang.ToString() + "/" + year1.ToString();
                                if (svalue != 0)
                                {
                                    d.Value = Math.Round(((decimal.Parse)(item.Value.ToString()) - svalue) / svalue, 2) * 100;
                                }
                                else
                                {
                                    d.Value = 100;
                                }
                                if (sreturn != 0)
                                {
                                    d.ReturnAmount = Math.Round(((decimal.Parse)(item.ReturnAmount.ToString()) - sreturn) / sreturn, 2) * 100;
                                }
                                else
                                {
                                    d.ReturnAmount = 100;
                                }

                                d.TransactionCount = (long.Parse)(item.TransactionCount.ToString()) - scount;
                                sname = String.Empty;
                                tile.Add(d);
                                nam1.Value = svalue;
                                nam1.ReturnAmount = sreturn;
                                nam2.Value = (decimal.Parse)(item.Value.ToString());
                                nam2.ReturnAmount = (decimal.Parse)(item.ReturnAmount.ToString());

                            }
                            else
                            {
                                d = new Models.Statistic();
                                if (nam == year1)
                                {
                                    d.Name = thang.ToString() + "/" + year2.ToString() + " - " + thang.ToString() + "/" + year1.ToString();
                                    if (svalue != 0)
                                    {
                                        d.Value = -100;
                                    }
                                    else
                                    {
                                        d.Value = 0;
                                    }
                                    if (sreturn != 0)
                                    {
                                        d.ReturnAmount = -100;
                                    }
                                    else
                                    {
                                        d.ReturnAmount = 0;
                                    }
                                    d.TransactionCount = scount;
                                    tile.Add(d);
                                    nam1.Value = svalue;
                                    nam1.ReturnAmount = sreturn;
                                }
                                else
                                {
                                    d.Name = thang.ToString() + "/" + year2.ToString() + " - " + thang.ToString() + "/" + year1.ToString();
                                    if (svalue != 0)
                                    {
                                        d.Value = 100;
                                    }
                                    else
                                    {
                                        d.Value = 0;
                                    }
                                    if (sreturn != 0)
                                    {
                                        d.ReturnAmount = 100;
                                    }
                                    else
                                    {
                                        d.ReturnAmount = 0;
                                    }
                                    d.TransactionCount = -scount;
                                    tile.Add(d);
                                    nam2.Value = svalue;
                                    nam2.ReturnAmount = sreturn;
                                }
                                thang = (int.Parse)(item.Name.Substring(0, vt));
                                nam = (int.Parse)(item.Name.Substring(vt + 1, item.Name.Length - vt - 1));
                                sname = item.Name;
                                svalue = (decimal.Parse)(item.Value.ToString());
                                sreturn = (decimal.Parse)(item.ReturnAmount.ToString());
                                scount = (long.Parse)(item.TransactionCount.ToString());
                            }
                        }
                    }
                    d = new Models.Statistic();
                    if(sname != ""){
                    if (nam == year1)
                    {
                        d.Name = thang.ToString() + "/" + year2.ToString() + " - " + thang.ToString() + "/" + year1.ToString();
                        if (svalue != 0)
                        {
                            d.Value = -100;
                        }
                        else
                        {
                            d.Value = 0;
                        }
                        if (sreturn != 0)
                        {
                            d.ReturnAmount = -100;
                        }
                        else
                        {
                            d.ReturnAmount = 0;
                        }
                        d.TransactionCount = scount;
                        tile.Add(d);
                        nam1.Value = svalue;
                        nam1.ReturnAmount = sreturn;
                    }
                    else
                    {
                        d.Name = thang.ToString() + "/" + year2.ToString() + " - " + thang.ToString() + "/" + year1.ToString();
                        if (svalue != 0)
                        {
                            d.Value = 100;
                        }
                        else
                        {
                            d.Value = 0;
                        }
                        if (sreturn != 0)
                        {
                            d.ReturnAmount = 100;
                        }
                        else
                        {
                            d.ReturnAmount = 0;
                        }
                        d.TransactionCount = -scount;
                        tile.Add(d);
                        nam2.Value = svalue;
                        nam2.ReturnAmount = sreturn;
                    }
                }
                    d = new Models.Statistic();
                    d.Name = year2 + " - " + year1;
                    if (nam1.Value != 0)
                    {

                        d.Value = Math.Round((nam2.Value - nam1.Value) / nam1.Value, 2);


                    }
                    else
                    {
                        if (nam2.Value != 0)
                        {
                            d.Value = 100;
                        }
                        else
                        {
                            d.Value = 0;
                        }
                    }
                    if (nam1.ReturnAmount != 0)
                    {
                        d.ReturnAmount = Math.Round((nam2.ReturnAmount - nam1.ReturnAmount) / nam1.ReturnAmount, 2);
                    }
                    else
                    {
                        if (nam2.ReturnAmount != 0)
                        {
                            d.ReturnAmount = 100;
                        }
                        else
                        {
                            d.ReturnAmount = 0;
                        }
                    }
                    tile.Add(d);
                }


            }


            return tile;
        }

    }
}