using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.UserException;
using TDH.Model.Common;
using TDH.Model.Personal;
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

                Services.Common.SkillGroupService _groupService = new Services.Common.SkillGroupService();
                SkillService _skillService = new SkillService();

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

        [HttpGet]
        public JsonResult GetSkillByGroup(Guid groupID)
        {
            try
            {
                #region " [ Declaration ] "

                SkillService _service = new SkillService();

                #endregion

                #region " [ Main processing ] "

                var model = _service.GetAll(groupID, UserID);

                #endregion

                return this.Json(model, JsonRequestBehavior.AllowGet);
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
                throw new ControllerException(FILE_NAME, "GetSkillByGroup", UserID, ex);
            }
        }

        [HttpPost]
        public JsonResult SaveSkillDefined(string id, string skillId, short level)
        {
            try
            {
                #region " [ Declaration ] "

                SkillService _service = new SkillService();

                #endregion

                #region " [ Main processing ] "

                var model = _service.SaveSkillDefined(new Model.Personal.SkillDefinedModel() { ID = new Guid(id), SkillID = new Guid(skillId), Level = level, CreateBy = UserID });

                #endregion

                return this.Json(model, JsonRequestBehavior.AllowGet);
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
                throw new ControllerException(FILE_NAME, "SaveSkillDefined", UserID, ex);
            }
        }
    }
}