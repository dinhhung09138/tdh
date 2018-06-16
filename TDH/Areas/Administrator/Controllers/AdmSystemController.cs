using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Utils;
using Utils.JqueryDatatable;
using TDH.Areas.Administrator.Models;


namespace TDH.Areas.Administrator.Controllers
{
    public class AdmSystemController : BaseController
    {

        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator.Controllers/AdmSystemController.cs";

        #endregion

        #region " [ Role ]  "

        [HttpGet]
        public ActionResult Role()
        {
            try
            {
                return View();
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
                Services.RoleService _service = new Services.RoleService();
                requestData = requestData.SetOrderingColumnName();
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<RoleModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<RoleModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
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
                Services.RoleService _service = new Services.RoleService();
                RoleModel model = _service.GetItemByID(new RoleModel() { ID = Guid.NewGuid(), CreateBy = UserID, Insert = true });
                return View(model);
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
                Services.RoleService _service = new Services.RoleService();
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

                if (_service.Save(model) == ResponseStatusCodeHelper.Success)
                {
                    Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                    {
                        Status = ResponseStatusCodeHelper.Success,
                        Message = Resources.Message.Success
                    };
                    TempData[CommonHelper.EXECUTE_RESULT] = _result;
                    return RedirectToAction("Role");
                }
                return View();
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
                Services.RoleService _service = new Services.RoleService();
                RoleModel model = _service.GetItemByID(new RoleModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                return View(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateRole", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(RoleModel model, FormCollection fc)
        {
            try
            {
                Services.RoleService _service = new Services.RoleService();
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

                if (_service.Save(model) == ResponseStatusCodeHelper.Success)
                {
                    Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                    {
                        Status = ResponseStatusCodeHelper.Success,
                        Message = Resources.Message.Success
                    };
                    TempData[CommonHelper.EXECUTE_RESULT] = _result;
                    return RedirectToAction("Role");
                }
                return View();
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
                Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.Success,
                    Message = Resources.Message.Success
                };
                //
                Services.RoleService _service = new Services.RoleService();
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.UpdateDate = DateTime.Now;
                if (_service.Publish(model) == ResponseStatusCodeHelper.Success)
                {
                    return this.Json(_result, JsonRequestBehavior.AllowGet);
                }
                _result.Status = ResponseStatusCodeHelper.Error;
                _result.Message = Resources.Message.Error;
                return this.Json(_result, JsonRequestBehavior.AllowGet);
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
                Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.Success,
                    Message = Resources.Message.Success
                };
                //
                Services.RoleService _service = new Services.RoleService();
                model.CreateBy = UserID;
                model.DeleteBy = UserID;
                model.DeleteDate = DateTime.Now;
                if (_service.Delete(model) == ResponseStatusCodeHelper.Success)
                {
                    return this.Json(_result, JsonRequestBehavior.AllowGet);
                }
                _result.Status = ResponseStatusCodeHelper.Error;
                _result.Message = Resources.Message.Error;
                return this.Json(_result, JsonRequestBehavior.AllowGet);
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
                Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.OK,
                    Message = ""
                };
                //
                Services.RoleService _service = new Services.RoleService();
                model.CreateBy = UserID;
                if (_service.CheckDelete(model) == ResponseStatusCodeHelper.OK)
                {
                    return this.Json(_result, JsonRequestBehavior.AllowGet);
                }
                _result.Status = ResponseStatusCodeHelper.NG;
                _result.Message = Resources.Message.CheckExists;
                return this.Json(_result, JsonRequestBehavior.AllowGet);
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
                ViewBag.currentID = UserID;
                return View();
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
                Services.UserService _service = new Services.UserService();
                requestData = requestData.SetOrderingColumnName();
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
                TDH.Services.Log.WriteLog(FILE_NAME, "User", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            try
            {
                Services.RoleService _rService = new Services.RoleService();
                ViewBag.Role = _rService.GetAll(UserID);
                UserModel model = new UserModel()
                {
                    ID = Guid.NewGuid(),
                    CreateBy = UserID,
                    Insert = true
                };
                ViewBag.currentID = UserID;
                return View(model);
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
                Services.UserService _service = new Services.UserService();
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                if (_service.Save(model) == ResponseStatusCodeHelper.Success)
                {
                    Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                    {
                        Status = ResponseStatusCodeHelper.Success,
                        Message = Resources.Message.Success
                    };
                    TempData[CommonHelper.EXECUTE_RESULT] = _result;
                    return RedirectToAction("User");
                }
                return View();
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
                Services.RoleService _rService = new Services.RoleService();
                ViewBag.Role = _rService.GetAll(UserID);
                Services.UserService _service = new Services.UserService();
                UserModel model = _service.GetItemByID(new UserModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                ViewBag.currentID = UserID;
                return View(model);
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
                Services.UserService _service = new Services.UserService();
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                if (_service.Save(model) == ResponseStatusCodeHelper.Success)
                {
                    Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                    {
                        Status = ResponseStatusCodeHelper.Success,
                        Message = Resources.Message.Success
                    };
                    TempData[CommonHelper.EXECUTE_RESULT] = _result;
                    return RedirectToAction("User");
                }
                return View();
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
                Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.Success,
                    Message = Resources.Message.Success
                };
                //
                Services.UserService _service = new Services.UserService();
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.UpdateDate = DateTime.Now;
                if (_service.Publish(model) == ResponseStatusCodeHelper.Success)
                {
                    return this.Json(_result, JsonRequestBehavior.AllowGet);
                }
                _result.Status = ResponseStatusCodeHelper.Error;
                _result.Message = Resources.Message.Error;
                return this.Json(_result, JsonRequestBehavior.AllowGet);
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
                Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.Success,
                    Message = Resources.Message.Success
                };
                //
                Services.UserService _service = new Services.UserService();
                model.CreateBy = UserID;
                model.DeleteBy = UserID;
                model.DeleteDate = DateTime.Now;
                if (_service.Delete(model) == ResponseStatusCodeHelper.Success)
                {
                    return this.Json(_result, JsonRequestBehavior.AllowGet);
                }
                _result.Status = ResponseStatusCodeHelper.Error;
                _result.Message = Resources.Message.Error;
                return this.Json(_result, JsonRequestBehavior.AllowGet);
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