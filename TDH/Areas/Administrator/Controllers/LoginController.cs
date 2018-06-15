using TDH.Areas.Administrator.Models;
using TDH.Areas.Administrator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TDH.Areas.Administrator.Controllers
{
    public class LoginController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            LoginModel model = new LoginModel();
            if(TempData["model"] != null)
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

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(LoginModel model)
        {
            UserService _services = new UserService();
            UserModel _model = _services.Login(model);
            if(_model.UserName == "")
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
            return RedirectToAction("index", "dashboard",new { area = "administrator" });
        }
    }
}