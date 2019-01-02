using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.Fillters;
using TDH.Common.UserException;
using TDH.Model.Website;
using TDH.Services.Website;

namespace TDH.Areas.Marketing.Controllers
{
    public class YoutubeController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Marketing.Controllers/YoutubeController.cs";

        #endregion

        public ActionResult Index()
        {
            try
            {
                //TODO
                // Setting in nuget package manager: google.apis.youtube
                // Tham khao .Net
                // https://developers.google.com/api-client-library/dotnet/get_started
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