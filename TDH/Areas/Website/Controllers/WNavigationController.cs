using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDH.Common;
using TDH.Model.Website;
using TDH.Services.Website;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Areas.Website.Controllers
{
    /// <summary>
    /// Navigation controller
    /// </summary>
    public class WNavigationController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Website.Controllers/WNavigationController.cs";

        #endregion

        [AllowAnonymous]
        public ActionResult Home()
        {
            return View();
        }

        /// <summary>
        /// Navigation form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Index", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Navigation form
        /// Post method
        /// </summary>
        /// <param name="requestData">Jquery datatable request</param>
        /// <returns>DataTableResponse<NavigationModel></returns>
        [HttpPost]
        public JsonResult Index(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                NavigationService _service = new NavigationService();

                #endregion

                #region " [ Main processing ] "

                // Process sorting column
                requestData = requestData.SetOrderingColumnName();

                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<NavigationModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<NavigationModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<NavigationModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Index", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Create category form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                #region " [ Declaration ] "

                NavigationModel model = new NavigationModel()
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
                Log.WriteLog(FILE_NAME, "Create", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Create category form
        /// Post method
        /// </summary>
        /// <param name="model">Navigation model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NavigationModel model)
        {
            try
            {
                #region " [ Declaration ] "

                NavigationService _service = new NavigationService();

                #endregion

                #region " [ Main process ] "

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
                Log.WriteLog(FILE_NAME, "Create", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Edit navigation
        /// </summary>
        /// <param name="id">Navigation identifier</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Edit(string id)
        {
            ViewBag.id = id;
            try
            {
                #region " [ Declaration ] "

                NavigationService _service = new NavigationService();
                
                ViewBag.id = id;

                #endregion

                //Call to service
                NavigationModel model = _service.GetItemByID(new NavigationModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Edit", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Edit navigation
        /// Post method
        /// </summary>
        /// <param name="model">Navigation model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NavigationModel model)
        {
            try
            {
                #region " [ Declaration ] "

                NavigationService _service = new NavigationService();

                #endregion

                #region " [ Main process ] "

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
                Log.WriteLog(FILE_NAME, "Edit", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Publish navigation
        /// </summary>
        /// <param name="model">Navigation model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult Publish(NavigationModel model)
        {
            try
            {
                #region " [ Declaration ] "

                NavigationService _service = new NavigationService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.UpdateDate = DateTime.Now;

                #endregion

                //Call to service
                return this.Json(_service.Publish(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Publish", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Delete navigation
        /// </summary>
        /// <param name="model">Navigation model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult Delete(NavigationModel model)
        {
            try
            {
                #region " [ Declaration ] "

                NavigationService _service = new NavigationService();

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

        /// <summary>
        /// Check delete navigation
        /// </summary>
        /// <param name="model">Navigation model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult CheckDelete(NavigationModel model)
        {
            try
            {
                #region " [ Declaration ] "

                NavigationService _service = new NavigationService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;

                #endregion

                //Call to service
                return this.Json(_service.CheckDelete(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "CheckDelete", UserID, ex);
                throw new HttpException();
            }
        }

    }
}