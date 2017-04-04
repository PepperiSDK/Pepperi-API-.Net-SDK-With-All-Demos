using Pepperi.SDK;
using Pepperi.SDK.Contracts;
using Pepperi.SDK.Model;
using Pepperi.Test.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pepperi.Test.MVC.Controllers
{

    public class TransactionController : Controller
    {
        #region Properties
        #endregion

        #region Constructor
        #endregion

        // GET: /Transactions/
        public ActionResult Transactions(int page=1, int page_size=10, DateTime? FromDate=null, DateTime? ToDate=null)
        {
            try
            {
                if (FromDate == null)
                {
                    //this is the first time the page is displayed
                    FromDate = DateTime.Now.AddDays(-30);
                }
                if (ToDate == null)
                {
                    //this is the first time the page is displayed
                    ToDate = DateTime.Now.AddDays(1);
                }

                //Create where 
                //  using ISO 8601   (CreationDateTime>'2017-01-11Z'  AND ActionDateTime <  '2017-01-16Z')
                string where = "ActionDateTime >" + "'" + FromDate.Value.Date.ToString("yyyy-MM-ddTHH:mm:ssZ") + "'";   
                where = where + " AND ";
                where = where + "CreationDateTime <" + "'" + ToDate.Value.Date.ToString("yyyy-MM-ddTHH:mm:ssZ") + "'";

                ILogger Logger = Factory.GetLogger();
                ApiClient ApiClient = Factory.GetAapiClient_ForPublicApplication(this.Session, Logger);
                IEnumerable<Transaction> Transactions = ApiClient.Transactions.Find(where: where, page: page, page_size: page_size);

                //Pass data to view via View Bag 
                ViewBag.page = page;
                ViewBag.page_size = page_size;
                ViewBag.FromDate = FromDate;
                ViewBag.ToDate = ToDate;

                return View(Transactions);
            }
            catch (Exception e)
            {
                throw;
            }           
        }

        /*
        public ActionResult TransactionLines(Int64 InternalID)
        {
            Transaction Transaction = ApiClient.Transactions.FindByID(WrntyID);
            IEnumerable<TransactionLine> TransactionLines = Transaction.TransactionLines.Data;

            return View(TransactionLines);
        }
        */

    }
}
