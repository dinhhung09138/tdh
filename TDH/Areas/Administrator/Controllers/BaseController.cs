using System;
using System.Reflection;
using System.Web.Mvc;
using TDH.Common.UserException;
using TDH.Services.System;

namespace TDH.Areas.Administrator.Controllers
{
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
        [ChildActionOnly]
        [HttpGet]
        public ActionResult ModuleNavigation(string moduleCode)
        {
            try
            {
                UserService _uService = new UserService(this.SessionID);
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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
            }
        }

        [ChildActionOnly]
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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
            }
        }

        [ChildActionOnly]
        [HttpGet]
        public ActionResult AdminSidebar()
        {
            try
            {
                UserService _uService = new UserService(this.SessionID); 
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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
            }
        }

        [ChildActionOnly]
        [AllowAnonymous]
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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
            }
        }

        [HttpPost]
        public ActionResult KeepAlive()
        {
            return new EmptyResult();
        }
        
    }
}