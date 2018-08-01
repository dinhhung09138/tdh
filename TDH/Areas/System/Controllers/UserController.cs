using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.Fillters;
using TDH.Model.System;
using TDH.Services.System;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Areas.System.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [AjaxExecuteFilterAttribute]
    public class UserController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "System.Controllers/UserController.cs";

        #endregion

        /// <summary>
        /// User form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public new ActionResult User()
        {
            try
            {
                #region " [ Declaration ] "

                ViewBag.currentID = UserID;

                #endregion

                return PartialView();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "User", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// User form
        /// Post method
        /// </summary>
        /// <param name="requestData">jquery datatable request</param>
        /// <returns>DataTableResponse<UserModel></returns>
        [HttpPost]
        public new JsonResult User(CustomDataTableRequestHelper requestData)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "User", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Create user form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult CreateUser()
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

                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "CreateUser", UserID, ex);
                throw new HttpException();
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
        public ActionResult CreateUser(UserModel model)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "CreateUser", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Edit user form
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult EditUser(string id)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "EditUser", UserID, ex);
                throw new HttpException();
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
        public ActionResult EditUser(UserModel model)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "EditUser", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Publish user
        /// </summary>
        /// <param name="model">User model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult PublishUser(UserModel model)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "PublishUser", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="model">User model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult DeleteUser(UserModel model)
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "DeleteUser", UserID, ex);
                throw new HttpException();
            }
        }

    }
}