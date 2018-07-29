using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.Fillters;
using TDH.Model.Website;
using TDH.Services.Website;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Areas.Administrator.Controllers
{
    /// <summary>
    /// Post controller
    /// </summary>
    [AjaxExecuteFilterAttribute]
    public class AdmPostController : TDH.Common.BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator.Controllers/AdmPostController.cs";

        #endregion

        /// <summary>
        /// Defalt view when user access
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            return PartialView();
        }

        #region " [ Navigation ] "

        /// <summary>
        /// Navigation form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Navigation()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Navigation", UserID, ex);
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
        public JsonResult Navigation(CustomDataTableRequestHelper requestData)
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
                Log.WriteLog(FILE_NAME, "Navigation", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Create category form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult CreateNavigation()
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
                Log.WriteLog(FILE_NAME, "CreateNavigation", UserID, ex);
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
        public ActionResult CreateNavigation(NavigationModel model)
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
                Log.WriteLog(FILE_NAME, "CreateNavigation", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Edit navigation
        /// </summary>
        /// <param name="id">Navigation identifier</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult EditNavigation(string id)
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
                Log.WriteLog(FILE_NAME, "EditNavigation", UserID, ex);
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
        public ActionResult EditNavigation(NavigationModel model)
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
                Log.WriteLog(FILE_NAME, "EditNavigation", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Publish navigation
        /// </summary>
        /// <param name="model">Navigation model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult PublishNavigation(NavigationModel model)
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
                Log.WriteLog(FILE_NAME, "PublishNavigation", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Delete navigation
        /// </summary>
        /// <param name="model">Navigation model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult DeleteNavigation(NavigationModel model)
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
                Log.WriteLog(FILE_NAME, "DeleteNavigation", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Check delete navigation
        /// </summary>
        /// <param name="model">Navigation model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult CheckDeleteNavigation(NavigationModel model)
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
                Log.WriteLog(FILE_NAME, "CheckDeleteNavigation", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion

        #region " [ Category ] "

        /// <summary>
        /// Category form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Category()
        {
            try
            {
                #region " [ Declaration ] "

                NavigationService _nServices = new NavigationService();
                ViewBag.navigation = _nServices.GetAll(UserID);

                #endregion
                
                return PartialView();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Category", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Category form
        /// Post method
        /// </summary>
        /// <param name="requestData">jquery datatable request</param>
        /// <returns>DataTableResponse<CategoryModel></returns>
        [HttpPost]
        public JsonResult Category(CustomDataTableRequestHelper requestData)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Category", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Create category form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult CreateCategory()
        {
            try
            {
                #region " [ Declaration ] "

                NavigationService _nServices = new NavigationService();
                
                ViewBag.navigation = _nServices.GetAll(UserID);
                CategoryModel model = new CategoryModel()
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
                Log.WriteLog(FILE_NAME, "CreateCategory", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Create category form
        /// Post method
        /// </summary>
        /// <param name="model">Category model</param>
        /// <returns>DataTableResponse<CategoryModel></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CategoryModel model)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "CreateCategory", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Edit category form
        /// </summary>
        /// <param name="id">Category identifier</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult EditCategory(string id)
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
                
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "EditCategory", UserID, ex);
                throw new HttpException();
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
        public ActionResult EditCategory(CategoryModel model)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "EditCategory", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Publish caterogy
        /// </summary>
        /// <param name="model">Category model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult PublishCategory(CategoryModel model)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "PublishCategory", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Set category show on navigation
        /// </summary>
        /// <param name="model">Category model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult OnNavigationCategory(CategoryModel model)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "OnNavigationCategory", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Delete caterogy
        /// </summary>
        /// <param name="model">Category model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult DeleteCategory(CategoryModel model)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "DeleteCategory", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Check delete caterogy
        /// </summary>
        /// <param name="model">Category model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult CheckDeleteCategory(CategoryModel model)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "CheckDeleteCategory", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion

        #region " [ Post ] "

        /// <summary>
        /// News form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult News()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "News", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// News form
        /// Post method
        /// </summary>
        /// <param name="requestData">Jquery datatable request</param>
        /// <returns>DataTableResponse<PostModel></returns>
        [HttpPost]
        public JsonResult News(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                PostService _service = new PostService();

                #endregion

                #region " [ Main processing ] "

                // Process sorting column
                requestData = requestData.SetOrderingColumnName();

                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<PostModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<PostModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                
                return this.Json(new DataTableResponse<PostModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "News", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Create news form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult CreateNews()
        {
            try
            {
                #region " [ Declaration ] "

                NavigationService _nServices = new NavigationService();
                
                ViewBag.navigation = _nServices.GetAll(UserID);
                CategoryService _cServices = new CategoryService();
                ViewBag.cate = _cServices.GetAll(UserID);
                PostModel model = new PostModel()
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
                Log.WriteLog(FILE_NAME, "CreateNews", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Create news form
        /// Post method
        /// </summary>
        /// <param name="model">Post model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateNews(PostModel model)
        {
            try
            {
                #region " [ Declaration ] "

                PostService _service = new PostService();

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
                Log.WriteLog(FILE_NAME, "CreateNews", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Edit news form
        /// </summary>
        /// <param name="id">The news identifier</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult EditNews(string id)
        {
            try
            {
                #region " [ Declaration ] "

                NavigationService _nServices = new NavigationService();
                CategoryService _cServices = new CategoryService();
                PostService _service = new PostService();
                
                ViewBag.id = id;
                ViewBag.navigation = _nServices.GetAll(UserID);
                ViewBag.cate = _cServices.GetAll(UserID);

                #endregion

                //Call to service
                PostModel model = _service.GetItemByID(new PostModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "EditNews", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Edit news form
        /// Post method
        /// </summary>
        /// <param name="model">Post model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditNews(PostModel model)
        {
            try
            {
                #region " [ Declaration ] "

                PostService _service = new PostService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;
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
                Log.WriteLog(FILE_NAME, "EditNews", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Publish news
        /// </summary>
        /// <param name="model">Post model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult PublishNews(PostModel model)
        {
            try
            {
                #region " [ Declaration ] "

                PostService _service = new PostService();

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
                Log.WriteLog(FILE_NAME, "PublishNews", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Delete news
        /// </summary>
        /// <param name="model">Post model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult DeleteNews(PostModel model)
        {
            try
            {
                #region " [ Declaration ] "

                PostService _service = new PostService();

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
                Log.WriteLog(FILE_NAME, "DeleteNews", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion

        #region " [ About ] "

        /// <summary>
        /// About form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult About()
        {
            try
            {
                #region " [ Declaration ] "

                AboutService _service = new AboutService();
                var _model = _service.GetItemByID(new AboutModel() { CreateBy = UserID, Insert = false });

                #endregion
                
                return PartialView(_model);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "About", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// About form
        /// Post method
        /// </summary>
        /// <param name="model">About model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult About(AboutModel model)
        {
            try
            {
                #region " [ Declaration ] "

                AboutService _service = new AboutService();

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
                Log.WriteLog(FILE_NAME, "About", UserID, ex);
                throw new HttpException();
            }
        }
        
        #endregion

    }
}
