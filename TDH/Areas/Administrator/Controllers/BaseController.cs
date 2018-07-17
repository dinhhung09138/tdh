using System;
using System.Web;
using System.Web.Mvc;
using Utils;

namespace TDH.Areas.Administrator.Controllers
{
    public class BaseController : Controller
    {
        #region " [ Properties ] "
        
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
            Message = Resources.Message.Success
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

            var areaName = "";
            if (filterContext.RouteData.DataTokens["area"] != null)
            {
                areaName = filterContext.RouteData.DataTokens["area"].ToString().ToLower();
            }
            var controllerName = filterContext.RouteData.Values["controller"].ToString().ToLower();
            var actionName = filterContext.RouteData.Values["action"].ToString().ToLower();

            Services.RoleService _roleService = new Services.RoleService();
            string _functionCode = GetFunctionCode(areaName, controllerName, actionName);
            var _type = GetFunctionType(areaName, controllerName, actionName);
            var _permision = _roleService.AllowAccess(_user.UserID, _functionCode);
            switch (_type)
            {
                case Services.RoleService.actionType.View:
                    if (_permision.View == false)
                        throw new MemberAccessException();
                    break;
                case Services.RoleService.actionType.Create:
                    if (_permision.Add == false)
                    throw new MemberAccessException();
                    break;
                case Services.RoleService.actionType.Edit:
                    if (_permision.Edit == false)
                        throw new MemberAccessException();
                    break;
                case Services.RoleService.actionType.Delete:
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

        #region " [ Partial View Page ] "

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AdminNavigation()
        {
            return PartialView();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AdminSidebar()
        {
            Services.UserService _uService = new Services.UserService();
            ViewBag.sidebar = _uService.GetSidebar(UserID);
            return PartialView();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AdminFooter()
        {
            return PartialView();
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
                                case "news":
                                case "createnews":
                                case "editnews":
                                case "publishnews":
                                case "deletenews":
                                    return "post_news";
                                case "category":
                                case "createcategory":
                                case "editcategory":
                                case "publishcategory":
                                case "deletecategory":
                                case "checkdeletecategory":
                                case "onnavigationcategory":
                                    return "post_category";
                                case "navigation":
                                case "createnavigation":
                                case "editnavigation":
                                case "publishnavigation":
                                case "deletenavigation":
                                case "checkdeletenavigation":
                                    return "post_navigation";
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
                        case "admmoney":
                            #region " [ Money ] "

                            switch (actionName)
                            {
                                case "report":
                                case "summaryreport":
                                case "summaryreportbyyear":
                                    return "money_report";
                                case "accounttype":
                                case "createaccounttype":
                                case "editaccounttype":
                                case "publishaccounttype":
                                case "checkdeleteaccounttype":
                                case "deleteaccounttype":
                                    return "money_account_type";
                                case "account":
                                case "accounthistory":
                                case "createaccount":
                                case "editaccount":
                                case "publishaccount":
                                case "checkdeleteaccount":
                                case "deleteaccount":
                                    return "money_account";
                                case "group":
                                case "creategroup":
                                case "editgroup":
                                case "publishgroup":
                                case "checkdeletegroup":
                                case "deletegroup":
                                case "getgroupsettinginfo":
                                case "savegroupsettinginfo":
                                    return "money_group";
                                case "category":
                                case "categoryhistory":
                                case "createcategory":
                                case "editcategory":
                                case "publishcategory":
                                case "checkdeletecategory":
                                case "deletecategory":
                                    return "money_category";
                                case "flowhistory":
                                case "saveincome":
                                case "savepayment":
                                case "savetransfer":
                                    return "money_flow";
                            }

                            #endregion
                            break;
                        case "dashboard":
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
        private Services.RoleService.actionType GetFunctionType(string areaName, string controllerName, string actionName)
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
                                    return Services.RoleService.actionType.View;
                                case "createbanner":
                                    return Services.RoleService.actionType.Create;
                                case "editbanner":
                                case "publishbanner":
                                case "editconfiguration":
                                case "savecategory":
                                case "savenavigation":
                                    return Services.RoleService.actionType.Edit;
                                case "deletebanner":
                                    return Services.RoleService.actionType.Delete;
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
                                    return Services.RoleService.actionType.View;
                                case "createrole":
                                case "createemployee":
                                case "createuser":
                                    return Services.RoleService.actionType.Create;
                                case "editrole":
                                case "publishrole":
                                case "editemployee":
                                case "publishemployee":
                                case "edituser":
                                case "publishuser":
                                    return Services.RoleService.actionType.Edit;
                                case "deleterole":
                                case "checkdeleterole":
                                case "deleteemployee":
                                case "deleteuser":
                                    return Services.RoleService.actionType.Delete;
                            }

                            #endregion
                            break;
                        case "admpost":
                            #region " [ Post ] "

                            switch (actionName)
                            {
                                case "news":
                                case "category":
                                case "navigation":
                                case "about":
                                    return Services.RoleService.actionType.View;
                                case "createnews":
                                case "createcategory":
                                case "createnavigation":
                                    return Services.RoleService.actionType.Create;
                                case "editnews":
                                case "publishnews":
                                case "editcategory":
                                case "publishcategory":
                                case "editnavigation":
                                case "publishnavigation":
                                case "onnavigationcategory":
                                    return Services.RoleService.actionType.Edit;
                                case "deletenews":
                                case "deletecategory":
                                case "checkdeletecategory":
                                case "deletenavigation":
                                case "checkdeletenavigation":
                                    return Services.RoleService.actionType.Delete;
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
                                    return Services.RoleService.actionType.View;
                                case "createnavigation":
                                case "createcategory":
                                case "createproduct":
                                    return Services.RoleService.actionType.Create;
                                case "editnavigation":
                                case "publishnavigation":
                                case "editcategory":
                                case "publishcategory":
                                case "editproduct":
                                case "publishproduct":
                                case "stopbusinessproduct":
                                    return Services.RoleService.actionType.Edit;
                                case "deletenavigation":
                                case "checkdeletenavigation":
                                case "deletecategory":
                                case "checkdeletecategory":
                                case "deleteproduct":
                                    return Services.RoleService.actionType.Delete;
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
                                    return Services.RoleService.actionType.View;
                                case "createidea":
                                    return Services.RoleService.actionType.Create;
                                case "editidea":
                                    return Services.RoleService.actionType.Edit;
                                case "deleteidea":
                                case "checkdeleteidea":
                                    return Services.RoleService.actionType.Delete;
                            }

                            #endregion
                            break;
                        case "admworking":
                            #region " [ Working ] "

                            switch (actionName)
                            {
                                case "report":
                                case "detailreport":
                                    return Services.RoleService.actionType.View;
                                case "createreport":
                                case "savereportcomment":
                                    return Services.RoleService.actionType.Create;
                                case "editreport":
                                    return Services.RoleService.actionType.Edit;
                                case "deletereport":
                                case "checkdeletereport":
                                    return Services.RoleService.actionType.Delete;
                            }

                            #endregion
                            break;
                        case "admmoney":
                            #region " [ Money ] "

                            switch (actionName)
                            {
                                case "report":
                                case "summaryreport":
                                case "summaryreportbyyear":
                                case "accounttype":
                                case "account":
                                case "accounthistory":
                                case "group":
                                case "getgroupsettinginfo":
                                case "category":
                                case "categoryhistory":
                                case "flowhistory":
                                    return Services.RoleService.actionType.View;
                                case "createaccounttype":
                                case "createaccount":
                                case "creategroup":
                                case "createcategory":
                                case "saveincome":
                                case "savepayment":
                                case "savetransfer":
                                    return Services.RoleService.actionType.Create;
                                case "editaccounttype":
                                case "publishaccounttype":
                                case "editaccount":
                                case "publishaccount":
                                case "editgroup":
                                case "publishgroup":
                                case "savegroupsettinginfo":
                                case "editcategory":
                                case "publishcategory":
                                    return Services.RoleService.actionType.Edit;
                                case "checkdeleteaccounttype":
                                case "deleteaccounttype":
                                case "checkdeleteaccount":
                                case "deleteaccount":
                                case "checkdeletegroup":
                                case "deletegroup":
                                case "checkdeletecategory":
                                case "deletecategory":
                                    return Services.RoleService.actionType.Delete;
                            }

                            #endregion
                            break;
                        case "dashboard":
                            break;
                    }
                    break;
            }
            return Services.RoleService.actionType.None;
        }

        #endregion
        
        #region " [ Protected function ] "
                

        #endregion

        #region " [  ] "

        #endregion

        #region " [  ] "

        #endregion

        #region " [  ] "

        #endregion

        #region " [  ] "

        #endregion
    }
}