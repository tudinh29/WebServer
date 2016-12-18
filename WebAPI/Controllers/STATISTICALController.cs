using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.Script.Serialization;
using WebAPI.EntityFramework;
using WebAPI.Models;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    public class STATISTICALController : ApiController
    {
        //
        // GET: /Statistical/
        private APIDbContext db = new APIDbContext();

        //----YEARLY----
        [HttpGet]
        public List<MERCHANT_SUMMARY_YEARLY> FindAllMerchantSummaryYearly(int pageIndex, int pageSize)
        {
            object[] paremeter = 
                {
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };
            var res = db.Database.SqlQuery<MERCHANT_SUMMARY_YEARLY>("sp_FindAllMerchantSummaryYearly_ForQuery @pageIndex, @pageSize", paremeter).ToList();
            return res;
        }

        [HttpGet]
        public List<MERCHANT_SUMMARY_YEARLY> FindMerchantSummaryYearlyElement(string searchString, int pageIndex, int pageSize)
        {
            object[] parameter =
                {
                    new SqlParameter("@Element", searchString),
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };

            var res = db.Database.SqlQuery<MERCHANT_SUMMARY_YEARLY>("exec sp_FindMerchantSummaryYearlyElement_ForQuery @Element, @pageIndex, @pageSize", parameter).ToList();
            return res;
        }
        [HttpGet]
        public int CountMerchantSummaryYearlyElement(string searchString)
        {
            object[] parameter =
                {
                    new SqlParameter("@Element", searchString)
                };
            var res = db.Database.SqlQuery<int>("sp_CountMerchantSummaryYearlyElement @Element", parameter).ToList();
            return res[0];
        }
        [HttpGet]
        public int CountMerchantSummaryYearly()
        {
            var res = db.Database.SqlQuery<int>("sp_CountMerchantSummaryYearly").ToList();
            return res[0];
        }

        [HttpGet]
        public MERCHANT_SUMMARY_YEARLY FindMerchantSummaryYearly(string ReportYear, string MerchantCode)
        {
            object[] paremeter = 
                {
                    new SqlParameter("@ReportYear", ReportYear),
                    new SqlParameter("@MerchantCode", MerchantCode)
                };
            MERCHANT_SUMMARY_YEARLY res = db.Database.SqlQuery<MERCHANT_SUMMARY_YEARLY>("sp_GetMerchantSummaryYearly @ReportYear, @MerchantCode", paremeter).FirstOrDefault();
            return res;
        }
        //AGENT
        [HttpGet]
        public List<MERCHANT_SUMMARY_YEARLY> FindAllMerchantSummaryYearly_Agent(int pageIndex, int pageSize, string AgentCode)
        {
            object[] paremeter = 
                {
                    new SqlParameter("@AgentCode", AgentCode),
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };
            var res = db.Database.SqlQuery<MERCHANT_SUMMARY_YEARLY>("sp_FindAllMerchantSummaryYearly_Agent_ForQuery @AgentCode, @pageIndex, @pageSize", paremeter).ToList();
            return res;
        }

        [HttpGet]
        public List<MERCHANT_SUMMARY_YEARLY> FindMerchantSummaryYearlyElement_Agent(string searchString, int pageIndex, int pageSize, string AgentCode)
        {
            object[] parameter =
                {
                    new SqlParameter("@AgentCode", AgentCode),
                    new SqlParameter("@Element", searchString),
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };

            var res = db.Database.SqlQuery<MERCHANT_SUMMARY_YEARLY>("exec sp_FindMerchantSummaryYearlyElement_Agent_ForQuery @AgentCode, @Element, @pageIndex, @pageSize", parameter).ToList();
            return res;
        }
        [HttpGet]
        public int CountMerchantSummaryYearlyElement_Agent(string searchString, string AgentCode)
        {
            object[] parameter =
                {
                    new SqlParameter("@AgentCode", AgentCode),
                    new SqlParameter("@Element", searchString)
                };
            var res = db.Database.SqlQuery<int>("sp_CountMerchantSummaryYearlyElement_Agent @AgentCode, @Element", parameter).ToList();
            return res[0];
        }
        [HttpGet]
        public int CountMerchantSummaryYearly_Agent(string AgentCode)
        {
            object[] parameter =
                {
                    new SqlParameter("@AgentCode", AgentCode)
                };
            var res = db.Database.SqlQuery<int>("sp_CountMerchantSummaryYearly_Agent @AgentCode", parameter).ToList();
            return res[0];
        }
        //MERCHANT
        [HttpGet]
        public List<MERCHANT_SUMMARY_YEARLY> FindAllMerchantSummaryYearly_Merchant(int pageIndex, int pageSize, string MerchantCode)
        {
            object[] paremeter = 
                {
                    new SqlParameter("@MerchantCode", MerchantCode),
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };
            var res = db.Database.SqlQuery<MERCHANT_SUMMARY_YEARLY>("sp_FindAllMerchantSummaryYearly_Merchant_ForQuery @MerchantCode, @pageIndex, @pageSize", paremeter).ToList();
            return res;
        }

        [HttpGet]
        public List<MERCHANT_SUMMARY_YEARLY> FindMerchantSummaryYearlyElement_Merchant(string searchString, int pageIndex, int pageSize, string MerchantCode)
        {
            object[] parameter =
                {
                    new SqlParameter("@MerchantCode", MerchantCode),
                    new SqlParameter("@Element", searchString),
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };

            var res = db.Database.SqlQuery<MERCHANT_SUMMARY_YEARLY>("exec sp_FindMerchantSummaryYearlyElement_Merchant_ForQuery @MerchantCode, @Element, @pageIndex, @pageSize", parameter).ToList();
            return res;
        }
        [HttpGet]
        public int CountMerchantSummaryYearlyElement_Merchant(string searchString, string MerchantCode)
        {
            object[] parameter =
                {
                    new SqlParameter("@MerchantCode", MerchantCode),
                    new SqlParameter("@Element", searchString)
                };
            var res = db.Database.SqlQuery<int>("sp_CountMerchantSummaryYearlyElement_Merchant @MerchantCode, @Element", parameter).ToList();
            return res[0];
        }
        [HttpGet]
        public int CountMerchantSummaryYearly_Merchant(string MerchantCode)
        {
            object[] parameter =
                {
                    new SqlParameter("@MerchantCode", MerchantCode)
                };
            var res = db.Database.SqlQuery<int>("sp_CountMerchantSummaryYearly_Merchant @MerchantCode", parameter).ToList();
            return res[0];
        }
        //----MONTHLY----
        [HttpGet]
        public List<MERCHANT_SUMMARY_MONTHLY> FindAllMerchantSummaryMonthly(int pageIndex, int pageSize)
        {
            object[] paremeter = 
                {
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };
            var res = db.Database.SqlQuery<MERCHANT_SUMMARY_MONTHLY>("sp_FindAllMerchantSummaryMonthly_ForQuery @pageIndex, @pageSize", paremeter).ToList();
            return res;
        }

        [HttpGet]
        public List<MERCHANT_SUMMARY_MONTHLY> FindMerchantSummaryMonthlyElement(string searchString, int pageIndex, int pageSize)
        {
            object[] parameter =
                {
                    new SqlParameter("@Element", searchString),
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };

            var res = db.Database.SqlQuery<MERCHANT_SUMMARY_MONTHLY>("exec sp_FindMerchantSummaryMonthlyElement_ForQuery @Element, @pageIndex, @pageSize", parameter).ToList();
            return res;
        }
        [HttpGet]
        public int CountMerchantSummaryMonthlyElement(string searchString)
        {
            object[] parameter =
                {
                    new SqlParameter("@Element", searchString)
                };
            var res = db.Database.SqlQuery<int>("sp_CountMerchantSummaryMonthlyElement @Element", parameter).ToList();
            return res[0];
        }
        [HttpGet]
        public int CountMerchantSummaryMonthly()
        {
            var res = db.Database.SqlQuery<int>("sp_CountMerchantSummaryMonthly").ToList();
            return res[0];
        }

        [HttpGet]
        public MERCHANT_SUMMARY_MONTHLY FindMerchantSummaryMonthly(int ReportMonth, string ReportYear, string MerchantCode)
        {
            object[] paremeter = 
                {
					new SqlParameter("@ReportMonth", ReportMonth),
                    new SqlParameter("@ReportYear", ReportYear),
                    new SqlParameter("@MerchantCode", MerchantCode)
                };
            MERCHANT_SUMMARY_MONTHLY res = db.Database.SqlQuery<MERCHANT_SUMMARY_MONTHLY>("sp_GetMerchantSummaryMonthly @ReportMonth, @ReportYear, @MerchantCode", paremeter).FirstOrDefault();
            return res;
        }
        //AGENT
        [HttpGet]
        public List<MERCHANT_SUMMARY_MONTHLY> FindAllMerchantSummaryMonthly_Agent(int pageIndex, int pageSize, string AgentCode)
        {
            object[] paremeter = 
                {
                    new SqlParameter("@AgentCode", AgentCode),
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };
            var res = db.Database.SqlQuery<MERCHANT_SUMMARY_MONTHLY>("sp_FindAllMerchantSummaryMonthly_Agent_ForQuery @AgentCode, @pageIndex, @pageSize", paremeter).ToList();
            return res;
        }

        [HttpGet]
        public List<MERCHANT_SUMMARY_MONTHLY> FindMerchantSummaryMonthlyElement_Agent(string searchString, int pageIndex, int pageSize, string AgentCode)
        {
            object[] parameter =
                {
                    new SqlParameter("@AgentCode", AgentCode),
                    new SqlParameter("@Element", searchString),
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };

            var res = db.Database.SqlQuery<MERCHANT_SUMMARY_MONTHLY>("exec sp_FindMerchantSummaryMonthlyElement_Agent_ForQuery @AgentCode, @Element, @pageIndex, @pageSize", parameter).ToList();
            return res;
        }
        [HttpGet]
        public int CountMerchantSummaryMonthlyElement_Agent(string searchString, string AgentCode)
        {
            object[] parameter =
                {
                    new SqlParameter("@AgentCode", AgentCode),
                    new SqlParameter("@Element", searchString)
                };
            var res = db.Database.SqlQuery<int>("sp_CountMerchantSummaryMonthlyElement_Agent @AgentCode, @Element", parameter).ToList();
            return res[0];
        }
        [HttpGet]
        public int CountMerchantSummaryMonthly_Agent(string AgentCode)
        {
            object[] parameter =
                {
                    new SqlParameter("@AgentCode", AgentCode)
                };
            var res = db.Database.SqlQuery<int>("sp_CountMerchantSummaryMonthly_Agent @AgentCode", parameter).ToList();
            return res[0];
        }
        //MERCHANT
        [HttpGet]
        public List<MERCHANT_SUMMARY_MONTHLY> FindAllMerchantSummaryMonthly_Merchant(int pageIndex, int pageSize, string MerchantCode)
        {
            object[] paremeter = 
                {
                    new SqlParameter("@MerchantCode", MerchantCode),
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };
            var res = db.Database.SqlQuery<MERCHANT_SUMMARY_MONTHLY>("sp_FindAllMerchantSummaryMonthly_Merchant_ForQuery @MerchantCode, @pageIndex, @pageSize", paremeter).ToList();
            return res;
        }

        [HttpGet]
        public List<MERCHANT_SUMMARY_MONTHLY> FindMerchantSummaryMonthlyElement_Merchant(string searchString, int pageIndex, int pageSize, string MerchantCode)
        {
            object[] parameter =
                {
                    new SqlParameter("@MerchantCode", MerchantCode),
                    new SqlParameter("@Element", searchString),
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };

            var res = db.Database.SqlQuery<MERCHANT_SUMMARY_MONTHLY>("exec sp_FindMerchantSummaryMonthlyElement_Merchant_ForQuery @MerchantCode, @Element, @pageIndex, @pageSize", parameter).ToList();
            return res;
        }
        [HttpGet]
        public int CountMerchantSummaryMonthlyElement_Merchant(string searchString, string MerchantCode)
        {
            object[] parameter =
                {
                    new SqlParameter("@MerchantCode", MerchantCode),
                    new SqlParameter("@Element", searchString)
                };
            var res = db.Database.SqlQuery<int>("sp_CountMerchantSummaryMonthlyElement_Merchant @MerchantCode, @Element", parameter).ToList();
            return res[0];
        }
        [HttpGet]
        public int CountMerchantSummaryMonthly_Merchant(string MerchantCode)
        {
            object[] parameter =
                {
                    new SqlParameter("@MerchantCode", MerchantCode)
                };
            var res = db.Database.SqlQuery<int>("sp_CountMerchantSummaryMonthly_Merchant @MerchantCode", parameter).ToList();
            return res[0];
        }
        //----QUARTERLY----
        [HttpGet]
        public List<MERCHANT_SUMMARY_QUARTERLY> FindAllMerchantSummaryQuarterly(int pageIndex, int pageSize)
        {
            object[] paremeter = 
                {
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };
            var res = db.Database.SqlQuery<MERCHANT_SUMMARY_QUARTERLY>("sp_FindAllMerchantSummaryQuarterly_ForQuery @pageIndex, @pageSize", paremeter).ToList();
            return res;
        }

        [HttpGet]
        public List<MERCHANT_SUMMARY_QUARTERLY> FindMerchantSummaryQuarterlyElement(string searchString, int pageIndex, int pageSize)
        {
            object[] parameter =
                {
                    new SqlParameter("@Element", searchString),
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };

            var res = db.Database.SqlQuery<MERCHANT_SUMMARY_QUARTERLY>("exec sp_FindMerchantSummaryQuarterlyElement_ForQuery @Element, @pageIndex, @pageSize", parameter).ToList();
            return res;
        }
        [HttpGet]
        public int CountMerchantSummaryQuarterlyElement(string searchString)
        {
            object[] parameter =
                {
                    new SqlParameter("@Element", searchString)
                };
            var res = db.Database.SqlQuery<int>("sp_CountMerchantSummaryQuarterlyElement @Element", parameter).ToList();
            return res[0];
        }
        [HttpGet]
        public int CountMerchantSummaryQuarterly()
        {
            var res = db.Database.SqlQuery<int>("sp_CountMerchantSummaryQuarterly").ToList();
            return res[0];
        }

        [HttpGet]
        public MERCHANT_SUMMARY_QUARTERLY FindMerchantSummaryQuarterly(int ReportQuarter, string ReportYear, string MerchantCode)
        {
            object[] paremeter = 
                {
					new SqlParameter("@ReportQuarter", ReportQuarter),
                    new SqlParameter("@ReportYear", ReportYear),
                    new SqlParameter("@MerchantCode", MerchantCode)
                };
            MERCHANT_SUMMARY_QUARTERLY res = db.Database.SqlQuery<MERCHANT_SUMMARY_QUARTERLY>("sp_GetMerchantSummaryQuarterly @ReportQuarter, @ReportYear, @MerchantCode", paremeter).FirstOrDefault();
            return res;
        }
        //AGENT
        [HttpGet]
        public List<MERCHANT_SUMMARY_QUARTERLY> FindAllMerchantSummaryQuarterly_Agent(int pageIndex, int pageSize, string AgentCode)
        {
            object[] paremeter = 
                {
                    new SqlParameter("@AgentCode", AgentCode),
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };
            var res = db.Database.SqlQuery<MERCHANT_SUMMARY_QUARTERLY>("sp_FindAllMerchantSummaryQuarterly_Agent_ForQuery @AgentCode, @pageIndex, @pageSize", paremeter).ToList();
            return res;
        }

        [HttpGet]
        public List<MERCHANT_SUMMARY_QUARTERLY> FindMerchantSummaryQuarterlyElement_Agent(string searchString, int pageIndex, int pageSize, string AgentCode)
        {
            object[] parameter =
                {
                    new SqlParameter("@AgentCode", AgentCode),
                    new SqlParameter("@Element", searchString),
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };

            var res = db.Database.SqlQuery<MERCHANT_SUMMARY_QUARTERLY>("exec sp_FindMerchantSummaryQuarterlyElement_Agent_ForQuery @AgentCode, @Element, @pageIndex, @pageSize", parameter).ToList();
            return res;
        }
        [HttpGet]
        public int CountMerchantSummaryQuarterlyElement_Agent(string searchString, string AgentCode)
        {
            object[] parameter =
                {
                    new SqlParameter("@AgentCode", AgentCode),
                    new SqlParameter("@Element", searchString)
                };
            var res = db.Database.SqlQuery<int>("sp_CountMerchantSummaryQuarterlyElement_Agent @AgentCode, @Element", parameter).ToList();
            return res[0];
        }
        [HttpGet]
        public int CountMerchantSummaryQuarterly_Agent(string AgentCode)
        {
            object[] parameter =
                {
                    new SqlParameter("@AgentCode", AgentCode)
                };
            var res = db.Database.SqlQuery<int>("sp_CountMerchantSummaryQuarterly_Agent @AgentCode", parameter).ToList();
            return res[0];
        }
        //MERCHANT
        [HttpGet]
        public List<MERCHANT_SUMMARY_QUARTERLY> FindAllMerchantSummaryQuarterly_Merchant(int pageIndex, int pageSize, string MerchantCode)
        {
            object[] paremeter = 
                {
                    new SqlParameter("@MerchantCode", MerchantCode),
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };
            var res = db.Database.SqlQuery<MERCHANT_SUMMARY_QUARTERLY>("sp_FindAllMerchantSummaryQuarterly_Merchant_ForQuery @MerchantCode, @pageIndex, @pageSize", paremeter).ToList();
            return res;
        }

        [HttpGet]
        public List<MERCHANT_SUMMARY_QUARTERLY> FindMerchantSummaryQuarterlyElement_Merchant(string searchString, int pageIndex, int pageSize, string MerchantCode)
        {
            object[] parameter =
                {
                    new SqlParameter("@MerchantCode", MerchantCode),
                    new SqlParameter("@Element", searchString),
                    new SqlParameter("@pageIndex", pageIndex),
                    new SqlParameter("@pageSize", pageSize)
                };

            var res = db.Database.SqlQuery<MERCHANT_SUMMARY_QUARTERLY>("exec sp_FindMerchantSummaryQuarterlyElement_Merchant_ForQuery @MerchantCode, @Element, @pageIndex, @pageSize", parameter).ToList();
            return res;
        }
        [HttpGet]
        public int CountMerchantSummaryQuarterlyElement_Merchant(string searchString, string MerchantCode)
        {
            object[] parameter =
                {
                    new SqlParameter("@MerchantCode", MerchantCode),
                    new SqlParameter("@Element", searchString)
                };
            var res = db.Database.SqlQuery<int>("sp_CountMerchantSummaryQuarterlyElement_Merchant @MerchantCode, @Element", parameter).ToList();
            return res[0];
        }
        [HttpGet]
        public int CountMerchantSummaryQuarterly_Merchant(string MerchantCode)
        {
            object[] parameter =
                {
                    new SqlParameter("@MerchantCode", MerchantCode)
                };
            var res = db.Database.SqlQuery<int>("sp_CountMerchantSummaryQuarterly_Merchant @MerchantCode", parameter).ToList();
            return res[0];
        }

        [HttpGet]
        public List<MERCHANT_SUMMARY_YEARLY> FindFilterYearly(string query)
        {
            var res = db.Database.SqlQuery<MERCHANT_SUMMARY_YEARLY>(query).ToList();
            return res;
        }

        [HttpGet]
        public List<MERCHANT_SUMMARY_MONTHLY> FindFilterMonthly(string query)
        {
            var res = db.Database.SqlQuery<MERCHANT_SUMMARY_MONTHLY>(query).ToList();
            return res;
        }

        [HttpGet]
        public List<MERCHANT_SUMMARY_QUARTERLY> FindFilterQuarterly(string query)
        {
            var res = db.Database.SqlQuery<MERCHANT_SUMMARY_QUARTERLY>(query).ToList();
            return res;
        }

        [HttpGet]
        public int CountFilter(string query)
        {
            var res = db.Database.SqlQuery<int>(query).ToList();
            return res[0];
        }
	}
}