﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TDH.DataAccess;
using TDH.Model.System;
using Utils;

namespace TDH.Common
{
    public class BaseController : Controller
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Common/BaseController.cs";

        /// <summary>
        /// Current user id
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// ResultModel message success model
        /// </summary>
        public Utils.CommonModel.ExecuteResultModel ResultModel = new Utils.CommonModel.ExecuteResultModel()
        {
            Status = ResponseStatusCodeHelper.Success,
            Message = Message.Success
        };

        #endregion

        #region " [ Protected overriding method ] "

        /// <summary>
        /// Call before action method start
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var _user = filterContext.HttpContext.Session[CommonHelper.SESSION_LOGIN_NAME] as Utils.CommonModel.UserLoginModel;
            if (_user != null)
            {
                UserID = _user.UserID;
                ViewBag.userID = UserID;
            }
            bool skipAuth = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
                 || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
                 || filterContext.IsChildAction;
            if (skipAuth)
            {
                return;
            }
            if (_user == null || _user.UserID.ToString().Length == 0 || _user.UserName.Length == 0)
            {
                //Return to login page
                throw new UnauthorizedAccessException();
            }
            base.OnActionExecuting(filterContext);

            var _areaName = "";
            if (filterContext.RouteData.DataTokens["area"] != null)
            {
                _areaName = filterContext.RouteData.DataTokens["area"].ToString().ToLower();
            }
            var _controllerName = filterContext.RouteData.Values["controller"].ToString().ToLower();
            var _actionName = filterContext.RouteData.Values["action"].ToString().ToLower();

