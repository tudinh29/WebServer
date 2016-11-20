using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.EntityFramework;

namespace WebAPI.Controllers
{
    public class MESSAGETYPEController : ApiController
    {
        private APIDbContext db = new APIDbContext();

        // GET: api/MESSAGETYPE
        [HttpGet]
        public List<MESSAGE> GetMessageSend(string sender)
        {

            object[] paremeter = 
                {
                    new SqlParameter("@sender", sender)
                };
            var res = db.Database.SqlQuery<MESSAGE>("sp_MessageSent @sender", paremeter).ToList();
            return res;

        }

        [HttpGet]
        public int CountUnreadMessage(string MaCode, string UserType)
        {
            object[] paremeter =
                {
                    new SqlParameter("@MaCode", MaCode),
                    new SqlParameter("@UserType", UserType)
                };
            int res = db.Database.SqlQuery<int>("sp_CountUnreadMessenge @MaCode, @UserType", paremeter).FirstOrDefault<int>();
            return res;
        }
    }
}