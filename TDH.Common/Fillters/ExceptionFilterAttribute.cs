using System;
using System.Web.Mvc;

namespace TDH.Common.Fillters
{
    /// <summary>
    /// Exception filter attribute
    /// </summary>
    public class ExceptionFilterAttribute : IExceptionFilter
    {
        /// <summary>
        /// On exception method
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnException(ExceptionContext filterContext)
        {
            //Stop execute when action is a child inside
            if (filterContext.IsChildAction)
            {
                return;
            }

            filterContext.ExceptionHandled = true;
            //Loggin required
            if (filterContext.Exception is UnauthorizedAccessException)
            {
                filterContext.Result = new RedirectResult("~/administrator/login");
                return;
            }
            //Page not found or something
            if (filterContext.Exception is NotImplementedException)
            {
                filterContext.Result = new RedirectResult("~/pagenotfound");
                return;
            }
        }
    }
}
