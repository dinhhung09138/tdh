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
            GZipEncodePage(filterContext);
        }

        /// <summary>
        /// Execute when has some errors
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            filterContext.ExceptionHandled = true;

            //Stop if action is partial or child
            if (filterContext.IsChildAction)
                return;

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
                    case 500: //Error in controller or services
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

        #region " [ SEO method ] "

        /// <summary>
        /// Determines if GZip is supported
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns>Boolearn</returns>
        private bool IsGZipSupported(ResultExecutingContext filterContext)
        {
            string acceptEncoding = filterContext.HttpContext.Request.Headers["Accept-Encoding"];
            if (!string.IsNullOrEmpty(acceptEncoding) && (acceptEncoding.Contains("gzip") || acceptEncoding.Contains("deflate")))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sets up the current page or handler to use GZip through a Response.Filter
        /// IMPORTANT:  
        /// You have to call this method before any output is generated!
        /// <paramref name="filterContext"/>
        /// </summary>
        /// <param name="filterContext"></param>
        private void GZipEncodePage(ResultExecutingContext filterContext)
        {

            HttpResponseBase response = filterContext.HttpContext.Response;
            if (response == null)
            {
                return;
            }
            if (!this.IsGZipSupported(filterContext))
            {
                string acceptEncoding = filterContext.HttpContext.Request.Headers["Accept-Encoding"];
                //
                if (acceptEncoding.ToLower().Contains("gzip"))
                {
                    response.Filter = new System.IO.Compression.GZipStream(response.Filter, System.IO.Compression.CompressionMode.Compress);
                    response.Headers.Remove("Content-Encoding");
                    response.AppendHeader("Content-Encoding", "gzip");
                }
                else
                {
                    response.Filter = new System.IO.Compression.DeflateStream(response.Filter, System.IO.Compression.CompressionMode.Compress);
                    response.Headers.Remove("Content-Encoding");
                    response.AppendHeader("Content-Encoding", "deflate");
                }
                // Allow proxy servers to cache encoded and unencoded versions separately
                response.AppendHeader("Vary", "Content-Endcoding");
            }
        }

        #endregion
    }
}