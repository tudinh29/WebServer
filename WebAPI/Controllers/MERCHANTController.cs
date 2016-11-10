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
    public class MERCHANTController : ApiController
    {
        private APIDbContext db = new APIDbContext();


        [HttpGet]
        public List<MERCHANT> FindAllMerchant()
        {
            var res = db.Database.SqlQuery<MERCHANT>("sp_FindAllMerchant").ToList();
            return res;
        }

        [HttpGet]
        public MERCHANT FindMerchant(string merchantCode)
        {
            object[] paremeter = 
                {
                    new SqlParameter("@MerchantCode", merchantCode)
                };
            MERCHANT res = db.Database.SqlQuery<MERCHANT>("sp_GetMerchant @MerchantCode", paremeter).FirstOrDefault();
            return res;
        }

        [HttpGet]
        public List<MERCHANT> FindMerchantByAgentCode(string agentCode)
        {
            object[] paremeter = 
                {
                    new SqlParameter("@AgentCode", agentCode)
                };
            var res = db.Database.SqlQuery<MERCHANT>("sp_GetMerchantByAgentCode @AgentCode", paremeter).ToList();
            return res;
        }
        

        [HttpPost]
        public bool ChangeStatus(string merchantCode)
        {
            try
            {
                object[] paremeter = 
                {
                    new SqlParameter("@MerchantCode", merchantCode)
                };
                db.Database.ExecuteSqlCommand("exec sp_InactiveOrActive_Merchant @MerchantCode", paremeter);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // PUT: api/MERCHANT/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMERCHANT(string id, MERCHANT mERCHANT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mERCHANT.MerchantCode)
            {
                return BadRequest();
            }

            db.Entry(mERCHANT).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MERCHANTExists(id))
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

        // POST: api/MERCHANT
        [ResponseType(typeof(MERCHANT))]
        public async Task<IHttpActionResult> PostMERCHANT(MERCHANT mERCHANT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MERCHANT.Add(mERCHANT);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MERCHANTExists(mERCHANT.MerchantCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = mERCHANT.MerchantCode }, mERCHANT);
        }

        // DELETE: api/MERCHANT/5
        [ResponseType(typeof(MERCHANT))]
        public async Task<IHttpActionResult> DeleteMERCHANT(string id)
        {
            MERCHANT mERCHANT = await db.MERCHANT.FindAsync(id);
            if (mERCHANT == null)
            {
                return NotFound();
            }

            db.MERCHANT.Remove(mERCHANT);
            await db.SaveChangesAsync();

            return Ok(mERCHANT);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MERCHANTExists(string id)
        {
            return db.MERCHANT.Count(e => e.MerchantCode == id) > 0;
        }
    }
}