using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using TDH.Areas.Administrator.Controllers;
using TDH.Common.UserException;
using TDH.Model.Personal;
using TDH.Services.Personal;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Areas.Personal.Controllers
{
    public class PNWorkingController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Personal.Controllers/WorkingController.cs";

        #endregion

        /// <summary>
        /// List of project form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Project()
        {
            try
            {                
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

        /// <summary>
        /// List of project function
        /// Post method
        /// </summary>
        /// <param name="requestData">Jquery datatable request</param>
        /// <returns>DataTableResponse<EventModel></returns>
        [HttpPost]
        public JsonResult Project(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                WorkingService _service = new WorkingService();

                #endregion

                #region " [ Main processing ] "
                
                #endregion

                //Call to service
                Dictionary<string, object> _return = _service.ListProject(requestData, UserID);
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<ProjectModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<ProjectModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<ProjectModel>(), JsonRequestBehavior.AllowGet);
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

        /// <summary>
        /// Create form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult CreateProject()
        {
            try
            {
                #region " [ Declaration ] "
                
                ProjectModel model = new ProjectModel()
                {
                    ID = Guid.NewGuid(),
                    CreateBy = UserID,
                    Insert = true
                };

                #endregion

                return View(model);
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

        /// <summary>
        /// Create function
        /// </summary>
        /// <param name="model">project model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateProject(ProjectModel model)
        {
            try
            {
                #region " [ Declaration ] "

                WorkingService _service = new WorkingService();

                #endregion

                #region " [ Main processing ] "
                                
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                // Call to service
                return this.Json(_service.SaveProject(model), JsonRequestBehavior.AllowGet);
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

        /// <summary>
        /// Edit form
        /// </summary>
        /// <param name="id">The project identifier</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult EditProject(string id)
        {
            try
            {
                #region " [ Declaration ] "
                
                WorkingService _service = new WorkingService();

                ViewBag.id = id;

                #endregion

                // Call to service
                ProjectModel model = _service.GetProjectItemByID(new ProjectModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });

                return View(model);
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

        /// <summary>
        /// Edit function
        /// </summary>
        /// <param name="model">project model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public JsonResult EditProject(ProjectModel model)
        {
            try
            {
                #region " [ Declaration ] "

                WorkingService _service = new WorkingService();

                #endregion

                #region " [ Main processing ] "
                
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                // Call to service
                return this.Json(_service.SaveProject(model), JsonRequestBehavior.AllowGet);
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

        /// <summary>
        /// Delete function
        /// </summary>
        /// <param name="model">project model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public JsonResult DeleteProject(ProjectModel model)
        {
            try
            {
                #region " [ Declaration ] "

                WorkingService _service = new WorkingService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;
                model.DeleteBy = UserID;
                model.DeleteDate = DateTime.Now;

                #endregion

                //Call to service
                return this.Json(_service.DeleteProject(model), JsonRequestBehavior.AllowGet);
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
        
        /// <summary>
        /// List of working experience form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Experience()
        {
            try
            {
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

        /// <summary>
        /// List of working experience function
        /// Post method
        /// </summary>
        /// <param name="requestData">Jquery datatable request</param>
        /// <returns>DataTableResponse<EventModel></returns>
        [HttpPost]
        public JsonResult Experience(CustomDataTableRequestHelper requestData)
        {
            try
            {
                #region " [ Declaration ] "

                WorkingService _service = new WorkingService();

                #endregion
                

                //Call to service
                Dictionary<string, object> _return = _service.ListExperience(requestData, UserID);
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<ExperienceModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<ExperienceModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                //
                return this.Json(new DataTableResponse<ExperienceModel>(), JsonRequestBehavior.AllowGet);
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

        /// <summary>
        /// Create form
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult CreateExperience()
        {
            try
            {
                #region " [ Declaration ] "
                
                ExperienceModel model = new ExperienceModel()
                {
                    ID = Guid.NewGuid(),
                    CreateBy = UserID,
                    Insert = true
                };
                
                #endregion

                return View(model);
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

        /// <summary>
        /// Create function
        /// </summary>
        /// <param name="model">EventModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateExperience(ExperienceModel model)
        {
            try
            {
                #region " [ Declaration ] "

                WorkingService _service = new WorkingService();

                #endregion

                #region " [ Main processing ] "
                
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                // Call to service
                return this.Json(_service.SaveExperience(model), JsonRequestBehavior.AllowGet);
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

        /// <summary>
        /// Edit form
        /// </summary>
        /// <param name="id">The experience identifier</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult EditExperience(string id)
        {
            try
            {
                #region " [ Declaration ] "
                
                WorkingService _service = new WorkingService();

                ViewBag.id = id;

                #endregion

                // Call to service
                ExperienceModel model = _service.GetExperienceItemByID(new ExperienceModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });

                return View(model);
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

        /// <summary>
        /// Edit function
        /// </summary>
        /// <param name="model">EventModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult EditExperience(ExperienceModel model)
        {
            try
            {
                #region " [ Declaration ] "

                WorkingService _service = new WorkingService();

                #endregion

                #region " [ Main processing ] "
                
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                #endregion

                // Call to service
                return this.Json(_service.SaveExperience(model), JsonRequestBehavior.AllowGet);
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

        /// <summary>
        /// Delete function
        /// </summary>
        /// <param name="model">EventModel</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        [HttpPost]
        public JsonResult DeleteExperience(ExperienceModel model)
        {
            try
            {
                #region " [ Declaration ] "

                WorkingService _service = new WorkingService();

                #endregion

                #region " [ Main process ] "

                model.CreateBy = UserID;
                model.DeleteBy = UserID;
                model.DeleteDate = DateTime.Now;

                #endregion

                //Call to service
                return this.Json(_service.DeleteExperience(model), JsonRequestBehavior.AllowGet);
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