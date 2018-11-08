using System;
using System.Collections.Generic;
using System.Reflection;
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
    /// Category controller
    /// </summary>
    public class MNCategoryController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Money.Controllers/MNCategoryController.cs";

        #endregion

        /// <summary>
        /// Category form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _groupServices = new GroupService();
                //
                ViewBag.group = _groupServices.GetAll(UserID);

                #endregion
                //
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
        /// Category form
        /// Post method
        /// </summary>
        /// <param name="requestData">Jquery datatable request</param>
        /// <returns>DataTableResponse<CategoryModel></returns>
        [HttpPost]
        public JsonResult Index(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                CategoryService _service = new CategoryService();

                #endregion

                #region " [ Main processing ] "

                if (requestData.Parameter1 == null) // by group
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
                    DataTableResponse<CategoryModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<CategoryModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<CategoryModel>(), JsonRequestBehavior.AllowGet);
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
        /// All setting data by category in a month
        /// </summary>
        /// <param name="id">the category identifier</param>
        /// <param name="name">Category name</param>
        /// <param name="yearMonth">year month</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult History(string id, string yearMonth = "")
        {
            try
            {
                #region " [ Declaration ] "

                CategoryService _service = new CategoryService();

                CategoryModel _model = _service.GetItemByID(new CategoryModel() { ID = new Guid(id), CreateBy = UserID });

                #endregion

                ViewBag.cateID = id;
                ViewBag.name = _model.Name;
                ViewBag.yearMonth = DateTime.Now.ToString("yyyyMM");
                ViewBag.yearMonthValue = DateTime.Now.ToString("yyyy/MM");
                ViewBag.listAccount = "1"; //Back  to list account
                if (yearMonth != "")
                {
                    ViewBag.yearMonth = yearMonth;
                    ViewBag.yearMonthValue = yearMonth.Insert(4, "/");
                    ViewBag.listAccount = "0";//Back to edit account
                }
                //
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
        /// All setting data by category in a month
        /// Post method
        /// </summary>
        /// <param name="requestData">Jquery datatable request</param>
        /// <returns>DataTableResponse<CategoryHistoryModel></returns>
        [HttpPost]
        public JsonResult History(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                CategoryService _service = new CategoryService();

                #endregion

                #region " [ Main processing ] "

                if (requestData.Parameter1 == null)
                {
                    requestData.Parameter1 = "";
                }
                if (requestData.Parameter2 == null)
                {
                    requestData.Parameter2 = "";
                }

                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.GetHistory(requestData, UserID);
                //
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<CategoryHistoryModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<CategoryHistoryModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<CategoryHistoryModel>(), JsonRequestBehavior.AllowGet);
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
        /// Create category form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _groupServices = new GroupService();
                CategoryService _service = new CategoryService();
                //
                ViewBag.group = _groupServices.GetAll(UserID);

                #endregion
                //
                CategoryModel model = new CategoryModel() { ID = Guid.NewGuid(), CreateBy = UserID, Insert = true };
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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
            }
        }

        /// <summary>
        /// Create category form
        /// Post form
        /// </summary>
        /// <param name="model">CategoryModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(CategoryModel model)
        {
            try
            {
                #region " [ Declaration ] "

                CategoryService _service = new CategoryService();

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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
            }
        }

        /// <summary>
        /// Edit category form
        /// </summary>
        /// <param name="id">The category identifier</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpGet]
        public ActionResult Edit(string id)
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _groupServices = new GroupService();
                CategoryService _service = new CategoryService();
                //
                ViewBag.group = _groupServices.GetAll(UserID);

                ViewBag.id = id;

                #endregion

                //Call to service
                CategoryModel model = _service.GetItemByID(new CategoryModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
            }
        }

        /// <summary>
        /// Edit category form
        /// Post method
        /// </summary>
        /// <param name="model">CategoryModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(CategoryModel model)
        {
            try
            {
                #region " [ Declaration ] "

                CategoryService _service = new CategoryService();

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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
            }
        }

        /// <summary>
        /// Publish category method
        /// </summary>
        /// <param name="model">CategoryModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public JsonResult Publish(CategoryModel model)
        {
            try
            {
                #region " [ Declaration ] "

                CategoryService _service = new CategoryService();

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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
            }
        }

        /// <summary>
        /// Delete category method
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public JsonResult Delete(CategoryModel model)
        {
            try
            {
                #region " [ Declaration ] "

                CategoryService _service = new CategoryService();

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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
            }
        }

        /// <summary>
        /// Check delete category method
        /// </summary>
        /// <param name="model">CategoryModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public JsonResult CheckDelete(CategoryModel model)
        {
            try
            {
                #region " [ Declaration ] "

                CategoryService _service = new CategoryService();

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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
            }
        }

        #region " [ Setting ] "

        /// <summary>
        /// All setting data by category in a month
        /// </summary>
        /// <param name="id">the category identifier</param>
        /// <param name="yearMonth">Category name</param>
        /// <returns>View</returns>
        [HttpGet]
        public JsonResult Setting(string id, string year)
        {
            try
            {
                #region " [ Declaration ] "

                CategorySettingService _service = new CategorySettingService();

                List<CategorySettingModel> _model = _service.GetAll(new Guid(id), int.Parse(year), UserID);

                #endregion

                return this.Json(_model, JsonRequestBehavior.AllowGet);
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
        /// Save setting infor
        /// </summary>
        /// <param name="model">List of setting info model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public JsonResult SaveSetting(List<CategorySettingModel> model)
        {
            try
            {
                #region " [ Declaration ] "

                CategorySettingService _service = new CategorySettingService();

                #endregion

                return this.Json(_service.Save(model, UserID), JsonRequestBehavior.AllowGet);
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

        #endregion
    }
}