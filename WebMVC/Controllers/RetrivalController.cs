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

namespace WebMVC.Controllers
{
    public class RetrivalController : BaseController
    {
        [HttpGet]
        // GET: Retrival
        public ViewResult Index(string searchString, string currentFilter, int? page, int size = 10)
        {
            List<RETRIVAL> list = new List<RETRIVAL>();
            HttpClient client = new AccessAPI().Access();
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:21212/");
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            int pageNumber = (page ?? 1);

            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (String.IsNullOrEmpty(searchString))
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/Retrival/FindAllRetrival")).Result;

                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<List<RETRIVAL>>().Result;
                }
                var listRetrival = list.ToPagedList(pageNumber, size);
                return View(listRetrival);
            }
            else
            {
                HttpResponseMessage response = client.GetAsync(string.Format("api/Retrival/FindRetrivalElement?searchString={0}", searchString)).Result;

                if (response.IsSuccessStatusCode)
                {
                    list = response.Content.ReadAsAsync<List<RETRIVAL>>().Result;
                }
                var listRetrival = list.ToPagedList(pageNumber, size);
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
    }
}