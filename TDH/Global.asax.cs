using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TDH.Common;
using Utils;

namespace TDH
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();
            HttpException httpException = exception as HttpException;

            if (httpException != null)
            {
                var _user = Session[CommonHelper.SESSION_LOGIN_NAME] as Utils.CommonModel.UserLoginModel;
                
                Log.WriteLog("Global.asax.cs", "Application_Error", new Guid(), exception);
                Server.ClearError();

                //User are logined. redirecto to admin page   
                if (_user != null && _user.UserID.ToString().Length > 0 && _user.UserName.Length > 0)
                {
                    Response.Redirect("/administrator/admerror/notfound");
                }
                else
                {
                    Response.Redirect("/error");
                }
            }
        }
    }
}
