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

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (model.UserName == null || model.Password == null)
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu còn trống. Vui lòng nhập lại !!!");
                return View("Index"); //khong nhat thiet phai co model
            }

            List<USER_INFORMATION> list = new List<USER_INFORMATION>();

            HttpClient client = new AccessAPI().Access();
            StringContent content = new StringContent("");
            HttpResponseMessage response = client.PostAsync(string.Format("api/USER_INFORMATION/Search?username={0}&password={1}", model.UserName, Encryptor.MD5Hash(model.Password)), content).Result;
            if (response.IsSuccessStatusCode)
            {
                list = response.Content.ReadAsAsync<List<USER_INFORMATION>>().Result;
            }
            if (list.Count == 1)
            {
                var userSession = new USER_INFORMATION();
                userSession = list[0];
                Session.Add(CommonConstants.HASH_PASSWORD, Encryptor.MD5Hash(model.Password));
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

        public ActionResult ChangePassword()
        {
            return View("ChangePassword");
        }

        public ActionResult ChangePassword_Action(string currentPassword, string newPassword, string confirmPassword)
        {
            if (String.IsNullOrEmpty(currentPassword) || String.IsNullOrEmpty(newPassword) || String.IsNullOrEmpty(confirmPassword) || newPassword != confirmPassword)
            {
                TempData["AlertMessage"] = "Vui lòng nhập đầy đủ thông tin!!!";
                TempData["AlertType"] = "alert-warning";
                return View("ChangePassword"); //khong nhat thiet phai co model
            }
            List<USER_INFORMATION> list = new List<USER_INFORMATION>();

            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:21212/");

            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = (USER_INFORMATION)Session[CommonConstants.USER_SESSION];
            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(string.Format("api/USER_INFORMATION/Change?username={0}&password={1}&newpassword={2}", result.UserName.ToString(), Encryptor.MD5Hash(currentPassword), Encryptor.MD5Hash(newPassword))).Result;
            if (response.IsSuccessStatusCode)
            {
                var check = response.Content.ReadAsAsync<bool>().Result;
                if (check == true)
                {
                    TempData["AlertMessage"] = "Đổi mật khẩu thành công !!!";
                    TempData["AlertType"] = "alert-success";
                }
                else
                {
                    TempData["AlertMessage"] = "Vui lòng nhập đúng mật khẩu hiện tại!!!";
                    TempData["AlertType"] = "alert-warning";
                }
                return View("ChangePassword");
            }
            else
            {
                TempData["AlertMessage"] = "Vui lòng nhập đúng mật khẩu hiện tại!!!";
                TempData["AlertType"] = "alert-warning";
                return View("Index"); //khong nhat thiet phai co model
            }
        }
    }
}