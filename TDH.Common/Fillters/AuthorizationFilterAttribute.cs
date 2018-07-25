using System;
using Utils;
using System.Web.Mvc;

namespace TDH.Common.Fillters
{
    /// <summary>
    /// Authorization filter attribute class
    /// </summary>
    public class AuthorizationFilterAttribute : IAuthorizationFilter
    {

        /// <summary>
        /// On authorization
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException();
            }
            string area = "";
            if (filterContext.RouteData.DataTokens["area"] != null)
            {
                area = filterContext.RouteData.DataTokens["area"].ToString();
            }
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            //Stop execute when action is a child inside or allowAnonymousAttribute
            bool skipAuth = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
                || filterContext.IsChildAction;
            if (skipAuth)
            {
                return;
            }

            var _user = filterContext.HttpContext.Session[CommonHelper.SESSION_LOGIN_NAME] as Utils.CommonModel.UserLoginModel;
            if (_user != null && _user.UserID.ToString().Length > 0 && _user.UserName.Length > 0)
            {
                //Check token
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }
            throw new UnauthorizedAccessException();
        }
    }
}
