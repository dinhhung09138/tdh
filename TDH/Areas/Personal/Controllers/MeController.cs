using System;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.UserException;
using TDH.Services.Personal;

namespace TDH.Areas.Personal.Controllers
{
    /// <summary>
    /// My info dashboard controller
    /// </summary>
    public class MeController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Personal.Controllers/MeController.cs";

        #endregion

        /// <summary>
        /// My info dashboar form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                #region " [ Declaration ] "

                TimelineService _timelineService = new TimelineService();
                CetificateService _cetificateService = new CetificateService();

                ViewBag.userName = "abc";
                ViewBag.timeline = _timelineService.GetAll(UserID);
                ViewBag.cetificate = _cetificateService.GetAll(UserID);

                #endregion

                return View();
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
                throw new ControllerException(FILE_NAME, "Index", UserID, ex);
            }
        }
    }
}