using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.Fillters;
using TDH.Model.Money;
using TDH.Services.Money;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Areas.Money.Controllers
{
    /// <summary>
    /// Group controller
    /// </summary>
    public class MNGroupController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Money.Controllers/MNGroupController.cs";

        #endregion

        /// <summary>
        /// Group form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Index", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Group form
        /// Post method
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns>DataTableResponse<GroupModel></returns>
        [HttpPost]
        public JsonResult Index(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _service = new GroupService();

                #endregion

                #region " [ Main processing ] "

                if (requestData.Parameter1 == null) // By type (income or payment)
                {
                    requestData.Parameter1 = "";
                }
                if (requestData.Parameter2 == null) // By year month
                {
                    requestData.Parameter2 = DateTime.Now.ToString("yyyyMM");
                }
                // Process sorting column
                requestData = requestData.SetOrderingColumnName();

                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                //
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<GroupModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<GroupModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<GroupModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Index", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Create Group form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _service = new GroupService();

                #endregion

                //Call to service
                GroupModel model = new GroupModel() { ID = Guid.NewGuid(), CreateBy = UserID, Insert = true };
                //
                return View(model);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Create", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Create Group form
        /// Post method
        /// </summary>
        /// <param name="model">GroupModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GroupModel model)
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _service = new GroupService();

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
                Log.WriteLog(FILE_NAME, "Create", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Edit group form
        /// </summary>
        /// <param name="id">The group identifier</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Edit(string id)
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _service = new GroupService();
                //
                ViewBag.id = id;

                #endregion

                //Call to service
                GroupModel model = _service.GetItemByID(new GroupModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                //
                return View(model);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Edit", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// All group data for setting form in a month
        /// </summary>
        /// <param name="year">Year</param>
        /// <returns>List<GroupSettingModel></returns>
        [HttpPost]
        public ActionResult GetGroupSettingInfo(decimal year)
        {
            try
            {
                #region " [ Declaration ] "

                GroupSettingService _service = new GroupSettingService();

                #endregion

                //Call to service
                List<GroupSettingModel> model = _service.GetAll(UserID, year);
                //
                return this.Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "GroupSettingInfo", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// All group data for setting form in a month
        /// Post method
        /// </summary>
        /// <param name="model">List<GroupSettingModel></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult SaveGroupSettingInfo(List<GroupSettingModel> model)
        {
            try
            {
                #region " [ Declaration ] "

                GroupSettingService _service = new GroupSettingService();

                #endregion

                #region " [ Main processing ] "

                if (model.Count == 0)
                {
                    return this.Json(ResponseStatusCodeHelper.OK, JsonRequestBehavior.AllowGet);
                }
                //
                model[0].CreateBy = UserID;
                model[0].UpdateBy = UserID;

                #endregion

                //Call to service
                return this.Json(_service.Save(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "GroupSettingInfo", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Edit group form
        /// Post method
        /// </summary>
        /// <param name="model">GroupModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GroupModel model)
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _service = new GroupService();

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
                Log.WriteLog(FILE_NAME, "Edit", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Publish Group method
        /// </summary>
        /// <param name="model">GroupModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult Publish(GroupModel model)
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _service = new GroupService();

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
        /// Delete Group method
        /// </summary>
        /// <param name="model">GroupModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult Delete(GroupModel model)
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _service = new GroupService();

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
        /// Check delete Group method
        /// </summary>
        /// <param name="model">GroupModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult CheckDelete(GroupModel model)
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _service = new GroupService();

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