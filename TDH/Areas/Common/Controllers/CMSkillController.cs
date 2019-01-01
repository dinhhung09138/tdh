using System;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.UserException;
using TDH.Model.Common;
using TDH.Services.Common;

namespace TDH.Areas.Common.Controllers
{
    public class CMSkillController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Common.Controllers/CMSkillController.cs";

        #endregion

        [AllowAnonymous]
        public ActionResult Home()
        {
            try
            {
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
                throw new ControllerException(FILE_NAME, "Home", UserID, ex);
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                #region " [ Declaration ] "

                SkillGroupService _groupService = new SkillGroupService();
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
        public JsonResult GetGroup()
        {
            try
            {
                #region " [ Declaration ] "

                SkillGroupService _service = new SkillGroupService();

                #endregion

                #region " [ Main processing ] "

                var model = _service.GetAll(UserID);

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
                throw new ControllerException(FILE_NAME, "GetGroup", UserID, ex);
            }
        }

        [HttpGet]
        public JsonResult GetGroupItem(Guid id)
        {
            try
            {
                #region " [ Declaration ] "

                SkillGroupService _service = new SkillGroupService();

                #endregion

                #region " [ Main processing ] "

                var model = _service.GetItemByID(new SkillGroupModel() { ID = id, CreateBy = UserID });

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
                throw new ControllerException(FILE_NAME, "GetGroupItem", UserID, ex);
            }
        }

        [HttpPost]
        public ActionResult CreateGroup(SkillGroupModel model)
        {
            try
            {
                #region " [ Declaration ] "

                SkillGroupService _service = new SkillGroupService();

                #endregion

                #region " [ Main processing ] "

                model.Insert = true;
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                //Call to service
                return this.Json(_service.Save(model), JsonRequestBehavior.AllowGet);
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
                throw new ControllerException(FILE_NAME, "ActionResult", UserID, ex);
            }
        }

        [HttpPost]
        public ActionResult EditGroup(SkillGroupModel model)
        {
            try
            {
                #region " [ Declaration ] "

                SkillGroupService _service = new SkillGroupService();

                #endregion

                #region " [ Main processing ] "

                model.Insert = false;
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                //Call to service
                return this.Json(_service.Save(model), JsonRequestBehavior.AllowGet);
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
                throw new ControllerException(FILE_NAME, "EditGroup", UserID, ex);
            }
        }
        
        [HttpPost]
        public ActionResult CheckDeleteGroup(SkillGroupModel model)
        {
            try
            {
                #region " [ Declaration ] "

                SkillGroupService _service = new SkillGroupService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;

                #endregion

                //Call to service
                return this.Json(_service.CheckDelete(model), JsonRequestBehavior.AllowGet);
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
                throw new ControllerException(FILE_NAME, "CheckDeleteGroup", UserID, ex);
            }
        }
        
        [HttpPost]
        public ActionResult DeleteGroup(SkillGroupModel model)
        {
            try
            {
                #region " [ Declaration ] "

                SkillGroupService _service = new SkillGroupService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;
                model.DeleteBy = UserID;
                model.DeleteDate = DateTime.Now;

                #endregion

                //Call to service
                return this.Json(_service.Delete(model), JsonRequestBehavior.AllowGet);
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
                throw new ControllerException(FILE_NAME, "DeleteGroup", UserID, ex);
            }
        }

        #region " [ Skill ] "

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
        
        [HttpGet]
        public JsonResult GetSkillItem(Guid id)
        {
            try
            {
                #region " [ Declaration ] "

                SkillService _service = new SkillService();

                #endregion

                #region " [ Main processing ] "

                var model = _service.GetItemByID(new SkillModel() { ID = id, CreateBy = UserID });

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
                throw new ControllerException(FILE_NAME, "GetSkillItem", UserID, ex);
            }
        }

        [HttpPost]
        public ActionResult CreateSkill(SkillModel model)
        {
            try
            {
                #region " [ Declaration ] "

                SkillService _service = new SkillService();

                #endregion

                #region " [ Main processing ] "

                model.Insert = true;
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                //Call to service
                return this.Json(_service.Save(model), JsonRequestBehavior.AllowGet);
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
                throw new ControllerException(FILE_NAME, "CreateSkill", UserID, ex);
            }
        }

        [HttpPost]
        public ActionResult EditSkill(SkillModel model)
        {
            try
            {
                #region " [ Declaration ] "

                SkillService _service = new SkillService();

                #endregion

                #region " [ Main processing ] "

                model.Insert = false;
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                //Call to service
                return this.Json(_service.Save(model), JsonRequestBehavior.AllowGet);
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
                throw new ControllerException(FILE_NAME, "EditSkill", UserID, ex);
            }
        }

        [HttpPost]
        public ActionResult CheckDeleteSkill(SkillModel model)
        {
            try
            {
                #region " [ Declaration ] "

                SkillService _service = new SkillService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;

                #endregion

                //Call to service
                return this.Json(_service.CheckDelete(model), JsonRequestBehavior.AllowGet);
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
                throw new ControllerException(FILE_NAME, "CheckDeleteSkill", UserID, ex);
            }
        }
        
        [HttpPost]
        public ActionResult DeleteSkill(SkillModel model)
        {
            try
            {
                #region " [ Declaration ] "

                SkillService _service = new SkillService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;
                model.DeleteBy = UserID;
                model.DeleteDate = DateTime.Now;

                #endregion

                //Call to service
                return this.Json(_service.Delete(model), JsonRequestBehavior.AllowGet);
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
                throw new ControllerException(FILE_NAME, "DeleteSkill", UserID, ex);
            }
        }

        #endregion

        #region " [ Skill Defined ] "

        [HttpGet]
        public JsonResult GetSkillDefinedItem(Guid id)
        {
            try
            {
                #region " [ Declaration ] "

                SkillDefinedService _service = new SkillDefinedService();

                #endregion

                #region " [ Main processing ] "

                var model = _service.GetItemByID(new SkillDefinedModel() { ID = id, CreateBy = UserID });

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
                throw new ControllerException(FILE_NAME, "GetSkillDefinedItem", UserID, ex);
            }
        }

        [HttpPost]
        public ActionResult CreateSkillDefined(SkillDefinedModel model)
        {
            try
            {
                #region " [ Declaration ] "

                SkillDefinedService _service = new SkillDefinedService();

                #endregion

                #region " [ Main processing ] "

                model.Insert = true;
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                //Call to service
                return this.Json(_service.Save(model), JsonRequestBehavior.AllowGet);
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
                throw new ControllerException(FILE_NAME, "CreateSkillDefined", UserID, ex);
            }
        }

        [HttpPost]
        public ActionResult EditSkillDefined(SkillDefinedModel model)
        {
            try
            {
                #region " [ Declaration ] "

                SkillDefinedService _service = new SkillDefinedService();

                #endregion

                #region " [ Main processing ] "

                model.Insert = false;
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                //Call to service
                return this.Json(_service.Save(model), JsonRequestBehavior.AllowGet);
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
                throw new ControllerException(FILE_NAME, "EditSkillDefined", UserID, ex);
            }
        }

        [HttpPost]
        public ActionResult DeleteSkillDefined(SkillDefinedModel model)
        {
            try
            {
                #region " [ Declaration ] "

                SkillDefinedService _service = new SkillDefinedService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;
                model.DeleteBy = UserID;
                model.DeleteDate = DateTime.Now;

                #endregion

                //Call to service
                return this.Json(_service.Delete(model), JsonRequestBehavior.AllowGet);
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
                throw new ControllerException(FILE_NAME, "DeleteSkillDefined", UserID, ex);
            }
        }

        #endregion

    }
}