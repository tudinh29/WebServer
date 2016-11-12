using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebMVC.EntityFramework;
using WebMVC.Common;
using PagedList;
using System.Web.Mvc.Html;

namespace WebMVC.Controllers
{
    public class MessageController : BaseController
    {
        // GET: Message
        public ActionResult Index(int page = 1, int size = 10)
        {
            var model = Session[CommonConstants.USER_SESSION]; 
            var user = new USER_INFORMATION();
            if (model != null)
            {
                user = (USER_INFORMATION)model;
            }

            List<MESSAGE> ListMs = new List<MESSAGE>();

            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(string.Format("api/MESSAGE/GetMessage?MaCode={0}&UserType={1}", user.UserName, user.UserType)).Result;

            if (response.IsSuccessStatusCode)
            {
                ListMs = response.Content.ReadAsAsync<List<MESSAGE>>().Result;
                //return RedirectToAction("Index", "Home");
            }
            var ListMessage = ListMs.ToPagedList(page, size);
            return View(ListMessage);
        }

       

        public ActionResult ViewMessage(string id)
        {

            MESSAGE message = new MESSAGE();

            HttpClient client = new AccessAPI().Access();
            StringContent content = new StringContent("");
            HttpResponseMessage response = client.GetAsync(string.Format("api/MESSAGE/{0}", id)).Result;
            HttpResponseMessage response1 = client.PostAsync(string.Format("api/MESSAGE/UpdateIsRead?ID={0}", id), content).Result;
            if (response.IsSuccessStatusCode)
            {
                message = response.Content.ReadAsAsync<MESSAGE>().Result;
                bool check = response1.IsSuccessStatusCode;       
            }

            return View(message);
        }

        [HttpGet]
        public ActionResult CreateMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMessage(MESSAGE Ms)
        {
            var model = Session[CommonConstants.USER_SESSION];
            var user = new USER_INFORMATION();
            if (model != null)
            {
                user = (USER_INFORMATION)model;
            }
            Ms.Sender = user.UserName;
            Ms.SenderType = user.UserType;
            Ms.DateSend = DateTime.Now;

            HttpClient client = new AccessAPI().Access();
            StringContent content = new StringContent("");
            HttpResponseMessage response = client.PostAsJsonAsync("api/MESSAGE", Ms).Result;
            response.EnsureSuccessStatusCode();
            TempData["AlertMessage"] = "Thêm thành công";
            TempData["AlertType"] = "alert-success";
            return RedirectToAction("MessageSent", "Message");
        }

        [HttpGet]
        public ActionResult MessageSent(int page = 1, int size = 10)
        {
            var model = Session[CommonConstants.USER_SESSION];
            var user = new USER_INFORMATION();
            if (model != null)
            {
                user = (USER_INFORMATION)model;
            }

            List<MESSAGE> ListMessageSend = new List<MESSAGE>();

            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(string.Format("api/MESSAGETYPE/GetMessageSend?sender={0}", user.UserName)).Result;

            if (response.IsSuccessStatusCode)
            {
                ListMessageSend = response.Content.ReadAsAsync<List<MESSAGE>>().Result;
                //return RedirectToAction("Index", "Home");
            }

            var ListMessage = ListMessageSend.ToPagedList(page, size);
            return View(ListMessage);
        }
    }
}