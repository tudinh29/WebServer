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
        public List<RETRIVAL_INVALID> FindRetrivalInvalid(string searchString)
        {
            object[] parameter =
                {
                    new SqlParameter("@Element", searchString)
                };

            var res = db.Database.SqlQuery<RETRIVAL_INVALID>("exec sp_FindRetrivalInvalid @Element", parameter).ToList();
            return res;
        }

        [HttpGet]
        public List<RETRIVAL_INVALID> FindAllRetrivalInvalid(int pageIndex, int pageSize)
        {
            object[] paremeter = 
                {
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };
            var res = db.Database.SqlQuery<RETRIVAL_INVALID>("sp_FindAllRetrivalInvalid @pageIndex, @pageSize", paremeter).ToList();
            return res;
        }

        [HttpGet]
        public int CountRetrivalInvalid()
        {
            var res = db.Database.SqlQuery<int>("sp_CountRetrivalInvalid").ToList();
            return res[0];
        }

        [HttpGet]
        public int CountRetrivalInvalidElement(string searchString)
        {
            object[] parameter =
                {
                    new SqlParameter("@Element", searchString)
                };
            var res = db.Database.SqlQuery<int>("sp_CountRetrivalInvalidElement @Element", parameter).ToList();
            return res[0];
        }

        [HttpGet]
        public List<RETRIVAL_INVALID> FindRetrivalInvalidElement(string searchString, int pageIndex, int pageSize)
        {
            object[] parameter =
                {
                    new SqlParameter("@Element", searchString),
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };

            var res = db.Database.SqlQuery<RETRIVAL_INVALID>("exec sp_FindRetrivalInvalidElement @Element, @pageIndex, @pageSize", parameter).ToList();
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