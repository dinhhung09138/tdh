using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDH.Common;
using TDH.Filters;
using TDH.Services;

namespace TDH.Controllers
{
    public class PostController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Controllers/PostController.cs";

        #endregion

        [Route("{navigationAlias}")]
        //[OutputCache(Duration = 604800, VaryByParam = "postAlias", Location = System.Web.UI.OutputCacheLocation.ServerAndClient)]
        public ActionResult Navigation(string navigationAlias)
        {
            try
            {
                navigationAlias = navigationAlias.ToLower();
                if (navigationAlias == "gioi-thieu")
                {
                    return View("About", PageService.About());
                }
                var _navInfo = PageService.GetNavigationInfor(navigationAlias);
                ViewBag.navInfor = _navInfo;
                //
                var _lPost = PageService.Top6LastedPostByNavigationID(_navInfo.ID);
                if (_lPost.Count() > 0)
                {
                    //Post conneted to navigation
                    ViewBag.post = 1;
                    ViewBag.data = _lPost;
                    return View(_navInfo);
                }
                ViewBag.post = 0;
                ViewBag.data = PageService.GetListCategoryDataByNavigation(navigationAlias);
                return View(_navInfo);
            }
            catch(Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Navigation", Guid.NewGuid(), ex);
                throw new UserException(ex.Message);
            }
        }

        [Route("{navigationAlias}/{categoryAlias}")]
        //[OutputCache(Duration = 86400, VaryByParam = "postAlias", Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult Category(string navigationAlias, string categoryAlias)
        {
            try
            {
                bool _isCategory = PageService.CheckIsCategoryPage(navigationAlias, categoryAlias);
                if (_isCategory)
                {
                    ViewBag.cateInfo = PageService.GetCategoryInfor(categoryAlias);
                    return View(PageService.GetListPostDataByCategory(navigationAlias, categoryAlias));
                }
                var _model = PageService.GetPostInfor(navigationAlias, categoryAlias);
                ViewBag.related = PageService.Top6LastedPostByNavigationID(_model.CategoryID);
                return View("Post", _model);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Navigation", Guid.NewGuid(), ex);
                throw new UserException(ex.Message);
            }
        }

        [Route("{navigationAlias}/{categoryAlias}/{postAlias}")]
        //[OutputCache(Duration = 604800, VaryByParam = "postAlias", Location = System.Web.UI.OutputCacheLocation.ServerAndClient)]
        public ActionResult Post(string navigationAlias, string categoryAlias, string postAlias)
        {
            try
            {
                var _model = PageService.GetPostInfor(navigationAlias, categoryAlias, postAlias);
                ViewBag.related = PageService.Top6LastedPostByCategoryID(_model.CategoryID);
                return View(_model);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Post", Guid.NewGuid(), ex);
                throw new UserException(ex.Message);
            }
        }

        public ActionResult About()
        {
            try
            {
                return View(PageService.About());
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "About", Guid.NewGuid(), ex);
                throw new UserException(ex.Message);
            }
        }

        #region  " [ Partial View ] "

        /// <summary>
        /// Get top 3 lasted news 
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult LastedPost()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "LastedPost", Guid.NewGuid(), ex);
                throw new UserException(ex.Message);
            }
        }

        [ChildActionOnly]
        public ActionResult SameCategory()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "SameCategory", Guid.NewGuid(), ex);
                throw new UserException(ex.Message);
            }
        }


        #endregion

    }
}