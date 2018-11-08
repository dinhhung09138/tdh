﻿using System;
using System.Reflection;
using System.Web.Mvc;
using TDH.Common.UserException;
using TDH.Model.System;
using TDH.Services.System;

namespace TDH.Areas.Administrator.Controllers
{
    /// <summary>
    /// Login controller
    /// </summary>
    public class LoginController : TDH.Common.BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator.Controllers/LoginController.cs";

        #endregion

        [AllowAnonymous]
        public ActionResult Index()
        {
            try
            {
                LoginModel model = new LoginModel();
                if (TempData["model"] != null)
                {
                    model = TempData["model"] as LoginModel;
                }
                ViewBag.msg = "";
                if (TempData["msg"] != null)
                {
                    ViewBag.msg = TempData["msg"].ToString();
                }
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
                throw new ControllerException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, UserID, ex);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(LoginModel model)
        {
            try
            {
                UserService _services = new UserService(this.SessionID);
                UserModel _model = _services.Login(model);
                if (_model.UserName == null || _model.UserName == "")
                {
                    TempData["model"] = new LoginModel() { UserName = model.UserName, RememberMe = model.RememberMe };
                    TempData["msg"] = "Tên đăng nhập hoặc mật khẩu không hợp lệ";
                    return RedirectToAction("Index");
                }
                Utils.CommonModel.UserLoginModel userModel = new Utils.CommonModel.UserLoginModel()
                {
                    UserID = _model.ID,
                    UserName = _model.UserName
                };
                Session[Utils.CommonHelper.SESSION_LOGIN_NAME] = userModel;
                return RedirectToAction("index", "dashboard", new { area = "administrator" });
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