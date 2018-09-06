using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TDH.Areas.Personal.Controllers
{
    [AllowAnonymous]
    public class MeController : Controller
    {
        // GET: Personal/Me
        public ActionResult Index()
        {
            return View();
        }
    }
}