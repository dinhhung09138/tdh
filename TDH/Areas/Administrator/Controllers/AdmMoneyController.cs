using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Utils;
using Utils.JqueryDatatable;
using TDH.Common;
using TDH.Common.Fillters;
using TDH.Services.Money;
using TDH.Model.Money;

namespace TDH.Areas.Administrator.Controllers
{
    /// <summary>
    /// Money controller
    /// </summary>
    [AjaxExecuteFilterAttribute]
    public class AdmMoneyController : TDH.Common.BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator.Controllers/AdmMoneyController.cs";

        #endregion

        /// <summary>
        /// Defalt view when user access
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            return PartialView();
        }

        #region " [ Report ] "

        /// <summary>
        /// Report form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Report()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Report", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Get data for summary report
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
        /// Get data for summary report
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
        

        #endregion
        
        #region " [ Account Type ]  "

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

        #endregion

        #region " [ Account ]  "

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

        #endregion

        #region " [ Group ]  "

        /// <summary>
        /// Group form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Group()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Group", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Group form
        /// Post method
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns>DataTableResponse<GroupModel></returns>
        [HttpPost]
        public JsonResult Group(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _service = new GroupService();

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
                    DataTableResponse<GroupModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<GroupModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<GroupModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Group", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Create Group form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult CreateGroup()
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _service = new GroupService();

                #endregion

                //Call to service
                GroupModel model = new GroupModel() { ID = Guid.NewGuid(), CreateBy = UserID, Insert = true };
                //
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "CreateGroup", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Create Group form
        /// Post method
        /// </summary>
        /// <param name="model">GroupModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroup(GroupModel model)
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _service = new GroupService();

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
                Log.WriteLog(FILE_NAME, "CreateGroup", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Edit group form
        /// </summary>
        /// <param name="id">The group identifier</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult EditGroup(string id)
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _service = new GroupService();
                //
                ViewBag.id = id;

                #endregion

                //Call to service
                GroupModel model = _service.GetItemByID(new GroupModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                //
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "EditGroup", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// All group data for setting form in a month
        /// </summary>
        /// <param name="year">Year</param>
        /// <returns>List<GroupSettingModel></returns>
        [HttpPost]
        public ActionResult GetGroupSettingInfo(decimal year)
        {
            try
            {
                #region " [ Declaration ] "

                GroupSettingService _service = new GroupSettingService();
                
                #endregion

                //Call to service
                List<GroupSettingModel> model = _service.GetAll(UserID, year);
                //
                return this.Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "GroupSettingInfo", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// All group data for setting form in a month
        /// Post method
        /// </summary>
        /// <param name="model">List<GroupSettingModel></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult SaveGroupSettingInfo(List<GroupSettingModel> model)
        {
            try
            {
                #region " [ Declaration ] "

                GroupSettingService _service = new GroupSettingService();

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
                Log.WriteLog(FILE_NAME, "GroupSettingInfo", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Edit group form
        /// Post method
        /// </summary>
        /// <param name="model">GroupModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGroup(GroupModel model)
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _service = new GroupService();

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
                Log.WriteLog(FILE_NAME, "EditGroup", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Publish Group method
        /// </summary>
        /// <param name="model">GroupModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult PublishGroup(GroupModel model)
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _service = new GroupService();

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
                Log.WriteLog(FILE_NAME, "PublishGroup", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Delete Group method
        /// </summary>
        /// <param name="model">GroupModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult DeleteGroup(GroupModel model)
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _service = new GroupService();

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
                Log.WriteLog(FILE_NAME, "DeleteGroup", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Check delete Group method
        /// </summary>
        /// <param name="model">GroupModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult CheckDeleteGroup(GroupModel model)
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _service = new GroupService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;

                #endregion

                //Call to service
                return this.Json(_service.CheckDelete(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "CheckDeleteGroup", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion

        #region " [ Category ]  "

        /// <summary>
        /// Category form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Category()
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _groupServices = new GroupService();
                //
                ViewBag.group = _groupServices.GetAll(UserID);

                #endregion
                //
                return PartialView();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Category", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Category form
        /// Post method
        /// </summary>
        /// <param name="requestData">Jquery datatable request</param>
        /// <returns>DataTableResponse<CategoryModel></returns>
        [HttpPost]
        public JsonResult Category(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "
                
                CategoryService _service = new CategoryService();

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
                    DataTableResponse<CategoryModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<CategoryModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<CategoryModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Category", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// All setting data by category in a month
        /// </summary>
        /// <param name="id">the category identifier</param>
        /// <param name="name">Category name</param>
        /// <param name="yearMonth">year month</param>
        /// <returns>View</returns>
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
                Log.WriteLog(FILE_NAME, "CategoryHistory", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// All setting data by category in a month
        /// Post method
        /// </summary>
        /// <param name="requestData">Jquery datatable request</param>
        /// <returns>DataTableResponse<CategoryHistoryModel></returns>
        [HttpPost]
        public JsonResult CategoryHistory(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                CategoryService _service = new CategoryService();

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
                    DataTableResponse<CategoryHistoryModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<CategoryHistoryModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<CategoryHistoryModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "CategoryHistory", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Create category form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult CreateCategory()
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _groupServices = new GroupService();
               CategoryService _service = new CategoryService();
                //
                ViewBag.group = _groupServices.GetAll(UserID);

                #endregion
                //
                CategoryModel model = new CategoryModel() { ID = Guid.NewGuid(), CreateBy = UserID, Insert = true };
                //
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "CreateCategory", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Create category form
        /// Post form
        /// </summary>
        /// <param name="model">CategoryModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CategoryModel model)
        {
            try
            {
                #region " [ Declaration ] "

                CategoryService _service = new CategoryService();

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
                Log.WriteLog(FILE_NAME, "CreateCategory", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Edit category form
        /// </summary>
        /// <param name="id">The category identifier</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpGet]
        public ActionResult EditCategory(string id)
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _groupServices = new GroupService();
                CategoryService _service = new CategoryService();
                //
                ViewBag.group = _groupServices.GetAll(UserID);

                ViewBag.id = id;

                #endregion

                //Call to service
                CategoryModel model = _service.GetItemByID(new CategoryModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                //
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "EditCategory", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Edit category form
        /// Post method
        /// </summary>
        /// <param name="model">CategoryModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(CategoryModel model)
        {
            try
            {
                #region " [ Declaration ] "

                CategoryService _service = new CategoryService();

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
                Log.WriteLog(FILE_NAME, "EditCategory", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Publish category method
        /// </summary>
        /// <param name="model">CategoryModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult PublishCategory(CategoryModel model)
        {
            try
            {
                #region " [ Declaration ] "

                CategoryService _service = new CategoryService();

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
                Log.WriteLog(FILE_NAME, "PublishCategory", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Delete category method
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult DeleteCategory(CategoryModel model)
        {
            try
            {
                #region " [ Declaration ] "

                CategoryService _service = new CategoryService();

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
        /// Check delete category method
        /// </summary>
        /// <param name="model">CategoryModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult CheckDeleteCategory(CategoryModel model)
        {
            try
            {
                #region " [ Declaration ] "

                CategoryService _service = new CategoryService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;

                #endregion

                //Call to service
                return this.Json(_service.CheckDelete(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "CheckDeleteCategory", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion

        #region " [ Income, Payment, Transfer ] "

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

        #endregion

    }
}