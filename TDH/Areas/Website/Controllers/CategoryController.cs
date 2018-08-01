using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TDH.Areas.Website.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Website/Category
        public ActionResult Index()
        {
            return View();
        }
    }
}