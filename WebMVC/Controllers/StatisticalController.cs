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

namespace WebMVC.Controllers
{
    public class StatisticalController : BaseController
    {
        // GET: Statistical
        public ActionResult Index(int page = 1,int size = 10)
        {
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View();

            if (temp.UserType == "T")   //Master
            {
                var list = getAllSumDaily().ToPagedList( page, size);
                return View(list);
                
            }
            else   //Agent
            {
                //Code here
                return Redirect("Home");
            }
           
        }

        public ActionResult ExportPDF()
        {
            string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Page: [page] of [toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            var list = getAllSumDaily().ToPagedList(1, 10);
            return new ViewAsPdf("Index",list)
            {
                FileName = Server.MapPath("~/Content/ListMerchant.pdf"),
                CustomSwitches = footer
            };
        }

        public ActionResult ExportExcel()
        {
            var list = getAllSumDaily().ToList();

            var gv = new GridView();
            gv.DataSource = list;
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

        public ActionResult ExportCSV()
        {
            var list = getAllSumDaily().ToList();
           
            StringWriter sw = new StringWriter();
            sw.WriteLine("Report Date,Merchant Code,Sale Amount,Return Amount,Region Code,Merchant Type,Agent Code");
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=MERCHANT_SUMMARY_DAILY.csv");
            Response.ContentType = "text/csv";
            var csv = new CsvWriter(sw);
            foreach (var item in list)
            {
                csv.WriteRecord(item);
            }
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("Index");
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

            string domain = "";
            string url = domain + "/api/MERCHANT_SUMMARY_DAILY/GetAllStatistic";

            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(url).Result;
            //HttpResponseMessage response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/GetAllStatistic")).Result;
            if (response.IsSuccessStatusCode)
            {
                marchantSummary = response.Content.ReadAsAsync<List<Models.MerchantSummaryDailyTiny>>().Result;
            }

            return marchantSummary;
        }
    }
}