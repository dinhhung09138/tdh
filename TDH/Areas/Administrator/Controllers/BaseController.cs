using System;
using System.Web;
using System.Web.Mvc;
using TDH.Services.System;

namespace TDH.Areas.Administrator.Controllers
{
    public class BaseController : TDH.Common.BaseController
    {
        /// <summary>
        /// Module navigation
        /// </summary>
        /// <param name="moduleCode">module name</param>
        /// <returns>View</returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ModuleNavigation(string moduleCode)
        {
            UserService _uService = new UserService();
            ViewBag.moduleCode = moduleCode;
            ViewBag.sidebar = _uService.GetSidebar(UserID, moduleCode);
            return PartialView();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AdminNavigation()
        {
            return PartialView();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AdminSidebar()
        {
            UserService _uService = new UserService();
            ViewBag.sidebar = _uService.GetSidebar(UserID);
            return PartialView();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AdminFooter()
        {
            return PartialView();
        }
        
    }
}