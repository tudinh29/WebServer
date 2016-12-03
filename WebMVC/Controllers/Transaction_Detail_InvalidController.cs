using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC.EntityFramework;
using WebMVC.Common;
using PagedList;
using System.Web.Mvc.Html;
using System.Net.Http;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;
using CsvHelper;
using WebAPI.Controllers;
using Newtonsoft.Json;
using System.Data;
using Rotativa;





namespace WebMVC.Controllers
{
    public class Transaction_Detail_InvalidController : BaseController
    {
        //
        // GET: /Transaction_Detail_Invalid/
        [HttpGet]
        public ActionResult Index(int page = 1, int size = 10)
        {
            List<Models.TransInvalidTiny> transInvalid = new List<Models.TransInvalidTiny>();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            HttpClient client = new AccessAPI().Access();
            if(temp.UserType == "T")
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindAllTransaction_Detail_Invalid")).Result;
                if (response.IsSuccessStatusCode)
                {
                    transInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                }
               
            }
            else
            {
                if(temp.UserType == "A")
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindTransInvalid_Agent?AgentCode={0}", temp.UserName)).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        transInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                    }
                }
                else
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindTransInvalid_Merchant?MerchantCode={0}", temp.UserName)).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        transInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                    }
                }
            }
            var listTransInvalid = transInvalid.ToPagedList(page, size);
            return View(listTransInvalid);
        }

        [HttpGet]
         public ActionResult FindTransactionDetailInvalidElement(string search, int page = 1, int size = 10)
        {
            List<Models.TransInvalidTiny> listTransInvalid = new List<Models.TransInvalidTiny>();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            HttpClient client = new AccessAPI().Access();
            if (temp.UserType == "T")
            {
                if (search == "")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindTransInvalidElement?searchString={0}", search)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        listTransInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                    }
                    @TempData["search"] = search;
                }

                var list=  listTransInvalid.ToPagedList(page, size);
                return View("Index", list);
            }
            else
            {
                if (temp.UserType == "A")
                {
                    if (search == "")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindTransInvalidElement_Agent?searchString={0}&agentCode={1}", search, temp.UserName)).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            listTransInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                        }
                        @TempData["search"] = search;
                        @ViewBag.agentCode = temp.UserName;
                    }

                    var list = listTransInvalid.ToPagedList(page, size);
                    return View("Index", list);
                }
                else
                {
                    if (search == "")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindTransInvalidElement_Merchant?searchString={0}&merchantCode={1}", search, temp.UserName)).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            listTransInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                        }
                        @TempData["search"] = search;
                        @ViewBag.agentCode = temp.UserName;
                    }

                    var list = listTransInvalid.ToPagedList(page, size);
                    return View("Index", list);
                }
            }
        }
        public ActionResult ExportToExcel(int page= 1, int size = 10)
        {
            List<Models.TransInvalidTiny> listTransInvalid = new List<Models.TransInvalidTiny>();
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            HttpClient client = new AccessAPI().Access();
            var searchString = @TempData["search"];
            if(temp.UserType == "T")
            {
                
                if(searchString == null)
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindAllTransaction_Detail_Invalid")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        listTransInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                    }
                }
                else
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindTransInvalidElement?searchString={0}", searchString)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        listTransInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                    }
                }
                
                
            }
            else
            {
                if(temp.UserType == "A")
                {

                    if(searchString == null)
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindTransInvalid_Agent?AgentCode={0}", temp.UserName)).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            listTransInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                        }
                    }
                    else
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindTransInvalidElement_Agent?searchString={0}&agentCode={1}", searchString, temp.UserName)).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            listTransInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                        }
                    }
                    
                   // @ViewBag.search = search;
                }
                else
                {
                    if(searchString == null)
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindTransInvalid_Merchant?MerchantCode={0}", temp.UserName)).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            listTransInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                        }
                    }
                    else
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindTransInvalidElement_Merchant?searchString={0}&merchantCode={1}", searchString, temp.UserName)).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            listTransInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                        }
                    }
                    
                    //@ViewBag.search = search;
                }
            }
         
            var gv = new GridView();
            gv.DataSource = listTransInvalid;
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename= Transaction_Detail_Invalid.xls");
            Response.ContentType = "application/ms-excel";

            //Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter tw = new HtmlTextWriter(sw);

            gv.RenderControl(tw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            
            Response.End();
            var list = listTransInvalid.ToPagedList(page, size);
            return View("Index", list); 

        }

        public ActionResult ExportToPDF()
        {

            var listTransInvalid = new List<Models.TransInvalidTiny>();
            string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Page: [page] of [toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            HttpClient client = new AccessAPI().Access();
            var searchString = @TempData["search"];
            if (temp.UserType == "T")
            {

                if (searchString == null)
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindAllTransaction_Detail_Invalid")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        listTransInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                    }
                }
                else
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindTransInvalidElement?searchString={0}", searchString)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        listTransInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                    }
                }


            }
            else
            {
                if (temp.UserType == "A")
                {

                    if (searchString == null)
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindTransInvalid_Agent?AgentCode={0}", temp.UserName)).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            listTransInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                        }
                    }
                    else
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindTransInvalidElement_Agent?searchString={0}&agentCode={1}", searchString, temp.UserName)).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            listTransInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                        }
                    }

                   
                }
                else
                {
                    if (searchString == null)
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindTransInvalid_Merchant?MerchantCode={0}", temp.UserName)).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            listTransInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                        }
                    }
                    else
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindTransInvalidElement_Merchant?searchString={0}&merchantCode={1}", searchString, temp.UserName)).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            listTransInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                        }
                    }

                   
                }
            }
            
           return new Rotativa.PartialViewAsPdf("TransInvalidPDF", listTransInvalid)
            {   //MerchantSumaryDailyStatistical
                FileName = "Transaction_Detail_Invalid.pdf",
                CustomSwitches = footer
            };
        }
        public ActionResult ExportToCSV(int page= 1, int size = 10)
        {
            List<Models.TransInvalidTiny> listTransInvalid = new List<Models.TransInvalidTiny>();
            string footer = "--footer-right \"Date: [date] [time]\" " + "--footer-center \"Page: [page] of [toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;
            }
            HttpClient client = new AccessAPI().Access();
            var searchString = @TempData["search"];
            if (temp.UserType == "T")
            {

                if (searchString == null)
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindAllTransaction_Detail_Invalid")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        listTransInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                    }
                }
                else
                {
                    HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindTransInvalidElement?searchString={0}", searchString)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        listTransInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                    }
                }


            }
            else
            {
                if (temp.UserType == "A")
                {

                    if (searchString == null)
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindTransInvalid_Agent?AgentCode={0}", temp.UserName)).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            listTransInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                        }
                    }
                    else
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindTransInvalidElement_Agent?searchString={0}&agentCode={1}", searchString, temp.UserName)).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            listTransInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                        }
                    }

                   
                }
                else
                {
                    if (searchString == null)
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindTransInvalid_Merchant?MerchantCode={0}", temp.UserName)).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            listTransInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                        }
                    }
                    else
                    {
                        HttpResponseMessage response = client.GetAsync(string.Format("api/TRANSACTION_DETAIL_INVALID/FindTransInvalidElement_Merchant?searchString={0}&merchantCode={1}", searchString, temp.UserName)).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            listTransInvalid = response.Content.ReadAsAsync<List<Models.TransInvalidTiny>>().Result;
                        }
                    }

                  
                }
            }
            StringWriter sw = new StringWriter();
            sw.WriteLine("TransactionCode,ReportDate,MerchantCode,CardtypeCode ,TransactionAmount ,TransactionDate,AccountNumber,TransactionTypeCode,AgentCode,ErrorMessage");
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Transaction_Detail_Invalid.csv");
            Response.ContentType = "text/csv";
            var csv = new CsvWriter(sw);
            foreach (var item in listTransInvalid)
            {
                csv.WriteRecord(item);
            }
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            
             var list = listTransInvalid.ToPagedList(page, size);
            return View("Index", list);
        }
	}
}