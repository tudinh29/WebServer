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
    public class CITYController : ApiController
    {
        private APIDbContext db = new APIDbContext();

        [HttpGet]
        public List<CITY> SelectAllCity()
        {
            var res = db.Database.SqlQuery<CITY>("sp_SelectAllCity").ToList();
            return res;
        }
        
        
        // PUT: api/CITY/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCITY(string id, CITY cITY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cITY.CityCode)
            {
                return BadRequest();
            }

            db.Entry(cITY).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CITYExists(id))
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

        // POST: api/CITY
        [ResponseType(typeof(CITY))]
        public async Task<IHttpActionResult> PostCITY(CITY cITY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CITY.Add(cITY);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CITYExists(cITY.CityCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cITY.CityCode }, cITY);
        }

        // DELETE: api/CITY/5
        [ResponseType(typeof(CITY))]
        public async Task<IHttpActionResult> DeleteCITY(string id)
        {
            CITY cITY = await db.CITY.FindAsync(id);
            if (cITY == null)
            {
                return NotFound();
            }

            db.CITY.Remove(cITY);
            await db.SaveChangesAsync();

            return Ok(cITY);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CITYExists(string id)
        {
            return db.CITY.Count(e => e.CityCode == id) > 0;
        }
    }
}