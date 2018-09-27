using System;
using System.Web.Mvc;
using TDH.Common;
using TDH.Common.UserException;
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
        public ActionResult Index()
        {
            try
            {
                var _metaModel = PageService.getHomeMetaContent();
                ViewBag.category = PageService.GetListCategoryShowOnHomePage();
                ViewBag.navigation = PageService.GetListNavigationShowOnHomePage();
                return View(_metaModel);
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "Index", 500, "Controller has an error", ex);
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
                return PartialView(PageService.GetListNavigation());
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "Navigation", 500, "Controller has an error", ex);
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
                throw new UserException(FILE_NAME, "Banner", 500, "Controller has an error", ex);
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
                return PartialView(PageService.GetShortIntroAboutMe());
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "ConnectedToMe", 500, "Controller has an error", ex);
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
                throw new UserException(FILE_NAME, "SidebarGalary", 500, "Controller has an error", ex);
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
                throw new UserException(FILE_NAME, "NewsLetter", 500, "Controller has an error", ex);
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
                return PartialView(PageService.GetTop4LastedNews());
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "LastedNews", 500, "Controller has an error", ex);
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
                return PartialView(PageService.GetTop2Views());
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "TopView", 500, "Controller has an error", ex);
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
                return PartialView(PageService.Get2NavigationOnFooter());
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "FooterPostByNavigation", 500, "Controller has an error", ex);
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
                return PartialView(PageService.GetListCategoryOnFooter());
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "PopuplarCategory", 500, "Controller has an error", ex);
            }
        }

        #endregion
    }
}