using System;
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
                //0: Account not found, 1: Success, -1: Account is login, -2, Exception
                int _returnOutput = 0;
                //
                model.IsMobile = HttpContext.Request.Browser.IsMobileDevice;
                model.PlatForm = HttpContext.Request.Browser.Platform;
                model.UserAgent = HttpContext.Request.UserAgent;
                model.Version = HttpContext.Request.Browser.Version;
                model.HostName = HttpContext.Request.UserHostName;
                model.HostAddress = Utils.RequestHelpers.GetClientIpAddress(HttpContext.Request);
                
                UserService _services = new UserService(this.SessionID);
                UserModel _model = _services.Login(model, out _returnOutput);
                if (_returnOutput == -1)
                {
                    TempData["model"] = new LoginModel() { UserName = model.UserName, RememberMe = model.RememberMe };
                    TempData["msg"] = "Tài khoản đang đăng nhập ở nơi khác";
                    return RedirectToAction("Index");
                }
                if (_model.UserName == null || _model.UserName == "" || _returnOutput == -2 || _returnOutput == 0)
                {
                    TempData["model"] = new LoginModel() { UserName = model.UserName, RememberMe = model.RememberMe };
                    TempData["msg"] = "Tên đăng nhập hoặc mật khẩu không hợp lệ";
                    return RedirectToAction("Index");
                }
                Utils.CommonModel.UserLoginModel userModel = new Utils.CommonModel.UserLoginModel()
                {
                    UserID = _model.ID,
                    UserName = _model.UserName,
                    SessionID = HttpContext.Session.SessionID,
                    ExpireTime = DateTime.Now.AddMinutes(HttpContext.Session.Timeout),
                    Token = _model.Token
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