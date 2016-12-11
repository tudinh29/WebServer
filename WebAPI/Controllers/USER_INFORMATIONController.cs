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
    public class USER_INFORMATIONController : ApiController
    {
        private APIDbContext db = new APIDbContext();

        [HttpPost]
        public List<USER_INFORMATION> Search(string username, string password)
        {
            object[] paremeter = 
                {
                    new SqlParameter("@UserName", username),
                    new SqlParameter("@Password", password)
                };
            var res = db.Database.SqlQuery<USER_INFORMATION>("sp_Login @UserName, @Password", paremeter).ToList();
            return res;
        }

        [HttpGet]
        public bool Change(string username, string password, string newpassword)
        {
            var retVal = new SqlParameter("@Success", SqlDbType.Int) { Direction = ParameterDirection.Output };
            object[] paremeter = 
                {
                    new SqlParameter("@UserName", username),
                    new SqlParameter("@NewPassword", newpassword),
                    new SqlParameter("@CurrentPassword", password), 
                    retVal
                };
            db.Database.ExecuteSqlCommand("exec @Success = sp_ChangePassword @UserName, @CurrentPassword, @NewPassword", paremeter);
            if ((int)retVal.Value == 1)
                return true;
            else
                return false;
        }
        //public List<USER_INFOMATION> Search(string username, string password)
        //{
        //    var result = db.USER_INFOMATION.Where(x => x.UserName == username && x.Password == password).ToList().Select(x => new USER_INFOMATION
        //    {
        //        UserName = x.UserName,
        //        Password = null,
        //        UserType = x.UserType,
        //        Status = x.Status
        //    }).ToList();
        //    return result;
        //}

        // GET: api/USER_INFORMATION
        public IQueryable<USER_INFORMATION> GetUSER_INFORMATION()
        {
            return db.USER_INFORMATION;
        }

        // GET: api/USER_INFORMATION/5
        [ResponseType(typeof(USER_INFORMATION))]
        public async Task<IHttpActionResult> GetUSER_INFORMATION(string id)
        {
            USER_INFORMATION uSER_INFORMATION = await db.USER_INFORMATION.FindAsync(id);
            if (uSER_INFORMATION == null)
            {
                return NotFound();
            }

            return Ok(uSER_INFORMATION);
        }

        // PUT: api/USER_INFORMATION/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUSER_INFORMATION(string id, USER_INFORMATION uSER_INFORMATION)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != uSER_INFORMATION.UserName)
            {
                return BadRequest();
            }

            db.Entry(uSER_INFORMATION).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!USER_INFORMATIONExists(id))
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

        // POST: api/USER_INFORMATION
        [ResponseType(typeof(USER_INFORMATION))]
        public async Task<IHttpActionResult> PostUSER_INFORMATION(USER_INFORMATION uSER_INFORMATION)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.USER_INFORMATION.Add(uSER_INFORMATION);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (USER_INFORMATIONExists(uSER_INFORMATION.UserName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = uSER_INFORMATION.UserName }, uSER_INFORMATION);
        }

        // DELETE: api/USER_INFORMATION/5
        [ResponseType(typeof(USER_INFORMATION))]
        public async Task<IHttpActionResult> DeleteUSER_INFORMATION(string id)
        {
            USER_INFORMATION uSER_INFORMATION = await db.USER_INFORMATION.FindAsync(id);
            if (uSER_INFORMATION == null)
            {
                return NotFound();
            }

            db.USER_INFORMATION.Remove(uSER_INFORMATION);
            await db.SaveChangesAsync();

            return Ok(uSER_INFORMATION);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool USER_INFORMATIONExists(string id)
        {
            return db.USER_INFORMATION.Count(e => e.UserName == id) > 0;
        }
    }
}