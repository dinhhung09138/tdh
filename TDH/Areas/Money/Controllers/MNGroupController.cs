using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.UserException;
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
                throw new ControllerException(FILE_NAME, "Index", UserID, ex);
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
                throw new ControllerException(FILE_NAME, "Index", UserID, ex);
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
                throw new ControllerException(FILE_NAME, "Create", UserID, ex);
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
        public JsonResult Create(GroupModel model)
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
                throw new ControllerException(FILE_NAME, "Create", UserID, ex);
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
                throw new ControllerException(FILE_NAME, "Edit", UserID, ex);
            }
        }

        /// <summary>
        /// All group data for setting form in a month
        /// </summary>
        /// <param name="year">Year</param>
        /// <returns>List<GroupSettingModel></returns>
        [HttpPost]
        public JsonResult GetGroupSettingInfo(decimal year)
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
                throw new ControllerException(FILE_NAME, "GetGroupSettingInfo", UserID, ex);
            }
        }

        /// <summary>
        /// All group data for setting form in a month
        /// Post method
        /// </summary>
        /// <param name="model">List<GroupSettingModel></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public JsonResult SaveGroupSettingInfo(List<GroupSettingModel> model)
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
                throw new ControllerException(FILE_NAME, "SaveGroupSettingInfo", UserID, ex);
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
        public JsonResult Edit(GroupModel model)
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
                throw new ControllerException(FILE_NAME, "Edit", UserID, ex);
            }
        }

        /// <summary>
        /// Publish Group method
        /// </summary>
        /// <param name="model">GroupModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public JsonResult Publish(GroupModel model)
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
                throw new ControllerException(FILE_NAME, "Publish", UserID, ex);
            }
        }

        /// <summary>
        /// Delete Group method
        /// </summary>
        /// <param name="model">GroupModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public JsonResult Delete(GroupModel model)
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
                throw new ControllerException(FILE_NAME, "Delete", UserID, ex);
            }
        }

        /// <summary>
        /// Check delete Group method
        /// </summary>
        /// <param name="model">GroupModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public JsonResult CheckDelete(GroupModel model)
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
                throw new ControllerException(FILE_NAME, "CheckDelete", UserID, ex);
            }
        }

    }
}