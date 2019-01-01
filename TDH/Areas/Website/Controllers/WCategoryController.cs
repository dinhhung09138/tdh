using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.UserException;
using TDH.Model.Website;
using TDH.Services.Website;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Areas.Website.Controllers
{
    /// <summary>
    /// Category controller
    /// </summary>
    public class WCategoryController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Website.Controllers/WCategoryController.cs";

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

                NavigationService _nServices = new NavigationService();

                ViewBag.navigation = _nServices.GetAll(UserID);

                #endregion

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

                if (requestData.Parameter1 == null)
                {
                    requestData.Parameter1 = "";
                }
                // Process sorting column
                requestData = requestData.SetOrderingColumnName();

                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<CategoryModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<CategoryModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }

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
                throw new ControllerException(FILE_NAME, "Index", UserID, ex);
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

                NavigationService _nServices = new NavigationService();

                CategoryModel model = new CategoryModel()
                {
                    ID = Guid.NewGuid(),
                    CreateBy = UserID,
                    Insert = true
                };

                ViewBag.navigation = _nServices.GetAll(UserID);

                #endregion

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
        /// Create category form
        /// Post method
        /// </summary>
        /// <param name="model">Category model</param>
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

                #region " [ Main process ] "

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
        /// Edit category form
        /// </summary>
        /// <param name="id">Category identifier</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Edit(string id)
        {
            ViewBag.id = id;
            try
            {
                #region " [ Declaration ] "

                NavigationService _nServices = new NavigationService();
                CategoryService _service = new CategoryService();

                ViewBag.id = id;
                ViewBag.navigation = _nServices.GetAll(UserID);

                #endregion

                //Call to service
                CategoryModel model = _service.GetItemByID(new CategoryModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });

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
        /// Edit category form
        /// Post method
        /// </summary>
        /// <param name="model">Category model</param>
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

                #region " [ Main process ] "

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
        /// Publish caterogy
        /// </summary>
        /// <param name="model">Category model</param>
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
                throw new ControllerException(FILE_NAME, "Publish", UserID, ex);
            }
        }

        /// <summary>
        /// Set category show on navigation
        /// </summary>
        /// <param name="model">Category model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public JsonResult OnNavigation(CategoryModel model)
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
                return this.Json(_service.OnNavigation(model), JsonRequestBehavior.AllowGet);
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
                throw new ControllerException(FILE_NAME, "OnNavigation", UserID, ex);
            }
        }

        /// <summary>
        /// Delete caterogy
        /// </summary>
        /// <param name="model">Category model</param>
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
                throw new ControllerException(FILE_NAME, "Delete", UserID, ex);
            }
        }

        /// <summary>
        /// Check delete caterogy
        /// </summary>
        /// <param name="model">Category model</param>
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
                throw new ControllerException(FILE_NAME, "CheckDelete", UserID, ex);
            }
        }

    }
}