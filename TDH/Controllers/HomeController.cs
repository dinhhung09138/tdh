using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDH.ViewModel;
using TDH.Services;
using TDH.Filters;

namespace TDH.Controllers
{
    public class HomeController : Controller
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Controllers/HomeController.cs";

        #endregion

        public ActionResult Index()
        {
            try
            {
                var _metaModel = PageService.getHomeMetaContent();
                ViewBag.category = PageService.GetListCategoryShowOnHomePage();
                ViewBag.navigation = PageService.GetListNavigationShowOnHomePage();
                return View(_metaModel);
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Index", Guid.NewGuid(), ex);
                throw new UserException();
            }
        }
        
        #region " [ Partial View ] "

        [ChildActionOnly]
        public ActionResult Navigation()
        {
            try
            {
                return PartialView(PageService.GetListNavigation());
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Navigation", Guid.NewGuid(), ex);
                throw new UserException();
            }
        }

        [ChildActionOnly]
        public ActionResult Banner()
        {
            try
            {
                ViewBag.banner = PageService.GetBannerInfor();
                return PartialView();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Banner", Guid.NewGuid(), ex);
                throw new UserException();
            }
        }

        [ChildActionOnly]
        public ActionResult ConnectedToMe()
        {
            try
            {
                return PartialView(PageService.GetShortIntroAboutMe());
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "ConnectedToMe", Guid.NewGuid(), ex);
                throw new UserException();
            }
        }

        [ChildActionOnly]
        public ActionResult SidebarGalary()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "SidebarGalary", Guid.NewGuid(), ex);
                throw new UserException();
            }
        }

        [ChildActionOnly]
        public ActionResult NewsLetter()
        {
            try
            {
                return PartialView();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "NewsLetter", Guid.NewGuid(), ex);
                throw new UserException();
            }
        }

        /// <summary>
        /// top 4 lasted news
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult LastedNews()
        {
            try
            {
                return PartialView(PageService.GetTop4LastedNews());
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "LastedNews", Guid.NewGuid(), ex);
                throw new UserException();
            }
        }

        /// <summary>
        /// Get two post item with the most view
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult TopView()
        {
            try
            {
                return PartialView(PageService.GetTop2Views());
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "TopView", Guid.NewGuid(), ex);
                throw new UserException();
            }
        }

        [ChildActionOnly]
        public ActionResult FooterPostByNavigation()
        {
            try
            {
                return PartialView(PageService.Get2NavigationOnFooter());
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "FooterPostByNavigation", Guid.NewGuid(), ex);
                throw new UserException();
            }
        }

        [ChildActionOnly]
        public ActionResult PopuplarCategory()
        {
            try
            {
                return PartialView(PageService.GetListCategoryOnFooter());
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "PopuplarCategory", Guid.NewGuid(), ex);
                throw new UserException();
            }
        }

        #endregion
    }
}