using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDH.Common;

namespace TDH.Areas.Personal.Controllers
{
    public class MeController : BaseController
    {
        // GET: Personal/Me
        public ActionResult Index()
        {
            ViewBag.userName = "abc";
            return View();
        }
    }
}