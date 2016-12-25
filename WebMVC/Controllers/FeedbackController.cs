using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC.EntityFramework;
using WebMVC.Common;
using WebMVC.Models;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Web.Configuration;
using System.Net.Http;

namespace WebMVC.Controllers
{
    public class FeedbackController : BaseController
    {
        // GET: Feedback
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendMail(FormCollection form)
        {
            var model = Session[CommonConstants.USER_SESSION];
            var temp = new USER_INFORMATION();
            var merchant = new MERCHANT();
            if (model != null)
            {
                temp = (USER_INFORMATION)model;

                HttpClient client = new AccessAPI().Access();
                HttpResponseMessage response = client.GetAsync(string.Format("api/MERCHANT/FindMerchant?merchantCode={0}", temp.UserName)).Result;
                if (response.IsSuccessStatusCode)
                {
                    merchant = response.Content.ReadAsAsync<MERCHANT>().Result;
                }
            }
            else return View("Index");

            var fbMail = new Mail();
            fbMail.Subject = form["Subject"];
            fbMail.Content = form["Content"];
            var Signature = "\n\n-------------\n" + merchant.MerchantName + "\n" + merchant.Owner + "\n" + merchant.Address1
                + "\n" + merchant.Email + "\n" + merchant.Phone;
            fbMail.MailMaster = WebConfigurationManager.AppSettings["toAddress"];
            fbMail.MailMerchant = WebConfigurationManager.AppSettings["fromAddress"];
            fbMail.Password = WebConfigurationManager.AppSettings["Password"];
            var fromAddress = new MailAddress(fbMail.MailMerchant);
            var toAddress = new MailAddress(fbMail.MailMaster);

            var smtp = new SmtpClient
            {
                Host = WebConfigurationManager.AppSettings["host"],
                Port = int.Parse(WebConfigurationManager.AppSettings["port"]),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(fromAddress.Address, fbMail.Password)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = fbMail.Subject,
                Body = fbMail.Content + Signature
            })
            {
                try
                {
                    smtp.Send(message);
                    TempData["Message"] = "true";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["Message"] = "false";
                    return RedirectToAction("Index");
                }
            }
        }
    }
}