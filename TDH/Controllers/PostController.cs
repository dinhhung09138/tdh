using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.UserException;
using TDH.Common.Caching;
using TDH.Services;
using TDH.Model.ViewModel.WebSite;
using System.Collections.Generic;

namespace TDH.Controllers
{
    public class PostController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Controllers/PostController.cs";

        /// <summary>
        /// 
        /// </summary>
        private readonly string PROGRAMMING = "lap-trinh";

        /// <summary>
        /// 
        /// </summary>
        private readonly string PORTFOLIO = "portfolio";

        #endregion

        [Route("{navigationAlias}")]
        [HttpGet]
        public async Task<ActionResult> Navigation(string navigationAlias)
        {
            try
            {
                navigationAlias = navigationAlias.ToLower();

                #region " [ Portfolio Page ] "

                if (navigationAlias == PORTFOLIO)
                {
                    ViewBag.skills = await PageService.GetListSkill();
                    ViewBag.education = await PageService.GetEducation();
                    return View("Portfolio", await PageService.PortfolioInfo());
                }

                #endregion

                #region " [ Get navigation info ] "

                var _navInfo = new NavigationViewModel();
                if (CacheExtension.Exists(navigationAlias))
                {
                    _navInfo = CacheExtension.Get<NavigationViewModel>(navigationAlias);
                }
                else
                {
                    _navInfo = await PageService.GetNavigationInfor(navigationAlias);
                    CacheExtension.Add<NavigationViewModel>(_navInfo, navigationAlias, 168);
                }

                #endregion

                #region " [ Programming Page ] "

                if (navigationAlias == PROGRAMMING)
                {
                    ViewBag.post = await PageService.ProgrammingGetTop20(navigationAlias);

                    return View("Programming", _navInfo);
                }

                #endregion

                #region " [ Intro page ] "

                if (navigationAlias == "gioi-thieu")
                {
                    return View("About", await PageService.About());
                }

                #endregion

                #region " [ Post Page ] "

                var _lPost = await PageService.Top6LastedPostByNavigationID(_navInfo.ID);
                if (_lPost.Count() > 0)
                {
                    //Post conneted to navigation
                    ViewBag.post = 1;
                    ViewBag.data = _lPost;
                    return View(_navInfo);
                }
                ViewBag.post = 0;
                ViewBag.data = await PageService.GetListCategoryDataByNavigation(navigationAlias);
                return View(_navInfo);

                #endregion

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
        public async Task<ActionResult> Category(string navigationAlias, string categoryAlias)
        {
            try
            {
                navigationAlias = navigationAlias.ToLower();

                #region " [ Programming Page ] "

                if (navigationAlias == PROGRAMMING)
                {
                    var _navInfo = new NavigationViewModel();
                    if (CacheExtension.Exists(navigationAlias))
                    {
                        _navInfo = CacheExtension.Get<NavigationViewModel>(navigationAlias);
                    }
                    else
                    {
                        _navInfo = await PageService.GetNavigationInfor(navigationAlias);
                        CacheExtension.Add(_navInfo, navigationAlias, 168);
                    }
                    ViewBag.navInfor = _navInfo;
                    //
                    ViewBag.cateInfo = await PageService.GetCategoryInfor(navigationAlias, categoryAlias);

                    return View("ProgrammingCategory", await PageService.GetListPostDataByCategory(navigationAlias, categoryAlias));
                }

                #endregion

                #region " [ Post ] "

                bool _isCategory = await PageService.CheckIsCategoryPage(navigationAlias, categoryAlias);
                if (_isCategory)
                {
                    //Get category info
                    var _cateInfo = new CategoryViewModel();
                    if (CacheExtension.Exists(navigationAlias + categoryAlias))
                    {
                        _cateInfo = CacheExtension.Get<CategoryViewModel>(navigationAlias + categoryAlias);
                    }
                    else
                    {
                        _cateInfo = await PageService.GetCategoryInfor(navigationAlias, categoryAlias);
                        CacheExtension.Add<CategoryViewModel>(_cateInfo, navigationAlias + categoryAlias, 168);
                    }
                    ViewBag.cateInfo = _cateInfo;

                    //List post by category
                    var _catePosts = new List<PostViewModel>();
                    if(CacheExtension.Exists("ListPostByCate" + categoryAlias))
                    {
                        _catePosts = CacheExtension.Get<List<PostViewModel>>("ListPostByCate" + categoryAlias);
                    }
                    else
                    {
                        _catePosts = await PageService.GetListPostDataByCategory(navigationAlias, categoryAlias);
                        CacheExtension.Add(_catePosts, "ListPostByCate" + categoryAlias, 72);
                    }
                    return View(_catePosts);
                }
                //Post info
                var _model = new PostViewModel();
                if (CacheExtension.Exists("Post" + navigationAlias))
                {
                    _model = CacheExtension.Get<PostViewModel>("Post" + navigationAlias);
                }
                else
                {
                    _model = await PageService.GetPostInfor(navigationAlias, categoryAlias);
                    CacheExtension.Add(_model, "Post" + navigationAlias, 48);
                }
                ViewBag.related = await PageService.Top6LastedPostByNavigationID(_model.CategoryID);
                return View("Post", _model);
                
                #endregion

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
        public async Task<ActionResult> Post(string navigationAlias, string categoryAlias, string postAlias)
        {
            try
            {
                navigationAlias = navigationAlias.ToLower();

                #region " [ Get navigation info ] "

                var _navInfo = new NavigationViewModel();
                if (CacheExtension.Exists(navigationAlias))
                {
                    _navInfo = CacheExtension.Get<NavigationViewModel>(navigationAlias);
                }
                else
                {
                    _navInfo = await PageService.GetNavigationInfor(navigationAlias);
                    CacheExtension.Add<NavigationViewModel>(_navInfo, navigationAlias, 168);
                }
                ViewBag.navInfor = _navInfo;

                #endregion

                #region " [ Get post info ] "

                var _model = new PostViewModel();
                if (CacheExtension.Exists(postAlias))
                {
                    _model = CacheExtension.Get<PostViewModel>(postAlias);
                }
                else
                {
                    _model = await PageService.GetPostInfor(navigationAlias, categoryAlias, postAlias);
                    CacheExtension.Add(_model, postAlias, 48);
                }

                #endregion

                ViewBag.related = await PageService.Top6LastedPostByCategoryID(_model.CategoryID);

                #region " [ Programming Page ] "

                if (navigationAlias == PROGRAMMING)
                {
                    return View("ProgrammingPost", _model);
                }

                #endregion

                #region " [ Post ] "

                return View(_model);

                #endregion

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

        #region " [ Programming Partial View ] "

        [ChildActionOnly]
        [HttpGet]
        public ActionResult ProgrammingCategoryPartialView(string navAlias)
        {
            try
            {
                ViewBag.cate = PageService.ProgrammingCategory(navAlias);
                return PartialView();
            }
            catch(UserException uEx)
            {
                throw uEx;
            }
            catch(Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorController, ex);
            }
        }

        [ChildActionOnly]
        [HttpGet]
        public ActionResult ProgrammingRelatedView()
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
        public ActionResult ProgrammingPopularView(string navAlias)
        {
            try
            {
                ViewBag.post = PageService.ProgrammingGetTop6View(navAlias);
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