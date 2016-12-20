using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.EntityFramework;
using System.Data.SqlClient;

namespace WebAPI.Controllers
{
    public class REGIONController : ApiController
    {
        private APIDbContext db = new APIDbContext();

        [HttpGet]
        public List<REGION> FindAllRegion()
        {
            var res = db.Database.SqlQuery<REGION>("SP_FindAllRegion").ToList();
            return res;
        }

        [HttpGet]
        public REGION GetRegionName(string RegionCode)
        {
            object[] paremeter = 
                {
                    new SqlParameter("@RegionCode", RegionCode)
                };
            REGION res = db.Database.SqlQuery<REGION>("SP_GetRegionName @RegionCode", paremeter).FirstOrDefault();
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

        private bool REGIONExists(string id)
        {
            return db.REGION.Count(e => e.RegionCode == id) > 0;
        }
    }
}