using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utils;
using Utils.JqueryDatatable;
using TDH.Areas.Administrator.Models;
using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;
using TDH.Common;

namespace TDH.Areas.Administrator.Controllers
{
    public class AdmTargetController : TDH.Common.BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator.Controllers/AdmTargetController.cs";
        
        #endregion

        public ActionResult OverView()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "OverView", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult SaveTarget(TargetModel model)
        {
            try
            {
                return this.Json("", JsonRequestBehavior.AllowGet);
                //
                //Services.TargetService _service = new Services.TargetService();
                //model.CreateBy = UserID;
                //model.UpdateBy = UserID;
                //model.UpdateDate = DateTime.Now;
                //
                //return this.Json(_service.Save(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "SaveTarget", UserID, ex);
                throw new HttpException();
            }
        }

        public ActionResult Dashboard()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Dashboard", UserID, ex);
                throw new HttpException();
            }
        }

        public ActionResult DailyTask()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "DailyTask", UserID, ex);
                throw new HttpException();
            }
        }
        
        #region " [ Idea ] "

        #endregion

    }
}