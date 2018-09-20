using System;
using System.Web;
using System.Web.Mvc;
using TDH.Common.UserException;
using TDH.Services.System;

namespace TDH.Areas.Administrator.Controllers
{
    [AllowAnonymous]
    public class BaseController : TDH.Common.BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator.Controllers/BaseController.cs";

        #endregion

        /// <summary>
        /// Module navigation
        /// </summary>
        /// <param name="moduleCode">module name</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult ModuleNavigation(string moduleCode)
        {
            try
            {
                UserService _uService = new UserService();
                ViewBag.moduleCode = moduleCode;
                ViewBag.sidebar = _uService.GetSidebar(UserID, moduleCode);
                return PartialView();
            }
            catch (ServiceException serviceEx)
            {
                throw serviceEx;
            }
            catch (DataAccessException accessEx)
            {
                throw accessEx;
            }
            catch (Exception ex)
            {
                throw new ControllerException(FILE_NAME, "ModuleNavigation", UserID, ex);
            }
        }

        [HttpGet]
        public ActionResult AdminNavigation()
        {
            try
            {
                return PartialView();
            }
            catch (ServiceException serviceEx)
            {
                throw serviceEx;
            }
            catch (DataAccessException accessEx)
            {
                throw accessEx;
            }
            catch (Exception ex)
            {
                throw new ControllerException(FILE_NAME, "AdminNavigation", UserID, ex);
            }
        }

        [HttpGet]
        public ActionResult AdminSidebar()
        {
            try
            {
                UserService _uService = new UserService();
                ViewBag.sidebar = _uService.GetSidebar(UserID);
                return PartialView();
            }
            catch (ServiceException serviceEx)
            {
                throw serviceEx;
            }
            catch (DataAccessException accessEx)
            {
                throw accessEx;
            }
            catch (Exception ex)
            {
                throw new ControllerException(FILE_NAME, "AdminSidebar", UserID, ex);
            }
        }

        [HttpGet]
        public ActionResult AdminFooter()
        {
            try
            {
                return PartialView();
            }
            catch (ServiceException serviceEx)
            {
                throw serviceEx;
            }
            catch (DataAccessException accessEx)
            {
                throw accessEx;
            }
            catch (Exception ex)
            {
                throw new ControllerException(FILE_NAME, "AdminFooter", UserID, ex);
            }
        }
        
    }
}