﻿using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.UserException;
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
        [HttpGet]
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
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorController, ex);
            }
        }

        [Route("{navigationAlias}/{categoryAlias}")]
        [HttpGet]
        //[OutputCache(Duration = 86400, VaryByParam = "postAlias", Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult Category(string navigationAlias, string categoryAlias)
        {
            try
            {
                bool _isCategory = PageService.CheckIsCategoryPage(navigationAlias, categoryAlias);
                if (_isCategory)
                {
                    ViewBag.cateInfo = PageService.GetCategoryInfor(navigationAlias, categoryAlias);
                    return View(PageService.GetListPostDataByCategory(navigationAlias, categoryAlias));
                }
                var _model = PageService.GetPostInfor(navigationAlias, categoryAlias);
                ViewBag.related = PageService.Top6LastedPostByNavigationID(_model.CategoryID);
                return View("Post", _model);
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorController, ex);
            }
        }

        [Route("{navigationAlias}/{categoryAlias}/{postAlias}")]
        [HttpGet]
        //[OutputCache(Duration = 604800, VaryByParam = "postAlias", Location = System.Web.UI.OutputCacheLocation.ServerAndClient)]
        public ActionResult Post(string navigationAlias, string categoryAlias, string postAlias)
        {
            try
            {
                var _model = PageService.GetPostInfor(navigationAlias, categoryAlias, postAlias);
                ViewBag.related = PageService.Top6LastedPostByCategoryID(_model.CategoryID);
                return View(_model);
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorController, ex);
            }
        }

        [HttpGet]
        public ActionResult About()
        {
            try
            {
                return View(PageService.About());
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorController, ex);
            }
        }

        #region  " [ Partial View ] "

        /// <summary>
        /// Get top 3 lasted news 
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        [HttpGet]
        public ActionResult LastedPost()
        {
            try
            {
                return PartialView();
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorController, ex);
            }
        }

        [ChildActionOnly]
        [HttpGet]
        public ActionResult SameCategory()
        {
            try
            {
                return PartialView();
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorController, ex);
            }
        }


        #endregion

    }
}