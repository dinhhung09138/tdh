using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDH.Common;
using TDH.Model.Money;
using TDH.Services.Money;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Areas.Money.Controllers
{
    /// <summary>
    /// Flow history controller
    /// </summary>
    public class FlowHistoryController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Money.Controllers/FlowHistoryController.cs";

        #endregion

        // GET: Money/FlowHistory
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Flow history form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult FlowHistory()
        {
            #region " [ Declaration ] "

            CategoryService _categoryServices = new CategoryService();
            AccountService _accountServices = new AccountService();
            //
            ViewBag.incomeCategory = _categoryServices.GetAll(UserID, true);
            ViewBag.paymentCategory = _categoryServices.GetAll(UserID, false);
            ViewBag.account = _accountServices.GetAll(UserID);
            ViewBag.accountHasMoney = _accountServices.GetAllWithFullMoney(UserID);

            #endregion
            //
            return View();
        }

        /// <summary>
        /// Flow history form
        /// Post method
        /// </summary>
        /// <param name="requestData">Jquery datatable request</param>
        /// <returns>DataTableResponse<FlowModel></returns>
        [HttpPost]
        public ActionResult FlowHistory(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                FlowService _service = new FlowService();

                #endregion

                #region " [ Main processing ] "

                if (requestData.Parameter1 == null)
                {
                    requestData.Parameter1 = "";
                }
                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                //
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<FlowModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<FlowModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<FlowModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "FlowHistory", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Save money income method
        /// </summary>
        /// <param name="model">IncomeModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult SaveIncome(IncomeModel model)
        {
            try
            {
                #region " [ Declaration ] "

                FlowService _service = new FlowService();

                #endregion

                #region " [ Main processing ] "

                string[] tmp = model.DateString.Split('/');
                model.Date = new DateTime(int.Parse(tmp[2]), int.Parse(tmp[1]), int.Parse(tmp[0]));
                //
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                //Call to service
                return this.Json(_service.SaveIncome(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "SaveIncome", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Save payment money method
        /// </summary>
        /// <param name="model">PaymentModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult SavePayment(PaymentModel model)
        {
            try
            {
                #region " [ Declaration ] "

                FlowService _service = new FlowService();

                #endregion

                #region " [ Main processing ] "

                string[] tmp = model.DateString.Split('/');
                model.Date = new DateTime(int.Parse(tmp[2]), int.Parse(tmp[1]), int.Parse(tmp[0]));
                //
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                //Call to service
                return this.Json(_service.SavePayment(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "SavePayment", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Save transfer money method
        /// </summary>
        /// <param name="model">TransferModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult SaveTransfer(TransferModel model)
        {
            try
            {
                #region " [ Declaration ] "

                FlowService _service = new FlowService();

                #endregion

                #region " [ Main processing ] "

                string[] tmp = model.DateString.Split('/');
                model.Date = new DateTime(int.Parse(tmp[2]), int.Parse(tmp[1]), int.Parse(tmp[0]));
                //
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                //Call to service
                return this.Json(_service.SaveTransfer(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "SaveTransfer", UserID, ex);
                throw new HttpException();
            }
        }

    }
}