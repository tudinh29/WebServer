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

        private USER_INFORMATION GetUserName()
        {
            var model = Session[CommonConstants.USER_SESSION];
            var user = new USER_INFORMATION();
            if (model != null)
            {
                user = (USER_INFORMATION)model;
            }
            return user;
        }
        // GET: Message
        public ActionResult Index(int page = 1, int size = 10)
        {
            var user = GetUserName();

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
            var user = GetUserName();

            MESSAGE message = new MESSAGE();

            HttpClient client = new AccessAPI().Access();
            StringContent content = new StringContent("");
            HttpResponseMessage response = client.GetAsync(string.Format("api/MESSAGE/GetMESSAGE/{0}", id)).Result;

           
            if (response.IsSuccessStatusCode)
            {
                message = response.Content.ReadAsAsync<MESSAGE>().Result;
                if (message.Receiver != "ALL" && message.Receiver != "all" && user.UserName != message.Sender)
                {
                    HttpResponseMessage response1 = client.PostAsync(string.Format("api/MESSAGE/UpdateIsRead?ID={0}", id), content).Result;
                    bool check = response1.IsSuccessStatusCode;
                }

            }

            HttpResponseMessage response2 = client.GetAsync(string.Format("api/MESSAGETYPE/CountUnreadMessage?MaCode={0}&UserType={1}", user.UserName, user.UserType)).Result;
            if (response2.IsSuccessStatusCode)
            {
                int number = response2.Content.ReadAsAsync<int>().Result;
                Session.Add(CommonConstants.NUMBER_UNREAD_MESSAGE, number);
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
            if (Ms.Receiver != null && Ms.ReceiverType != null && Ms.Message != null)
            {
                var user = GetUserName();
                Ms.Sender = user.UserName;
                Ms.SenderType = user.UserType;
                Ms.DateSend = DateTime.Now;

                HttpClient client = new AccessAPI().Access();
                StringContent content = new StringContent("");
                HttpResponseMessage response = client.PostAsJsonAsync("api/MESSAGE/InsertMassage", Ms).Result;
                response.EnsureSuccessStatusCode();
                TempData["AlertMessage"] = "Tin nhắn đã gửi đi thành công";
                TempData["AlertType"] = "alert-success";
                return RedirectToAction("MessageSent", "Message");
            }
            TempData["AlertMessage"] = "Tin nhắn gửi không thành công vui lòng kiểm tra lại";
            TempData["AlertType"] = "alert-danger";
            return View();

        }

        [HttpGet]
        public ActionResult MessageSent(int page = 1, int size = 10)
        {
            var user = GetUserName();

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