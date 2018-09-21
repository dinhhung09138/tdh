using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.UserException;
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
            catch(ServiceException serviceEx)
            {
                throw serviceEx;
            }
            catch (DataAccessException accessEx)
            {
                throw accessEx;
            }
            catch (Exception ex)
            {
                throw new ControllerException(FILE_NAME, "Index", UserID, ex);
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
            catch (ServiceException serviceEx)
            {
                throw serviceEx;
            }
            catch (DataAccessException accessEx)
            {
                throw accessEx;
            }
            catch (Exception ex)
            {
                throw new ControllerException(FILE_NAME, "SummaryReport", UserID, ex);
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
            catch (ServiceException serviceEx)
            {
                throw serviceEx;
            }
            catch (DataAccessException accessEx)
            {
                throw accessEx;
            }
            catch (Exception ex)
            {
                throw new ControllerException(FILE_NAME, "SummaryReportByYear", UserID, ex);
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
            catch (ServiceException serviceEx)
            {
                throw serviceEx;
            }
            catch (DataAccessException accessEx)
            {
                throw accessEx;
            }
            catch (Exception ex)
            {
                throw new ControllerException(FILE_NAME, "IncomeByYearReport", UserID, ex);
            }
        }

        /// <summary>
        /// Payment report
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <returns>Task<ActionResult></returns>
        [HttpPost]
        public async Task<ActionResult> PaymentByYearReport(int year)
        {
            try
            {
                #region " [ Declaration ] "

                ReportService _service = new ReportService();

                #endregion

                #region " [ Main processing ] "

                var _model = await _service.SummaryPaymentByYear(year, UserID);

                #endregion

                return this.Json(_model, JsonRequestBehavior.AllowGet);
            }
            catch (ServiceException serviceEx)
            {
                throw serviceEx;
            }
            catch (DataAccessException accessEx)
            {
                throw accessEx;
            }
            catch (Exception ex)
            {
                throw new ControllerException(FILE_NAME, "PaymentByYearReport", UserID, ex);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> BorrowAccountStatus()
        {
            try
            {
                #region " [ Declaration ] "

                ReportService _service = new ReportService();

                #endregion

                #region " [ Main processing ] "

                var _model = await _service.BorrowAccountStatus(UserID);

                #endregion

                return PartialView("Partials/_BorrowAccountStatus", _model);
            }
            catch (ServiceException serviceEx)
            {
                throw serviceEx;
            }
            catch (DataAccessException accessEx)
            {
                throw accessEx;
            }
            catch (Exception ex)
            {
                throw new ControllerException(FILE_NAME, "BorrowAccountStatus", UserID, ex);
            }
        }

        //[AllowAnonymous]
        //[HttpGet]
        //public ActionResult BorrowAccountStatus()
        //{
        //    try
        //    {
        //        #region " [ Declaration ] "

        //        ReportService _service = new ReportService();

        //        #endregion

        //        #region " [ Main processing ] "

        //        var _model = _service.BorrowAccountStatus(UserID);

        //        #endregion

        //        return PartialView("Partials/_BorrowAccountStatus", _model);
        //    }
        //    catch (ServiceException serviceEx)
        //    {
        //        throw serviceEx;
        //    }
        //    catch (DataAccessException accessEx)
        //    {
        //        throw accessEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ControllerException(FILE_NAME, "BorrowAccountStatus", UserID, ex);
        //    }
        //}
    }
}