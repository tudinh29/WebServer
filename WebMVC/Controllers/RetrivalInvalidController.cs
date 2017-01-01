using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebMVC.EntityFramework;
using WebMVC.Common;
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
            int totalPage = 0;
            int maxPage = 4;
            int totalRetrival = 0;
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:21212/");


            HttpResponseMessage response1 = client.GetAsync(string.Format("api/RETRIVAL_INVALID/CountRetrivalInvalid")).Result;

            if (response1.IsSuccessStatusCode)
            {
                totalRetrival = response1.Content.ReadAsAsync<int>().Result;
            }
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (String.IsNullOrEmpty(searchString))
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/RETRIVAL_INVALID/FindAllRetrivalInvalid?pageIndex={0}&pageSize={1}", page - 1, size)).Result;

                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<List<RETRIVAL_INVALID>>().Result;
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
                HttpResponseMessage response = client.GetAsync(string.Format("api/RETRIVAL_INVALID/FindRetrivalInvalidElement?searchString={0}&pageIndex={1}&pageSize={2}", searchString, page - 1, size)).Result;

                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<List<RETRIVAL_INVALID>>().Result;
                    HttpResponseMessage response2 = client.GetAsync(string.Format("api/RETRIVAL_INVALID/CountRetrivalInvalidElement?searchString={0}", searchString)).Result;
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

        public ActionResult ExportPDF(string searchString)
        {
            var list = new List<RETRIVAL_INVALID>();
            string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Page: [page] of [toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }

            HttpClient client = new AccessAPI().Access();

            if (!String.IsNullOrEmpty(searchString))
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/RETRIVAL_INVALID/FindRetrivalInvalid?searchString={0}", searchString)).Result;
                list = response.Content.ReadAsAsync<List<RETRIVAL_INVALID>>().Result;
            }
            else
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/RETRIVAL_INVALID/GetAllRetrivalInvalid")).Result;
                list = response.Content.ReadAsAsync<List<RETRIVAL_INVALID>>().Result;
            }

            return new Rotativa.PartialViewAsPdf("RetrivalError", list)
            {   //RetrivalInvalid
                FileName = "RetrivalInvalid.pdf",
                CustomSwitches = footer
            };
        }

        public ActionResult ExportExcel(string searchString)
        {
            var list = getAllRetrivalInvalid().ToList();
            HttpClient client = new AccessAPI().Access();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();

            if (!String.IsNullOrEmpty(searchString))
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/RETRIVAL_INVALID/FindRetrivalInvalid?searchString={0}", searchString)).Result;
                list = response.Content.ReadAsAsync<List<RETRIVAL_INVALID>>().Result;
            }
            else
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/RETRIVAL_INVALID/GetAllRetrivalInvalid")).Result;
                list = response.Content.ReadAsAsync<List<RETRIVAL_INVALID>>().Result;
            }

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

        public ActionResult ExportCSV(string searchString)
        {
            var list = getAllRetrivalInvalid().ToList();
            HttpClient client = new AccessAPI().Access();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();

            if (!String.IsNullOrEmpty(searchString))
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/RETRIVAL_INVALID/FindRetrivalInvalid?searchString={0}", searchString)).Result;
                list = response.Content.ReadAsAsync<List<RETRIVAL_INVALID>>().Result;
            }
            else
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/RETRIVAL_INVALID/GetAllRetrivalInvalid")).Result;
                list = response.Content.ReadAsAsync<List<RETRIVAL_INVALID>>().Result;
            }

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