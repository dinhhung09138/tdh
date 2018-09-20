using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.UserException;
using TDH.Model.System;
using TDH.Services.System;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Areas.System.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    public class STUserController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "System.Controllers/STUserController.cs";

        #endregion

        /// <summary>
        /// User form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                #region " [ Declaration ] "

                ViewBag.currentID = UserID;

                #endregion

                return PartialView();
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
        /// User form
        /// Post method
        /// </summary>
        /// <param name="requestData">jquery datatable request</param>
        /// <returns>DataTableResponse<UserModel></returns>
        [HttpPost]
        public JsonResult Index(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                UserService _service = new UserService();

                #endregion

                #region " [ Main processing ] "

                // Process sorting column
                requestData = requestData.SetOrderingColumnName();

                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<UserModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<UserModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }

                return this.Json(new DataTableResponse<UserModel>(), JsonRequestBehavior.AllowGet);
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
        /// Create user form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                #region " [ Declaration ] "

                RoleService _rService = new RoleService();

                ViewBag.Role = _rService.GetAll(UserID);
                UserModel model = new UserModel()
                {
                    ID = Guid.NewGuid(),
                    CreateBy = UserID,
                    Insert = true
                };
                ViewBag.currentID = UserID;

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
        /// Create user form
        /// Post method
        /// </summary>
        /// <param name="model">User model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserModel model)
        {
            try
            {
                #region " [ Declaration ] "

                UserService _service = new UserService();

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
        /// Edit user form
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Edit(string id)
        {
            try
            {
                #region " [ Declaration ] "

                RoleService _rService = new RoleService();
                UserService _service = new UserService();

                ViewBag.id = id;
                ViewBag.Role = _rService.GetAll(UserID);
                ViewBag.currentID = UserID;

                #endregion

                //Call to service
                UserModel model = _service.GetItemByID(new UserModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });

                return PartialView(model);
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
        /// Edit user form
        /// Post method
        /// </summary>
        /// <param name="model">User model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserModel model)
        {
            try
            {
                #region " [ Declaration ] "

                UserService _service = new UserService();

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
        /// Publish user
        /// </summary>
        /// <param name="model">User model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult Publish(UserModel model)
        {
            try
            {
                #region " [ Declaration ] "

                UserService _service = new UserService();

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
        /// Delete user
        /// </summary>
        /// <param name="model">User model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult Delete(UserModel model)
        {
            try
            {
                #region " [ Declaration ] "

                UserService _service = new UserService();

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