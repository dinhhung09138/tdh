using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.UserException;
using TDH.Model.Personal;
using TDH.Services.Personal;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Areas.Personal.Controllers
{
    /// <summary>
    /// Cetificate controller
    /// </summary>
    public class PNCetificateController : BaseController
    {

        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Personal.Controllers/PNCetificateController.cs";

        #endregion

        /// <summary>
        /// List of cetificate form
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
        /// List of cetificate function
        /// Post method
        /// </summary>
        /// <param name="requestData">Jquery datatable request</param>
        /// <returns>DataTableResponse<CetificateModel></returns>
        [HttpPost]
        public JsonResult Index(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                CetificateService _service = new CetificateService();

                #endregion

                #region " [ Main processing ] "

                requestData = requestData.SetOrderingColumnName();

                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<CetificateModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<CetificateModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<CetificateModel>(), JsonRequestBehavior.AllowGet);
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
        /// Create form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                #region " [ Declaration ] "

                CetificateModel model = new CetificateModel()
                {
                    ID = Guid.NewGuid(),
                    CreateBy = UserID,
                    Insert = true
                };

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
        /// Create function
        /// </summary>
        /// <param name="model">CetificateModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(CetificateModel model)
        {
            try
            {
                #region " [ Declaration ] "

                CetificateService _service = new CetificateService();

                #endregion

                #region " [ Main processing ] "

                model.Publish = true;
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                // Call to service
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
        /// Edit form
        /// </summary>
        /// <param name="id">The cetificate identifier</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Edit(string id)
        {
            try
            {
                #region " [ Declaration ] "

                CetificateService _service = new CetificateService();

                ViewBag.id = id;

                #endregion

                // Call to service
                CetificateModel model = _service.GetItemByID(new CetificateModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });

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
        /// Edit function
        /// </summary>
        /// <param name="model">CetificateModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(CetificateModel model)
        {
            try
            {
                #region " [ Declaration ] "

                CetificateService _service = new CetificateService();

                #endregion

                #region " [ Main processing ] "

                model.Publish = true;
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                // Call to service
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
        /// Delete function
        /// </summary>
        /// <param name="model">CetificateModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public JsonResult Delete(CetificateModel model)
        {
            try
            {
                #region " [ Declaration ] "

                CetificateService _service = new CetificateService();

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

    }
}