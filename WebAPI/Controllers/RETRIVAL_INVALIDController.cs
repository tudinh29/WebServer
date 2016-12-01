using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using WebAPI.EntityFramework;

namespace WebAPI.Controllers
{
    public class RETRIVAL_INVALIDController : ApiController
    {
        private APIDbContext db = new APIDbContext();
        [HttpGet]
        public List<RETRIVAL_INVALID> GetAllRetrivalInvalid()
        {
            var res = db.Database.SqlQuery<RETRIVAL_INVALID>("sp_GetAllRetrivalInvalid").ToList();
            return res;
        }

        [HttpGet]
        public List<RETRIVAL> FindRetrivalInvalid(string searchString)
        {
            object[] parameter =
                {
                    new SqlParameter("@Element", searchString)
                };

            var res = db.Database.SqlQuery<RETRIVAL>("exec sp_FindRetrivalInvalid @Element", parameter).ToList();
            return res;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
    }
}