using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.UserException;
using TDH.Model.Money;
using TDH.Services.Money;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Areas.Money.Controllers
{
    /// <summary>
    /// Account controller
    /// </summary>
    public class MNAccountController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Money.Controllers/MNAccountController.cs";

        #endregion

        /// <summary>
        /// Default View when user click on left sidebar
        /// </summary>
        /// <returns>View</returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Home()
        {
            try
            {
                return View();
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
        /// Account form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return View();
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
        /// Account form
        /// Post method
        /// 
        /// </summary>
        /// <param name="requestData">jquery datatable request</param>
        /// <returns>DataTableResponse<AccountModel></returns>
        [HttpPost]
        public JsonResult Index(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                AccountService _service = new AccountService();

                #endregion

                #region " [ Main processing ] "

                // Process sorting column
                requestData = requestData.SetOrderingColumnName();

                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                //
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<AccountModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<AccountModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<AccountModel>(), JsonRequestBehavior.AllowGet);
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
        /// AccountHistory form
        /// </summary>
        /// <param name="id">the account identifier</param>
        /// <param name="name">acocunt name</param>
        /// <param name="yearMonth">yearMonth</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult History(string id)
        {
            try
            {

                AccountService _service = new AccountService();

                ViewBag.accID = id;
                ViewBag.name = "";
                ViewBag.yearMonth = DateTime.Now.ToString("yyyyMM");
                ViewBag.yearMonthValue = DateTime.Now.ToString("yyyy/MM");
                ViewBag.listAccount = "1"; //Back  to list account
               
                //Call to service
                AccountModel model = _service.GetItemByID(new AccountModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                ViewBag.name = model.Name;
                //
                return View();
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
        /// AccountHistory form
        /// Post method
        /// </summary>
        /// <param name="requestData">Jquery datatable request</param>
        /// <returns>DataTableResponse<AccountHistoryModel></returns>
        [HttpPost]
        public JsonResult History(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                AccountService _service = new AccountService();

                #endregion

                #region " [ Main processing ] "

                if (requestData.Parameter1 == null)
                {
                    requestData.Parameter1 = "";
                }
                if (requestData.Parameter2 == null)
                {
                    requestData.Parameter2 = "";
                }
                // Process sorting column
                requestData = requestData.SetOrderingColumnName();

                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.GetHistory(requestData, UserID);
                //
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<AccountHistoryModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<AccountHistoryModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<AccountHistoryModel>(), JsonRequestBehavior.AllowGet);
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
        /// Create Account form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                #region " [ Declaration ] "

                AccountTypeService _typeServices = new AccountTypeService();
                AccountService _service = new AccountService();
                //
                ViewBag.type = _typeServices.GetAll(UserID);

                #endregion

                //Call to service
                AccountModel model = new AccountModel() { ID = Guid.NewGuid(), CreateBy = UserID, Insert = true };
                //
                return View(model);
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
        /// Create Account form
        /// Post method
        /// </summary>
        /// <param name="model">AccountModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(AccountModel model)
        {
            try
            {
                #region " [ Declaration ] "

                AccountService _service = new AccountService();

                #endregion

                #region " [ Main processing ] "

                model.MaxPayment = decimal.Parse(model.MaxPaymentString.Replace(",", ""));
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                
                #endregion

                //Call to service
                return this.Json(_service.Save(model), JsonRequestBehavior.AllowGet);
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
        /// Edit Account form
        /// </summary>
        /// <param name="id">the account identifier</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Edit(string id)
        {
            try
            {
                #region " [ Declaration ] "

                AccountTypeService _typeServices = new AccountTypeService();
                AccountService _service = new AccountService();
                //
                ViewBag.type = _typeServices.GetAll(UserID);

                ViewBag.id = id;

                #endregion

                //Call to service
                AccountModel model = _service.GetItemByID(new AccountModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                //
                return View(model);
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
        /// Edit Account form
        /// Post method
        /// </summary>
        /// <param name="model">AccountModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(AccountModel model)
        {
            try
            {
                #region " [ Declaration ] "

                AccountService _service = new AccountService();

                #endregion

                #region " [ Main processing ] "

                model.MaxPayment = decimal.Parse(model.MaxPaymentString.Replace(",", ""));
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                //Call to service
                return this.Json(_service.Save(model), JsonRequestBehavior.AllowGet);
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
        /// Publish Account method
        /// </summary>
        /// <param name="model">AccountModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public JsonResult Publish(AccountModel model)
        {
            try
            {
                #region " [ Declaration ] "

                AccountService _service = new AccountService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.UpdateDate = DateTime.Now;

                #endregion

                //Call to service
                return this.Json(_service.Publish(model), JsonRequestBehavior.AllowGet);
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
        /// Delete Account method
        /// </summary>
        /// <param name="model">AccountModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public JsonResult Delete(AccountModel model)
        {
            try
            {
                #region " [ Declaration ] "

                AccountService _service = new AccountService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;
                model.DeleteBy = UserID;
                model.DeleteDate = DateTime.Now;

                #endregion

                //Call to service
                return this.Json(_service.Delete(model), JsonRequestBehavior.AllowGet);
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
        /// Delete Account method
        /// </summary>
        /// <param name="model">AccountModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public JsonResult CheckDelete(AccountModel model)
        {
            try
            {
                #region " [ Declaration ] "

                AccountService _service = new AccountService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;

                #endregion

                //Call to service
                return this.Json(_service.CheckDelete(model), JsonRequestBehavior.AllowGet);
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