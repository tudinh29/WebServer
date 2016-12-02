using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebMVC.EntityFramework;
using WebMVC.Common;
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
    public class RetrivalInvalidController : BaseController
    {
        [HttpGet]
        // GET: RetrivalInvalid
        public ActionResult Index(string searchString, int page = 1, int size = 10)
        {
            List<RETRIVAL_INVALID> list = new List<RETRIVAL_INVALID>();
            HttpClient client = new AccessAPI().Access();
            if (String.IsNullOrEmpty(searchString))
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/RETRIVAL_INVALID/GetAllRetrivalInvalid")).Result;

                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<List<RETRIVAL_INVALID>>().Result;
                }
                var listRetrival = list.ToPagedList(page, size);
                return View(listRetrival);
            }
            else
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/RETRIVAL_INVALID/FindRetrivalInvalid?searchString={0}", searchString)).Result;

                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<List<RETRIVAL_INVALID>>().Result;
                }
                var listRetrival = list.ToPagedList(page, size);
                return View(listRetrival);
            }
        }

        public ActionResult ExportPDF()
        {
            HttpClient client = new AccessAPI().Access();
            string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Page: [page] of [toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            HttpResponseMessage response = client.GetAsync(string.Format("api/RETRIVAL_INVALID/GetAllRetrivalInvalid")).Result;
            var list = new List<RETRIVAL_INVALID>();
            if (response.IsSuccessStatusCode)
            {
                list = response.Content.ReadAsAsync<List<RETRIVAL_INVALID>>().Result;
            }
            return new Rotativa.PartialViewAsPdf("RetrivalError", list)
            {   //RetrivalInvalid
                FileName = "RetrivalInvalid.pdf",
                CustomSwitches = footer
            };
        }

        public ActionResult ExportExcel()
        {
            var list = getAllRetrivalInvalid().ToList();

            var gv = new GridView();
            gv.DataSource = list;
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Retrival_Invalid.xls");
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
            var list = getAllRetrivalInvalid().ToList();

            StringWriter sw = new StringWriter();
            sw.WriteLine("Retrival Code,Account Number,Merchant Code,Transaction Code,Tracsaction Date,Report Date,Amount, Error Message");
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Retrival_Invalid.csv");
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

        private List<RETRIVAL_INVALID> getAllRetrivalInvalid()
        {
            var RetrivalInvalid = new List<RETRIVAL_INVALID>();

            string domain = "";
            string url = domain + "api/RETRIVAL_INVALID/GetAllRetrivalInvalid";

            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                RetrivalInvalid = response.Content.ReadAsAsync<List<RETRIVAL_INVALID>>().Result;
            }

            return RetrivalInvalid;
        }
    }
}