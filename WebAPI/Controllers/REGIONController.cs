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