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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Index", UserID, ex);
                throw new HttpException();
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Index", UserID, ex);
                throw new HttpException();
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "History", UserID, ex);
                throw new HttpException();
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "History", UserID, ex);
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Create", UserID, ex);
                throw new HttpException();
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
        public ActionResult Create(CategoryModel model)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Create", UserID, ex);
                throw new HttpException();
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Edit", UserID, ex);
                throw new HttpException();
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
        public ActionResult Edit(CategoryModel model)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Edit", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Publish category method
        /// </summary>
        /// <param name="model">CategoryModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult Publish(CategoryModel model)
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
                Log.WriteLog(FILE_NAME, "Publish", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Delete category method
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult Delete(CategoryModel model)
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
                Log.WriteLog(FILE_NAME, "Delete", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Check delete category method
        /// </summary>
        /// <param name="model">CategoryModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult CheckDelete(CategoryModel model)
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
                Log.WriteLog(FILE_NAME, "CheckDelete", UserID, ex);
                throw new HttpException();
            }
        }

    }
}