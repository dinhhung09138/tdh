using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Utils;
using Utils.JqueryDatatable;
using TDH.Areas.Administrator.Models;
using TDH.Areas.Administrator.Filters;


namespace TDH.Areas.Administrator.Controllers
{
    /// <summary>
    /// System controller
    /// </summary>
    [AjaxExecuteFilterAttribute]
    public class AdmSystemController : BaseController
    {

        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator.Controllers/AdmSystemController.cs";

        /// <summary>
        /// ResultModel message success model
        /// </summary>
        private Utils.CommonModel.ExecuteResultModel ResultModel = new Utils.CommonModel.ExecuteResultModel()
        {
            Status = ResponseStatusCodeHelper.Success,
            Message = Resources.Message.Success
        };
        
        #endregion

        #region " [ Role ]  "

        /// <summary>
        /// Role form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Role()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Role", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public JsonResult Role(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                Services.RoleService _service = new Services.RoleService();

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
                //
                return this.Json(new DataTableResponse<RoleModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Role", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult CreateRole()
        {
            try
            {
                #region " [ Declaration ] "

                Services.RoleService _service = new Services.RoleService();

                #endregion

                //Call to service
                RoleModel model = _service.GetItemByID(new RoleModel() { ID = Guid.NewGuid(), CreateBy = UserID, Insert = true });
                //
                return PartialView(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateRole", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole(RoleModel model, FormCollection fc)
        {
            try
            {
                #region " [ Declaration ] "

                Services.RoleService _service = new Services.RoleService();

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
                if (_service.Save(model) != ResponseStatusCodeHelper.Success)
                {
                    ResultModel.Status = ResponseStatusCodeHelper.Error;
                    ResultModel.Message = Resources.Message.Error;
                }
                //
                return this.Json(ResultModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateRole", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult EditRole(string id)
        {
            try
            {
                #region " [ Declaration ] "

                Services.RoleService _service = new Services.RoleService();
                //
                ViewBag.id = id;

                #endregion

                //Call to service
                RoleModel model = _service.GetItemByID(new RoleModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                //
                return PartialView(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "EditRole", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(RoleModel model, FormCollection fc)
        {
            try
            {
                #region " [ Declaration ] "

                Services.RoleService _service = new Services.RoleService();

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
                if (_service.Save(model) != ResponseStatusCodeHelper.Success)
                {
                    ResultModel.Status = ResponseStatusCodeHelper.Error;
                    ResultModel.Message = Resources.Message.Error;
                }
                //
                return this.Json(ResultModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "EditRole", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult PublishRole(RoleModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.RoleService _service = new Services.RoleService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.UpdateDate = DateTime.Now;

                #endregion

                //Call to service
                if (_service.Publish(model) != ResponseStatusCodeHelper.Success)
                {
                    ResultModel.Status = ResponseStatusCodeHelper.Error;
                    ResultModel.Message = Resources.Message.Error;
                }
                //
                return this.Json(ResultModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "PublishRole", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult DeleteRole(RoleModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.RoleService _service = new Services.RoleService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;
                model.DeleteBy = UserID;
                model.DeleteDate = DateTime.Now;

                #endregion

                //Call to service
                if (_service.Delete(model) != ResponseStatusCodeHelper.Success)
                {
                    ResultModel.Status = ResponseStatusCodeHelper.Error;
                    ResultModel.Message = Resources.Message.Error;
                }
                //
                return this.Json(ResultModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "DeleteRole", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult CheckDeleteRole(RoleModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.RoleService _service = new Services.RoleService();
                //
                ResultModel = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.OK,
                    Message = ""
                };

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;

                #endregion

                //Call to service
                if (_service.CheckDelete(model) != ResponseStatusCodeHelper.OK)
                {
                    ResultModel.Status = ResponseStatusCodeHelper.NG;
                    ResultModel.Message = Resources.Message.CheckExists;
                }
                //
                return this.Json(ResultModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CheckDeleteRole", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion
        
        #region " [ User ]  "

        [HttpGet]
        public new ActionResult User()
        {
            try
            {
                #region " [ Declaration ] "

                ViewBag.currentID = UserID;

                #endregion
                //
                return PartialView();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "User", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public new JsonResult User(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                Services.UserService _service = new Services.UserService();
               
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
                //
                return this.Json(new DataTableResponse<UserModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "User", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            try
            {
                #region " [ Declaration ] "

                Services.RoleService _rService = new Services.RoleService();
                //
                ViewBag.Role = _rService.GetAll(UserID);
                UserModel model = new UserModel()
                {
                    ID = Guid.NewGuid(),
                    CreateBy = UserID,
                    Insert = true
                };
                ViewBag.currentID = UserID;

                #endregion

                //
                return PartialView(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateUser", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(UserModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.UserService _service = new Services.UserService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                //Call to service
                if (_service.Save(model) != ResponseStatusCodeHelper.Success)
                {
                    ResultModel.Status = ResponseStatusCodeHelper.Error;
                    ResultModel.Message = Resources.Message.Error;
                }
                //
                return this.Json(ResultModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateUser", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult EditUser(string id)
        {
            try
            {
                #region " [ Declaration ] "

                Services.RoleService _rService = new Services.RoleService();
                Services.UserService _service = new Services.UserService();
                //
                ViewBag.id = id;
                ViewBag.Role = _rService.GetAll(UserID);
                ViewBag.currentID = UserID;

                #endregion
                
                //Call to service
                UserModel model = _service.GetItemByID(new UserModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                //
                return PartialView(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "EditUser", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(UserModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.UserService _service = new Services.UserService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                //Call to service
                if (_service.Save(model) != ResponseStatusCodeHelper.Success)
                {
                    ResultModel.Status = ResponseStatusCodeHelper.Error;
                    ResultModel.Message = Resources.Message.Error;
                }
                //
                return this.Json(ResultModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "EditUser", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult PublishUser(UserModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.UserService _service = new Services.UserService();
                
                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.UpdateDate = DateTime.Now;

                #endregion

                //Call to service
                if (_service.Publish(model) != ResponseStatusCodeHelper.Success)
                {
                    ResultModel.Status = ResponseStatusCodeHelper.Error;
                    ResultModel.Message = Resources.Message.Error;
                }
                //
                return this.Json(ResultModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "PublishUser", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult DeleteUser(UserModel model)
        {
            try
            {
                #region " [ Declaration ] "

                Services.UserService _service = new Services.UserService();
                //
                ResultModel.Message = Resources.Message.Success;
               
                #endregion
               
                #region " [ Main process ] "

                model.CreateBy = UserID;
                model.DeleteBy = UserID;
                model.DeleteDate = DateTime.Now;

                #endregion

                //Call to service
                if (_service.Delete(model) != ResponseStatusCodeHelper.Success)
                {
                    ResultModel.Status = ResponseStatusCodeHelper.Error;
                    ResultModel.Message = Resources.Message.Error;
                }
                //
                return this.Json(ResultModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "DeleteUser", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion
        

    }
}