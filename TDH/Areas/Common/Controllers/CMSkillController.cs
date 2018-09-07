using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.Fillters;
using TDH.Model.Common;
using TDH.Services.Common;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Areas.Common.Controllers
{
    public class CMSkillController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Common.Controllers/CMSkillController.cs";

        #endregion

        [AllowAnonymous]
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}