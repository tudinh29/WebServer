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
    public class MERCHANT_TYPEController : ApiController
    {
        private APIDbContext db = new APIDbContext();
        [HttpGet]
        public List<MERCHANT_TYPE> SelectAllMerchantType()
        {
            var res = db.Database.SqlQuery<MERCHANT_TYPE>("sp_SelectAllMerchantType").ToList();
            return res;
        }
       

        

        

        // PUT: api/MERCHANT_TYPE/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMERCHANT_TYPE(string id, MERCHANT_TYPE mERCHANT_TYPE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mERCHANT_TYPE.MerchantType)
            {
                return BadRequest();
            }

            db.Entry(mERCHANT_TYPE).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MERCHANT_TYPEExists(id))
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

        // POST: api/MERCHANT_TYPE
        [ResponseType(typeof(MERCHANT_TYPE))]
        public async Task<IHttpActionResult> PostMERCHANT_TYPE(MERCHANT_TYPE mERCHANT_TYPE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MERCHANT_TYPE.Add(mERCHANT_TYPE);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MERCHANT_TYPEExists(mERCHANT_TYPE.MerchantType))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = mERCHANT_TYPE.MerchantType }, mERCHANT_TYPE);
        }

        // DELETE: api/MERCHANT_TYPE/5
        [ResponseType(typeof(MERCHANT_TYPE))]
        public async Task<IHttpActionResult> DeleteMERCHANT_TYPE(string id)
        {
            MERCHANT_TYPE mERCHANT_TYPE = await db.MERCHANT_TYPE.FindAsync(id);
            if (mERCHANT_TYPE == null)
            {
                return NotFound();
            }

            db.MERCHANT_TYPE.Remove(mERCHANT_TYPE);
            await db.SaveChangesAsync();

            return Ok(mERCHANT_TYPE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MERCHANT_TYPEExists(string id)
        {
            return db.MERCHANT_TYPE.Count(e => e.MerchantType == id) > 0;
        }
    }
}