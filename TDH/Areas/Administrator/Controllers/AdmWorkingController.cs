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

namespace TDH.Areas.Administrator.Controllers
{
    public class AdmWorkingController : BaseController
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
                return View();
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
                Services.ReportService _service = new Services.ReportService();
                requestData = requestData.SetOrderingColumnName();
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<ReportModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<ReportModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
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
                ReportModel model = new ReportModel()
                {
                    ID = Guid.NewGuid(),
                    CreateBy = UserID,
                    Insert = true
                };
                return View(model);
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
                Services.ReportService _service = new Services.ReportService();
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
                    TempData[CommonHelper.EXECUTE_RESULT] = _result;
                    return RedirectToAction("Report");
                }
                return View();
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
                Services.ReportService _service = new Services.ReportService();
                ReportModel model = _service.GetItemByID(new ReportModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                return View(model);
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
                Services.ReportService _service = new Services.ReportService();
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
                    TempData[CommonHelper.EXECUTE_RESULT] = _result;
                    return RedirectToAction("Report");
                }
                return View();
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
                Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.Success,
                    Message = Resources.Message.Success
                };
                //
                Services.ReportService _service = new Services.ReportService();
                model.CreateBy = UserID;
                model.DeleteBy = UserID;
                model.DeleteDate = DateTime.Now;
                if (_service.Delete(model) == ResponseStatusCodeHelper.Success)
                {
                    return this.Json(_result, JsonRequestBehavior.AllowGet);
                }
                _result.Status = ResponseStatusCodeHelper.Error;
                _result.Message = Resources.Message.Error;
                return this.Json(_result, JsonRequestBehavior.AllowGet);
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
                Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.OK,
                    Message = ""
                };
                //
                Services.ReportService _service = new Services.ReportService();
                model.CreateBy = UserID;
                if (_service.CheckDelete(model) == ResponseStatusCodeHelper.OK)
                {
                    return this.Json(_result, JsonRequestBehavior.AllowGet);
                }
                _result.Status = ResponseStatusCodeHelper.NG;
                _result.Message = Resources.Message.CheckExists;
                return this.Json(_result, JsonRequestBehavior.AllowGet);
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
                Services.ReportService _service = new Services.ReportService();
                ReportModel _model = _service.GetItemByID(new ReportModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                ViewBag.reportData = _model;
                ViewBag.comments = _service.GetAllComment(new Guid(id), UserID);
                return View(new ReportCommentModel() { ReportID = _model.ID, Title = "" });
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Detail", UserID, ex);
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
                Services.ReportService _service = new Services.ReportService();
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                if (_service.SaveComment(model) == ResponseStatusCodeHelper.Success)
                {
                    Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                    {
                        Status = ResponseStatusCodeHelper.Success,
                        Message = Resources.Message.Success
                    };
                    TempData[CommonHelper.EXECUTE_RESULT] = _result;
                    return RedirectToAction("DetailReport", new { @id = model.ReportID });
                }
                return View();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "EditReport", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion

    }
}