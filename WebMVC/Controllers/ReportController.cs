using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebMVC.Common;
using WebMVC.EntityFramework;
using PagedList;
using Rotativa;

namespace WebMVC.Controllers
{
    public class ReportController : BaseController
    {
        // GET: Report
        public ActionResult Index()
        {
            List<MERCHANT_SUMMARY_DAILY> list = new List<MERCHANT_SUMMARY_DAILY>();
            HttpClient client = new AccessAPI().Access();
            HttpResponseMessage response = client.GetAsync(string.Format("api/MERCHANT_SUMMARY_DAILY/GetReportData")).Result;

            if (response.IsSuccessStatusCode)
            {
                list = response.Content.ReadAsAsync<List<MERCHANT_SUMMARY_DAILY>>().Result;
            }
            else return View("Index");

            var listMerchantType = getListMerchantType(list);
            var listRegion = getListRegion(list);
            var cardTypeReport = getCardTypeReport(list);
            ViewBag.listRegion = listRegion;
            ViewBag.listMerchantType = listMerchantType;
            ViewBag.listSummary = list;
            ViewBag.cardTypeReport = cardTypeReport;
            return View();
        }

        private List<MERCHANT_SUMMARY_DAILY> getListMerchantType(List<MERCHANT_SUMMARY_DAILY> list)
        {
            var res = list.GroupBy(a => a.MerchantType).Select(x => new MERCHANT_SUMMARY_DAILY
            {
                MerchantType = x.Key,
                SaleAmount = x.Sum(y => y.SaleAmount),
                SaleCount = x.Sum(y => y.SaleCount),
                ReturnAmount = x.Sum(y => y.ReturnAmount),
                ReturnCount = x.Sum(y => y.ReturnCount),
                NetAmount = x.Sum(y => y.NetAmount),
                TransactionCount = x.Sum(y => y.TransactionCount),
                KeyedAmount = x.Sum(y => y.KeyedAmount),
                KeyedCount = x.Sum(y => y.KeyedCount),
                KeyedReturnAmount = x.Sum(y => y.KeyedReturnAmount),
                KeyedReturnCount = x.Sum(y => y.KeyedReturnCount),
                KeyedNetAmount = x.Sum(y => y.KeyedNetAmount),
                KeyedTransactionCount = x.Sum(y => y.KeyedTransactionCount),
                ForeignCardAmount = x.Sum(y => y.ForeignCardAmount),
                ForeignCardCount = x.Sum(y => y.ForeignCardCount),
                ForeignCardReturnAmount = x.Sum(y => y.ForeignCardReturnAmount),
                ForeignCardReturnCount = x.Sum(y => y.ForeignCardReturnCount),
                ForeignCardNetAmount = x.Sum(y => y.ForeignCardNetAmount),
                ForeignCardTransactionCount = x.Sum(y => y.ForeignCardTransactionCount),
                DebitCardAmount = x.Sum(y => y.DebitCardAmount),
                DebitCardCount = x.Sum(y => y.DebitCardCount),
                DebitCardReturnAmount = x.Sum(y => y.DebitCardReturnAmount),
                DebitCardReturnCount = x.Sum(y => y.DebitCardReturnCount),
                DebitCardNetAmount = x.Sum(y => y.DebitCardNetAmount),
                DebitCardTransactionCount = x.Sum(y => y.DebitCardTransactionCount),
                VisaCardAmount = x.Sum(y => y.VisaCardAmount),
                VisaCardCount = x.Sum(y => y.VisaCardCount),
                VisaCardReturnAmount = x.Sum(y => y.VisaCardReturnAmount),
                VisaCardReturnCount = x.Sum(y => y.VisaCardReturnCount),
                VisaCardNetAmount = x.Sum(y => y.VisaCardNetAmount),
                VisaCardTransactionCount = x.Sum(y => y.VisaCardTransactionCount),
                DiscoverCardAmount = x.Sum(y => y.DiscoverCardAmount),
                DiscoverCardCount = x.Sum(y => y.DiscoverCardCount),
                DiscoverCardReturnAmount = x.Sum(y => y.DiscoverCardReturnAmount),
                DiscoverCardReturnCount = x.Sum(y => y.DiscoverCardReturnCount),
                DiscoverCardNetAmount = x.Sum(y => y.DiscoverCardNetAmount),
                DiscoverCardTransactionCount = x.Sum(y => y.DiscoverCardTransactionCount),
                MasterCardAmount = x.Sum(y => y.MasterCardAmount),
                MasterCardCount = x.Sum(y => y.MasterCardCount),
                MasterCardReturnAmount = x.Sum(y => y.MasterCardReturnAmount),
                MasterCardReturnCount = x.Sum(y => y.MasterCardReturnCount),
                MasterCardNetAmount = x.Sum(y => y.MasterCardNetAmount),
                MasterCardTransactionCount = x.Sum(y => y.MasterCardTransactionCount),
                AmericanExpressAmount = x.Sum(y => y.AmericanExpressAmount),
                AmericanExpressCount = x.Sum(y => y.AmericanExpressCount),
                AmericanExpressReturnAmount = x.Sum(y => y.AmericanExpressReturnAmount),
                AmericanExpressReturnCount = x.Sum(y => y.AmericanExpressReturnCount),
                AmericanExpressNetAmount = x.Sum(y => y.AmericanExpressNetAmount),
                AmericanExpressTransactionCount = x.Sum(y => y.AmericanExpressTransactionCount),
                OtherCardAmount = x.Sum(y => y.OtherCardAmount),
                OtherCardCount = x.Sum(y => y.OtherCardCount),
                OtherCardReturnAmount = x.Sum(y => y.OtherCardReturnAmount),
                OtherCardReturnCount = x.Sum(y => y.OtherCardReturnCount),
                OtherCardNetAmount = x.Sum(y => y.OtherCardNetAmount),
                OtherCardTransactionCount = x.Sum(y => y.OtherCardTransactionCount),
            }).ToList();
            return res;
        }


