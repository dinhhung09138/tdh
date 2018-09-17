using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TDH.Areas.Marketing.Controllers
{
    [AllowAnonymous]
    public class ZaloController : Controller
    {
        // GET: Marketing/Zalo
        public ActionResult Index()
        {
            return View();
        }
    }
}