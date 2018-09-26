using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TDH.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PageNotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            ViewBag.URL = RouteData.Values["url"].ToString();
            return View();
        }

        [HttpGet]
        public ActionResult GeneralError()
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ViewBag.Message = "Error occured!";
            return View();
        }
    }
}