        private List<MERCHANT_SUMMARY_DAILY> getListRegion(List<MERCHANT_SUMMARY_DAILY> list)
        {
            var res = list.GroupBy(a => a.RegionCode).Select(x => new MERCHANT_SUMMARY_DAILY
            {
                RegionCode = x.Key,
                SaleAmount = x.Sum(y => y.SaleAmount),
                SaleCount = x.Sum(y => y.SaleCount),
                ReturnAmount = x.Sum(y => y.ReturnAmount),
                ReturnCount = x.Sum(y => y.ReturnCount),
                NetAmount = x.Sum(y => y.NetAmount),
                TransactionCount = x.Sum(y => y.TransactionCount),
                KeyedAmount = x.Sum(y => y.KeyedAmount),
                KeyedCount = x.Sum(y => y.KeyedCount),
                KeyedReturnAmount = x.Sum(y => y.KeyedReturnAmount),
                KeyedReturnCount = x.Sum(y => y.KeyedReturnCount),
                KeyedNetAmount = x.Sum(y => y.KeyedNetAmount),
                KeyedTransactionCount = x.Sum(y => y.KeyedTransactionCount),
                ForeignCardAmount = x.Sum(y => y.ForeignCardAmount),
                ForeignCardCount = x.Sum(y => y.ForeignCardCount),
                ForeignCardReturnAmount = x.Sum(y => y.ForeignCardReturnAmount),
                ForeignCardReturnCount = x.Sum(y => y.ForeignCardReturnCount),
                ForeignCardNetAmount = x.Sum(y => y.ForeignCardNetAmount),
                ForeignCardTransactionCount = x.Sum(y => y.ForeignCardTransactionCount),
                DebitCardAmount = x.Sum(y => y.DebitCardAmount),
                DebitCardCount = x.Sum(y => y.DebitCardCount),
                DebitCardReturnAmount = x.Sum(y => y.DebitCardReturnAmount),
                DebitCardReturnCount = x.Sum(y => y.DebitCardReturnCount),
                DebitCardNetAmount = x.Sum(y => y.DebitCardNetAmount),
                DebitCardTransactionCount = x.Sum(y => y.DebitCardTransactionCount),
                VisaCardAmount = x.Sum(y => y.VisaCardAmount),
                VisaCardCount = x.Sum(y => y.VisaCardCount),
                VisaCardReturnAmount = x.Sum(y => y.VisaCardReturnAmount),
                VisaCardReturnCount = x.Sum(y => y.VisaCardReturnCount),
                VisaCardNetAmount = x.Sum(y => y.VisaCardNetAmount),
                VisaCardTransactionCount = x.Sum(y => y.VisaCardTransactionCount),
                DiscoverCardAmount = x.Sum(y => y.DiscoverCardAmount),
                DiscoverCardCount = x.Sum(y => y.DiscoverCardCount),
                DiscoverCardReturnAmount = x.Sum(y => y.DiscoverCardReturnAmount),
                DiscoverCardReturnCount = x.Sum(y => y.DiscoverCardReturnCount),
                DiscoverCardNetAmount = x.Sum(y => y.DiscoverCardNetAmount),
                DiscoverCardTransactionCount = x.Sum(y => y.DiscoverCardTransactionCount),
                MasterCardAmount = x.Sum(y => y.MasterCardAmount),
                MasterCardCount = x.Sum(y => y.MasterCardCount),
                MasterCardReturnAmount = x.Sum(y => y.MasterCardReturnAmount),
                MasterCardReturnCount = x.Sum(y => y.MasterCardReturnCount),
                MasterCardNetAmount = x.Sum(y => y.MasterCardNetAmount),
                MasterCardTransactionCount = x.Sum(y => y.MasterCardTransactionCount),
                AmericanExpressAmount = x.Sum(y => y.AmericanExpressAmount),
                AmericanExpressCount = x.Sum(y => y.AmericanExpressCount),
                AmericanExpressReturnAmount = x.Sum(y => y.AmericanExpressReturnAmount),
                AmericanExpressReturnCount = x.Sum(y => y.AmericanExpressReturnCount),
                AmericanExpressNetAmount = x.Sum(y => y.AmericanExpressNetAmount),
                AmericanExpressTransactionCount = x.Sum(y => y.AmericanExpressTransactionCount),
                OtherCardAmount = x.Sum(y => y.OtherCardAmount),
                OtherCardCount = x.Sum(y => y.OtherCardCount),
                OtherCardReturnAmount = x.Sum(y => y.OtherCardReturnAmount),
                OtherCardReturnCount = x.Sum(y => y.OtherCardReturnCount),
                OtherCardNetAmount = x.Sum(y => y.OtherCardNetAmount),
                OtherCardTransactionCount = x.Sum(y => y.OtherCardTransactionCount),
            }).ToList();
            return res;
        }


