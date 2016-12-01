using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebMVC.Common;
using WebMVC.EntityFramework;
using PagedList;
using System.Web.Mvc.Html;
using System.IO;
using CsvHelper;
using ClosedXML.Excel;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace WebMVC.Controllers
{
    public class RetrivalController : BaseController
    {
        [HttpGet]
        // GET: Retrival
        public ViewResult Index(string searchString, int page = 1, int size = 10)
        {
            List<RETRIVAL> list = new List<RETRIVAL>();
            HttpClient client = new AccessAPI().Access();
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:21212/");

            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (String.IsNullOrEmpty(searchString))
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/Retrival/FindAllRetrival")).Result;

                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<List<RETRIVAL>>().Result;
                }
                var listRetrival = list.ToPagedList(page, size);
                return View(listRetrival);
            }
            else
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/Retrival/FindRetrivalElement?searchString={0}", searchString)).Result;

                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<List<RETRIVAL>>().Result;
                }
                @ViewBag.searchString = searchString;
                var listRetrival = list.ToPagedList(page, size);
                return View(listRetrival);
            }
            
        }

        [HttpGet]
        public ActionResult ViewDetail_Retrival()
        {
            RETRIVAL list = new RETRIVAL();
            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(string.Format("api/RETRIVAL/FindRetrival?RetrivalCode={0}", Request.QueryString["Retrivalcode"])).Result;
            if (response.IsSuccessStatusCode)
            {
                list = response.Content.ReadAsAsync<RETRIVAL>().Result;
            }
            return View(list);
        }

        public ActionResult ExportExcel(string searchString)
        {
            List<RETRIVAL> list = new List<RETRIVAL>();
            HttpClient client = new AccessAPI().Access();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View("Index");

            if (!String.IsNullOrEmpty(searchString))
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/Retrival/FindRetrivalElement?searchString={0}", searchString)).Result;
                list = response.Content.ReadAsAsync<List<RETRIVAL>>().Result;
            }
            else
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/Retrival/FindAllRetrival")).Result;
                list = response.Content.ReadAsAsync<List<RETRIVAL>>().Result;
            }

            var gv = new GridView();
            gv.DataSource = list;
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=RETRIVAL.xls");
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
            List<RETRIVAL> list = new List<RETRIVAL>();
            HttpClient client = new AccessAPI().Access();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            else return View("Index");

            if (!String.IsNullOrEmpty(searchString))
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/Retrival/FindRetrivalElement?searchString={0}", searchString)).Result;
                list = response.Content.ReadAsAsync<List<RETRIVAL>>().Result;
            }
            else
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/Retrival/FindAllRetrival")).Result;
                list = response.Content.ReadAsAsync<List<RETRIVAL>>().Result;
            }

            StringWriter sw = new StringWriter();
            sw.WriteLine("Retrival Code,Account Number,Merchant Code,Transaction Code,Transaction Date,Report Date,Amount");
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=RETRIVAL.csv");
            Response.ContentType = "text/csv";
            //var csv = new CsvWriter(sw);
            foreach (var item in list)
            {
                sw.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6}",
                   item.RetrivalCode.ToString(),
                   item.AccountNumber.ToString(),
                   item.MerchantCode.ToString(),
                   item.TransactionCode.ToString(),
                   item.TransactionDate.ToString(),
                   item.ReportDate.ToString(),
                   item.Amout.ToString()));
            }
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("Index");
        }  
    }
}