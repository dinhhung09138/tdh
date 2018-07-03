using System;
using System.Web;
using System.Web.Mvc;

namespace TDH.Areas.Administrator.Filters
{
    /// <summary>
    /// Filter process
    /// Prevent if request do not call from ajax
    /// </summary>
    public class AjaxExecuteFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Call before action executing
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
            {
                throw new NotImplementedException();
            }
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                //Prevent if request don't call from ajax
                throw new HttpException();
            }
        }
    }
}