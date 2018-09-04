using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.Fillters;
using TDH.Model.Website;
using TDH.Services.Website;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Areas.Administrator.Controllers
{
    /// <summary>
    /// Post controller
    /// </summary>
    [AjaxExecuteFilterAttribute]
    public class AdmPostController : TDH.Common.BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator.Controllers/AdmPostController.cs";

        #endregion

        /// <summary>
        /// Defalt view when user access
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            return PartialView();
        }

        #region " [ Navigation ] "

        #endregion

        #region " [ Category ] "

        #endregion

        #region " [ Post ] "

        #endregion

        #region " [ About ] "

        /// <summary>
        /// About form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult About()
        {
            try
            {
                #region " [ Declaration ] "

                AboutService _service = new AboutService();
                var _model = _service.GetItemByID(new AboutModel() { CreateBy = UserID, Insert = false });

                #endregion
                
                return PartialView(_model);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "About", UserID, ex);
                throw new HttpException();
            }
        }

        /// <summary>
        /// About form
        /// Post method
        /// </summary>
        /// <param name="model">About model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult About(AboutModel model)
        {
            try
            {
                #region " [ Declaration ] "

                AboutService _service = new AboutService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                //Call to service
                return this.Json(_service.Save(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "About", UserID, ex);
                throw new HttpException();
            }
        }
        
        #endregion

    }
}
