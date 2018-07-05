using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Utils;
using Utils.JqueryDatatable;
using TDH.Areas.Administrator.Models;
using TDH.Areas.Administrator.Filters;

namespace TDH.Areas.Administrator.Controllers
{
    /// <summary>
    /// Money controller
    /// </summary>
    [AjaxExecuteFilterAttribute]
    public class AdmMoneyController : BaseController
    {


        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator.Controllers/AdmMoneyController.cs";

        #endregion

        #region " [ Account Type ]  "

        /// <summary>
        /// AccountType form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AccountType()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "AccountType", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public JsonResult AccountType(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyAccountTypeService _service = new Services.MoneyAccountTypeService();

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
                    DataTableResponse<MoneyAccountTypeModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<MoneyAccountTypeModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<MoneyAccountTypeModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "AccountType", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult CreateAccountType()
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyAccountTypeService _service = new Services.MoneyAccountTypeService();

                #endregion

                //Call to service
                MoneyAccountTypeModel model = new MoneyAccountTypeModel() { ID = Guid.NewGuid(), CreateBy = UserID, Insert = true };
                //
                return PartialView(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateAccountType", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccountType(MoneyAccountTypeModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyAccountTypeService _service = new Services.MoneyAccountTypeService();

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
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateAccountType", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult EditAccountType(string id)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyAccountTypeService _service = new Services.MoneyAccountTypeService();
                //
                ViewBag.id = id;

                #endregion

                //Call to service
                MoneyAccountTypeModel model = _service.GetItemByID(new MoneyAccountTypeModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                //
                return PartialView(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "EditAccountType", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccountType(MoneyAccountTypeModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyAccountTypeService _service = new Services.MoneyAccountTypeService();

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
                TDH.Services.Log.WriteLog(FILE_NAME, "EditAccountType", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult PublishAccountType(MoneyAccountTypeModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyAccountTypeService _service = new Services.MoneyAccountTypeService();

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
                TDH.Services.Log.WriteLog(FILE_NAME, "PublishAccountType", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult DeleteAccountType(MoneyAccountTypeModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyAccountTypeService _service = new Services.MoneyAccountTypeService();

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
                TDH.Services.Log.WriteLog(FILE_NAME, "DeleteAccountType", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult CheckDeleteAccountType(MoneyAccountTypeModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyAccountTypeService _service = new Services.MoneyAccountTypeService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;

                #endregion

                //Call to service
                return this.Json(_service.CheckDelete(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CheckDeleteAccountType", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion

        #region " [ Account ]  "

        /// <summary>
        /// Account form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Account()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Account", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public JsonResult Account(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyAccountService _service = new Services.MoneyAccountService();

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
                    DataTableResponse<MoneyAccountModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<MoneyAccountModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<MoneyAccountModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Account", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyAccountTypeService _typeServices = new Services.MoneyAccountTypeService();
                Services.MoneyAccountService _service = new Services.MoneyAccountService();
                //
                ViewBag.type = _typeServices.GetAll(UserID);

                #endregion

                //Call to service
                MoneyAccountModel model = new MoneyAccountModel() { ID = Guid.NewGuid(), CreateBy = UserID, Insert = true };
                //
                return PartialView(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateAccount", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccount(MoneyAccountModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyAccountService _service = new Services.MoneyAccountService();

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
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateAccount", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult EditAccount(string id)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyAccountTypeService _typeServices = new Services.MoneyAccountTypeService();
                Services.MoneyAccountService _service = new Services.MoneyAccountService();
                //
                ViewBag.type = _typeServices.GetAll(UserID);

                ViewBag.id = id;

                #endregion

                //Call to service
                MoneyAccountModel model = _service.GetItemByID(new MoneyAccountModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                //
                return PartialView(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "EditAccount", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccount(MoneyAccountModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyAccountService _service = new Services.MoneyAccountService();

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
                TDH.Services.Log.WriteLog(FILE_NAME, "EditAccountType", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult PublishAccount(MoneyAccountModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyAccountService _service = new Services.MoneyAccountService();

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
                TDH.Services.Log.WriteLog(FILE_NAME, "PublishAccountType", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult DeleteAccount(MoneyAccountModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyAccountService _service = new Services.MoneyAccountService();

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
                TDH.Services.Log.WriteLog(FILE_NAME, "DeleteAccountType", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult CheckDeleteAccount(MoneyAccountModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyAccountService _service = new Services.MoneyAccountService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;

                #endregion

                //Call to service
                return this.Json(_service.CheckDelete(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CheckDeleteAccountType", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion

        #region " [ Group ]  "

        /// <summary>
        /// Group form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Group()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Group", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public JsonResult Group(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyGroupService _service = new Services.MoneyGroupService();

                #endregion

                #region " [ Main processing ] "

                if(requestData.Parameter1 == null)
                {
                    requestData.Parameter1 = "";
                }
                // Process sorting column
                requestData = requestData.SetOrderingColumnName();

                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                //
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<MoneyGroupModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<MoneyGroupModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<MoneyGroupModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Group", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult CreateGroup()
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyGroupService _service = new Services.MoneyGroupService();

                #endregion

                //Call to service
                MoneyGroupModel model = new MoneyGroupModel() { ID = Guid.NewGuid(), CreateBy = UserID, Insert = true };
                //
                return PartialView(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateGroup", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroup(MoneyGroupModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyGroupService _service = new Services.MoneyGroupService();

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
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateGroup", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult EditGroup(string id)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyGroupService _service = new Services.MoneyGroupService();
                //
                ViewBag.id = id;

                #endregion

                //Call to service
                MoneyGroupModel model = _service.GetItemByID(new MoneyGroupModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                //
                return PartialView(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "EditGroup", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGroup(MoneyGroupModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyGroupService _service = new Services.MoneyGroupService();

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
                TDH.Services.Log.WriteLog(FILE_NAME, "EditGroup", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult PublishGroup(MoneyGroupModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyGroupService _service = new Services.MoneyGroupService();

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
                TDH.Services.Log.WriteLog(FILE_NAME, "PublishGroup", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult DeleteGroup(MoneyGroupModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyGroupService _service = new Services.MoneyGroupService();

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
                TDH.Services.Log.WriteLog(FILE_NAME, "DeleteGroup", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult CheckDeleteGroup(MoneyGroupModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyGroupService _service = new Services.MoneyGroupService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;

                #endregion

                //Call to service
                return this.Json(_service.CheckDelete(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CheckDeleteGroup", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion

    }
}