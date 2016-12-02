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

        // GET: api/MERCHANT_SUMMARY_DAILY
        public IQueryable<MERCHANT_SUMMARY_DAILY> GetMERCHANT_SUMMARY_DAILY()
        {
            return db.MERCHANT_SUMMARY_DAILY;
        }

        // GET: api/MERCHANT_SUMMARY_DAILY/5
        [ResponseType(typeof(MERCHANT_SUMMARY_DAILY))]
        public async Task<IHttpActionResult> GetMERCHANT_SUMMARY_DAILY(DateTime id)
        {
            MERCHANT_SUMMARY_DAILY mERCHANT_SUMMARY_DAILY = await db.MERCHANT_SUMMARY_DAILY.FindAsync(id);
            if (mERCHANT_SUMMARY_DAILY == null)
            {
                return NotFound();
            }

            return Ok(mERCHANT_SUMMARY_DAILY);
        }

        // PUT: api/MERCHANT_SUMMARY_DAILY/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMERCHANT_SUMMARY_DAILY(DateTime id, MERCHANT_SUMMARY_DAILY mERCHANT_SUMMARY_DAILY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mERCHANT_SUMMARY_DAILY.ReportDate)
            {
                return BadRequest();
            }

            db.Entry(mERCHANT_SUMMARY_DAILY).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MERCHANT_SUMMARY_DAILYExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/MERCHANT_SUMMARY_DAILY
        [ResponseType(typeof(MERCHANT_SUMMARY_DAILY))]
        public async Task<IHttpActionResult> PostMERCHANT_SUMMARY_DAILY(MERCHANT_SUMMARY_DAILY mERCHANT_SUMMARY_DAILY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MERCHANT_SUMMARY_DAILY.Add(mERCHANT_SUMMARY_DAILY);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MERCHANT_SUMMARY_DAILYExists(mERCHANT_SUMMARY_DAILY.ReportDate))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = mERCHANT_SUMMARY_DAILY.ReportDate }, mERCHANT_SUMMARY_DAILY);
        }

        // DELETE: api/MERCHANT_SUMMARY_DAILY/5
        [ResponseType(typeof(MERCHANT_SUMMARY_DAILY))]
        public async Task<IHttpActionResult> DeleteMERCHANT_SUMMARY_DAILY(DateTime id)
        {
            MERCHANT_SUMMARY_DAILY mERCHANT_SUMMARY_DAILY = await db.MERCHANT_SUMMARY_DAILY.FindAsync(id);
            if (mERCHANT_SUMMARY_DAILY == null)
            {
                return NotFound();
            }

            db.MERCHANT_SUMMARY_DAILY.Remove(mERCHANT_SUMMARY_DAILY);
            await db.SaveChangesAsync();

            return Ok(mERCHANT_SUMMARY_DAILY);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        private bool MERCHANT_SUMMARY_DAILYExists(DateTime id)
        {
            return db.MERCHANT_SUMMARY_DAILY.Count(e => e.ReportDate == id) > 0;
        }

        [HttpGet]
        public List<Models.MerchantSummaryDailyTiny> GetMerchantSummaryDefault()
        {
            var dbReturn = db.Database.SqlQuery<Models.MerchantSummaryDailyTiny>("SP_GetMerchantSummary_Default").ToList();
            return dbReturn;
        }

        [HttpGet]
        public List<Models.MerchantSummaryDailyTiny> GetMerchantSummaryForAgentDefault(string AgentCode)
        {
            object[] paremeter = 
                {
                    new SqlParameter("@AgentCode", AgentCode)
                };
            var res = db.Database.SqlQuery<Models.MerchantSummaryDailyTiny>("SP_GetMerchantSummaryForAgent_Default @AgentCode", paremeter).ToList();
            return res;
        }

        [HttpGet]
        public List<MERCHANT_SUMMARY_DAILY> GetReportData()
        {
            var dbReturn = db.Database.SqlQuery<MERCHANT_SUMMARY_DAILY>("SP_GetReportData_Default").ToList();
            return dbReturn;
        }

        [HttpGet]
        public List<Models.Statistic> GetReportDateForLineChart()
        {
            var dbReturn = db.Database.SqlQuery<Models.Statistic>("SP_GetReportDataForLineChart_Default").ToList();
            return dbReturn;
        }

        [HttpGet]
        public List<MERCHANT_SUMMARY_DAILY> GetReportDataGenerality(string startDate, string endDate)
        {
            object[] paremeter =
               {
                    new SqlParameter("@startDate", startDate),
                    new SqlParameter("@endDate", endDate)
                };
            var dbReturn = db.Database.SqlQuery<MERCHANT_SUMMARY_DAILY>("SP_GetReportData_Generality @startDate, @endDate", paremeter).ToList();
            return dbReturn;
        }

        [HttpGet]
        public List<Models.Statistic> GetReportDateForLineChartGenerality(string startDate, string endDate)
        {
            object[] paremeter =
               {
                    new SqlParameter("@startDate", startDate),
                    new SqlParameter("@endDate", endDate)
                };
            var dbReturn = db.Database.SqlQuery<Models.Statistic>("SP_GetReportDataForLineChart_Generality @startDate, @endDate", paremeter).ToList();
            return dbReturn;
        }


    }
}