        private List<MERCHANT_SUMMARY_DAILY> getCardTypeReport(List<MERCHANT_SUMMARY_DAILY> list)
        {
            var res = list.GroupBy(a => a.ReportDate).Select(x => new MERCHANT_SUMMARY_DAILY
            {
                ReportDate = x.Key,
                SaleAmount = x.Sum(y => y.SaleAmount),
                SaleCount = x.Sum(y => y.SaleCount),
                ReturnAmount = x.Sum(y => y.ReturnAmount),
                ReturnCount = x.Sum(y => y.ReturnCount),
                NetAmount = x.Sum(y => y.NetAmount),
                TransactionCount = x.Sum(y => y.TransactionCount),
                KeyedAmount = x.Sum(y => y.KeyedAmount),
                KeyedCount = x.Sum(y => y.KeyedCount),
                KeyedReturnAmount = x.Sum(y => y.KeyedReturnAmount),
                KeyedReturnCount = x.Sum(y => y.KeyedReturnCount),
                KeyedNetAmount = x.Sum(y => y.KeyedNetAmount),
                KeyedTransactionCount = x.Sum(y => y.KeyedTransactionCount),
                ForeignCardAmount = x.Sum(y => y.ForeignCardAmount),
                ForeignCardCount = x.Sum(y => y.ForeignCardCount),
                ForeignCardReturnAmount = x.Sum(y => y.ForeignCardReturnAmount),
                ForeignCardReturnCount = x.Sum(y => y.ForeignCardReturnCount),
                ForeignCardNetAmount = x.Sum(y => y.ForeignCardNetAmount),
                ForeignCardTransactionCount = x.Sum(y => y.ForeignCardTransactionCount),
                DebitCardAmount = x.Sum(y => y.DebitCardAmount),
                DebitCardCount = x.Sum(y => y.DebitCardCount),
                DebitCardReturnAmount = x.Sum(y => y.DebitCardReturnAmount),
                DebitCardReturnCount = x.Sum(y => y.DebitCardReturnCount),
                DebitCardNetAmount = x.Sum(y => y.DebitCardNetAmount),
                DebitCardTransactionCount = x.Sum(y => y.DebitCardTransactionCount),
                VisaCardAmount = x.Sum(y => y.VisaCardAmount),
                VisaCardCount = x.Sum(y => y.VisaCardCount),
                VisaCardReturnAmount = x.Sum(y => y.VisaCardReturnAmount),
                VisaCardReturnCount = x.Sum(y => y.VisaCardReturnCount),
                VisaCardNetAmount = x.Sum(y => y.VisaCardNetAmount),
                VisaCardTransactionCount = x.Sum(y => y.VisaCardTransactionCount),
                DiscoverCardAmount = x.Sum(y => y.DiscoverCardAmount),
                DiscoverCardCount = x.Sum(y => y.DiscoverCardCount),
                DiscoverCardReturnAmount = x.Sum(y => y.DiscoverCardReturnAmount),
                DiscoverCardReturnCount = x.Sum(y => y.DiscoverCardReturnCount),
                DiscoverCardNetAmount = x.Sum(y => y.DiscoverCardNetAmount),
                DiscoverCardTransactionCount = x.Sum(y => y.DiscoverCardTransactionCount),
                MasterCardAmount = x.Sum(y => y.MasterCardAmount),
                MasterCardCount = x.Sum(y => y.MasterCardCount),
                MasterCardReturnAmount = x.Sum(y => y.MasterCardReturnAmount),
                MasterCardReturnCount = x.Sum(y => y.MasterCardReturnCount),
                MasterCardNetAmount = x.Sum(y => y.MasterCardNetAmount),
                MasterCardTransactionCount = x.Sum(y => y.MasterCardTransactionCount),
                AmericanExpressAmount = x.Sum(y => y.AmericanExpressAmount),
                AmericanExpressCount = x.Sum(y => y.AmericanExpressCount),
                AmericanExpressReturnAmount = x.Sum(y => y.AmericanExpressReturnAmount),
                AmericanExpressReturnCount = x.Sum(y => y.AmericanExpressReturnCount),
                AmericanExpressNetAmount = x.Sum(y => y.AmericanExpressNetAmount),
                AmericanExpressTransactionCount = x.Sum(y => y.AmericanExpressTransactionCount),
                OtherCardAmount = x.Sum(y => y.OtherCardAmount),
                OtherCardCount = x.Sum(y => y.OtherCardCount),
                OtherCardReturnAmount = x.Sum(y => y.OtherCardReturnAmount),
                OtherCardReturnCount = x.Sum(y => y.OtherCardReturnCount),
                OtherCardNetAmount = x.Sum(y => y.OtherCardNetAmount),
                OtherCardTransactionCount = x.Sum(y => y.OtherCardTransactionCount),
            }).ToList();
            return res;
        }
    }
}