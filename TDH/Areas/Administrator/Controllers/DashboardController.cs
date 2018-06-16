using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TDH.Areas.Administrator.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: Admin/Dashboard
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
    }
}