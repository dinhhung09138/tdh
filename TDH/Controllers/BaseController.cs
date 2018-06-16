using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDH.Filters;

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
                filterContext.Result = View("../Error/Index");
                return;
            }
        }

    }
}