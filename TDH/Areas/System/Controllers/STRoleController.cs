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
    /// Role controller
    /// </summary>
    public class STRoleController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "System.Controllers/STRoleController.cs";

        #endregion

        [AllowAnonymous]
        public ActionResult Home()
        {
            return View();
        }

        /// <summary>
        /// Role form
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
        /// Role form
        /// </summary>
        /// <param name="requestData">Jquery datatable request</param>
        /// <returns>DataTableResponse<RoleModel></returns>
        [HttpPost]
        public JsonResult Index(CustomDataTableRequestHelper requestData)
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
                Log.WriteLog(FILE_NAME, "Index", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Create role form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Create()
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
                Log.WriteLog(FILE_NAME, "Create", UserID, ex);
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
        public ActionResult Create(RoleModel model, FormCollection fc)
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
                Log.WriteLog(FILE_NAME, "Create", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Edit role form
        /// </summary>
        /// <param name="id">Role identifier</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Edit(string id)
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
                Log.WriteLog(FILE_NAME, "Edit", UserID, ex);
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
        public ActionResult Edit(RoleModel model, FormCollection fc)
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
                Log.WriteLog(FILE_NAME, "Edit", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Publish role
        /// </summary>
        /// <param name="model">Role model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult Publish(RoleModel model)
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
                Log.WriteLog(FILE_NAME, "Publish", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Delete role
        /// </summary>
        /// <param name="model">Role model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult Delete(RoleModel model)
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
                Log.WriteLog(FILE_NAME, "Delete", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// Check delete role
        /// </summary>
        /// <param name="model">Role model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public ActionResult CheckDelete(RoleModel model)
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
                Log.WriteLog(FILE_NAME, "CheckDelete", UserID, ex);
                throw new HttpException();
            }
        }

    }
}