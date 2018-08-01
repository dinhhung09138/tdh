﻿using System;
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
    /// Account controller
    /// </summary>
    public class AccountController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Money.Controllers/AccountController.cs";

        #endregion

        // GET: Money/Account
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Account form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Account()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Account", UserID, ex);
                throw new HttpException();
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
        public JsonResult Account(CustomDataTableRequestHelper requestData)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Account", UserID, ex);
                throw new HttpException();
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
        public ActionResult AccountHistory(string id, string name, string yearMonth)
        {
            try
            {
                ViewBag.accID = id;
                ViewBag.name = name;
                ViewBag.yearMonth = DateTime.Now.ToString("yyyyMM");
                ViewBag.yearMonthValue = DateTime.Now.ToString("yyyy/MM");
                ViewBag.listAccount = "1"; //Back  to list account
                if (yearMonth != "")
                {
                    ViewBag.yearMonth = yearMonth;
                    ViewBag.yearMonthValue = yearMonth.Insert(4, "/");
                    ViewBag.listAccount = "0";//Back to edit account
                }
                //
                return PartialView();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "AccountHistory", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// AccountHistory form
        /// Post method
        /// </summary>
        /// <param name="requestData">Jquery datatable request</param>
        /// <returns>DataTableResponse<AccountHistoryModel></returns>
        [HttpPost]
        public JsonResult AccountHistory(CustomDataTableRequestHelper requestData)
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
                if (requestData.Parameter3 == null)
                {
                    requestData.Parameter3 = "";
                }
                // Process sorting column

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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "AccountHistory", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Create Account form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult CreateAccount()
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
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "CreateAccount", UserID, ex);
                throw new HttpException();
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
        public ActionResult CreateAccount(AccountModel model)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "CreateAccount", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Edit Account form
        /// </summary>
        /// <param name="id">the account identifier</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult EditAccount(string id)
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
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "EditAccount", UserID, ex);
                throw new HttpException();
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
        public ActionResult EditAccount(AccountModel model)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "EditAccount", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Publish Account method
        /// </summary>
        /// <param name="model">AccountModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult PublishAccount(AccountModel model)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "PublishAccount", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Delete Account method
        /// </summary>
        /// <param name="model">AccountModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult DeleteAccount(AccountModel model)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "DeleteAccount", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Delete Account method
        /// </summary>
        /// <param name="model">AccountModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult CheckDeleteAccount(AccountModel model)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "CheckDeleteAccount", UserID, ex);
                throw new HttpException();
            }
        }

    }
}