using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.UserException;
using TDH.Model.Personal;
using TDH.Services.Common;
using TDH.Services.Personal;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Areas.Personal.Controllers
{
    /// <summary>
    /// Skill controller
    /// </summary>
    public class PNSkillController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Personal.Controllers/PNSkillController.cs";

        #endregion

        /// <summary>
        /// Skill form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                #region " [ Declaration ] "

                SkillGroupService _groupService = new SkillGroupService();
                Services.Common.SkillService _skillService = new Services.Common.SkillService();

                ViewBag.groupID = "";
                ViewBag.skills = null;

                #endregion

                #region " [ Main processing ] "

                var model = _groupService.GetAll(UserID);


                if (model.Count > 0)
                {
                    ViewBag.groupID = model[0].ID.ToString();
                    ViewBag.skills = _skillService.GetAll(model[0].ID, UserID);
                }

                #endregion

                return View(model);
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