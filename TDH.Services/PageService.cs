using System;
using System.Collections.Generic;
using System.Linq;
using TDH.Common;
using TDH.Common.UserException;
using TDH.DataAccess;
using TDH.Model.ViewModel.WebSite;

namespace TDH.Services
{
    /// <summary>
    /// User display
    /// Page services
    /// </summary>
    public class PageService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private static readonly string FILE_NAME = "Services/PageService.cs";

        #endregion

        #region " [ Home page ] "

        /// <summary>
        /// Get home page meta content
        /// </summary>
        /// <returns>MetaViewModel</returns>
        public static MetaViewModel getHomeMetaContent()
        {
            MetaViewModel _meta = new MetaViewModel();
            try
            {
                using (var _context = new TDHEntities())
                {
                    var _list = _context.WEB_CONFIGURATION.Where(m => m.key.Contains("homepage_")).ToList();
                    _meta.MetaTitle = _list.FirstOrDefault(m => m.key == "homepage_title").value;
                    _meta.MetaDescription = _list.FirstOrDefault(m => m.key == "homepage_description").value;
                    _meta.MetaKeywords = _list.FirstOrDefault(m => m.key == "homepage_keyword").value;
                    _meta.MetaImage = _list.FirstOrDefault(m => m.key == "homepage_image").value;
                    _meta.MetaOgImage = _list.FirstOrDefault(m => m.key == "homepage_og_image").value;
                    _meta.MetaTwitterImage = _list.FirstOrDefault(m => m.key == "homepage_tt_image").value;
                    return _meta;
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "getHomeMetaContent", ex);
            }
        }

