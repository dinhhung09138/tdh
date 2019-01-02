using System;
using System.Reflection;
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

                EducationService _educationService = new EducationService();
                TimelineService _timelineService = new TimelineService();
                CetificateService _cetificateService = new CetificateService();
                SkillService _service = new SkillService();

                ViewBag.education = _educationService.GetAll(UserID);
                ViewBag.timeline = _timelineService.GetAll(UserID);
                ViewBag.cetificate = _cetificateService.GetAll(UserID);
                ViewBag.mySkill = _service.MySkill(UserID);

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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
            }
        }
    }
}