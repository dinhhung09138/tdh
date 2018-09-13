using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDH.Common;
using TDH.Model.Personal;
using TDH.Services.Personal;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Areas.Personal.Controllers
{
    /// <summary>
    /// Skill controller
    /// </summary>
    public class PNSkillController : BaseController
    {
        // GET: Personal/PNSkill
        public ActionResult Index()
        {
            return View();
        }
    }
}