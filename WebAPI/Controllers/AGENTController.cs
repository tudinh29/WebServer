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
using PagedList;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class AGENTController : ApiController
    {
        private APIDbContext db = new APIDbContext();


        [HttpGet]
        public List<AGENT> FindFilter(string query)
        {
            var res = db.Database.SqlQuery<AGENT>(query).ToList();
            return res;
        }

        [HttpGet]
        public List<AGENT> FindAllAgent()
        {
            var res = db.Database.SqlQuery<AGENT>("sp_FindAllAgent").ToList();
            return res;
        }

        [HttpGet]
        public List<AGENT> FindAgentElement(string searchString)
        {
            object[] parameter =
                {
                    new SqlParameter("@Element", searchString)
                };

            var res = db.Database.SqlQuery<AGENT>("exec sp_FindAgentElement @Element", parameter).ToList();
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
            catch (Exception)
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

        [HttpPost]
        public bool UpdateAgent(string id, AGENT agent)
        {
            //m nhap merchant name vao
            try
            {
                object[] parameter =
                {
                    new SqlParameter("@AgentCode", agent.AgentCode),
                    new SqlParameter("@AgentName", agent.AgentName),
                    new SqlParameter("@Status", agent.AgentStatus??(object)DBNull.Value),
                    new SqlParameter("@Owner", agent.Owner??(object)DBNull.Value),
                    new SqlParameter("@Address1", agent.Address1??(object)DBNull.Value),
                    new SqlParameter("@Address2", agent.Address2??(object)DBNull.Value),
                    new SqlParameter("@Address3", agent.Address3??(object)DBNull.Value),
                    new SqlParameter("@CityCode", agent.CityCode),
                    new SqlParameter("@Zip", agent.Zip??System.Data.SqlTypes.SqlInt32.Null),
                    new SqlParameter("@Phone",string.IsNullOrEmpty( agent.Phone) ? "" : agent.Phone),
                    new SqlParameter("@Fax",string.IsNullOrEmpty( agent.Fax) ? "" : agent.Fax),
                    new SqlParameter("@Email",string.IsNullOrEmpty(agent.Fax) ? "" : agent.Fax),
                    new SqlParameter("@ApprovalDate", agent.ApprovalDate??(object)DBNull.Value),
                    new SqlParameter("@CloseDate", agent.CloseDate??(object)DBNull.Value),
                    new SqlParameter("@FirstActiveDate", agent.FirstActiveDate??(object)DBNull.Value),
                    new SqlParameter("@LastActiveDate", agent.LastActiveDate??(object)DBNull.Value)

                };
                string sql = "Sp_EditAgent @AgentCode, @AgentName, @Status, @Owner"
                    + ",@Address1,@Address2,@Address3,@CityCode,@Zip,@Phone,@Fax,@Email, @ApprovalDate,@CloseDate,@FirstActiveDate"
                    + ",@LastActiveDate";

                db.Database.ExecuteSqlCommand(sql, parameter);
                return true;
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.ToString());
                return false;
            }
        }

        [HttpPost]
        public bool AddNewAgent(AGENT agent)
        {
            //m nhap merchant name vao
            try
            {
                object[] parameter =
                {
                    new SqlParameter("@AgentName", agent.AgentName),
                    new SqlParameter("@Status", agent.AgentStatus??(object)DBNull.Value),
                    new SqlParameter("@Owner", agent.Owner??(object)DBNull.Value),
                    new SqlParameter("@Address1", agent.Address1??(object)DBNull.Value),
                    new SqlParameter("@Address2", agent.Address2??(object)DBNull.Value),
                    new SqlParameter("@Address3", agent.Address3??(object)DBNull.Value),
                    new SqlParameter("@CityCode", agent.CityCode),
                    new SqlParameter("@Zip", agent.Zip??System.Data.SqlTypes.SqlInt32.Null),
                    new SqlParameter("@Phone",string.IsNullOrEmpty( agent.Phone) ? "" : agent.Phone),
                    new SqlParameter("@Fax",string.IsNullOrEmpty( agent.Fax) ? "" : agent.Fax),
                    new SqlParameter("@Email",string.IsNullOrEmpty(agent.Fax) ? "" : agent.Fax),
                    new SqlParameter("@ApprovalDate", agent.ApprovalDate??(object)DBNull.Value),
                    new SqlParameter("@CloseDate", agent.CloseDate??(object)DBNull.Value),
                    new SqlParameter("@FirstActiveDate", agent.FirstActiveDate??(object)DBNull.Value),
                    new SqlParameter("@LastActiveDate", agent.LastActiveDate??(object)DBNull.Value)

                };
                string sql = "sp_AddNewAgent @AgentName, @Status, @Owner"
                    + ",@Address1,@Address2,@Address3,@CityCode,@Zip,@Phone,@Fax,@Email, @ApprovalDate,@CloseDate,@FirstActiveDate"
                    + ",@LastActiveDate";
                db.Database.ExecuteSqlCommand(sql, parameter);


                return true;
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.ToString());
                return false;
            }
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

        [HttpGet]
        public List<AGENT> FindAllAgent_ForQuery(int pageIndex, int pageSize)
        {
            object[] parameter =
                {
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };
            var res = db.Database.SqlQuery<AGENT>("sp_FindAllAgent_ForQuery @pageIndex, @pageSize", parameter).ToList();
            return res;
        }

        [HttpGet]
        public int CountAgent()
        {
            var res = db.Database.SqlQuery<int>("sp_CountAgent").ToList();
            return res[0];
        }

        [HttpGet]
        public List<AGENT> FindAgentElement_ForQuery(string searchString, int pageIndex, int pageSize)
        {
            object[] parameter =
                {
                    new SqlParameter("@Element", searchString),
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };

            var res = db.Database.SqlQuery<AGENT>("sp_FindAgentElement_ForQuery @Element, @pageIndex, @pageSize", parameter).ToList();
            return res;
        }

        [HttpGet]
        public int CountAgentElement_ForQuery(string searchString)
        {
            object[] parameter =
                {
                    new SqlParameter("@Element", searchString)
                };

            var res = db.Database.SqlQuery<int>("sp_CountAgentElement_ForQuery @Element", parameter).ToList();
            return res[0];
        }

        [HttpGet]
        public List<DoanhThuAgent> LayDoanhThuAgent(string agentCode)
        {
            object[] paremeter = 
            {
                new SqlParameter("@AgentCode", agentCode)
            };
            var res = db.Database.SqlQuery<DoanhThuAgent>("sp_LayDoanhThuAgent @AgentCode", paremeter).ToList();
            return res;
        }

        [HttpGet]
        public int CountFindFilter(string query)
        {
            var res = db.Database.SqlQuery<int>(query).ToList();
            return res[0];
        }
    }
}