        /// <summary>
        /// List of navigation
        /// </summary>
        /// <returns>List<NavigationViewModel></returns>
        public static List<NavigationViewModel> GetListNavigation()
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    return (from nav in _context.WEB_NAVIGATION
                            where nav.publish && !nav.deleted
                            orderby nav.ordering descending
                            select new NavigationViewModel()
                            {
                                Title = nav.title,
                                Alias = nav.alias,
                                Categories = (from cate in _context.WEB_CATEGORY
                                              where nav.id == cate.navigation_id && cate.publish && !cate.deleted && cate.show_on_nav
                                              orderby cate.ordering descending
                                              select new CategoryViewModel()
                                              {
                                                  Title = cate.title,
                                                  Alias = cate.alias
                                              }).ToList()
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "GetListNavigation", ex);
            }
        }

        /// <summary>
        /// Get list category and post data to show on homepage
        /// Get top 6 post for each navigation
        /// </summary>
        /// <returns>List<NavigationViewModel></returns>
        public static List<NavigationViewModel> GetListNavigationShowOnHomePage()
        {
            List<NavigationViewModel> _return = new List<NavigationViewModel>();
            try
            {
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.WEB_HOME_NAVIGATION
                                 join n in _context.WEB_NAVIGATION on m.navigation_id equals n.id
                                 where n.publish && !n.deleted
                                 select new NavigationViewModel()
                                 {
                                     Title = n.title,
                                     Alias = n.alias,
                                     Posts = _context.WEB_POST.Where(p => p.navigation_id == n.id && p.publish && !p.deleted).OrderByDescending(p => p.create_date).Select(p => new PostViewModel()
                                     {
                                         Title = p.title,
                                         Description = p.description,
                                         Image = p.image,
                                         Alias = p.alias,
                                         CreateDate = p.create_date
                                     }).Take(6).ToList()
                                 }).ToList();
                    return _list;
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "GetListNavigationShowOnHomePage", ex);
            }
        }

        /// <summary>
        /// Get list category and post data to show on homepage
        /// Get list of top 5 of post for each category
        /// </summary>
        /// <returns>List<CategoryViewModel></returns>
        public static List<CategoryViewModel> GetListCategoryShowOnHomePage()
        {
            List<CategoryViewModel> _return = new List<CategoryViewModel>();
            try
            {
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.WEB_HOME_CATEGORY
                                 join c in _context.WEB_CATEGORY on m.category_id equals c.id
                                 join n in _context.WEB_NAVIGATION on c.navigation_id equals n.id
                                 where c.publish && !c.deleted && n.publish && !n.deleted
                                 select new CategoryViewModel()
                                 {
                                     Title = c.title,
                                     Alias = c.alias,
                                     Posts = _context.WEB_POST.Where(p => p.category_id == c.id && p.publish && !p.deleted).OrderByDescending(p => p.create_date).Select(p => new PostViewModel()
                                     {
                                         Title = p.title,
                                         Description = p.description,
                                         Image = p.image,
                                         Alias = p.alias,
                                         CreateDate = p.create_date
                                     }).Take(5).ToList()
                                 }).ToList();
                    return _list;
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "GetListCategoryShowOnHomePage", ex);
            }
        }

        /// <summary>
        /// Get list category and count to show on footer
        /// </summary>
        /// <returns>List<CategoryViewModel></returns>
        public static List<CategoryViewModel> GetListCategoryOnFooter()
        {
            List<CategoryViewModel> _return = new List<CategoryViewModel>();
            try
            {
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.WEB_CATEGORY
                                 join n in _context.WEB_NAVIGATION on m.navigation_id equals n.id
                                 where m.publish && !m.deleted && n.publish && !n.deleted
                                 select new CategoryViewModel()
                                 {
                                     Title = m.title,
                                     Alias = m.alias,
                                     Count = _context.WEB_POST.Count(p => p.category_id == m.id)
                                 }).Take(9).ToList();

                    var _lNavigation = (from m in _context.WEB_NAVIGATION
                                        where m.publish && !m.deleted
                                        select new CategoryViewModel()
                                        {
                                            Title = m.title,
                                            Alias = m.alias,
                                            Count = _context.WEB_POST.Count(p => p.navigation_id == m.id)
                                        }).Take(9).ToList();

                    foreach (var item in _lNavigation)
                    {
                        _list.Add(item);
                    }

                    return _list.OrderByDescending(m => m.Count).Take(9).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "GetListCategoryOnFooter", ex);
            }
        }

        /// <summary>
        /// Get two navigation item to show on footer
        /// Get top 2 navigation
        /// Get top 3 posts for each navigation.
        /// </summary>
        /// <returns>List<NavigationViewModel></returns>
        public static List<NavigationViewModel> Get2NavigationOnFooter()
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    return (from hn in _context.WEB_HOME_NAVIGATION
                            join nav in _context.WEB_NAVIGATION on hn.navigation_id equals nav.id
                            orderby hn.ordering descending
                            select new NavigationViewModel()
                            {
                                Title = nav.title,
                                Alias = nav.alias,
                                Posts = (from p in _context.WEB_POST
                                         where p.navigation_id == nav.id && p.publish && !p.deleted
                                         orderby p.create_date descending
                                         select new PostViewModel()
                                         {
                                             Image = p.image,
                                             Title = p.title,
                                             Alias = p.alias,
                                             CreateDate = p.create_date
                                         }).Take(3).ToList()
                            }).Take(2).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "Get2NavigationOnFooter", ex);
            }
        }

        /// <summary>
        /// Get banner infor
        /// </summary>
        /// <returns>string</returns>
        public static string GetBannerInfor()
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    return _context.WEB_CONFIGURATION.Where(m => m.key.Contains("banner")).FirstOrDefault().value;
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "GetBannerInfor", ex);
            }
        }

        #endregion

        #region " [ Post Common ] "

        /// <summary>
        /// Get navigation infor
        /// </summary>
        /// <param name="navAlias">Navigation alias</param>
        /// <returns>NavigationViewModel</returns>
        public static NavigationViewModel GetNavigationInfor(string navAlias)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    var _item = context.WEB_NAVIGATION.Where(m => m.alias == navAlias && m.publish && !m.deleted)
                                             .Select(m => new { m.id, m.alias, m.title, m.image, m.meta_title, m.meta_description, m.meta_keywords, m.meta_og_image, m.meta_twitter_image })
                                             .FirstOrDefault();
                    if (_item == null)
                    {
                        throw new UserException(FILE_NAME, "GetNavigationInfor", null);
                    }
                    return new NavigationViewModel()
                    {
                        ID = _item.id,
                        Alias = _item.alias,
                        Title = _item.title,
                        MetaImage = _item.image,
                        MetaTitle = _item.meta_title,
                        MetaDescription = _item.meta_description,
                        MetaKeywords = _item.meta_keywords,
                        MetaOgImage = _item.meta_og_image,
                        MetaTwitterImage = _item.meta_twitter_image
                    };
                }
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "GetNavigationInfor", ex);
            }
        }

        /// <summary>
        /// Get category infor
        /// </summary>
        /// <param name="cateAlias">Category alias</param>
        /// <returns>CategoryViewModel</returns>
        public static CategoryViewModel GetCategoryInfor(string cateAlias)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    var _item = context.WEB_CATEGORY.Where(m => m.alias == cateAlias && m.publish && !m.deleted)
                                             .Select(m => new { m.id, m.navigation_id, m.alias, m.title, m.image, m.meta_title, m.meta_description, m.meta_keywords, m.meta_og_image, m.meta_twitter_image })
                                             .FirstOrDefault();
                    if (_item == null)
                    {
                        throw new UserException(FILE_NAME, "GetCategoryInfor", null);
                    }
                    return new CategoryViewModel()
                    {
                        ID = _item.id,
                        NavigationID = _item.navigation_id,
                        Alias = _item.alias,
                        Title = _item.title,
                        MetaImage = _item.image,
                        MetaTitle = _item.meta_title,
                        MetaDescription = _item.meta_description,
                        MetaKeywords = _item.meta_keywords,
                        MetaOgImage = _item.meta_og_image,
                        MetaTwitterImage = _item.meta_twitter_image
                    };
                }
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "GetCategoryInfor", ex);
            }
        }

        /// <summary>
        /// Get post infor
        /// </summary>
        /// <param name="navAlias">Navigation alias</param>
        /// <param name="postAlias">Post alias</param>
        /// <returns>PostViewModel</returns>
        public static PostViewModel GetPostInfor(string navAlias, string postAlias)
        {
            try
            {
                var _nav = GetNavigationInfor(navAlias);
                using (var context = new TDHEntities())
                {
                    var _item = context.WEB_POST.Where(m => m.alias == postAlias && m.publish && !m.deleted)
                                             .Select(m => new { m.navigation_id, m.alias, m.title, m.description, m.content, m.image, m.is_navigation, m.meta_title, m.meta_description, m.meta_keywords, m.meta_og_image, m.meta_twitter_image })
                                             .FirstOrDefault();
                    if (_item == null || !_item.is_navigation)
                    {
                        throw new UserException(FILE_NAME, "GetPostInfor", null);
                    }
                    return new PostViewModel()
                    {
                        CategoryID = _item.navigation_id ?? Guid.NewGuid(),
                        Alias = _item.alias,
                        Title = _item.title,
                        Description = _item.description,
                        Content = _item.content,
                        Image = _item.image,
                        MetaImage = _item.image,
                        MetaTitle = _item.meta_title,
                        MetaDescription = _item.meta_description,
                        MetaKeywords = _item.meta_keywords,
                        MetaOgImage = _item.meta_og_image,
                        MetaTwitterImage = _item.meta_twitter_image
                    };
                }
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "GetPostInfor", ex);
            }
        }

        /// <summary>
        /// Get post infor
        /// </summary>
        /// <param name="navAlias">Navigation alias</param>
        /// <param name="cateAlias">Category alias</param>
        /// <param name="postAlias">Post alias</param>
        /// <returns>PostViewModel</returns>
        public static PostViewModel GetPostInfor(string navAlias, string cateAlias, string postAlias)
        {
            try
            {
                var _nav = GetNavigationInfor(navAlias);
                var _cate = GetCategoryInfor(cateAlias);
                using (var context = new TDHEntities())
                {
                    var _item = context.WEB_POST.Where(m => m.alias == postAlias && m.publish && !m.deleted)
                                             .Select(m => new { m.alias, m.title, m.category_id, m.description, m.content, m.create_date, m.image, m.is_navigation, m.meta_title, m.meta_description, m.meta_keywords, m.meta_og_image, m.meta_twitter_image })
                                             .FirstOrDefault();
                    if (_item == null || _item.is_navigation)
                    {
                        throw new UserException(FILE_NAME, "GetPostInfor", null);
                    }
                    return new PostViewModel()
                    {
                        CategoryID = _item.category_id ?? Guid.NewGuid(),
                        Alias = _item.alias,
                        Title = _item.title,
                        Description = _item.description,
                        Content = _item.content,
                        CreateDate = _item.create_date,
                        Image = _item.image,
                        MetaImage = _item.image,
                        MetaTitle = _item.meta_title,
                        MetaDescription = _item.meta_description,
                        MetaKeywords = _item.meta_keywords,
                        MetaOgImage = _item.meta_og_image,
                        MetaTwitterImage = _item.meta_twitter_image
                    };
                }
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "GetPostInfor", ex);
            }
        }

        /// <summary>
        /// Get top 4 lasted news
        /// </summary>
        /// <returns>List<PostViewModel></returns>
        public static List<PostViewModel> GetTop4LastedNews()
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    return _context.WEB_POST.Where(m => m.publish && !m.deleted)
                                             .OrderByDescending(m => m.create_date)
                                             .Select(m => new PostViewModel()
                                             {
                                                 Alias = m.alias,
                                                 Title = m.title,
                                                 Image = m.image,
                                                 CreateDate = m.create_date
                                             })
                                             .Take(4).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "GetTop4LastedNews", ex);
            }
        }

        /// <summary>
        /// Get 2 news has largest view
        /// </summary>
        /// <returns>List<PostViewModel></returns>
        public static List<PostViewModel> GetTop2Views()
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    return _context.WEB_POST.Where(m => m.publish && !m.deleted)
                                             .OrderByDescending(m => m.view)
                                             .Select(m => new PostViewModel()
                                             {
                                                 Alias = m.alias,
                                                 Title = m.title,
                                                 Image = m.image,
                                                 CreateDate = m.create_date
                                             })
                                             .Take(2).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "GetTop2Views", ex);
            }
        }

        #endregion

        #region " [ Navigation Page ] "

        /// <summary>
        /// Get list category by navigation alias
        /// </summary>
        /// <param name="navigationAlias"></param>
        /// <returns>List<CategoryViewModel></returns>
        public static List<CategoryViewModel> GetListCategoryDataByNavigation(string navigationAlias)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    return (from m in context.WEB_CATEGORY
                            join n in context.WEB_NAVIGATION on m.navigation_id equals n.id
                            where m.publish && !m.deleted && n.publish && !n.deleted && n.alias == navigationAlias
                            orderby m.ordering descending
                            select new CategoryViewModel()
                            {
                                Title = m.title,
                                Alias = m.alias,
                                Posts = context.WEB_POST.Where(p => p.category_id == m.id && p.publish && !p.deleted).OrderByDescending(p => p.create_date).Select(p => new PostViewModel()
                                {
                                    Title = p.title,
                                    Description = p.description,
                                    Image = p.image,
                                    Alias = p.alias,
                                    CreateDate = p.create_date
                                }).Take(12).ToList()
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "GetListCategoryDataByNavigation", ex);
            }
        }

        #endregion

        #region " [ Category Page ] "

        /// <summary>
        /// Check page is post of category
        /// </summary>
        /// <param name="navigationAlias"></param>
        /// <param name="categoryAlias"></param>
        /// <returns>true: category page, false: post page</returns>
        public static bool CheckIsCategoryPage(string navigationAlias, string categoryAlias)
        {
            try
            {
                NavigationViewModel _nav = GetNavigationInfor(navigationAlias);
                using (var context = new TDHEntities())
                {
                    var _post = context.WEB_POST.FirstOrDefault(m => m.alias == categoryAlias && m.publish && !m.deleted);
                    if (_post != null)
                    {
                        if (!_post.is_navigation || _nav.ID != _post.navigation_id)
                        {
                            throw new UserException(FILE_NAME, "CheckIsCategoryPage", null);
                        }
                        return false;
                    }
                    var _cate = context.WEB_CATEGORY.FirstOrDefault(m => m.alias == categoryAlias);
                    if (_cate == null || _cate.navigation_id != _nav.ID)
                    {
                        throw new UserException(FILE_NAME, "CheckIsCategoryPage", null);
                    }
                    return true;
                }
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "CheckIsCategoryPage", ex);
            }
        }

        /// <summary>
        /// Get list post data with has category and navigation
        /// </summary>
        /// <param name="categoryAlias">Category alias</param>
        /// <returns>List<PostViewModel></returns>
        public static List<PostViewModel> GetListPostDataByCategory(string navigationAlias, string categoryAlias)
        {
            try
            {
                NavigationViewModel _nav = GetNavigationInfor(navigationAlias);
                CategoryViewModel _cate = GetCategoryInfor(categoryAlias);
                if (_cate.NavigationID != _nav.ID)
                {
                    throw new UserException(FILE_NAME, "GetListPostDataByCategory", null);
                }
                List<PostViewModel> _return = new List<PostViewModel>();
                using (var context = new TDHEntities())
                {
                    return context.WEB_POST.Where(p => p.category_id == _cate.ID)
                                             .OrderByDescending(m => m.create_date)
                                             .Select(m => new PostViewModel()
                                             {
                                                 Alias = m.alias,
                                                 Title = m.title,
                                                 Description = m.description,
                                                 Image = m.image,
                                                 CreateDate = m.create_date
                                             })
                                             .Take(12).ToList();
                }
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "GetListPostDataByCategory", ex);
            }
        }


        #endregion

        #region " [ Post ] "

        /// <summary>
        /// Get top 6 news by navigation identifier
        /// </summary>
        /// <param name="navID">Navigation identifier</param>
        /// <returns>List<PostViewModel></returns>
        public static List<PostViewModel> Top6LastedPostByNavigationID(Guid navID)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    return _context.WEB_POST.Where(p => p.navigation_id == navID)
                                             .OrderByDescending(m => m.create_date)
                                             .Select(m => new PostViewModel()
                                             {
                                                 Alias = m.alias,
                                                 Title = m.title,
                                                 Description = m.description,
                                                 Image = m.image,
                                                 CreateDate = m.create_date
                                             })
                                             .Take(6).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "Top6LastedPostByNavigationID", ex);
            }
        }

        /// <summary>
        /// Get top 6 news by category identifier
        /// </summary>
        /// <param name="cateID">Category identifier</param>
        /// <returns>List<PostViewModel></returns>
        public static List<PostViewModel> Top6LastedPostByCategoryID(Guid cateID)
        {
            try
            {
                List<PostViewModel> _return = new List<PostViewModel>();
                using (var _context = new TDHEntities())
                {
                    return _context.WEB_POST.Where(p => p.category_id == cateID)
                                             .OrderByDescending(m => m.create_date)
                                             .Select(m => new PostViewModel()
                                             {
                                                 Alias = m.alias,
                                                 Title = m.title,
                                                 Description = m.description,
                                                 Image = m.image,
                                                 CreateDate = m.create_date
                                             })
                                             .Take(6).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "Top6LastedPostByCategoryID", ex);
            }
        }


        #endregion

        /// <summary>
        /// Get short introduction about me
        /// </summary>
        /// <returns>MetaViewModel</returns>
        public static MetaViewModel GetShortIntroAboutMe()
        {
            MetaViewModel _meta = new MetaViewModel();
            try
            {
                using (var _context = new TDHEntities())
                {
                    var _list = _context.WEB_CONFIGURATION.Where(m => m.key.Contains("intro_")).ToList();
                    _meta.MetaDescription = _list.FirstOrDefault(m => m.key == "intro_content").value;
                    _meta.MetaImage = _list.FirstOrDefault(m => m.key == "intro_avatar").value;
                    return _meta;
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "GetShortIntroAboutMe", ex);
            }
        }

        #region " [ About ] "

        /// <summary>
        /// About infor
        /// </summary>
        /// <returns>PostViewModel</returns>
        public static PostViewModel About()
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    var _item = _context.WEB_ABOUT.FirstOrDefault();
                    if (_item == null)
                    {
                        throw new UserException(FILE_NAME, "About", null);
                    }
                    return new PostViewModel()
                    {
                        Title = _item.meta_title,
                        Description = _item.meta_description,
                        Content = _item.content,
                        CreateDate = _item.create_date,
                        Image = _item.image,
                        MetaImage = _item.image,
                        MetaTitle = _item.meta_title,
                        MetaDescription = _item.meta_description,
                        MetaKeywords = _item.meta_keywords,
                        MetaOgImage = _item.meta_og_image,
                        MetaTwitterImage = _item.meta_twitter_image
                    };
                }
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, "About", ex);
            }
        }


        #endregion
    }
}
