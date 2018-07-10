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
        public ActionResult AccountHistory(string id, string name, string yearMonth)
        {
            try
            {
                ViewBag.accID = id;
                ViewBag.name = name;
                ViewBag.yearMonth = DateTime.Now.ToString("yyyyMM");
                ViewBag.yearMonthValue = DateTime.Now.ToString("yyyy/MM");
                ViewBag.listAccount = "1"; //Back  to list account
                if(yearMonth != "")
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
                TDH.Services.Log.WriteLog(FILE_NAME, "AccountHistory", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public JsonResult AccountHistory(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyAccountService _service = new Services.MoneyAccountService();

                #endregion

                #region " [ Main processing ] "

                if(requestData.Parameter1 == null)
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
                    DataTableResponse<MoneyAccountHistoryModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<MoneyAccountHistoryModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<MoneyAccountHistoryModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "AccountHistory", UserID, ex);
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
                TDH.Services.Log.WriteLog(FILE_NAME, "EditAccount", UserID, ex);
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
                TDH.Services.Log.WriteLog(FILE_NAME, "PublishAccount", UserID, ex);
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
                TDH.Services.Log.WriteLog(FILE_NAME, "DeleteAccount", UserID, ex);
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
                TDH.Services.Log.WriteLog(FILE_NAME, "CheckDeleteAccount", UserID, ex);
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

                if(requestData.Parameter1 == null) // By type (income or payment)
                {
                    requestData.Parameter1 = "";
                }
                if (requestData.Parameter2 == null) // By year month
                {
                    requestData.Parameter2 = DateTime.Now.ToString("yyyyMM");
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

        /// <summary>
        /// Get all group to set in a month
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetGroupSettingInfo(decimal year)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyGroupSettingService _service = new Services.MoneyGroupSettingService();
                
                #endregion

                //Call to service
                List< MoneyGroupSettingModel> model = _service.GetAll(UserID, year);
                //
                return this.Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "GroupSettingInfo", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// All group to set in a month
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveGroupSettingInfo(List<MoneyGroupSettingModel> model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyGroupSettingService _service = new Services.MoneyGroupSettingService();

                #endregion

                #region " [ Main processing ] "

                if(model.Count == 0)
                {
                    return this.Json(ResponseStatusCodeHelper.OK, JsonRequestBehavior.AllowGet);
                }
                //
                model[0].CreateBy = UserID;
                model[0].UpdateBy = UserID;

                #endregion

                //Call to service
                return this.Json(_service.Save(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "GroupSettingInfo", UserID, ex);
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

        #region " [ Category ]  "

        /// <summary>
        /// Category form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Category()
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyGroupService _groupServices = new Services.MoneyGroupService();
                //
                ViewBag.group = _groupServices.GetAll(UserID);

                #endregion
                //
                return PartialView();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Category", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public JsonResult Category(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "
                
                Services.MoneyCategoryService _service = new Services.MoneyCategoryService();

                #endregion

                #region " [ Main processing ] "

                if(requestData.Parameter1 == null) // by group
                {
                    requestData.Parameter1 = "";
                }
                if (requestData.Parameter2 == null) // By year month
                {
                    requestData.Parameter2 = DateTime.Now.ToString("yyyyMM");
                }
                // Process sorting column
                requestData = requestData.SetOrderingColumnName();

                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                //
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<MoneyCategoryModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<MoneyCategoryModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<MoneyCategoryModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Category", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult CategoryHistory(string id, string name, string yearMonth)
        {
            try
            {
                ViewBag.cateID = id;
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
                TDH.Services.Log.WriteLog(FILE_NAME, "CategoryHistory", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public JsonResult CategoryHistory(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyCategoryService _service = new Services.MoneyCategoryService();

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

                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.GetHistory(requestData, UserID);
                //
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<MoneyCategoryHistoryModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<MoneyCategoryHistoryModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<MoneyCategoryHistoryModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CategoryHistory", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyGroupService _groupServices = new Services.MoneyGroupService();
                Services.MoneyCategoryService _service = new Services.MoneyCategoryService();
                //
                ViewBag.group = _groupServices.GetAll(UserID);

                #endregion
                //
                MoneyCategoryModel model = new MoneyCategoryModel() { ID = Guid.NewGuid(), CreateBy = UserID, Insert = true };
                //
                return PartialView(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateCategory", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(MoneyCategoryModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyCategoryService _service = new Services.MoneyCategoryService();

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
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateCategory", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult EditCategory(string id)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyGroupService _groupServices = new Services.MoneyGroupService();
                Services.MoneyCategoryService _service = new Services.MoneyCategoryService();
                //
                ViewBag.group = _groupServices.GetAll(UserID);

                ViewBag.id = id;

                #endregion

                //Call to service
                MoneyCategoryModel model = _service.GetItemByID(new MoneyCategoryModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                //
                return PartialView(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "EditCategory", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(MoneyCategoryModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyCategoryService _service = new Services.MoneyCategoryService();

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
                TDH.Services.Log.WriteLog(FILE_NAME, "EditCategory", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult PublishCategory(MoneyCategoryModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyCategoryService _service = new Services.MoneyCategoryService();

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
                TDH.Services.Log.WriteLog(FILE_NAME, "PublishCategory", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult DeleteCategory(MoneyCategoryModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyCategoryService _service = new Services.MoneyCategoryService();

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
                TDH.Services.Log.WriteLog(FILE_NAME, "DeleteAccount", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult CheckDeleteCategory(MoneyCategoryModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyCategoryService _service = new Services.MoneyCategoryService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;

                #endregion

                //Call to service
                return this.Json(_service.CheckDelete(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CheckDeleteCategory", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion

        #region " [ Income, Payment, Transfer ] "

        [HttpGet]
        public ActionResult FlowHistory()
        {
            #region " [ Declaration ] "

            Services.MoneyCategoryService _categoryServices = new Services.MoneyCategoryService();
            Services.MoneyAccountService _accountServices = new Services.MoneyAccountService();
            //
            ViewBag.incomeCategory = _categoryServices.GetAll(UserID, true);
            ViewBag.paymentCategory = _categoryServices.GetAll(UserID, false);
            ViewBag.account = _accountServices.GetAll(UserID);
            ViewBag.accountHasMoney = _accountServices.GetAllWithFullMoney(UserID);

            #endregion
            //
            return View();
        }

        [HttpPost]
        public ActionResult FlowHistory(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyFlowService _service = new Services.MoneyFlowService();

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
                    DataTableResponse<MoneyFlowModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<MoneyFlowModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<MoneyFlowModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "FlowHistory", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult SaveIncome(MoneyIncomeModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyFlowService _service = new Services.MoneyFlowService();

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
                TDH.Services.Log.WriteLog(FILE_NAME, "SaveIncome", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult SavePayment(MoneyPaymentModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyFlowService _service = new Services.MoneyFlowService();

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
                TDH.Services.Log.WriteLog(FILE_NAME, "SavePayment", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult SaveTransfer(MoneyTransferModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.MoneyFlowService _service = new Services.MoneyFlowService();

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
                TDH.Services.Log.WriteLog(FILE_NAME, "SaveTransfer", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion

    }
}