﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.UserException;
using TDH.Model.System;
using TDH.Services.System;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Areas.System.Controllers
{
    /// <summary>
    /// Error log controller
    /// </summary>
    public class STErrorLogController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "System.Controllers/STErrorLogController.cs";

        #endregion
        
        /// <summary>
        /// Error log form
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
        /// Error log form
        /// Post method
        /// </summary>
        /// <param name="requestData">Jquery datatable request</param>
        /// <returns>DataTableResponse<ErrorLogModel></returns>
        [HttpPost]
        public JsonResult Index(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                ErrorLogService _service = new ErrorLogService();

                #endregion

                #region " [ Main processing ] "

                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.List(requestData, UserID);

                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<ErrorLogModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<ErrorLogModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }

                return this.Json(new DataTableResponse<ErrorLogModel>(), JsonRequestBehavior.AllowGet);
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
        /// Detail error log
        /// </summary>
        /// <param name="id">The log identifier</param>
        /// <returns>View</returns>
        [HttpPost]
        public JsonResult Detail(string id)
        {
            try
            {
                #region " [ Declaration ] "

                ErrorLogService _service = new ErrorLogService();
                //
                ViewBag.id = id;

                #endregion

                //Call to service
                ErrorLogModel model = _service.GetItemByID(new ErrorLogModel() { ID = new Guid(id), CreateBy = UserID });

                return this.Json(model, JsonRequestBehavior.AllowGet);
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
        /// Delete all log method
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteAll()
        {
            try
            {
                #region " [ Declaration ] "

                ErrorLogService _service = new ErrorLogService();

                #endregion
                
                return this.Json(_service.DeleteAll(UserID), JsonRequestBehavior.AllowGet);
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