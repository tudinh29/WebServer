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
using Newtonsoft.Json;
using PagedList;

namespace WebAPI.Controllers
{
    public class AGENTController : ApiController
    {
        private APIDbContext db = new APIDbContext();

        [HttpGet]
        public List<AGENT> FindAllAgent()
        {
            var res = db.Database.SqlQuery<AGENT>("sp_FindAllAgent").ToList();
            return res;
        }

        public List<AGENT> FindAllAgent(string agentCode, string agentName)
        {
            string cityCode = "Austin";
            string address = "197 Clarendon Way";
            string owner = "S0EM6MXZ4TTGZ8QMHF7P4N2957OPPUW46SZ7CEUGUW4";
            object[] paremeter = 
                {
                    new SqlParameter("@MaAgent", agentCode),
                    new SqlParameter("@MaThanhPho", cityCode),
                    new SqlParameter("@agentName", agentName),
                    new SqlParameter("@owner", owner),
                    new SqlParameter("@diaChi", address)
                };
            var res = db.Database.SqlQuery<AGENT>("sp_SearchEngine_Agent @MaAgent, @MaThanhPho, @agentName, @owner, @diaChi", paremeter).ToList();
            return res;
        }

        [HttpGet]
        public AGENT FindAgent(string agentCode)
        {
            object[] paremeter = 
                {
                    new SqlParameter("@AgentCode", agentCode)
                };
            AGENT res = db.Database.SqlQuery<AGENT>("sp_GetAgent @AgentCode", paremeter).FirstOrDefault();
            return res;
        }

        [HttpPost]
        public bool ChangeStatus(string agentCode)
        {
            try
            {
                object[] paremeter = 
                {
                    new SqlParameter("@AgentCode", agentCode)
                };
                db.Database.ExecuteSqlCommand("exec sp_InactiveOrActive_Agent @AgentCode", paremeter);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        // PUT: api/AGENT/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAGENT(string id, AGENT aGENT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aGENT.AgentCode)
            {
                return BadRequest();
            }

            db.Entry(aGENT).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AGENTExists(id))
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

        // POST: api/AGENT
        [ResponseType(typeof(AGENT))]
        public async Task<IHttpActionResult> PostAGENT(AGENT aGENT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AGENT.Add(aGENT);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AGENTExists(aGENT.AgentCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = aGENT.AgentCode }, aGENT);
        }

        // DELETE: api/AGENT/5
        [ResponseType(typeof(AGENT))]
        public async Task<IHttpActionResult> DeleteAGENT(string id)
        {
            AGENT aGENT = await db.AGENT.FindAsync(id);
            if (aGENT == null)
            {
                return NotFound();
            }

            db.AGENT.Remove(aGENT);
            await db.SaveChangesAsync();

            return Ok(aGENT);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AGENTExists(string id)
        {
            return db.AGENT.Count(e => e.AgentCode == id) > 0;
        }

        public IEnumerable<AGENT> ListAgents(string agentCode, string agentName, int page, int pageSize)
        {
            IOrderedQueryable<AGENT> model = db.AGENT;
            if (!string.IsNullOrEmpty(agentCode))
            {
                model = model.Where(x => x.AgentCode.Contains(agentCode) || x.AgentName.Contains(agentName)).OrderByDescending(x => x.AgentCode);
            }
            return model.ToPagedList(page, pageSize);
        }
    }
}