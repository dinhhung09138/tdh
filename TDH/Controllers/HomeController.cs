using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.Caching;
using TDH.Common.UserException;
using TDH.Model.ViewModel.WebSite;
using TDH.Services;

namespace TDH.Controllers
{
    /// <summary>
    /// Home controller
    /// </summary>
    public class HomeController : BaseController
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Controllers/HomeController.cs";

        #endregion

        /// <summary>
        /// Home page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                ViewBag.category = await PageService.GetListCategoryShowOnHomePage();
                ViewBag.navigation = await PageService.GetListNavigationShowOnHomePage();
                return View(await PageService.GetHomeMetaContent());
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

        #region " [ Partial View ] "

        /// <summary>
        /// Navigation partial view
        /// </summary>
        /// <returns>PartialView</returns>
        [ChildActionOnly]
        [HttpGet]
        public ActionResult Navigation()
        {
            try
            {
                string key = "partial_nav";
                var model = new List<NavigationViewModel>();
                if (CacheExtension.Exists(key))
                {
                    model = CacheExtension.Get<List<NavigationViewModel>>(key);
                }
                else
                {
                    model = PageService.GetListNavigation();
                    CacheExtension.Set(model, key, DateTime.Now.AddDays(7));
                }
                return PartialView(model);
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

        /// <summary>
        /// Banner partial view
        /// </summary>
        /// <returns>PartialView</returns>
        [ChildActionOnly]
        [HttpGet]
        public ActionResult Banner()
        {
            try
            {
                ViewBag.banner = PageService.GetBannerInfor();
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

        /// <summary>
        /// Connect to me info partial view
        /// </summary>
        /// <returns>PartialView</returns>
        [ChildActionOnly]
        [HttpGet]
        public ActionResult ConnectedToMe()
        {
            try
            {
                string key = "partial_connecttome";
                var model = new MetaViewModel();
                if (CacheExtension.Exists(key))
                {
                    model = CacheExtension.Get<MetaViewModel>(key);
                }
                else
                {
                    model = PageService.GetShortIntroAboutMe();
                    CacheExtension.Set(model, key, DateTime.Now.AddDays(10));
                }
                return PartialView(model);
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
        public ActionResult SidebarGalary()
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

        /// <summary>
        /// Newsletter partial view
        /// </summary>
        /// <returns>PartialView</returns>
        [ChildActionOnly]
        [HttpGet]
        public ActionResult NewsLetter()
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

        /// <summary>
        /// top 4 lasted news
        /// </summary>
        /// <returns>PartialView</returns>
        [ChildActionOnly]
        [HttpGet]
        public ActionResult LastedNews()
        {
            try
            {
                string key = "partial_lastednew";
                var model = new List<PostViewModel>();
                if (CacheExtension.Exists(key))
                {
                    model = CacheExtension.Get<List<PostViewModel>>(key);
                }
                else
                {
                    model = PageService.GetTop4LastedNews();
                    CacheExtension.Set(model, key, DateTime.Now.AddDays(2));
                }
                return PartialView(model);
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

        /// <summary>
        /// Get two post item with the most view
        /// </summary>
        /// <returns>PartialView</returns>
        [ChildActionOnly]
        [HttpGet]
        public ActionResult TopView()
        {
            try
            {
                string key = "partial_topview";
                var model = new List<PostViewModel>();
                if (CacheExtension.Exists(key))
                {
                    model = CacheExtension.Get<List<PostViewModel>>(key);
                }
                else
                {
                    model = PageService.GetTop2Views();
                    CacheExtension.Set(model, key, DateTime.Now.AddDays(2));
                }
                return PartialView(model);
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

        /// <summary>
        /// Footer post by navigation partial view
        /// </summary>
        /// <returns>PartialView</returns>
        [ChildActionOnly]
        [HttpGet]
        public ActionResult FooterPostByNavigation()
        {
            try
            {
                string key = "partial_footerbynav";
                var model = new List<NavigationViewModel>();
                if (CacheExtension.Exists(key))
                {
                    model = CacheExtension.Get<List<NavigationViewModel>>(key);
                }
                else
                {
                    model = PageService.Get2NavigationOnFooter();
                    CacheExtension.Set(model, key, DateTime.Now.AddDays(2));
                }
                return PartialView(model);
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

        /// <summary>
        /// Popular category partial view
        /// </summary>
        /// <returns>PartialView</returns>
        [ChildActionOnly]
        [HttpGet]
        public ActionResult PopuplarCategory()
        {
            try
            {
                string key = "partial_populercate";
                var model = new List<CategoryViewModel>();
                if (CacheExtension.Exists(key))
                {
                    model = CacheExtension.Get<List<CategoryViewModel>>(key);
                }
                else
                {
                    model = PageService.GetListCategoryOnFooter();
                    CacheExtension.Set(model, key, DateTime.Now.AddDays(7));
                }
                return PartialView(model);
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