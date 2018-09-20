﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.UserException;
using TDH.Model.Website;
using TDH.Services.Website;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Areas.Website.Controllers
{
    public class WSettingController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Website.Controllers/WSettingController.cs";

        #endregion

        #region " [ Navigation ] "

        /// <summary>
        /// Navigation form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Navigation()
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
                throw new ControllerException(FILE_NAME, "Navigation", UserID, ex);
            }
        }

        /// <summary>
        /// Navigation form
        /// Post method
        /// </summary>
        /// <param name="requestData">Jquery datatable request</param>
        /// <returns> DataTableResponse<HomeNavigationModel></returns>
        [HttpPost]
        public JsonResult Navigation(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                HomeNavigationService _service = new HomeNavigationService();

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
                throw new ControllerException(FILE_NAME, "Navigation", UserID, ex);
            }
        }

        /// <summary>
        /// Save navigation
        /// Post method
        /// </summary>
        /// <param name="model">Home navigation model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult SaveNavigation(HomeNavigationModel model)
        {
            try
            {
                #region " [ Declaration ] "

                HomeNavigationService _service = new HomeNavigationService();

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
                throw new ControllerException(FILE_NAME, "SaveNavigation", UserID, ex);
            }
        }

        #endregion

        #region " [ Category ] "

        /// <summary>
        /// Category form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Category()
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
                throw new ControllerException(FILE_NAME, "Category", UserID, ex);
            }
        }

        /// <summary>
        /// Category form
        /// Post method
        /// </summary>
        /// <param name="requestData">jquery datatable request</param>
        /// <returns>DataTableResponse<HomeCategoryModel></returns>
        [HttpPost]
        public JsonResult Category(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                HomeCategoryService _service = new HomeCategoryService();

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

                return this.Json(new DataTableResponse<HomeCategoryModel>(), JsonRequestBehavior.AllowGet);
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
                throw new ControllerException(FILE_NAME, "Category", UserID, ex);
            }
        }

        /// <summary>
        /// Save category
        /// </summary>
        /// <param name="model">Home category model</param>
        /// <returns>DataTableResponse<CategoryModel></returns>
        [HttpPost]
        public ActionResult SaveCategory(HomeCategoryModel model)
        {
            try
            {
                #region " [ Declaration ] "

                HomeCategoryService _service = new HomeCategoryService();

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
                throw new ControllerException(FILE_NAME, "SaveCategory", UserID, ex);
            }
        }

        #endregion

        #region " [ Configuration ] "

        /// <summary>
        /// Configuration form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Configuration()
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
                throw new ControllerException(FILE_NAME, "Configuration", UserID, ex);
            }
        }

        /// <summary>
        /// Configuration form
        /// Post method
        /// Post method
        /// </summary>
        /// <param name="requestData">Jquery datatable request</param>
        /// <returns>DataTableResponse<ConfigurationModel></returns>
        [HttpPost]
        public JsonResult Configuration(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                ConfigurationService _service = new ConfigurationService();

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

                return this.Json(new DataTableResponse<ConfigurationModel>(), JsonRequestBehavior.AllowGet);
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
                throw new ControllerException(FILE_NAME, "Configuration", UserID, ex);
            }
        }

        /// <summary>
        /// Edit cofiguration
        /// </summary>
        /// <param name="id">Configuration identifier</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult EditConfiguration(string id)
        {
            try
            {
                #region " [ Declaration ] "

                ConfigurationService _service = new ConfigurationService();

                ViewBag.id = id;

                #endregion

                //Call to service
                ConfigurationModel model = _service.GetItemByID(new ConfigurationModel() { Key = id, CreateBy = UserID, Insert = false });
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
                throw new ControllerException(FILE_NAME, "EditConfiguration", UserID, ex);
            }
        }

        /// <summary>
        /// Edit cofiguration
        /// Post method
        /// </summary>
        /// <param name="model">Configuration model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditConfiguration(ConfigurationModel model)
        {
            try
            {
                #region " [ Declaration ] "

                ConfigurationService _service = new ConfigurationService();

                #endregion

                #region " [ Main processing ] "

                model.CreateBy = UserID;

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
                throw new ControllerException(FILE_NAME, "EditConfiguration", UserID, ex);
            }
        }

        #endregion
    }
}