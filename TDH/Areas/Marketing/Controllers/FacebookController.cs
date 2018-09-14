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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Index", UserID, ex);
                throw new HttpException();
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "GetUser", UserID, ex);
                throw new HttpException();
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "SaveUser", UserID, ex);
                throw new HttpException();
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "DeleteUser", UserID, ex);
                throw new HttpException();
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "GetFanpage", UserID, ex);
                throw new HttpException();
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "SaveFanpage", UserID, ex);
                throw new HttpException();
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "DeleteFanpage", UserID, ex);
                throw new HttpException();
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "GetGroup", UserID, ex);
                throw new HttpException();
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "SaveGroup", UserID, ex);
                throw new HttpException();
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "DeleteGroup", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion


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