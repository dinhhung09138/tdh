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
    public class WNewsController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Website.Controllers/WNewsController.cs";

        #endregion

        /// <summary>
        /// News form
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
        /// News form
        /// Post method
        /// </summary>
        /// <param name="requestData">Jquery datatable request</param>
        /// <returns>DataTableResponse<PostModel></returns>
        [HttpPost]
        public JsonResult Index(CustomDataTableRequestHelper requestData)
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
                Log.WriteLog(FILE_NAME, "Index", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Create news form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Create()
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
                Log.WriteLog(FILE_NAME, "Create", UserID, ex);
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
        public ActionResult Create(PostModel model)
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
                Log.WriteLog(FILE_NAME, "Create", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Edit news form
        /// </summary>
        /// <param name="id">The news identifier</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Edit(string id)
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
                Log.WriteLog(FILE_NAME, "Edit", UserID, ex);
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
        public ActionResult Edit(PostModel model)
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
                Log.WriteLog(FILE_NAME, "Edit", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Publish news
        /// </summary>
        /// <param name="model">Post model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult Publish(PostModel model)
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
                Log.WriteLog(FILE_NAME, "Publish", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Delete news
        /// </summary>
        /// <param name="model">Post model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult Delete(PostModel model)
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
                Log.WriteLog(FILE_NAME, "Delete", UserID, ex);
                throw new HttpException();
            }
        }

    }
}