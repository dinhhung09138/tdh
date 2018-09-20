﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TDH.Areas.Administrator.Controllers
{
    [AllowAnonymous]
    public class AdmErrorController : BaseController
    {
        public ActionResult Forbiden()
        {
            return View();
        }
        /// <summary>
        /// Error page
        /// </summary>
        /// <returns></returns>
        public ActionResult Error()
        {
            return View();
        }

        /// <summary>
        /// Show when data access is permitted
        /// </summary>
        /// <returns></returns>
        public ActionResult DataAccess()
        {
            return View();
        }
        
    }
}