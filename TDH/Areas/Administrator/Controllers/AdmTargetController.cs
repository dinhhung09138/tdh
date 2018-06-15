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
    public class AdmTargetController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator.Controllers/AdmTargetController.cs";

        #endregion

        public ActionResult OverView()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "OverView", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult SaveTarget(TargetModel model)
        {
            try
            {
                return this.Json("", JsonRequestBehavior.AllowGet);
                Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.Success,
                    Message = Resources.Message.Success
                };
                //
                Services.TargetService _service = new Services.TargetService();
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.UpdateDate = DateTime.Now;
                if (_service.Save(model) == ResponseStatusCodeHelper.Success)
                {
                    return this.Json(_result, JsonRequestBehavior.AllowGet);
                }
                _result.Status = ResponseStatusCodeHelper.Error;
                _result.Message = Resources.Message.Error;
                return this.Json(_result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "SaveTarget", UserID, ex);
                throw new HttpException();
            }
        }

        public ActionResult Dashboard()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Dashboard", UserID, ex);
                throw new HttpException();
            }
        }

        public ActionResult DailyTask()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "DailyTask", UserID, ex);
                throw new HttpException();
            }
        }

        public async Task Send()
        {
            var hubConnection = new HubConnection("http://localhost:53542");
            IHubProxy notifyHub = hubConnection.CreateHubProxy("SignalrHub");
            hubConnection.Start().Wait();
            await notifyHub.Invoke("SendSignalRTestNotification", UserID);
        }


        #region " [ Idea ] "

        [HttpGet]
        public ActionResult Idea()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Idea", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public JsonResult Idea(CustomDataTableRequestHelper requestData)
        {
            try
            {
                Services.IdeaService _service = new Services.IdeaService();
                requestData = requestData.SetOrderingColumnName();
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<IdeaModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<IdeaModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                return this.Json(new DataTableResponse<IdeaModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Idea", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult CreateIdea()
        {
            try
            {
                IdeaModel model = new IdeaModel()
                {
                    ID = Guid.NewGuid(),
                    CreateBy = UserID,
                    Insert = true
                };
                return View(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateIdea", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateIdea(IdeaModel model)
        {
            try
            {
                Services.IdeaService _service = new Services.IdeaService();
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
                    return RedirectToAction("Idea");
                }
                return View();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateIdea", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult EditIdea(string id)
        {
            try
            {
                Services.IdeaService _service = new Services.IdeaService();
                IdeaModel model = _service.GetItemByID(new IdeaModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                return View(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "EditIdea", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditIdea(IdeaModel model)
        {
            try
            {
                Services.IdeaService _service = new Services.IdeaService();
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
                    return RedirectToAction("Idea");
                }
                return View();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "EditIdea", UserID, ex);
                throw new HttpException();
            }
        }
        
        [HttpPost]
        public ActionResult DeleteIdea(IdeaModel model)
        {
            try
            {
                Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.Success,
                    Message = Resources.Message.Success
                };
                //
                Services.IdeaService _service = new Services.IdeaService();
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
                TDH.Services.Log.WriteLog(FILE_NAME, "DeleteIdea", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult CheckDeleteIdea(IdeaModel model)
        {
            try
            {
                Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.OK,
                    Message = ""
                };
                //
                Services.IdeaService _service = new Services.IdeaService();
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
                TDH.Services.Log.WriteLog(FILE_NAME, "CheckDeleteIdea", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult DetailIdea(string id)
        {
            try
            {
                Services.IdeaService _service = new Services.IdeaService();
                IdeaModel _model = _service.GetItemByID(new IdeaModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                ViewBag.ideaModel = _model;
                return View(new IdeaDetailModel() { IdeaID = _model.ID });
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "DetailIdea", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion

    }
}