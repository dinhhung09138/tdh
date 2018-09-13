using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDH.Common;
using TDH.Model.Marketing.Facebook;
using TDH.Services.Marketing.Facebook;
using Utils;

namespace TDH.Areas.Marketing.Controllers
{
    [AllowAnonymous]
    /// <summary>
    /// Facebook controller
    /// </summary>
    public class FacebookController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Fanpage()
        {
            return View();
        }

        public ActionResult Group()
        {
            return View();
        }

        public ActionResult PostType()
        {
            return View();
        }
    }
}