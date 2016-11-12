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
     
    public class PROCESSORController : ApiController
    {

        private APIDbContext db = new APIDbContext();

        [HttpGet]
        public List<PROCESSOR> SelectAllProcessor()
        {
            var res = db.Database.SqlQuery<PROCESSOR>("sp_SelectAllProcessor").ToList();
            return res;
        }




        // PUT: api/PROCESSOR/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPROCESSOR(int id, PROCESSOR pROCESSOR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pROCESSOR.ID)
            {
                return BadRequest();
            }

            db.Entry(pROCESSOR).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PROCESSORExists(id))
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

        // POST: api/PROCESSOR
        [ResponseType(typeof(PROCESSOR))]
        public async Task<IHttpActionResult> PostPROCESSOR(PROCESSOR pROCESSOR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PROCESSOR.Add(pROCESSOR);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pROCESSOR.ID }, pROCESSOR);
        }

        // DELETE: api/PROCESSOR/5
        [ResponseType(typeof(PROCESSOR))]
        public async Task<IHttpActionResult> DeletePROCESSOR(int id)
        {
            PROCESSOR pROCESSOR = await db.PROCESSOR.FindAsync(id);
            if (pROCESSOR == null)
            {
                return NotFound();
            }

            db.PROCESSOR.Remove(pROCESSOR);
            await db.SaveChangesAsync();

            return Ok(pROCESSOR);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PROCESSORExists(int id)
        {
            return db.PROCESSOR.Count(e => e.ID == id) > 0;
        }
    }
}