using System;
using System.Web.Mvc;

namespace TDH.Filters
{
    public class UserExceptionFilterAttribute : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            //Stop execute when action is a child inside
            if (filterContext.IsChildAction)
            {
                return;
            }

            filterContext.ExceptionHandled = true;
            //Page not found or something
            if (filterContext.Exception is UserException)
            {
                filterContext.Result = new RedirectResult("~/error/index");
                return;
            }
        }
    }
}