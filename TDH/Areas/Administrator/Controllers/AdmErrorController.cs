using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TDH.Areas.Administrator.Controllers
{
    [AllowAnonymous]
    public class AdmErrorController : TDH.Common.BaseController
    {
        /// <summary>
        /// Forbiden page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Forbiden()
        {
            return View();
        }

        /// <summary>
        /// Page not found.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NotFound()
        {
            return View();
        }

        /// <summary>
        /// Error page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        /// <summary>
        /// Show when data access is permitted
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DataAccess()
        {
            return View();
        }
        
    }
}