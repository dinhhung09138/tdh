using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TDH.Areas.Administrator.Controllers
{
    public class DashboardController : TDH.Common.BaseController
    {
        /// <summary>
        /// Main dashboard form
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}