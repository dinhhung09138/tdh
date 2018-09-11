using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDH.Common;
using TDH.Model.Personal;
using TDH.Services.Personal;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Areas.Personal.Controllers
{
    /// <summary>
    /// Education controller
    /// </summary>
    public class PNEducationController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Personal.Controllers/PNEducationController.cs";

        #endregion


        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                EducationTypeService _typeService = new EducationTypeService();
                ViewBag.type = _typeService.GetAll(UserID);

                return View();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Index", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public JsonResult Index(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                EducationService _service = new EducationService();

                #endregion

                #region " [ Main processing ] "

                if(requestData.Parameter1 == null)
                {
                    requestData.Parameter1 = "";
                }
                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<EducationModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<EducationModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<EducationModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Index", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {

                #region " [ Declaration ] "

                EducationTypeService _typeService = new EducationTypeService();

                EducationModel model = new EducationModel()
                {
                    ID = Guid.NewGuid(),
                    CreateBy = UserID,
                    Insert = true
                };

                ViewBag.type = _typeService.GetAll(UserID);

                #endregion

                return View(model);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Create", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(EducationModel model)
        {
            try
            {
                #region " [ Declaration ] "

                EducationService _service = new EducationService();

                #endregion

                #region " [ Main processing ] "

                string[] tmp = model.DateString.Split('/');
                model.Date = new DateTime(int.Parse(tmp[2]), int.Parse(tmp[1]), int.Parse(tmp[0]));

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
                Log.WriteLog(FILE_NAME, "Create", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            try
            {
                #region " [ Declaration ] "

                EducationTypeService _typeService = new EducationTypeService();

                EducationService _service = new EducationService();
                //
                ViewBag.id = id;
                ViewBag.type = _typeService.GetAll(UserID);

                #endregion

                // Call to service
                EducationModel model = _service.GetItemByID(new EducationModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                return View(model);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Edit", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(EducationModel model)
        {
            try
            {
                #region " [ Declaration ] "

                EducationService _service = new EducationService();

                #endregion

                #region " [ Main processing ] "

                string[] tmp = model.DateString.Split('/');
                model.Date = new DateTime(int.Parse(tmp[2]), int.Parse(tmp[1]), int.Parse(tmp[0]));

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
                Log.WriteLog(FILE_NAME, "Edit", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult Delete(EducationModel model)
        {
            try
            {
                #region " [ Declaration ] "

                EducationService _service = new EducationService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;
                model.DeleteBy = UserID;
                model.DeleteDate = DateTime.Now;

                #endregion

                //Call to service
                return this.Json(_service.Delete(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Delete", UserID, ex);
                throw new HttpException();
            }
        }
        
    }
}