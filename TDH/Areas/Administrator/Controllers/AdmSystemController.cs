using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Utils;
using Utils.JqueryDatatable;
using TDH.Common;
using TDH.Services.System;
using TDH.Common.Fillters;
using TDH.Model.System;

namespace TDH.Areas.Administrator.Controllers
{
    /// <summary>
    /// System controller
    /// </summary>
    [AjaxExecuteFilterAttribute]
    public class AdmSystemController : TDH.Common.BaseController
    {

        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator.Controllers/AdmSystemController.cs";

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

        #region " [ Role ]  "

        /// <summary>
        /// Role form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Role()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Role", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Role form
        /// </summary>
        /// <param name="requestData">Jquery datatable request</param>
        /// <returns>DataTableResponse<RoleModel></returns>
        [HttpPost]
        public JsonResult Role(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                RoleService _service = new RoleService();

                #endregion

                #region " [ Main processing ] "

                // Process sorting column
                requestData = requestData.SetOrderingColumnName();

                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                //
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<RoleModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<RoleModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                
                return this.Json(new DataTableResponse<RoleModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Role", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Create role form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult CreateRole()
        {
            try
            {
                #region " [ Declaration ] "

                RoleService _service = new RoleService();

                #endregion

                //Call to service
                RoleModel model = _service.GetItemByID(new RoleModel() { ID = Guid.NewGuid(), CreateBy = UserID, Insert = true });
                
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "CreateRole", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Create role form
        /// Post method
        /// </summary>
        /// <param name="model">Role model</param>
        /// <param name="fc">Form collection</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole(RoleModel model, FormCollection fc)
        {
            try
            {
                #region " [ Declaration ] "

                RoleService _service = new RoleService();

                #endregion

                #region " [ Main processing ] "

                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #region " [ Permision procesing ] "

                var _lFunction = fc["functionCode"].ToString();
                var _arrFunction = _lFunction.Split(',');
                foreach (var code in _arrFunction)
                {
                    if (code.Length == 0)
                    {
                        continue;
                    }
                    RoleDetailModel _rolePerm = new RoleDetailModel() { FunctionCode = code };
                    if (fc["View_" + code] != null)
                    {
                        _rolePerm.View = true;
                    }
                    if (fc["Add_" + code] != null)
                    {
                        _rolePerm.Add = true;
                    }
                    if (fc["Edit_" + code] != null)
                    {
                        _rolePerm.Edit = true;
                    }
                    if (fc["Delete_" + code] != null)
                    {
                        _rolePerm.Delete = true;
                    }
                    model.Detail.Add(_rolePerm);
                }

                #endregion

                #endregion

                //Call to service
                return this.Json(_service.Save(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "CreateRole", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Edit role form
        /// </summary>
        /// <param name="id">Role identifier</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult EditRole(string id)
        {
            try
            {
                #region " [ Declaration ] "

                RoleService _service = new RoleService();
                
                ViewBag.id = id;

                #endregion

                //Call to service
                RoleModel model = _service.GetItemByID(new RoleModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                
                return PartialView(model);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "EditRole", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Edit role form
        /// Post method
        /// </summary>
        /// <param name="model">Role model</param>
        /// <param name="fc">Form collection</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(RoleModel model, FormCollection fc)
        {
            try
            {
                #region " [ Declaration ] "

                RoleService _service = new RoleService();

                #endregion

                #region " [ Main processing ] "

                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #region " [ Permision ] "

                var _lFunction = fc["functionCode"].ToString();
                var _arrFunction = _lFunction.Split(',');
                foreach (var code in _arrFunction)
                {
                    if (code.Length == 0)
                    {
                        continue;
                    }
                    RoleDetailModel _rolePerm = new RoleDetailModel() { FunctionCode = code };
                    if (fc["View_" + code] != null)
                    {
                        _rolePerm.View = true;
                    }
                    if (fc["Add_" + code] != null)
                    {
                        _rolePerm.Add = true;
                    }
                    if (fc["Edit_" + code] != null)
                    {
                        _rolePerm.Edit = true;
                    }
                    if (fc["Delete_" + code] != null)
                    {
                        _rolePerm.Delete = true;
                    }
                    model.Detail.Add(_rolePerm);
                }

                #endregion

                #endregion

                //Call to service
                return this.Json(_service.Save(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "EditRole", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Publish role
        /// </summary>
        /// <param name="model">Role model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult PublishRole(RoleModel model)
        {
            try
            {
                #region " [ Declaration ] "

                RoleService _service = new RoleService();

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
                Log.WriteLog(FILE_NAME, "PublishRole", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Delete role
        /// </summary>
        /// <param name="model">Role model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult DeleteRole(RoleModel model)
        {
            try
            {
                #region " [ Declaration ] "

                RoleService _service = new RoleService();

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
                Log.WriteLog(FILE_NAME, "DeleteRole", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Check delete role
        /// </summary>
        /// <param name="model">Role model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult CheckDeleteRole(RoleModel model)
        {
            try
            {
                #region " [ Declaration ] "

                RoleService _service = new RoleService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;

                #endregion

                //Call to service
                return this.Json(_service.CheckDelete(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "CheckDeleteRole", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion
        
        #region " [ User ]  "

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

        #endregion

        #region " [ Error Log ] "

        /// <summary>
        /// Error log form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult ErrorLog()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "ErrorLog", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Error log form
        /// Post method
        /// </summary>
        /// <param name="requestData">Jquery datatable request</param>
        /// <returns>DataTableResponse<ErrorLogModel></returns>
        [HttpPost]
        public JsonResult ErrorLog(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                ErrorLogService _service = new ErrorLogService();

                #endregion

                #region " [ Main processing ] "
                
                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<ErrorLogModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<ErrorLogModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                
                return this.Json(new DataTableResponse<ErrorLogModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "ErrorLog", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Detail error log
        /// </summary>
        /// <param name="id">log identifier</param>
        /// <returns>View</returns>
        [HttpPost]
        public JsonResult DetailErroLog(string id)
        {
            try
            {
                #region " [ Declaration ] "

                ErrorLogService _service = new ErrorLogService();
                //
                ViewBag.id = id;

                #endregion

                //Call to service
                ErrorLogModel model = _service.GetItemByID(new ErrorLogModel() { ID = new Guid(id), CreateBy = UserID });
                
                return this.Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "DetailErroLog", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion
        
    }
}