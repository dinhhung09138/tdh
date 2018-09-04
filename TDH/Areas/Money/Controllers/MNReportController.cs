using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.Fillters;
using TDH.Services.Money;

namespace TDH.Areas.Money.Controllers
{
    /// <summary>
    /// Report controller
    /// </summary>
    public class MNReportController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Money.Controllers/MNReportController.cs";

        #endregion

        /// <summary>
        /// Report form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Index", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Summary dashboard
        /// Get data for summary report (income, payment)
        /// </summary>
        /// <returns>Task<ActionResult></returns>
        [HttpPost]
        public async Task<ActionResult> SummaryReport()
        {
            try
            {
                #region " [ Declaration ] "

                ReportService _service = new ReportService();

                #endregion

                #region " [ Main processing ] "

                var _model = await _service.Summary(UserID);

                #endregion

                return this.Json(_model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "SummaryReport", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Summary dashboard
        /// Get data for summary report
        /// Report (income, payment) by month in a year
        /// </summary>
        /// <param name="year"></param>
        /// <returns>Task<ActionResult></returns>
        [HttpPost]
        public async Task<ActionResult> SummaryReportByYear(int year)
        {
            try
            {
                #region " [ Declaration ] "

                ReportService _service = new ReportService();

                #endregion

                #region " [ Main processing ] "

                var _model = await _service.SummaryByYear(year, UserID);

                #endregion

                return this.Json(_model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "SummaryReportByYear", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Income report
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <returns>Task<ActionResult></returns>
        [HttpPost]
        public async Task<ActionResult> IncomeByYearReport(int year)
        {
            try
            {
                #region " [ Declaration ] "

                ReportService _service = new ReportService();

                #endregion

                #region " [ Main processing ] "

                var _model = await _service.SummaryIncomeByYear(year, UserID);

                #endregion

                return this.Json(_model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "IncomeByYearReport", UserID, ex);
                throw new HttpException();
            }
        }


    }
}