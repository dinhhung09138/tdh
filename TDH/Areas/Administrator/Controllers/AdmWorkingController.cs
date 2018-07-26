using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utils;
using Utils.JqueryDatatable;
using TDH.Areas.Administrator.Models;
using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;
using TDH.Areas.Administrator.Filters;

namespace TDH.Areas.Administrator.Controllers
{
    [AjaxExecuteFilter]
    public class AdmWorkingController : TDH.Common.BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator.Controllers/AdmWorkingController.cs";
        
        #endregion


        #region " [ Report ] "

        [HttpGet]
        public ActionResult Report()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Report", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public JsonResult Report(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                Services.ReportService _service = new Services.ReportService();

                #endregion

                #region " [ Main processing ] "

                // Process sorting column
                requestData = requestData.SetOrderingColumnName();

                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<ReportModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<ReportModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<ReportModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Report", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult CreateReport()
        {
            try
            {
                #region " [ Declaration ] "

                ReportModel model = new ReportModel()
                {
                    ID = Guid.NewGuid(),
                    CreateBy = UserID,
                    Insert = true
                };

                #endregion
                
                return PartialView(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateReport", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateReport(ReportModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.ReportService _service = new Services.ReportService();

                #endregion

                #region " [ Main processing ] "

                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                // Call to service
                return this.Json(_service.Save(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateReport", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult EditReport(string id)
        {
            try
            {
                #region " [ Declaration ] "

                Services.ReportService _service = new Services.ReportService();
                //
                ViewBag.id = id;

                #endregion

                // Call to service
                ReportModel model = _service.GetItemByID(new ReportModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                //
                return PartialView(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "EditReport", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditReport(ReportModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.ReportService _service = new Services.ReportService();

                #endregion

                #region " [ Main processing ] "

                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                // Call to service
                return this.Json(_service.Save(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "EditReport", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult DeleteReport(ReportModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.ReportService _service = new Services.ReportService();

                #endregion

                #region " [ Main processing ] "

                model.CreateBy = UserID;
                model.DeleteBy = UserID;
                model.DeleteDate = DateTime.Now;

                #endregion

                // Call to service
                return this.Json(_service.Delete(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "DeleteReport", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult CheckDeleteReport(ReportModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.ReportService _service = new Services.ReportService();
                //
                ResultModel = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.OK,
                    Message = ""
                };
                #endregion

                #region " [ Main processing ] "

                model.CreateBy = UserID;

                #endregion

                // Call to service
                return this.Json(_service.CheckDelete(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CheckDeleteReport", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult DetailReport(string id)
        {
            try
            {
                #region " [ Declaration ] "

                Services.ReportService _service = new Services.ReportService();
                //
                ViewBag.id = id;

                #endregion

                #region " [ Main processing ] "

                // Call to service
                ReportModel _model = _service.GetItemByID(new ReportModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                ViewBag.reportData = _model;
                ViewBag.comments = _service.GetAllComment(new Guid(id), UserID);

                #endregion

                //
                return PartialView(new ReportCommentModel() { ReportID = _model.ID, Title = "" });
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "DetailReport", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveReportComment(ReportCommentModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.ReportService _service = new Services.ReportService();

                #endregion

                #region " [ Main processing ] "

                model.Title = "";
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                // Call to service
                return this.Json(_service.SaveComment(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "SaveReportComment", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion

    }
}