using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.Fillters;
using TDH.Model.System;
using TDH.Services.System;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Areas.System.Controllers
{
    /// <summary>
    /// Error log controller
    /// </summary>
    [AjaxExecuteFilterAttribute]
    public class ErrorLogController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "System.Controllers/ErrorLogController.cs";

        #endregion

        /// <summary>
        /// Error log form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult ErrorLog()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "ErrorLog", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Error log form
        /// Post method
        /// </summary>
        /// <param name="requestData">Jquery datatable request</param>
        /// <returns>DataTableResponse<ErrorLogModel></returns>
        [HttpPost]
        public JsonResult ErrorLog(CustomDataTableRequestHelper requestData)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "ErrorLog", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Detail error log
        /// </summary>
        /// <param name="id">log identifier</param>
        /// <returns>View</returns>
        [HttpPost]
        public JsonResult DetailErroLog(string id)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "DetailErroLog", UserID, ex);
                throw new HttpException();
            }
        }

    }
}