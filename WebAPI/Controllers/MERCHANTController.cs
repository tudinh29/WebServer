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
        public List<MERCHANT> FindMerchantElement(string searchString)
        {
            object[] parameter =
                {
                    new SqlParameter("@Element", searchString)
                };

            var res = db.Database.SqlQuery<MERCHANT>("exec sp_FindMerchantElement @Element", parameter).ToList();
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
        [HttpPost]
        public bool AddNewMerchant(MERCHANT merchant)
        {
            //m nhap merchant name vao
            try
            {
                object[] parameter = 
                {
                    new SqlParameter("@MerchantName",merchant.MerchantName),
                    new SqlParameter("@BackEndProcessor",merchant.BackEndProcessor),
                    new SqlParameter("@Status",merchant.Status??(object)DBNull.Value),
                    new SqlParameter("@Owner",merchant.Owner??(object)DBNull.Value),
                    new SqlParameter("@MerchantType",merchant.MerchantType??(object)DBNull.Value),
                    new SqlParameter("@Address1",merchant.Address1??(object)DBNull.Value),
                    new SqlParameter("@Address2",merchant.Address2??(object)DBNull.Value),
                    new SqlParameter("@Address3",merchant.Address3??(object)DBNull.Value),
                    new SqlParameter("@CityCode",merchant.CityCode),
                    new SqlParameter("@Zip",merchant.Zip??System.Data.SqlTypes.SqlInt32.Null),
                    new SqlParameter("@Phone",string.IsNullOrEmpty(merchant.Phone)?"":merchant.Phone),
                    new SqlParameter("@Fax",string.IsNullOrEmpty(merchant.Fax)?"":merchant.Fax),
                    new SqlParameter("@Email",string.IsNullOrEmpty(merchant.Fax)?"":merchant.Fax),
                    new SqlParameter("@ApprovalDate",merchant.ApprovalDate??(object)DBNull.Value),
                    new SqlParameter("@CloseDate",merchant.CloseDate??(object)DBNull.Value),
                    new SqlParameter("@BankCardDBA",merchant.BankCardDBA??(object)DBNull.Value),
                    new SqlParameter("@FirstActiveDate",merchant.FirstActiveDate??(object)DBNull.Value),
                    new SqlParameter("@LastActiveDate",merchant.LastActiveDate??(object)DBNull.Value),
                    new SqlParameter("@AgentCode",merchant.AgentCode)

                };
                string sql = "sp_AddNewMerchant @MerchantName,@BackEndProcessor ,@Status, @Owner, @MerchantType"
                    + ",@Address1,@Address2,@Address3,@CityCode,@Zip,@Phone,@Fax,@Email, @ApprovalDate,@CloseDate,@BankCardDBA,@FirstActiveDate"
                    + ",@LastActiveDate,@AgentCode";
                db.Database.ExecuteSqlCommand(sql, parameter);
                  
               //trong sql server t de null dc debug lại đi
                return true;
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.ToString());
                return false;
            }
        }

        [HttpPost]
        public bool UpdateMerchant(string id, MERCHANT merchant)
        {
            //m nhap merchant name vao
            try
            {
                object[] parameter = 
                {
                    new SqlParameter("@MerchantCode", merchant.MerchantCode),
                    new SqlParameter("@MerchantName",merchant.MerchantName),
                    new SqlParameter("@BackEndProcessor",merchant.BackEndProcessor),
                    new SqlParameter("@Status",merchant.Status??(object)DBNull.Value),
                    new SqlParameter("@Owner",merchant.Owner??(object)DBNull.Value),
                    new SqlParameter("@MerchantType",merchant.MerchantType??(object)DBNull.Value),
                    new SqlParameter("@Address1",merchant.Address1??(object)DBNull.Value),
                    new SqlParameter("@Address2",merchant.Address2??(object)DBNull.Value),
                    new SqlParameter("@Address3",merchant.Address3??(object)DBNull.Value),
                    new SqlParameter("@CityCode",merchant.CityCode),
                    new SqlParameter("@Zip",merchant.Zip??System.Data.SqlTypes.SqlInt32.Null),
                    new SqlParameter("@Phone",string.IsNullOrEmpty(merchant.Phone)?"":merchant.Phone),
                    new SqlParameter("@Fax",string.IsNullOrEmpty(merchant.Fax)?"":merchant.Fax),
                    new SqlParameter("@Email",string.IsNullOrEmpty(merchant.Fax)?"":merchant.Fax),
                    new SqlParameter("@ApprovalDate",merchant.ApprovalDate??(object)DBNull.Value),
                    new SqlParameter("@CloseDate",merchant.CloseDate??(object)DBNull.Value),
                    new SqlParameter("@BankCardDBA",merchant.BankCardDBA??(object)DBNull.Value),
                    new SqlParameter("@FirstActiveDate",merchant.FirstActiveDate??(object)DBNull.Value),
                    new SqlParameter("@LastActiveDate",merchant.LastActiveDate??(object)DBNull.Value),
                    new SqlParameter("@AgentCode",merchant.AgentCode)

                };
                string sql = "sp_Editmerchant @MerchantCode, @MerchantName,@BackEndProcessor ,@Status, @Owner, @MerchantType"
                    + ",@Address1,@Address2,@Address3,@CityCode,@Zip,@Phone,@Fax,@Email, @ApprovalDate,@CloseDate,@BankCardDBA,@FirstActiveDate"
                    + ",@LastActiveDate,@AgentCode";
                db.Database.ExecuteSqlCommand(sql, parameter);

                
                return true;
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.ToString());
                return false;
            }
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