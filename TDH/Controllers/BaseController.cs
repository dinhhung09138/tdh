using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TDH.Common.UserException;

namespace TDH.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Execute before render view engine
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
            ViewBag.Url = filterContext.HttpContext.Request.Url.AbsoluteUri;
        }

        /// <summary>
        /// Execute when has some errors
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            filterContext.ExceptionHandled = true;
            
            if (filterContext.Exception is UserException)
            {
                UserException ex = filterContext.Exception as UserException;
                switch (ex.Status)
                {
                    case 204: //No content
                        filterContext.Result = new ViewResult
                        {
                            ViewName = "~/Views/Error/PageNotFound.cshtml"
                        };
                        return;
                    case 500:
                        filterContext.Result = new ViewResult
                        {
                            ViewName = "~/Views/Error/Index.cshtml"
                        };
                        return;
                    default:
                        filterContext.Result = new ViewResult
                        {
                            ViewName = "~/Views/Error/Index.cshtml"
                        };
                        return;
                }
            }
        }

    }
}