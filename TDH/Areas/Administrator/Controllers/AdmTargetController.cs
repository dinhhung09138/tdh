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
                //
                Services.TargetService _service = new Services.TargetService();
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.UpdateDate = DateTime.Now;
                //
                return this.Json(_service.Save(model), JsonRequestBehavior.AllowGet);
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
        
        #region " [ Idea ] "

        [HttpGet]
        public ActionResult Idea()
        {
            try
            {
                return PartialView();
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
                #region " [ Declaration ] "

                Services.IdeaService _service = new Services.IdeaService();

                #endregion

                #region " [ Main processing ] "

                // Process sorting column
                requestData = requestData.SetOrderingColumnName();

                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<IdeaModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<IdeaModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
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
                #region " [ Declaration ] "

                IdeaModel model = new IdeaModel()
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
                #region " [ Declaration ] "

                Services.IdeaService _service = new Services.IdeaService();

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
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateIdea", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult EditIdea(string id)
        {
            try
            {
                #region " [ Declaration ] "

                Services.IdeaService _service = new Services.IdeaService();
                //
                ViewBag.id = id;

                #endregion

                // Call to service
                IdeaModel model = _service.GetItemByID(new IdeaModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                return PartialView(model);
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
                #region " [ Declaration ] "

                Services.IdeaService _service = new Services.IdeaService();

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
                TDH.Services.Log.WriteLog(FILE_NAME, "EditIdea", UserID, ex);
                throw new HttpException();
            }
        }
        
        [HttpPost]
        public ActionResult DeleteIdea(IdeaModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.IdeaService _service = new Services.IdeaService();

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
                TDH.Services.Log.WriteLog(FILE_NAME, "DeleteIdea", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult CheckDeleteIdea(IdeaModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.IdeaService _service = new Services.IdeaService();
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
                TDH.Services.Log.WriteLog(FILE_NAME, "CheckDeleteIdea", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult DetailIdea(string id)
        {
            try
            {
                #region " [ Declaration ] "

                Services.IdeaService _service = new Services.IdeaService();
                //
                ViewBag.id = id;

                #endregion

                #region " [ Main processing ] "

                // Call to service
                IdeaModel _model = _service.GetItemByID(new IdeaModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                ViewBag.ideaModel = _model;

                #endregion

                //
                return PartialView(new IdeaDetailModel() { IdeaID = _model.ID });
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