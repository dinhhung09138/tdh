using System;
using System.Reflection;
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
        /// <returns></returns>
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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
            }
        }

        /// <summary>
        /// Borrow account status
        /// Show history dept state of borrow accounts
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        [HttpGet]
        public ActionResult BorrowAccountStatus()
        {
            try
            {
                #region " [ Declaration ] "

                ReportService _service = new ReportService();

                #endregion

                #region " [ Main processing ] "

                var _model = _service.BorrowAccountStatus(UserID);

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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
            }
        }

        /// <summary>
        /// List of top 10 max payment
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        [HttpGet]
        public ActionResult Top10Payment()
        {
            try
            {
                #region " [ Declaration ] "

                ReportService _service = new ReportService();

                #endregion

                #region " [ Main processing ] "

                var _model = _service.Top10Payment(DateTime.Now.Year, DateTime.Now.Month, UserID);

                ViewBag.title = "Top 10 chi tiêu";

                #endregion

                return PartialView("Partials/_Top10", _model);
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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
            }
        }

        /// <summary>
        /// List of top 10 max income
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        [HttpGet]
        public ActionResult Top10Income()
        {
            try
            {
                #region " [ Declaration ] "

                ReportService _service = new ReportService();

                #endregion

                #region " [ Main processing ] "

                var _model = _service.Top10Income(DateTime.Now.Year, DateTime.Now.Month, UserID);

                ViewBag.title = "Top 10 thu nhập";

                #endregion

                return PartialView("Partials/_Top10", _model);
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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
            }
        }

        /// <summary>
        /// Percent payment by group
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        [HttpPost]
        public async Task<ActionResult> PercentByGroup()
        {
            try
            {
                #region " [ Declaration ] "

                ReportService _service = new ReportService();

                #endregion

                #region " [ Main processing ] "

                var _model = await _service.GroupPercent(DateTime.Now.Year, DateTime.Now.Month, UserID);

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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
            }
        }

        /// <summary>
        /// Real vs Setting of category
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        [HttpGet]
        public ActionResult CateOver()
        {
            try
            {
                #region " [ Declaration ] "

                ReportService _service = new ReportService();

                #endregion

                #region " [ Main processing ] "

                var _model = _service.CategorySettingByMonth(DateTime.Now.Year, DateTime.Now.Month, UserID);
                
                #endregion

                return PartialView("Partials/_OverSetting", _model);
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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
            }
        }
        
        /// <summary>
        /// Money summary
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        [HttpGet]
        public ActionResult MonthSummary()
        {
            try
            {
                #region " [ Declaration ] "

                ReportService _service = new ReportService();

                #endregion

                #region " [ Main processing ] "

                var _model = _service.CategorySettingByMonth(DateTime.Now.Year, DateTime.Now.Month, UserID);

                #endregion

                return PartialView("Partials/_MonthSummary", _model);
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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
            }
        }
    }
}