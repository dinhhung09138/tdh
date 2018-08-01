using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.Fillters;
using TDH.Model.Money;
using TDH.Services.Money;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Areas.Money.Controllers
{
    /// <summary>
    /// Account type controller
    /// </summary>
    [AjaxExecuteFilterAttribute]
    public class AccountTypeController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Money.Controllers/AccountTypeController.cs";

        #endregion

        // GET: Money/AccountType
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// AccountType form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult AccountType()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "AccountType", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// AccountType form
        /// Post method 
        /// </summary>
        /// <param name="requestData">jquery datatable request</param>
        /// <returns>DataTableResponse<AccountTypeModel></returns>
        [HttpPost]
        public JsonResult AccountType(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                AccountTypeService _service = new AccountTypeService();

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
                    DataTableResponse<AccountTypeModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<AccountTypeModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<AccountTypeModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "AccountType", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Create AccountType form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult CreateAccountType()
        {
            try
            {
                #region " [ Declaration ] "

                AccountTypeService _service = new AccountTypeService();

                #endregion

                //Call to service
                AccountTypeModel model = new AccountTypeModel() { ID = Guid.NewGuid(), CreateBy = UserID, Insert = true };
                //
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "CreateAccountType", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Create AccountType form
        /// Post method
        /// </summary>
        /// <param name="model">Account type model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccountType(AccountTypeModel model)
        {
            try
            {
                #region " [ Declaration ] "

                AccountTypeService _service = new AccountTypeService();

                #endregion

                #region " [ Main processing ] "

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
                Log.WriteLog(FILE_NAME, "CreateAccountType", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Edit AccountType form
        /// 
        /// </summary>
        /// <param name="id">the identifier</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpGet]
        public ActionResult EditAccountType(string id)
        {
            try
            {
                #region " [ Declaration ] "

                AccountTypeService _service = new AccountTypeService();
                //
                ViewBag.id = id;

                #endregion

                //Call to service
                AccountTypeModel model = _service.GetItemByID(new AccountTypeModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                //
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "EditAccountType", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Edit AccountType form
        /// Post method
        /// </summary>
        /// <param name="model">AccountTypeModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccountType(AccountTypeModel model)
        {
            try
            {
                #region " [ Declaration ] "

                AccountTypeService _service = new AccountTypeService();

                #endregion

                #region " [ Main processing ] "

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
                Log.WriteLog(FILE_NAME, "EditAccountType", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Publish AccountType method
        /// </summary>
        /// <param name="model">AccountTypeModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult PublishAccountType(AccountTypeModel model)
        {
            try
            {
                #region " [ Declaration ] "

                AccountTypeService _service = new AccountTypeService();

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
                Log.WriteLog(FILE_NAME, "PublishAccountType", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Delete AccountType method
        /// </summary>
        /// <param name="model">AccountTypeModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult DeleteAccountType(AccountTypeModel model)
        {
            try
            {
                #region " [ Declaration ] "

                AccountTypeService _service = new AccountTypeService();

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
                Log.WriteLog(FILE_NAME, "DeleteAccountType", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Delete AccountType method
        /// </summary>
        /// <param name="model">AccountTypeModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult CheckDeleteAccountType(AccountTypeModel model)
        {
            try
            {
                #region " [ Declaration ] "

                AccountTypeService _service = new AccountTypeService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;

                #endregion

                //Call to service
                return this.Json(_service.CheckDelete(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "CheckDeleteAccountType", UserID, ex);
                throw new HttpException();
            }
        }

    }
}