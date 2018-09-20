using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.UserException;
using TDH.Model.Marketing.Facebook;
using TDH.Services.Marketing.Facebook;

namespace TDH.Areas.Marketing.Controllers
{
    /// <summary>
    /// Facebook controller
    /// </summary>
    public class FacebookController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Marketing.Controllers/FacebookController.cs";

        #endregion

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                #region " [ Declaration ] "

                UserService _userService = new UserService();
                FanpageService _fanpageService = new FanpageService();
                GroupService _groupService = new GroupService();

                ViewBag.user = _userService.GetAll(UserID);
                ViewBag.fanpage = _fanpageService.GetAll(UserID);
                ViewBag.group = _groupService.GetAll(UserID);

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

        #region " [ User ] "

        [HttpGet]
        public ActionResult GetUser()
        {
            try
            {
                #region " [ Declaration ] "

                UserService _service = new UserService();

                #endregion
                
                // Call to service
                return this.Json(_service.GetAll(UserID), JsonRequestBehavior.AllowGet);
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
                throw new ControllerException(FILE_NAME, "GetUser", UserID, ex);
            }
        }

        [HttpPost]
        public ActionResult SaveUser(UserModel model)
        {
            try
            {
                #region " [ Declaration ] "

                UserService _service = new UserService();

                #endregion

                #region " [ Main processing ] "

                model.Publish = true;
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                // Call to service
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
                throw new ControllerException(FILE_NAME, "SaveUser", UserID, ex);
            }
        }

        [HttpPost]
        public ActionResult DeleteUser(UserModel model)
        {
            try
            {
                #region " [ Declaration ] "

                UserService _service = new UserService();

                #endregion

                #region " [ Main processing ] "

                model.Publish = true;
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                // Call to service
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
                throw new ControllerException(FILE_NAME, "DeleteUser", UserID, ex);
            }
        }

        #endregion

        #region " [ Fanpage ] "

        [HttpGet]
        public ActionResult GetFanpage()
        {
            try
            {
                #region " [ Declaration ] "

                FanpageService _service = new FanpageService();

                #endregion

                // Call to service
                return this.Json(_service.GetAll(UserID), JsonRequestBehavior.AllowGet);
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
                throw new ControllerException(FILE_NAME, "GetFanpage", UserID, ex);
            }
        }

        [HttpPost]
        public ActionResult SaveFanpage(FanpageModel model)
        {
            try
            {
                #region " [ Declaration ] "

                FanpageService _service = new FanpageService();

                #endregion

                #region " [ Main processing ] "

                model.Publish = true;
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                // Call to service
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
                throw new ControllerException(FILE_NAME, "SaveFanpage", UserID, ex);
            }
        }

        [HttpPost]
        public ActionResult DeleteFanpage(FanpageModel model)
        {
            try
            {
                #region " [ Declaration ] "

                FanpageService _service = new FanpageService();

                #endregion

                #region " [ Main processing ] "

                model.Publish = true;
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                // Call to service
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
                throw new ControllerException(FILE_NAME, "DeleteFanpage", UserID, ex);
            }
        }

        #endregion

        #region " [ Group ] "

        [HttpGet]
        public ActionResult GetGroup()
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _service = new GroupService();

                #endregion

                // Call to service
                return this.Json(_service.GetAll(UserID), JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public ActionResult SaveGroup(GroupModel model)
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _service = new GroupService();

                #endregion

                #region " [ Main processing ] "

                model.Publish = true;
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                // Call to service
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
                throw new ControllerException(FILE_NAME, "SaveGroup", UserID, ex);
            }
        }

        [HttpPost]
        public ActionResult DeleteGroup(GroupModel model)
        {
            try
            {
                #region " [ Declaration ] "

                GroupService _service = new GroupService();

                #endregion

                #region " [ Main processing ] "

                model.Publish = true;
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                // Call to service
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

        #endregion


        public ActionResult Fanpage()
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
                throw new ControllerException(FILE_NAME, "Fanpage", UserID, ex);
            }
        }

        public ActionResult Group()
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
                throw new ControllerException(FILE_NAME, "Group", UserID, ex);
            }
        }

        public ActionResult PostType()
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
                throw new ControllerException(FILE_NAME, "PostType", UserID, ex);
            }
        }

    }
}