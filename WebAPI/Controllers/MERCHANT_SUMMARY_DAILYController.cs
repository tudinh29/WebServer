using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.Script.Serialization;
using WebAPI.EntityFramework;

namespace WebAPI.Controllers
{
    public class MERCHANT_SUMMARY_DAILYController : ApiController
    {
        private APIDbContext db = new APIDbContext();

        private bool MERCHANT_SUMMARY_DAILYExists(DateTime id)
        {
            return db.MERCHANT_SUMMARY_DAILY.Count(e => e.ReportDate == id) > 0;
        }

        [HttpGet]
        public List<MERCHANT_SUMMARY_DAILY> GetAllStatistic()
        {
            var dbReturn = db.Database.SqlQuery<MERCHANT_SUMMARY_DAILY>("SP_GetAllStatistic_Default").ToList();
            return dbReturn;
        }

        [HttpGet]
        public List<MERCHANT_SUMMARY_DAILY> GetReportData()
        {
            var dbReturn = db.Database.SqlQuery<MERCHANT_SUMMARY_DAILY>("SP_GetReportData_Default").ToList();
            return dbReturn;
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