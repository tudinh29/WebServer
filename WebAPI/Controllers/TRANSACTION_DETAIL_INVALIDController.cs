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
        //============================MASTER======================================
        [HttpGet]
        public List<Models.TransInvalidTiny> FindAllTransaction_Detail_Invalid()
        {
            var res = db.Database.SqlQuery<Models.TransInvalidTiny>("sp_FindAllTransaction_Detail_Invalid").ToList();
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
        public int CountTransInvalid_Master()
        {
            var res = db.Database.SqlQuery<int>("sp_CountTransInvalid_Master").ToList();
            return res[0];
        }
        [HttpGet]
        public List<Models.TransInvalidTiny> GetTransaction_Detail_Invalid_Master(int pageIndex, int pageSize)
        {
            object[] parameter = 
                {
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };
            var res = db.Database.SqlQuery<Models.TransInvalidTiny>("sp_GetTransaction_Detail_Invalid_Master @pageIndex, @pageSize", parameter).ToList();
            return res;
        }
        [HttpGet]
        public int sp_CountTransInvalidElements_Master()
        {
            var res = db.Database.SqlQuery<int>("sp_CountTransInvalidElements_Master").ToList();
            return res[0];
        }
        [HttpGet]
        public List<Models.TransInvalidTiny> FindTransInvalidElement_Master(string searchString, int pageIndex, int pageSize)
        {
            object[] parameter = 
                {
                    new SqlParameter("@Element", searchString),
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                   
                };
            var res = db.Database.SqlQuery<Models.TransInvalidTiny>("sp_FindTransInvalidElement_Master @Element, @pageIndex, @pageSize", parameter).ToList();
            return res;
        }
        //============================================================================
        //==========================AGENT=============================================
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
        public int CountTransInvalid_Agent(string AgentCode)
        {
            object[] parameter = 
                {
                    new SqlParameter("@AgentCode", AgentCode)
                };
            var res = db.Database.SqlQuery<int>("sp_CountTransInvalid_Agent @AgentCode", parameter).ToList();
            return res[0];
        }
        [HttpGet]
        public List<Models.TransInvalidTiny> GetTransaction_Detail_Invalid_Agent(string AgentCode, int pageIndex, int pageSize)
        {
            object[] parameter = 
                {
                    new SqlParameter("@AgentCode", AgentCode),
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };
            var res = db.Database.SqlQuery<Models.TransInvalidTiny>("sp_GetTransaction_Detail_Invalid_Agent @AgentCode, @pageIndex, @pageSize", parameter).ToList();
            return res;

        }
        [HttpGet]
        public int sp_CountTransInvalidElements_Agent(string searchString)
        {
            object[] parameter = 
                {
                    new SqlParameter("@Element", searchString)
                };
            var res = db.Database.SqlQuery<int>("sp_CountTransInvalidElements_Agent @Element", parameter).ToList();
            return res[0];
        }
        [HttpGet]
        public List<Models.TransInvalidTiny> FindTransInvalidElement_Agent(string searchString, string agentCode, int pageIndex, int pageSize)
        {
            object[] parameter = 
                {
                    new SqlParameter("@Element", searchString),
                    new SqlParameter("@AgentCode", agentCode),
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                   
                };
            var res = db.Database.SqlQuery<Models.TransInvalidTiny>("sp_FindTransInvalidElement_Agent @Element, @AgentCode, @pageIndex, @pageSize", parameter).ToList();
            return res;
        }
        //============================================================================
        //========================MERCHANT============================================
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
        public int CountTransInvalid_Merchant(string MerchantCode)
        {
            object[] parameter = 
                {
                    new SqlParameter("@MerchantCode", MerchantCode)
                };
            var res = db.Database.SqlQuery<int>("sp_CountTransInvalid_Merchant @MerchantCode", parameter).ToList();
            return res[0];
        }
        [HttpGet]
        public List<Models.TransInvalidTiny> GetTransaction_Detail_Invalid_Merchant(string MerchantCode, int pageIndex, int pageSize)
        {
            object[] parameter = 
                {
                    new SqlParameter("@MerchantCode", MerchantCode),
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };
            var res = db.Database.SqlQuery<Models.TransInvalidTiny>("sp_GetTransaction_Detail_Invalid_Merchant @MerchantCode, @pageIndex, @pageSize", parameter).ToList();
            return res;

        }

        [HttpGet]
        public int sp_CountTransInvalidElements_Merchant(string searchString)
        {
            object[] parameter = 
                {
                    new SqlParameter("@Element", searchString)
                };
            var res = db.Database.SqlQuery<int>("sp_CountTransInvalidElements_Merchant @Element", parameter).ToList();
            return res[0];
        }
        [HttpGet]
        public List<Models.TransInvalidTiny> FindTransInvalidElement_Merchant(string searchString, string merchantCode, int pageIndex, int pageSize)
        {
            object[] parameter = 
                {
                    new SqlParameter("@Element", searchString),
                    new SqlParameter("@MerchantCode", merchantCode),
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                   
                };
            var res = db.Database.SqlQuery<Models.TransInvalidTiny>("sp_FindTransInvalidElement_Merchant @Element, @MerchantCode, @pageIndex, @pageSize", parameter).ToList();
            return res;
        }
        //============================================================================
    }
}
