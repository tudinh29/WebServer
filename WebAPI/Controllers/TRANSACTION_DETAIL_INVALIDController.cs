using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.EntityFramework;
using System.Data.SqlClient;

namespace WebAPI.Controllers
{
    public class TRANSACTION_DETAIL_INVALIDController : ApiController
    {
        private APIDbContext db = new APIDbContext();
        [HttpGet]
        public List<Models.TransInvalidTiny> FindAllTransaction_Detail_Invalid()
        {
            var res = db.Database.SqlQuery<Models.TransInvalidTiny>("sp_FindAllTransaction_Detail_Invalid").ToList();
            return res;
        }

        [HttpGet]
        public List<Models.TransInvalidTiny> FindTransInvalid_Agent(string AgentCode)
        {
            object[] parameter = 
                {
                    new SqlParameter("@AgentCode", AgentCode)
                };
            var res = db.Database.SqlQuery<Models.TransInvalidTiny>("sp_TransInvalid_Agent @AgentCode", parameter).ToList();
            return res;
            
        }
        [HttpGet]
        public List<Models.TransInvalidTiny> FindTransInvalid_Merchant(string MerchantCode)
        {
            object[] parameter = 
                {
                    new SqlParameter("@MerchantCode", MerchantCode)
                };
            var res = db.Database.SqlQuery<Models.TransInvalidTiny>("sp_TransInvalid_Merchant @MerchantCode", parameter).ToList();
            return res;
            
        }
     [HttpGet]
        public List<Models.TransInvalidTiny> FindTransInvalidElement(string searchString)
        {
            object[] parameter = 
                {
                    new SqlParameter("@Element", searchString)
                   
                };
            var res = db.Database.SqlQuery<Models.TransInvalidTiny>("sp_FindTransInvalidElement @Element", parameter).ToList();
            return res;
        }
        [HttpGet]
        public List<Models.TransInvalidTiny> FindTransInvalidElement_Agent(string searchString, string agentCode)
        {
            object[] parameter = 
                {
                    new SqlParameter("@Element", searchString),
                    new SqlParameter("@AgentCode", agentCode)
                   
                };
            var res = db.Database.SqlQuery<Models.TransInvalidTiny>("sp_FindTransInvalidElement_Agent @Element, @AgentCode", parameter).ToList();
            return res;
        }
        [HttpGet]
        public List<Models.TransInvalidTiny> FindTransInvalidElement_Merchant(string searchString, string merchantCode)
        {
            object[] parameter = 
                {
                    new SqlParameter("@Element", searchString),
                    new SqlParameter("@MerchantCode", merchantCode)
                   
                };
            var res = db.Database.SqlQuery<Models.TransInvalidTiny>("sp_FindTransInvalidElement_Merchant @Element, @MerchantCode", parameter).ToList();
            return res;
        }
    }
}
