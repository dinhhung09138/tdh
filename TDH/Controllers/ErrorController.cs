using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TDH.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        /// <summary>
        /// Default view error page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Page not found
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PageNotFound()
        {
            return View();
        }
        
    }
}