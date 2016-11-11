using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebMVC.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using WebMVC.EntityFramework;
using WebMVC.Common;
using System.Threading.Tasks;
namespace WebMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            if (model.UserName == null || model.Password == null)
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu còn trống. Vui lòng nhập lại !!!");
                return View("Index"); //khong nhat thiet phai co model
            }

            List<USER_INFORMATION> list = new List<USER_INFORMATION>();

            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:21212/");

            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(string.Format("api/USER_INFORMATION/Search?username={0}&password={1}", model.UserName, Encryptor.MD5Hash(model.Password))).Result;

            if (response.IsSuccessStatusCode)
            {
                list = response.Content.ReadAsAsync<List<USER_INFORMATION>>().Result;
                //return RedirectToAction("Index", "Home");
            }
            //System.Console.Write(list[0].UserName);
            if(list.Count == 1)
            {
                var userSession = new USER_INFORMATION();
                userSession = list[0];
                Session.Add(CommonConstants.USER_SESSION, userSession);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác. Vui lòng xem lại !!!");
                return View("Index"); //khong nhat thiet phai co model
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}