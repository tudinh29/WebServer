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
    public class MESSAGEController : ApiController
    {
        private APIDbContext db = new APIDbContext();

        [HttpGet]
        public List<MESSAGE> GetMessage(string MaCode, string UserType)
        {
            object[] paremeter = 
                {
                    new SqlParameter("@MaCode", MaCode),
                    new SqlParameter("@UserType", UserType)
                };
            var res = db.Database.SqlQuery<MESSAGE>("sp_ReadMessage @MaCode, @UserType", paremeter).ToList();
            return res;
        }

        [HttpPost]
        public bool UpdateIsRead(string ID)
        {
            try
            {
                object[] paremeter = 
                {
                    new SqlParameter("@ID", ID)
                };
                var res = db.Database.ExecuteSqlCommand("sp_UpdateIsRead @ID", paremeter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        public bool InsertMassage(MESSAGE message)
        {
            try
            {
                object[] paremeter = 
                {
                    new SqlParameter("@sender", message.Sender),
                    new SqlParameter("@senderType", message.SenderType),
                    new SqlParameter("@receive", message.Receiver),
                    new SqlParameter("@receiveType", message.ReceiverType),
                    new SqlParameter("@message", message.Message),
                };
                var res = db.Database.ExecuteSqlCommand("sp_InsertMessage @sender, @senderType, @receive, @receiveType, @message", paremeter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        // GET: api/MESSAGE
        public IQueryable<MESSAGE> GetMESSAGE()
        {
            return db.MESSAGE;
        }

         //GET: api/MESSAGE/5
        [ResponseType(typeof(MESSAGE))]
        public async Task<IHttpActionResult> GetMESSAGE(int id)
        {
            MESSAGE mESSAGE = await db.MESSAGE.FindAsync(id);
            if (mESSAGE == null)
            {
                return NotFound();
            }

            return Ok(mESSAGE);
        }

        // PUT: api/MESSAGE/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMESSAGE(int id, MESSAGE mESSAGE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mESSAGE.ID)
            {
                return BadRequest();
            }

            db.Entry(mESSAGE).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MESSAGEExists(id))
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

        // POST: api/MESSAGE
        //[ResponseType(typeof(MESSAGE))]
        //public async Task<IHttpActionResult> PostMESSAGE(MESSAGE mESSAGE)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.MESSAGE.Add(mESSAGE);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = mESSAGE.ID }, mESSAGE);
        //}

        // DELETE: api/MESSAGE/5
        [ResponseType(typeof(MESSAGE))]
        public async Task<IHttpActionResult> DeleteMESSAGE(int id)
        {
            MESSAGE mESSAGE = await db.MESSAGE.FindAsync(id);
            if (mESSAGE == null)
            {
                return NotFound();
            }

            db.MESSAGE.Remove(mESSAGE);
            await db.SaveChangesAsync();

            return Ok(mESSAGE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MESSAGEExists(int id)
        {
            return db.MESSAGE.Count(e => e.ID == id) > 0;
        }
    }
}