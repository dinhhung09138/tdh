using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utils;
using Utils.JqueryDatatable;
using TDH.Areas.Administrator.Models;


namespace TDH.Areas.Administrator.Controllers
{
    public class AdmSettingController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator.Controllers/AdmSettingController.cs";

        #endregion

        #region " [ Navigation ] "

        [HttpGet]
        public ActionResult Navigation()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Navigation", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public JsonResult Navigation(CustomDataTableRequestHelper requestData)
        {
            try
            {
                Services.HomeNavigationService _service = new Services.HomeNavigationService();
                requestData = requestData.SetOrderingColumnName();
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<HomeNavigationModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<HomeNavigationModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                return this.Json(new DataTableResponse<HomeNavigationModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Navigation", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult SaveNavigation(HomeNavigationModel model)
        {
            try
            {
                Services.HomeNavigationService _service = new Services.HomeNavigationService();
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                if (_service.Save(model) == ResponseStatusCodeHelper.Success)
                {
                    Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                    {
                        Status = ResponseStatusCodeHelper.Success,
                        Message = Resources.Message.Success
                    };
                    return this.Json(_result, JsonRequestBehavior.AllowGet);
                }
                return View();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "SaveNavigation", UserID, ex);
                throw new HttpException();
            }
        }
        
        #endregion

        #region " [ Category ] "

        [HttpGet]
        public ActionResult Category()
        {
            try
            {
                return View();
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
                Services.HomeCategoryService _service = new Services.HomeCategoryService();
                requestData = requestData.SetOrderingColumnName();
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<HomeCategoryModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<HomeCategoryModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                return this.Json(new DataTableResponse<HomeCategoryModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Category", UserID, ex);
                throw new HttpException();
            }
        }
        
        [HttpPost]
        public ActionResult SaveCategory(HomeCategoryModel model)
        {
            try
            {
                Services.HomeCategoryService _service = new Services.HomeCategoryService();
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                if (_service.Save(model) == ResponseStatusCodeHelper.Success)
                {
                    Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                    {
                        Status = ResponseStatusCodeHelper.Success,
                        Message = Resources.Message.Success
                    };
                    return this.Json(_result, JsonRequestBehavior.AllowGet);
                }
                return View();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "SaveCategory", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion

        #region " [ Configuration ] "

        [HttpGet]
        public ActionResult Configuration()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Configuration", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public JsonResult Configuration(CustomDataTableRequestHelper requestData)
        {
            try
            {
                Services.ConfigurationService _service = new Services.ConfigurationService();
                requestData = requestData.SetOrderingColumnName();
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<ConfigurationModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<ConfigurationModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                return this.Json(new DataTableResponse<ConfigurationModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Configuration", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult EditConfiguration(string id)
        {
            try
            {
                Services.ConfigurationService _service = new Services.ConfigurationService();
                ConfigurationModel model = _service.GetItemByID(new ConfigurationModel() { Key = id, CreateBy = UserID, Insert = false });
                return View(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "EditConfiguration", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditConfiguration(ConfigurationModel model)
        {
            try
            {
                Services.ConfigurationService _service = new Services.ConfigurationService();
                model.CreateBy = UserID;
                if (_service.Save(model) == ResponseStatusCodeHelper.Success)
                {
                    Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                    {
                        Status = ResponseStatusCodeHelper.Success,
                        Message = Resources.Message.Success
                    };
                    TempData[CommonHelper.EXECUTE_RESULT] = _result;
                    return RedirectToAction("Configuration");
                }
                return View();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "EditConfiguration", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion

    }
}