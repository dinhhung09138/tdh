using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utils;
using Utils.JqueryDatatable;
using TDH.Areas.Administrator.Models;
using TDH.Areas.Administrator.Filters;


namespace TDH.Areas.Administrator.Controllers
{
    [AjaxExecuteFilter]
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
                return PartialView();
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
                #region " [ Declaration ] "

                Services.HomeNavigationService _service = new Services.HomeNavigationService();

                #endregion

                #region " [ Main processing ] "

                // Process sorting column
                requestData = requestData.SetOrderingColumnName();

                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<HomeNavigationModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<HomeNavigationModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
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
                #region " [ Declaration ] "

                Services.HomeNavigationService _service = new Services.HomeNavigationService();

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

                Services.HomeCategoryService _service = new Services.HomeCategoryService();

                #endregion

                #region " [ Main processing ] "

                // Process sorting column
                requestData = requestData.SetOrderingColumnName();

                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<HomeCategoryModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<HomeCategoryModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
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
                #region " [ Declaration ] "

                Services.HomeCategoryService _service = new Services.HomeCategoryService();

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
                #region " [ Declaration ] "

                Services.ConfigurationService _service = new Services.ConfigurationService();

                #endregion

                #region " [ Main processing ] "

                // Process sorting column
                requestData = requestData.SetOrderingColumnName();

                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<ConfigurationModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<ConfigurationModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
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
                #region " [ Declaration ] "

                Services.ConfigurationService _service = new Services.ConfigurationService();
                //
                ViewBag.id = id;

                #endregion

                //Call to service
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
            try
            {
                #region " [ Declaration ] "

                Services.ConfigurationService _service = new Services.ConfigurationService();

                #endregion

                #region " [ Main processing ] "

                model.CreateBy = UserID;

                #endregion

                //Call to service
                return this.Json(_service.Save(model), JsonRequestBehavior.AllowGet);
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