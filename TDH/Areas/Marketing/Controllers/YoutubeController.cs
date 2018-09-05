using System;
using System.Web;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.Fillters;
using TDH.Model.Website;
using TDH.Services.Website;

namespace TDH.Areas.Marketing.Controllers
{
    [AllowAnonymous]
    public class YoutubeController : BaseController
    {
        // GET: Marketing/Youtube

        public ActionResult Index()
        {

            //TODO
            // Setting in nuget package manager: google.apis.youtube
            // Tham khao .Net
            // https://developers.google.com/api-client-library/dotnet/get_started

            return View();
        }
    }
}