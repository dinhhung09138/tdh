using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDH.Filters;
using TDH.Models;
using TDH.ViewModel;
using Utils;

namespace TDH.Services
{
    public class PageService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private static readonly string FILE_NAME = "Services/PageService.cs";

        #endregion

        #region " [ Home page ] "

        public static MetaViewModel getHomeMetaContent()
        {
            MetaViewModel _meta = new MetaViewModel();
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _list = context.CONFIGURATIONs.Where(m => m.key.Contains("homepage_")).ToList();
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
                Log.WriteLog(FILE_NAME, "getHomeMetaContent", Guid.NewGuid(), ex);
                throw new UserException() { Source = ex.Message };
            }
        }

        /// <summary>
        /// List of navigation
        /// </summary>
        /// <returns></returns>
        public static List<NavigationViewModel> GetListNavigation()
        {
            List<NavigationViewModel> _return = new List<NavigationViewModel>();
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _list = context.NAVIGATIONs.Where(m => m.publish && !m.deleted)
                                                     .OrderByDescending(m => m.ordering)
                                                     .Select(m => new NavigationViewModel()
                                                     {
                                                         Title = m.title,
                                                         Alias = "/" + m.alias,
                                                         Categories = context.CATEGORies.Where(c => c.navigation_id == m.id && c.show_on_nav && c.publish && !c.deleted)
                                                                                        .OrderByDescending(c => c.ordering)
                                                                                        .Select(c => new CategoryViewModel()
                                                                                        {
                                                                                            Alias = "/" + m.alias + "/" + c.alias,
                                                                                            Title = c.title
                                                                                        }).ToList()
                                                     }).ToList();
                    return _list;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "GetListNavigation", Guid.NewGuid(), ex);
                throw new UserException() { Source = ex.Message };
            }
        }
        
        /// <summary>
        /// Get list category and post data to show on homepage
        /// </summary>
        /// <returns></returns>
        public static List<NavigationViewModel> GetListNavigationShowOnHomePage()
        {
            List<NavigationViewModel> _return = new List<NavigationViewModel>();
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _list = (from m in context.HOME_NAVIGATION
                                 join n in context.NAVIGATIONs on m.navigation_id equals n.id
                                 where n.publish && !n.deleted
                                 select new NavigationViewModel()
                                 {
                                     Title = n.title,
                                     Alias = "/" + n.alias,
                                     Posts = context.POSTs.Where(p => p.navigation_id == n.id && p.publish && !p.deleted).OrderByDescending(p => p.create_date).Select(p => new PostViewModel()
                                     {
                                         Title = p.title,
                                         Description = p.description,
                                         Image = p.image,
                                         Alias = "/" + n.alias + "/" + p.alias,
                                         CreateDate = p.create_date
                                     }).Take(6).ToList()
                                 }).ToList();
                    return _list;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "GetListNavigationShowOnHomePage", Guid.NewGuid(), ex);
                throw new UserException() { Source = ex.Message };
            }
        }

        /// <summary>
        /// Get list category and post data to show on homepage
        /// </summary>
        /// <returns></returns>
        public static List<CategoryViewModel> GetListCategoryShowOnHomePage()
        {
            List<CategoryViewModel> _return = new List<CategoryViewModel>();
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _list = (from m in context.HOME_CATEGORY
                                 join c in context.CATEGORies on m.category_id equals c.id
                                 join n in context.NAVIGATIONs on c.navigation_id equals n.id
                                 where c.publish && !c.deleted && n.publish && !n.deleted
                                 select new CategoryViewModel()
                                 {
                                     Title = c.title,
                                     Alias = "/" + n.alias + "/" + c.alias,
                                     Posts = context.POSTs.Where(p => p.category_id == c.id && p.publish && !p.deleted).OrderByDescending(p => p.create_date).Select(p => new PostViewModel()
                                     {
                                         Title = p.title,
                                         Description = p.description,
                                         Image = p.image,
                                         Alias = "/" + n.alias + "/" + c.alias + "/" + p.alias,
                                         CreateDate = p.create_date
                                     }).Take(5).ToList()
                                 }).ToList();
                    return _list;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "GetListCategoryShowOnHomePage", Guid.NewGuid(), ex);
                throw new UserException() { Source = ex.Message };
            }
        }

        /// <summary>
        /// Get list category and count to show on footer
        /// </summary>
        /// <returns></returns>
        public static List<CategoryViewModel> GetListCategoryOnFooter()
        {
            List<CategoryViewModel> _return = new List<CategoryViewModel>();
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _list = (from m in context.CATEGORies
                                 join n in context.NAVIGATIONs on m.navigation_id equals n.id
                                 where m.publish && !m.deleted && n.publish && !n.deleted
                                 select new CategoryViewModel()
                                 {
                                     Title = m.title,
                                     Alias = "/" + n.alias + "/" + m.alias,
                                     Count = context.POSTs.Count(p => p.category_id == m.id)
                                 }).Take(9).ToList();
                    var _lNavigation = (from m in context.NAVIGATIONs
                                        where m.publish && !m.deleted
                                        select new CategoryViewModel()
                                        {
                                            Title = m.title,
                                            Alias = "/" + m.alias,
                                            Count = context.POSTs.Count(p => p.navigation_id == m.id)
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
                Log.WriteLog(FILE_NAME, "GetListCategoryOnFooter", Guid.NewGuid(), ex);
                throw new UserException() { Source = ex.Message };
            }
        }

        /// <summary>
        /// Get two navigation item to show on footer
        /// </summary>
        /// <returns></returns>
        public static List<NavigationViewModel> Get2NavigationOnFooter()
        {
            List<NavigationViewModel> _return = new List<NavigationViewModel>();
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _list = (from m in context.NAVIGATIONs
                                 where m.publish && !m.deleted && m.no_child
                                 select new NavigationViewModel()
                                 {
                                     Title = m.title,
                                     Alias = "/" + m.alias,
                                     Posts = context.POSTs.Where(p => p.navigation_id == m.id && p.publish && !p.deleted)
                                                          .OrderByDescending(p => p.create_date)
                                                          .Select(p => new PostViewModel()
                                                          {
                                                              Image = p.image,
                                                              Alias = "/" + m.alias + "/" + p.alias,
                                                              Title = p.title,
                                                              CreateDate = p.create_date
                                                          }).Take(3).ToList()
                                 }).Take(2).ToList();
                    return _list;
                }
            }
            catch (Exception e)
            {
                Log.WriteLog(FILE_NAME, "Get2NavigationOnFooter", Guid.NewGuid(), e);
                throw new UserException();
            }
        }

        /// <summary>
        /// Get banner infor
        /// </summary>
        /// <returns></returns>
        public static string GetBannerInfor()
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    return context.CONFIGURATIONs.Where(m => m.key.Contains("banner")).FirstOrDefault().value;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "GetBannerInfor", Guid.NewGuid(), ex);
                throw new UserException() { Source = ex.Message };
            }
        }
        #endregion

        #region " [ Post Common ] "

        /// <summary>
        /// Get navigation infor
        /// </summary>
        /// <param name="navAlias">Navigation alias</param>
        /// <returns></returns>
        public static NavigationViewModel GetNavigationInfor(string navAlias)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _item = context.NAVIGATIONs.Where(m => m.alias == navAlias && m.publish && !m.deleted)
                                             .Select(m => new { m.id, m.alias, m.title, m.image, m.meta_title, m.meta_description, m.meta_keywords, m.meta_og_image, m.meta_twitter_image })
                                             .FirstOrDefault();
                    if (_item == null)
                    {
                        throw new UserException();
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "GetNavigationInfor", Guid.NewGuid(), ex);
                throw new UserException() { Source = ex.Message };
            }
        }

        /// <summary>
        /// Get category infor
        /// </summary>
        /// <param name="cateAlias">Category information</param>
        /// <returns></returns>
        public static CategoryViewModel GetCategoryInfor(string cateAlias)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _item = context.CATEGORies.Where(m => m.alias == cateAlias && m.publish && !m.deleted)
                                             .Select(m => new { m.id, m.navigation_id, m.alias, m.title, m.image, m.meta_title, m.meta_description, m.meta_keywords, m.meta_og_image, m.meta_twitter_image })
                                             .FirstOrDefault();
                    if (_item == null)
                    {
                        throw new UserException();
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "GetCategoryInfor", Guid.NewGuid(), ex);
                throw new UserException() { Source = ex.Message };
            }
        }

        /// <summary>
        /// Get post infor
        /// </summary>
        /// <param name="navAlias">Navigation alias</param>
        /// <param name="postAlias"></param>
        /// <returns></returns>
        public static PostViewModel GetPostInfor(string navAlias, string postAlias)
        {
            try
            {
                var _nav = GetNavigationInfor(navAlias);
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _item = context.POSTs.Where(m => m.alias == postAlias && m.publish && !m.deleted)
                                             .Select(m => new { m.navigation_id, m.alias, m.title, m.description, m.content, m.image, m.is_navigation, m.meta_title, m.meta_description, m.meta_keywords, m.meta_og_image, m.meta_twitter_image })
                                             .FirstOrDefault();
                    if (_item == null || !_item.is_navigation)
                    {
                        throw new UserException();
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "GetPostInfor", Guid.NewGuid(), ex);
                throw new UserException() { Source = ex.Message };
            }
        }

        /// <summary>
        /// Get post infor
        /// </summary>
        /// <param name="navAlias">Navigation alias</param>
        /// <param name="cateAlias">Category alias</param>
        /// <param name="postAlias"></param>
        /// <returns></returns>
        public static PostViewModel GetPostInfor(string navAlias, string cateAlias, string postAlias)
        {
            try
            {
                var _nav = GetNavigationInfor(navAlias);
                var _cate = GetCategoryInfor(cateAlias);
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _item = context.POSTs.Where(m => m.alias == postAlias && m.publish && !m.deleted)
                                             .Select(m => new { m.alias, m.title, m.category_id, m.description, m.content, m.create_date, m.image, m.is_navigation, m.meta_title, m.meta_description, m.meta_keywords, m.meta_og_image, m.meta_twitter_image })
                                             .FirstOrDefault();
                    if (_item == null || _item.is_navigation)
                    {
                        throw new UserException();
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "GetPostInfor", Guid.NewGuid(), ex);
                throw new UserException() { Source = ex.Message };
            }
        }

        /// <summary>
        /// Get top 4 lasted news
        /// </summary>
        /// <returns></returns>
        public static List<PostViewModel> GetTop4LastedNews()
        {
            try
            {
                List<PostViewModel> _return = new List<PostViewModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    string _cateAlias = "";
                    var _list = context.POSTs.Where(m => m.publish && !m.deleted)
                                             .OrderByDescending(m => m.create_date)
                                             .Select(m => new { m.alias, m.title, m.image, m.is_navigation, m.navigation_id, m.category_id, m.create_date })
                                             .Take(4).ToList();
                    foreach (var item in _list)
                    {
                        if (item.is_navigation)
                        {
                            var _nav = context.NAVIGATIONs.FirstOrDefault(m => m.id == item.navigation_id);
                            _cateAlias = "/" + _nav.alias;
                        }
                        else
                        {
                            var _cate = context.CATEGORies.FirstOrDefault(m => m.id == item.category_id);
                            var _nav = context.NAVIGATIONs.FirstOrDefault(m => m.id == _cate.navigation_id);
                            _cateAlias = "/" + _nav.alias + "/" + _cate.alias;
                        }
                        _return.Add(new PostViewModel()
                        {
                            Title = item.title,
                            Alias = _cateAlias + "/" + item.alias,
                            Image = item.image,
                            CreateDate = item.create_date
                        });
                    }
                }
                return _return;
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "GetTop4LastedNews", Guid.NewGuid(), ex);
                throw new UserException() { Source = ex.Message };
            }
        }

        /// <summary>
        /// Get 2 news has largest view
        /// </summary>
        /// <returns></returns>
        public static List<PostViewModel> GetTop2Views()
        {
            try
            {
                List<PostViewModel> _return = new List<PostViewModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    string _cateAlias = "";
                    var _list = context.POSTs.Where(m => m.publish && !m.deleted)
                                             .OrderByDescending(m => m.view)
                                             .Select(m => new { m.alias, m.title, m.image, m.is_navigation, m.navigation_id, m.category_id, m.create_date })
                                             .Take(2).ToList();
                    foreach (var item in _list)
                    {
                        if (item.is_navigation)
                        {
                            var _nav = context.NAVIGATIONs.FirstOrDefault(m => m.id == item.navigation_id);
                            _cateAlias = "/" + _nav.alias;
                        }
                        else
                        {
                            var _cate = context.CATEGORies.FirstOrDefault(m => m.id == item.category_id);
                            var _nav = context.NAVIGATIONs.FirstOrDefault(m => m.id == _cate.navigation_id);
                            _cateAlias = "/" + _nav.alias + "/" + _cate.alias;
                        }
                        _return.Add(new PostViewModel()
                        {
                            Title = item.title,
                            Alias = _cateAlias + "/" + item.alias,
                            Image = item.image,
                            CreateDate = item.create_date
                        });
                    }
                }
                return _return;
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "GetTop2Views", Guid.NewGuid(), ex);
                throw new UserException() { Source = ex.Message };
            }
        }

        #endregion

        #region " [ Navigation Page ] "

        /// <summary>
        /// Get list category by navigation alias
        /// </summary>
        /// <param name="navigationAlias"></param>
        /// <returns></returns>
        public static List<CategoryViewModel> GetListCategoryDataByNavigation(string navigationAlias)
        {
            List<CategoryViewModel> _return = new List<CategoryViewModel>();
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _list = (from m in context.CATEGORies
                                 join n in context.NAVIGATIONs on m.navigation_id equals n.id
                                 where m.publish && !m.deleted && n.publish && !n.deleted && n.alias == navigationAlias
                                 orderby m.ordering descending
                                 select new CategoryViewModel()
                                 {
                                     Title = m.title,
                                     Alias = "/" + n.alias + "/" + m.alias,
                                     Posts = context.POSTs.Where(p => p.category_id == m.id && p.publish && !p.deleted).OrderByDescending(p => p.create_date).Select(p => new PostViewModel()
                                     {
                                         Title = p.title,
                                         Description = p.description,
                                         Image = p.image,
                                         Alias = "/" + n.alias + "/" + m.alias + "/" + p.alias,
                                         CreateDate = p.create_date
                                     }).Take(12).ToList()
                                 }).ToList();
                    return _list;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "GetListCategoryDataByNavigation", Guid.NewGuid(), ex);
                throw new UserException() { Source = ex.Message };
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
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _post = context.POSTs.FirstOrDefault(m => m.alias == categoryAlias && m.publish && !m.deleted);
                    if (_post != null)
                    {
                        if (!_post.is_navigation || _nav.ID != _post.navigation_id)
                        {
                            throw new UserException();
                        }
                        return false;
                    }
                    var _cate = context.CATEGORies.FirstOrDefault(m => m.alias == categoryAlias);
                    if (_cate == null || _cate.navigation_id != _nav.ID)
                    {
                        throw new UserException();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "CheckIsCategoryPage", Guid.NewGuid(), ex);
                throw new UserException() { Source = ex.Message };
            }
        }

        /// <summary>
        /// Get list post data with has category and navigation
        /// </summary>
        /// <param name="categoryAlias"></param>
        /// <returns></returns>
        public static List<PostViewModel> GetListPostDataByCategory(string navigationAlias, string categoryAlias)
        {
            try
            {
                NavigationViewModel _nav = GetNavigationInfor(navigationAlias);
                CategoryViewModel _cate = GetCategoryInfor(categoryAlias);
                if (_cate.NavigationID != _nav.ID)
                {
                    throw new UserException();
                }
                List<PostViewModel> _return = new List<PostViewModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _list = context.POSTs.Where(p => p.category_id == _cate.ID)
                                             .OrderByDescending(m => m.create_date)
                                             .Select(m => new { m.alias, m.title, m.description, m.image, m.is_navigation, m.navigation_id, m.category_id, m.create_date })
                                             .Take(12).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new PostViewModel()
                        {
                            Title = item.title,
                            Description = item.description,
                            Alias = "/" + _nav.Alias + "/" + _cate.Alias + "/" + item.alias,
                            Image = item.image,
                            CreateDate = item.create_date
                        });
                    }
                }
                return _return;
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "GetListPostDataByCategory", Guid.NewGuid(), ex);
                throw new UserException() { Source = ex.Message };
            }
        }


        #endregion

        #region " [ Post ] "

        /// <summary>
        /// Get top 6 news by navigation
        /// </summary>
        /// <param name="navID"></param>
        /// <returns></returns>
        public static List<PostViewModel> Top6LastedPostByNavigationID(Guid navID)
        {
            try
            {
                List<PostViewModel> _return = new List<PostViewModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _navAlias = context.NAVIGATIONs.FirstOrDefault(m => m.id == navID).alias;
                    var _list = context.POSTs.Where(p => p.navigation_id == navID)
                                             .OrderByDescending(m => m.create_date)
                                             .Select(m => new { m.alias, m.title, m.description, m.image, m.is_navigation, m.navigation_id, m.category_id, m.create_date })
                                             .Take(6).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new PostViewModel()
                        {
                            Title = item.title,
                            Description = item.description,
                            Alias = "/" + _navAlias + "/" + item.alias,
                            Image = item.image,
                            CreateDate = item.create_date
                        });
                    }
                }
                return _return;
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Top6LastedPostByNavigationID", Guid.NewGuid(), ex);
                throw new UserException() { Source = ex.Message };
            }
        }

        /// <summary>
        /// Get top 6 news by category
        /// </summary>
        /// <param name="cateID"></param>
        /// <returns></returns>
        public static List<PostViewModel> Top6LastedPostByCategoryID(Guid cateID)
        {
            try
            {
                List<PostViewModel> _return = new List<PostViewModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _cate = context.CATEGORies.FirstOrDefault(m => m.id == cateID);
                    var _navAlias = context.NAVIGATIONs.FirstOrDefault(m => m.id == _cate.navigation_id).alias;
                    var _list = context.POSTs.Where(p => p.category_id == cateID)
                                             .OrderByDescending(m => m.create_date)
                                             .Select(m => new { m.alias, m.title, m.description, m.image, m.is_navigation, m.navigation_id, m.category_id, m.create_date })
                                             .Take(6).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new PostViewModel()
                        {
                            Title = item.title,
                            Description = item.description,
                            Alias = "/" + _navAlias + "/" + _cate.alias + "/" + item.alias,
                            Image = item.image,
                            CreateDate = item.create_date
                        });
                    }
                }
                return _return;
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Top6LastedPostByCategoryID", Guid.NewGuid(), ex);
                throw new UserException() { Source = ex.Message };
            }
        }


        #endregion
        
        public static MetaViewModel GetShortIntroAboutMe()
        {
            MetaViewModel _meta = new MetaViewModel();
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _list = context.CONFIGURATIONs.Where(m => m.key.Contains("intro_")).ToList();
                    _meta.MetaDescription = _list.FirstOrDefault(m => m.key == "intro_content").value;
                    _meta.MetaImage = _list.FirstOrDefault(m => m.key == "intro_avatar").value;
                    return _meta;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "GetShortIntroAboutMe", Guid.NewGuid(), ex);
                throw new UserException() { Source = ex.Message };
            }
        }

        #region " [ About ] "

        /// <summary>
        /// About infor
        /// </summary>
        /// <returns></returns>
        public static PostViewModel About()
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _item = context.ABOUTs.FirstOrDefault();
                    if (_item == null)
                    {
                        throw new UserException();
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
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "About", Guid.NewGuid(), ex);
                throw new UserException() { Source = ex.Message };
            }
        }


        #endregion
    }
}