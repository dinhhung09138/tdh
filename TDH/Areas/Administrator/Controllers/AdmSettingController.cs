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
            if (!Request.IsAjaxRequest())
            {
                throw new HttpException();
            }
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
                Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.Success,
                    Message = Resources.Message.Success
                };
                Services.HomeNavigationService _service = new Services.HomeNavigationService();
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                if (_service.Save(model) != ResponseStatusCodeHelper.Success)
                {
                    _result.Status = ResponseStatusCodeHelper.Error;
                    _result.Message = Resources.Message.Error;
                }
                return this.Json(_result, JsonRequestBehavior.AllowGet);
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
            if (!Request.IsAjaxRequest())
            {
                throw new HttpException();
            }
            try
            {
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
            if (!Request.IsAjaxRequest())
            {
                throw new HttpException();
            }
            try
            {
                Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.Success,
                    Message = Resources.Message.Success
                };
                Services.HomeCategoryService _service = new Services.HomeCategoryService();
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                if (_service.Save(model) != ResponseStatusCodeHelper.Success)
                {
                    _result.Status = ResponseStatusCodeHelper.Error;
                    _result.Message = Resources.Message.Error;
                }
                return this.Json(_result, JsonRequestBehavior.AllowGet);
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
            if (!Request.IsAjaxRequest())
            {
                throw new HttpException();
            }
            try
            {
                return PartialView();
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
            if (!Request.IsAjaxRequest())
            {
                throw new HttpException();
            }
            ViewBag.id = id;
            try
            {
                Services.ConfigurationService _service = new Services.ConfigurationService();
                ConfigurationModel model = _service.GetItemByID(new ConfigurationModel() { Key = id, CreateBy = UserID, Insert = false });
                return PartialView(model);
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
            if (!Request.IsAjaxRequest())
            {
                throw new HttpException();
            }
            try
            {
                Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.Success,
                    Message = Resources.Message.Success
                };
                Services.ConfigurationService _service = new Services.ConfigurationService();
                model.CreateBy = UserID;
                if (_service.Save(model) != ResponseStatusCodeHelper.Success)
                {
                    _result.Status = ResponseStatusCodeHelper.Error;
                    _result.Message = Resources.Message.Error;
                }
                return this.Json(_result, JsonRequestBehavior.AllowGet);
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