            string _functionCode = GetFunctionCode(_areaName, _controllerName, _actionName);
            var _type = GetFunctionType(_areaName, _controllerName, _actionName);
            var _permision = AllowAccess(_user.UserID, _functionCode);
            switch (_type)
            {
                case ActionType.View:
                    if (_permision.View == false)
                        throw new MemberAccessException();
                    break;
                case ActionType.Create:
                    if (_permision.Add == false)
                        throw new MemberAccessException();
                    break;
                case ActionType.Edit:
                    if (_permision.Edit == false)
                        throw new MemberAccessException();
                    break;
                case ActionType.Delete:
                    if (_permision.Delete == false)
                        throw new MemberAccessException();
                    break;
                default:
                    throw new ApplicationException();
            }
            ViewBag.View = _permision.View;
            ViewBag.Add = _permision.Add;
            ViewBag.Edit = _permision.Edit;
            ViewBag.Delete = _permision.Delete;
        }

        /// <summary>
        /// Execute after action method finish
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        /// <summary>
        /// Execute before render view engine
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
            //Message show to show on view.
            //Return after an action execute
            ViewBag.message = "";
            ViewBag.messageType = "";
            if (filterContext.IsChildAction || filterContext.HttpContext.Request.IsAjaxRequest())
            {
                return;
            }
            //if (TempData[CommonHelper.EXECUTE_RESULT] != null)
            //{
            //    Utils.CommonModel.ExecuteResultModel _result = TempData[CommonHelper.EXECUTE_RESULT] as Utils.CommonModel.ExecuteResultModel;
            //    ViewBag.message = _result.Message;
            //    ViewBag.messageType = _result.Status == ResponseStatusCodeHelper.Success ? "success" : "error";
            //    TempData.Clear();
            //}
        }

        /// <summary>
        /// Execute when has some errors
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            filterContext.ExceptionHandled = true;
            //Member dont have permission try to access
            if (filterContext.Exception is MemberAccessException)
            {
                filterContext.Result = View("../admerror/forbiden");
                return;
            }
            //Try to collect data was deleted
            if (filterContext.Exception is FieldAccessException)
            {
                filterContext.Result = View("../admerror/forbiden");
                return;
            }
            //controller exception
            if (filterContext.Exception is HttpException)
            {
                filterContext.Result = View("../admerror/error");
                return;
            }
            //Error accur in business logic code
            if (filterContext.Exception is ApplicationException)
            {
                filterContext.Result = View("../admerror/error");
                return;
            }
        }

        #endregion

        #region " [ Authorize ] "

        /// <summary>
        /// Get function code
        /// </summary>
        /// <param name="areaName">area name</param>
        /// <param name="controllerName">controller name</param>
        /// <param name="actionName">method name</param>
        /// <returns>stringforbiden</returns>
        private string GetFunctionCode(string areaName, string controllerName, string actionName)
        {
            switch (areaName)
            {
                case "administrator":
                    switch (controllerName)
                    {
                        case "admsetting":
                            #region " [ setting ] "

                            switch (actionName)
                            {
                                case "banner":
                                case "createbanner":
                                case "editbanner":
                                case "publishbanner":
                                case "deletebanner":
                                    return "banner";
                                case "configuration":
                                case "editconfiguration":
                                    return "setting_configuration";
                                case "category":
                                case "savecategory":
                                    return "setting_category";
                                case "navigation":
                                case "savenavigation":
                                    return "setting_category";
                            }

                            #endregion
                            break;
                        case "admsystem":
                            #region " [ System ] "

                            switch (actionName)
                            {
                                case "role":
                                case "createrole":
                                case "editrole":
                                case "publishrole":
                                case "deleterole":
                                case "checkdeleterole":
                                    return "system_role";
                                case "user":
                                case "createuser":
                                case "edituser":
                                case "eublishuser":
                                case "deleteuser":
                                    return "system_user";
                                case "errorlog":
                                case "detailerrolog":
                                    return "system_error_log";
                            }

                            #endregion
                            break;
                        case "admpost":
                            #region " [ Post ] "

                            switch (actionName)
                            {
                                case "about":
                                    return "post_about";
                            }

                            #endregion
                            break;
                        case "admtarget":
                            #region " [ Target ] "

                            switch (actionName)
                            {
                                case "idea":
                                case "createidea":
                                case "editidea":
                                case "deleteidea":
                                case "checkdeleteidea":
                                case "detailidea":
                                    return "target_idea";
                                case "overview":
                                case "savetarget":
                                    return "target_overview";
                                case "dashboard":
                                    return "target_dashboard";
                                case "dailytask":
                                    return "target_dailytask";
                            }

                            #endregion
                            break;
                        case "admworking":
                            #region " [ Working ] "

                            switch (actionName)
                            {
                                case "report":
                                case "createreport":
                                case "editreport":
                                case "deletereport":
                                case "checkdeletereport":
                                case "detailreport":
                                case "savereportcomment":
                                    return "working_report";
                            }

                            #endregion
                            break;
                        case "dashboard":
                            break;
                    }
                    break;
                case "money":
                    switch (controllerName)
                    {
                        case "mnaccounttype":
                            switch (actionName)
                            {
                                case "index":
                                case "create":
                                case "edit":
                                case "publish":
                                case "checkdelete":
                                case "delete":
                                    return "money_account_type";
                                default:
                                    return "";
                            }
                        case "mnaccount":
                            switch (actionName)
                            {
                                case "index":
                                case "create":
                                case "edit":
                                case "publish":
                                case "checkdelete":
                                case "delete":
                                    return "money_account";
                                default:
                                    return "";
                            }
                        case "mngroup":
                            switch (actionName)
                            {
                                case "index":
                                case "create":
                                case "edit":
                                case "publish":
                                case "checkdelete":
                                case "delete":
                                case "getgroupsettinginfo":
                                case "savegroupsettinginfo":
                                    return "money_group";
                                default:
                                    return "";
                            }
                        case "mncategory":
                            switch (actionName)
                            {
                                case "index":
                                case "history":
                                case "create":
                                case "edit":
                                case "publish":
                                case "checkdelete":
                                case "delete":
                                    return "money_category";
                                default:
                                    return "";
                            }
                        case "mnflow":
                            switch (actionName)
                            {
                                case "index":
                                case "saveincome":
                                case "savepayment":
                                case "savetransfer":
                                    return "money_flow";
                                default:
                                    return "";
                            }
                        case "mnreport":
                            switch (actionName)
                            {
                                case "index":
                                case "summaryreport":
                                case "summaryreportbyyear":
                                case "incomebyyearreport":
                                    return "money_report";
                                default:
                                    return "";
                            }
                        default:
                            break;
                    }
                    break;
                case "website":
                    switch (controllerName)
                    {
                        case "wnavigation":
                            switch (actionName)
                            {
                                case "index":
                                case "create":
                                case "edit":
                                case "publish":
                                case "checkdelete":
                                case "delete":
                                    return "post_navigation";
                                default:
                                    return "";
                            }
                        case "wcategory":
                            switch (actionName)
                            {
                                case "index":
                                case "create":
                                case "onnavigation":
                                case "edit":
                                case "publish":
                                case "checkdelete":
                                case "delete":
                                    return "post_category";
                                default:
                                    return "";
                            }
                        case "wnews":
                            switch (actionName)
                            {
                                case "index":
                                case "create":
                                case "edit":
                                case "publish":
                                case "checkdelete":
                                case "delete":
                                    return "post_news";
                                default:
                                    return "";
                            }
                        default:
                            break;
                    }
                    break;
            }
            return "";
        }

        /// <summary>
        /// Get permision type
        /// </summary>
        /// <param name="areaName">area name</param>
        /// <param name="controllerName">controller name</param>
        /// <param name="actionName">method name</param>
        /// <returns></returns>
        private ActionType GetFunctionType(string areaName, string controllerName, string actionName)
        {
            switch (areaName)
            {
                case "administrator":
                    switch (controllerName)
                    {
                        case "admsetting":
                            #region " [ setting ] "

                            switch (actionName)
                            {
                                case "banner":
                                case "configuration":
                                case "category":
                                case "navigation":
                                    return ActionType.View;
                                case "createbanner":
                                    return ActionType.Create;
                                case "editbanner":
                                case "publishbanner":
                                case "editconfiguration":
                                case "savecategory":
                                case "savenavigation":
                                    return ActionType.Edit;
                                case "deletebanner":
                                    return ActionType.Delete;
                            }

                            #endregion
                            break;
                        case "admsystem":
                            #region " [ System ] "

                            switch (actionName)
                            {
                                case "role":
                                case "employee":
                                case "user":
                                case "errorlog":
                                case "detailerrolog":
                                    return ActionType.View;
                                case "createrole":
                                case "createemployee":
                                case "createuser":
                                    return ActionType.Create;
                                case "editrole":
                                case "publishrole":
                                case "editemployee":
                                case "publishemployee":
                                case "edituser":
                                case "publishuser":
                                    return ActionType.Edit;
                                case "deleterole":
                                case "checkdeleterole":
                                case "deleteemployee":
                                case "deleteuser":
                                    return ActionType.Delete;
                            }

                            #endregion
                            break;
                        case "admpost":
                            #region " [ Post ] "

                            switch (actionName)
                            {
                                case "about":
                                    return ActionType.View;
                            }

                            #endregion
                            break;
                        case "admproduct":
                            #region " [ admproduct ] "

                            switch (actionName)
                            {
                                case "navigation":
                                case "category":
                                case "product":
                                    return ActionType.View;
                                case "createnavigation":
                                case "createcategory":
                                case "createproduct":
                                    return ActionType.Create;
                                case "editnavigation":
                                case "publishnavigation":
                                case "editcategory":
                                case "publishcategory":
                                case "editproduct":
                                case "publishproduct":
                                case "stopbusinessproduct":
                                    return ActionType.Edit;
                                case "deletenavigation":
                                case "checkdeletenavigation":
                                case "deletecategory":
                                case "checkdeletecategory":
                                case "deleteproduct":
                                    return ActionType.Delete;
                            }

                            #endregion
                            break;
                        case "admtarget":
                            #region " [ Target ] "

                            switch (actionName)
                            {
                                case "idea":
                                case "overview":
                                case "dashboard":
                                case "dailytask":
                                case "savetarget":
                                case "detailidea":
                                    return ActionType.View;
                                case "createidea":
                                    return ActionType.Create;
                                case "editidea":
                                    return ActionType.Edit;
                                case "deleteidea":
                                case "checkdeleteidea":
                                    return ActionType.Delete;
                            }

                            #endregion
                            break;
                        case "admworking":
                            #region " [ Working ] "

                            switch (actionName)
                            {
                                case "report":
                                case "detailreport":
                                    return ActionType.View;
                                case "createreport":
                                case "savereportcomment":
                                    return ActionType.Create;
                                case "editreport":
                                    return ActionType.Edit;
                                case "deletereport":
                                case "checkdeletereport":
                                    return ActionType.Delete;
                            }

                            #endregion
                            break;
                        case "dashboard":
                            break;
                    }
                    break;
                case "money":
                    switch (controllerName)
                    {
                        case "mnaccounttype":
                            switch (actionName)
                            {
                                case "index":
                                    return ActionType.View;
                                case "create":
                                    return ActionType.Create;
                                case "edit":
                                case "publish":
                                    return ActionType.Edit;
                                case "checkdelete":
                                case "delete":
                                    return ActionType.Delete;
                                default:
                                    return ActionType.None;
                            }
                        case "mnaccount":
                            switch (actionName)
                            {
                                case "index":
                                case "history":
                                    return ActionType.View;
                                case "create":
                                    return ActionType.Create;
                                case "edit":
                                case "publish":
                                    return ActionType.Edit;
                                case "checkdelete":
                                case "delete":
                                    return ActionType.Delete;
                                default:
                                    return ActionType.None;
                            }
                        case "mngroup":
                            switch (actionName)
                            {
                                case "index":
                                case "getgroupsettinginfo":
                                    return ActionType.View;
                                case "create":
                                    return ActionType.Create;
                                case "edit":
                                case "publish":
                                case "savegroupsettinginfo":
                                    return ActionType.Edit;
                                case "checkdelete":
                                case "delete":
                                    return ActionType.Delete;
                                default:
                                    return ActionType.None;
                            }
                        case "mncategory":
                            switch (actionName)
                            {
                                case "index":
                                case "history":
                                    return ActionType.View;
                                case "create":
                                    return ActionType.Create;
                                case "edit":
                                case "publish":
                                    return ActionType.Edit;
                                case "checkdelete":
                                case "delete":
                                    return ActionType.Delete;
                                default:
                                    return ActionType.None;
                            }
                        case "mnflow":
                            switch (actionName)
                            {
                                case "index":
                                    return ActionType.View;
                                case "saveincome":
                                case "savepayment":
                                case "savetransfer":
                                    return ActionType.Create;
                                default:
                                    return ActionType.None;
                            }
                        case "mnreport":
                            switch (actionName)
                            {
                                case "index":
                                case "summaryreport":
                                case "summaryreportbyyear":
                                case "incomebyyearreport":
                                    return ActionType.View;
                                default:
                                    return ActionType.None;
                            }
                    }
                    break;
                case "website":
                    switch (controllerName)
                    {
                        case "wnavigation":
                            switch (actionName)
                            {
                                case "index":
                                    return ActionType.View;
                                case "create":
                                    return ActionType.Create;
                                case "edit":
                                case "publish":
                                    return ActionType.Edit;
                                case "checkdelete":
                                case "delete":
                                    return ActionType.Delete;
                                default:
                                    return ActionType.None;
                            }
                        case "wcategory":
                            switch (actionName)
                            {
                                case "index":
                                    return ActionType.View;
                                case "create":
                                    return ActionType.Create;
                                case "edit":
                                case "publish":
                                case "onnavigation":
                                    return ActionType.Edit;
                                case "checkdelete":
                                case "delete":
                                    return ActionType.Delete;
                                default:
                                    return ActionType.None;
                            }
                        case "wnews":
                            switch (actionName)
                            {
                                case "index":
                                    return ActionType.View;
                                case "create":
                                    return ActionType.Create;
                                case "edit":
                                case "publish":
                                    return ActionType.Edit;
                                case "checkdelete":
                                case "delete":
                                    return ActionType.Delete;
                                default:
                                    return ActionType.None;
                            }
                    }
                    break;
                default:
                    break;
            }
            return ActionType.None;
        }

        #endregion

        #region " [ Protected function ] "

        /// <summary>
        /// Check access into method
        /// </summary>
        /// <param name="userID">User identifier</param>
        /// <param name="functionCode">Function code</param>
        /// <returns>RoleDetailModel</returns>
        private RoleDetailModel AllowAccess(Guid userID, string functionCode)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    var _permision = (from dt in _context.SYS_ROLE_DETAIL
                                      join r in _context.SYS_ROLE on dt.role_id equals r.id
                                      join ur in _context.SYS_USER_ROLE on r.id equals ur.role_id
                                      where ur.user_id == userID && dt.function_code == functionCode && !r.deleted && r.publish
                                      select dt).FirstOrDefault();
                    if (_permision != null)
                    {
                        return new RoleDetailModel()
                        {
                            View = _permision.view,
                            Add = _permision.add,
                            Edit = _permision.edit,
                            Delete = _permision.delete
                        };
                    }
                    return new RoleDetailModel()
                    {
                        View = false,
                        Add = false,
                        Edit = false,
                        Delete = false
                    };
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(userID, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "AllowAccess", userID, ex);
                throw new ApplicationException();
            }
        }

        #endregion
    }
}
