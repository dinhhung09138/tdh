using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDH.Common;
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
                Log.WriteLog(FILE_NAME, "Index", Guid.NewGuid(), ex);
                throw new UserException(ex.Message);
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
                throw new UserException(FILE_NAME, "Navigation", ex);
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
                throw new UserException(FILE_NAME, "Banner", ex);
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
                throw new UserException(FILE_NAME, "ConnectedToMe", ex);
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
                throw new UserException(FILE_NAME, "SidebarGalary", ex);
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
                throw new UserException(FILE_NAME, "NewsLetter", ex);
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
                throw new UserException(FILE_NAME, "LastedNews", ex);
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
                throw new UserException(FILE_NAME, "TopView", ex);
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
                throw new UserException(FILE_NAME, "FooterPostByNavigation", ex);
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
                throw new UserException(FILE_NAME, "PopuplarCategory", ex);
            }
        }

        #endregion
    }
}