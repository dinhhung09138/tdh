using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utils;
using Utils.JqueryDatatable;
using TDH.Areas.Administrator.Models;

namespace TDH.Areas.Administrator.Controllers
{
    public class AdmPostController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator.Controllers/AdmPostController.cs";

        #endregion

        #region " [ Navigation ] "

        [HttpGet]
        public ActionResult Navigation()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Navigation", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public JsonResult Navigation(CustomDataTableRequestHelper requestData)
        {
            try
            {
                Services.NavigationService _service = new Services.NavigationService();
                requestData = requestData.SetOrderingColumnName();
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<NavigationModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<NavigationModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                return this.Json(new DataTableResponse<NavigationModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Navigation", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult CreateNavigation()
        {
            try
            {
                NavigationModel model = new NavigationModel()
                {
                    ID = Guid.NewGuid(),
                    CreateBy = UserID,
                    Insert = true
                };
                return View(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateNavigation", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNavigation(NavigationModel model)
        {
            try
            {
                Services.NavigationService _service = new Services.NavigationService();
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                if (_service.Save(model) == ResponseStatusCodeHelper.Success)
                {
                    Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                    {
                        Status = ResponseStatusCodeHelper.Success,
                        Message = Resources.Message.Success
                    };
                    TempData[CommonHelper.EXECUTE_RESULT] = _result;
                    return RedirectToAction("Navigation");
                }
                return View();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateNavigation", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult EditNavigation(string id)
        {
            try
            {
                Services.NavigationService _service = new Services.NavigationService();
                NavigationModel model = _service.GetItemByID(new NavigationModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                return View(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "EditNavigation", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNavigation(NavigationModel model)
        {
            try
            {
                Services.NavigationService _service = new Services.NavigationService();
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                if (_service.Save(model) == ResponseStatusCodeHelper.Success)
                {
                    Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                    {
                        Status = ResponseStatusCodeHelper.Success,
                        Message = Resources.Message.Success
                    };
                    TempData[CommonHelper.EXECUTE_RESULT] = _result;
                    return RedirectToAction("Navigation");
                }
                return View();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "EditNavigation", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult PublishNavigation(NavigationModel model)
        {
            try
            {
                Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.Success,
                    Message = Resources.Message.Success
                };
                //
                Services.NavigationService _service = new Services.NavigationService();
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.UpdateDate = DateTime.Now;
                if (_service.Publish(model) == ResponseStatusCodeHelper.Success)
                {
                    return this.Json(_result, JsonRequestBehavior.AllowGet);
                }
                _result.Status = ResponseStatusCodeHelper.Error;
                _result.Message = Resources.Message.Error;
                return this.Json(_result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "PublishNavigation", UserID, ex);
                throw new HttpException();
            }
        }
        
        [HttpPost]
        public ActionResult DeleteNavigation(NavigationModel model)
        {
            try
            {
                Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.Success,
                    Message = Resources.Message.Success
                };
                //
                Services.NavigationService _service = new Services.NavigationService();
                model.CreateBy = UserID;
                model.DeleteBy = UserID;
                model.DeleteDate = DateTime.Now;
                if (_service.Delete(model) == ResponseStatusCodeHelper.Success)
                {
                    return this.Json(_result, JsonRequestBehavior.AllowGet);
                }
                _result.Status = ResponseStatusCodeHelper.Error;
                _result.Message = Resources.Message.Error;
                return this.Json(_result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "DeleteNavigation", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult CheckDeleteNavigation(NavigationModel model)
        {
            try
            {
                Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.OK,
                    Message = ""
                };
                //
                Services.NavigationService _service = new Services.NavigationService();
                model.CreateBy = UserID;
                if (_service.CheckDelete(model) == ResponseStatusCodeHelper.OK)
                {
                    return this.Json(_result, JsonRequestBehavior.AllowGet);
                }
                _result.Status = ResponseStatusCodeHelper.NG;
                _result.Message = Resources.Message.CheckExists;
                return this.Json(_result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CheckDeleteNavigation", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion

        #region " [ Category ] "

        [HttpGet]
        public ActionResult Category()
        {
            try
            {
                Services.NavigationService _nServices = new Services.NavigationService();
                ViewBag.navigation = _nServices.GetAllWithNoChild(UserID);
                return View();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Category", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public JsonResult Category(CustomDataTableRequestHelper requestData)
        {
            try
            {
                if (requestData.Parameter1 == null)
                {
                    requestData.Parameter1 = "";
                }
                Services.CategoryService _service = new Services.CategoryService();
                requestData = requestData.SetOrderingColumnName();
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<CategoryModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<CategoryModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                return this.Json(new DataTableResponse<CategoryModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Category", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            try
            {
                Services.NavigationService _nServices = new Services.NavigationService();
                ViewBag.navigation = _nServices.GetAllWithNoChild(UserID);
                CategoryModel model = new CategoryModel()
                {
                    ID = Guid.NewGuid(),
                    CreateBy = UserID,
                    Insert = true
                };
                return View(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateCategory", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CategoryModel model)
        {
            try
            {
                Services.CategoryService _service = new Services.CategoryService();
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                if (_service.Save(model) == ResponseStatusCodeHelper.Success)
                {
                    Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                    {
                        Status = ResponseStatusCodeHelper.Success,
                        Message = Resources.Message.Success
                    };
                    TempData[CommonHelper.EXECUTE_RESULT] = _result;
                    return RedirectToAction("Category");
                }
                return View();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateCategory", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult EditCategory(string id)
        {
            try
            {
                Services.NavigationService _nServices = new Services.NavigationService();
                ViewBag.navigation = _nServices.GetAllWithNoChild(UserID);
                Services.CategoryService _service = new Services.CategoryService();
                CategoryModel model = _service.GetItemByID(new CategoryModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                return View(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "EditCategory", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(CategoryModel model)
        {
            try
            {
                Services.CategoryService _service = new Services.CategoryService();
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                if (_service.Save(model) == ResponseStatusCodeHelper.Success)
                {
                    Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                    {
                        Status = ResponseStatusCodeHelper.Success,
                        Message = Resources.Message.Success
                    };
                    TempData[CommonHelper.EXECUTE_RESULT] = _result;
                    return RedirectToAction("Category");
                }
                return View();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "EditCategory", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult PublishCategory(CategoryModel model)
        {
            try
            {
                Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.Success,
                    Message = Resources.Message.Success
                };
                //
                Services.CategoryService _service = new Services.CategoryService();
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.UpdateDate = DateTime.Now;
                if (_service.Publish(model) == ResponseStatusCodeHelper.Success)
                {
                    return this.Json(_result, JsonRequestBehavior.AllowGet);
                }
                _result.Status = ResponseStatusCodeHelper.Error;
                _result.Message = Resources.Message.Error;
                return this.Json(_result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "PublishCategory", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult OnNavigationCategory(CategoryModel model)
        {
            try
            {
                Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.Success,
                    Message = Resources.Message.Success
                };
                //
                Services.CategoryService _service = new Services.CategoryService();
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.UpdateDate = DateTime.Now;
                if (_service.OnNavigation(model) == ResponseStatusCodeHelper.Success)
                {
                    return this.Json(_result, JsonRequestBehavior.AllowGet);
                }
                _result.Status = ResponseStatusCodeHelper.Error;
                _result.Message = Resources.Message.Error;
                return this.Json(_result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "OnNavigationCategory", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult DeleteCategory(CategoryModel model)
        {
            try
            {
                Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.Success,
                    Message = Resources.Message.Success
                };
                //
                Services.CategoryService _service = new Services.CategoryService();
                model.CreateBy = UserID;
                model.DeleteBy = UserID;
                model.DeleteDate = DateTime.Now;
                if (_service.Delete(model) == ResponseStatusCodeHelper.Success)
                {
                    return this.Json(_result, JsonRequestBehavior.AllowGet);
                }
                _result.Status = ResponseStatusCodeHelper.Error;
                _result.Message = Resources.Message.Error;
                return this.Json(_result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "DeleteCategory", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult CheckDeleteCategory(CategoryModel model)
        {
            try
            {
                Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.OK,
                    Message = ""
                };
                //
                Services.CategoryService _service = new Services.CategoryService();
                model.CreateBy = UserID;
                if (_service.CheckDelete(model) == ResponseStatusCodeHelper.OK)
                {
                    return this.Json(_result, JsonRequestBehavior.AllowGet);
                }
                _result.Status = ResponseStatusCodeHelper.NG;
                _result.Message = Resources.Message.CheckExists;
                return this.Json(_result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CheckDeleteCategory", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion

        #region " [ News ] "

        [HttpGet]
        public ActionResult News()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "News", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public JsonResult News(CustomDataTableRequestHelper requestData)
        {
            try
            {
                Services.PostService _service = new Services.PostService();
                requestData = requestData.SetOrderingColumnName();
                Dictionary<string, object> _return = _service.List(requestData, UserID);
                if ((ResponseStatusCodeHelper)_return[DatatableCommonSetting.Response.STATUS] == ResponseStatusCodeHelper.OK)
                {
                    DataTableResponse<PostModel> itemResponse = _return[DatatableCommonSetting.Response.DATA] as DataTableResponse<PostModel>;
                    return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
                }
                return this.Json(new DataTableResponse<PostModel>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "News", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult CreateNews()
        {
            try
            {
                Services.NavigationService _nServices = new Services.NavigationService();
                ViewBag.navigation = _nServices.GetAllWithChild(UserID);
                Services.CategoryService _cServices = new Services.CategoryService();
                ViewBag.cate = _cServices.GetAll(UserID);
                PostModel model = new PostModel()
                {
                    ID = Guid.NewGuid(),
                    CreateBy = UserID,
                    Insert = true
                };
                return View(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateNews", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateNews(PostModel model)
        {
            try
            {
                Services.PostService _service = new Services.PostService();
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                if (_service.Save(model) == ResponseStatusCodeHelper.Success)
                {
                    Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                    {
                        Status = ResponseStatusCodeHelper.Success,
                        Message = Resources.Message.Success
                    };
                    TempData[CommonHelper.EXECUTE_RESULT] = _result;
                    return RedirectToAction("News");
                }
                return View();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CreateNews", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpGet]
        public ActionResult EditNews(string id)
        {
            try
            {
                Services.NavigationService _nServices = new Services.NavigationService();
                ViewBag.navigation = _nServices.GetAllWithChild(UserID);
                Services.CategoryService _cServices = new Services.CategoryService();
                ViewBag.cate = _cServices.GetAll(UserID);
                Services.PostService _service = new Services.PostService();
                PostModel model = _service.GetItemByID(new PostModel() { ID = new Guid(id), CreateBy = UserID, Insert = false });
                return View(model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "EditNews", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditNews(PostModel model)
        {
            try
            {
                Services.PostService _service = new Services.PostService();
                model.CreateBy = UserID;
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                if (_service.Save(model) == ResponseStatusCodeHelper.Success)
                {
                    Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                    {
                        Status = ResponseStatusCodeHelper.Success,
                        Message = Resources.Message.Success
                    };
                    TempData[CommonHelper.EXECUTE_RESULT] = _result;
                    return RedirectToAction("News");
                }
                return View();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "EditNews", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult PublishNews(PostModel model)
        {
            try
            {
                Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.Success,
                    Message = Resources.Message.Success
                };
                //
                Services.PostService _service = new Services.PostService();
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.UpdateDate = DateTime.Now;
                if (_service.Publish(model) == ResponseStatusCodeHelper.Success)
                {
                    return this.Json(_result, JsonRequestBehavior.AllowGet);
                }
                _result.Status = ResponseStatusCodeHelper.Error;
                _result.Message = Resources.Message.Error;
                return this.Json(_result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "PublishNews", UserID, ex);
                throw new HttpException();
            }
        }

        [HttpPost]
        public ActionResult DeleteNews(PostModel model)
        {
            try
            {
                Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                {
                    Status = ResponseStatusCodeHelper.Success,
                    Message = Resources.Message.Success
                };
                //
                Services.PostService _service = new Services.PostService();
                model.CreateBy = UserID;
                model.DeleteBy = UserID;
                model.DeleteDate = DateTime.Now;
                if (_service.Delete(model) == ResponseStatusCodeHelper.Success)
                {
                    return this.Json(_result, JsonRequestBehavior.AllowGet);
                }
                _result.Status = ResponseStatusCodeHelper.Error;
                _result.Message = Resources.Message.Error;
                return this.Json(_result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "DeleteNews", UserID, ex);
                throw new HttpException();
            }
        }

        #endregion

        #region " [ About ] "

        [HttpGet]
        public ActionResult About()
        {
            try
            {
                Services.AboutService _service = new Services.AboutService();
                var _model = _service.GetItemByID(new AboutModel() { CreateBy = UserID, Insert = false });
                return View(_model);
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "About", UserID, ex);
                throw new HttpException();
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult About(AboutModel model)
        {
            try
            {
                Services.AboutService _service = new Services.AboutService();
                model.CreateBy = UserID;
                model.UpdateBy = UserID;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                if (_service.Save(model) == ResponseStatusCodeHelper.Success)
                {
                    Utils.CommonModel.ExecuteResultModel _result = new Utils.CommonModel.ExecuteResultModel()
                    {
                        Status = ResponseStatusCodeHelper.Success,
                        Message = Resources.Message.Success
                    };
                    TempData[CommonHelper.EXECUTE_RESULT] = _result;
                    return RedirectToAction("About");
                }
                return View();
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "About", UserID, ex);
                throw new HttpException();
            }
        }
        
        #endregion

    }
}