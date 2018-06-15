using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TDH.Areas.Administrator.Controllers
{
    public class AdmErrorController : Controller
    {
        [AllowAnonymous]
        public ActionResult Forbiden()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Error()
        {
            return View();
        }
    }
}