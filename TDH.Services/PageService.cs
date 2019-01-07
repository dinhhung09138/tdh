using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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
        public static async Task<MetaViewModel> GetHomeMetaContent()
        {
            await Task.Yield();
            MetaViewModel _meta = new MetaViewModel();
            try
            {
                using (var _context = new TDHEntities())
                {
                    var _md = _context.PROC_WEB_VIEW_HOME_META().FirstOrDefault();
                    if (_md != null)
                    {
                        _meta.MetaTitle = _md.title;
                        _meta.MetaDescription = _md.description;
                        _meta.MetaKeywords = _md.keyword;
                        _meta.MetaImage = _md.image;
                        _meta.MetaOgImage = _md.google_image;
                        _meta.MetaTwitterImage = _md.twitter_image;
                    }
                    return _meta;
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
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
                    //Return list
                    List<NavigationViewModel> _returnList = new List<NavigationViewModel>();

                    //Get data from database
                    var _list = _context.PROC_WEB_VIEW_HOME_LISTNAVIGATION().ToList();

                    //Get list navigation
                    var _listNav = _list.Where(m => m.title.Length > 0).Select(m => m.id).Distinct().ToList();
                    foreach (var item in _listNav)
                    {
                        var _nav = _list.FirstOrDefault(m => m.id == item);
                        if (_nav.title.Length == 0)
                        {
                            continue;
                        }
                        _list.Remove(_nav);
                        _returnList.Add(new NavigationViewModel()
                        {
                            Title = _nav.title,
                            Alias = _nav.alias,
                            Categories = _list.Where(m => m.title == _nav.title && m.title.Length > 0)
                                              .Select(m => new CategoryViewModel() { Title = m.category_title, Alias = m.category_alias })
                                              .ToList()
                        });
                    }
                    
                    //Return list
                    return _returnList;
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
            }
        }

        /// <summary>
        /// Get list category and post data to show on homepage
        /// Get top 6 post for each navigation
        /// </summary>
        /// <returns>List<NavigationViewModel></returns>
        public static async Task<List<NavigationViewModel>> GetListNavigationShowOnHomePage()
        {
            await Task.Yield();
            List<NavigationViewModel> _return = new List<NavigationViewModel>();
            try
            {
                using (var _context = new TDHEntities())
                {
                    //Return list
                    List<NavigationViewModel> _returnList = new List<NavigationViewModel>();

                    //Get data
                    var _list = _context.PROC_WEB_VIEW_HOME_PostByNavigation().ToList();

                    //Get list navigation
                    var _listNav = _list.Where(m => m.title.Length > 0);
                    foreach (var item in _listNav)
                    {
                        _returnList.Add(new NavigationViewModel()
                        {
                            Title = item.title,
                            Alias = item.alias,
                            Posts = _list.Where(m => m.id == item.id && m.title.Length == 0)
                                              .Select(m => new PostViewModel()
                                              {
                                                  Title = m.post_title,
                                                  Alias = m.post_alias,
                                                  Description = m.post_description,
                                                  Image = m.post_image,
                                                  CreateDate = m.post_create_date
                                              })
                                              .ToList()
                        });
                    }

                    //Return list
                    return _returnList;

                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
            }
        }

        /// <summary>
        /// Get list category and post data to show on homepage
        /// Get list of top 5 of post for each category
        /// </summary>
        /// <returns>List<CategoryViewModel></returns>
        public static async Task<List<CategoryViewModel>> GetListCategoryShowOnHomePage()
        {
            await Task.Yield();
            List<CategoryViewModel> _return = new List<CategoryViewModel>();
            try
            {
                using (var _context = new TDHEntities())
                {
                    //Return list
                    List<CategoryViewModel> _returnList = new List<CategoryViewModel>();

                    //Get data
                    var _list = _context.PROC_WEB_VIEW_HOME_PostByCategory().ToList();

                    //Get list category
                    var _listCate = _list.Where(m => m.title.Length > 0);
                    foreach (var item in _listCate)
                    {
                        _returnList.Add(new CategoryViewModel()
                        {
                            Title = item.title,
                            Alias = item.alias,
                            Posts = _list.Where(m => m.id == item.id && m.title.Length == 0)
                                              .Select(m => new PostViewModel()
                                              {
                                                  Title = m.post_title,
                                                  Alias = m.post_alias,
                                                  Description = m.post_description,
                                                  Image = m.post_image,
                                                  CreateDate = m.post_create_date
                                              })
                                              .ToList()
                        });
                    }

                    //Return list
                    return _returnList;

                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
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
                    return (from m in _context.PROC_WEB_VIEW_HOME_ListCategoryOnFooter()
                            select new CategoryViewModel()
                            {
                                Title = m.title,
                                Alias = m.alias,
                                Count = (int)m.count
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
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

                    //Return list
                    List<NavigationViewModel> _returnList = new List<NavigationViewModel>();

                    //Get data
                    var _list = _context.PROC_WEB_VIEW_HOME_2NavigationOnFooter().ToList();

                    //Get list navigation
                    var _listNav = _list.Where(m => m.title.Length > 0);
                    foreach (var item in _listNav)
                    {
                        _returnList.Add(new NavigationViewModel()
                        {
                            Title = item.title,
                            Alias = item.alias,
                            Posts = _list.Where(m => m.id == item.id && m.title.Length == 0)
                                              .Select(m => new PostViewModel()
                                              {
                                                  Title = m.post_title,
                                                  Alias = m.post_alias,
                                                  Image = m.post_image,
                                                  CreateDate = m.post_create_date
                                              })
                                              .ToList()
                        });
                    }

                    //Return list
                    return _returnList;

                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
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
                    return _context.PROC_WEB_CONFIGURATION_GetByKey("banner").FirstOrDefault().value;
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, ex);
            }
        }

        #endregion

        #region " [ Post Common ] "

        /// <summary>
        /// Get navigation infor
        /// </summary>
        /// <param name="navAlias">Navigation alias</param>
        /// <returns>NavigationViewModel</returns>
        public static async Task<NavigationViewModel> GetNavigationInfor(string navAlias)
        {
            try
            {
                await Task.Yield();
                using (var context = new TDHEntities())
                {
                    var _item = context.PROC_WEB_VIEW_NAVIGATION_Info(navAlias).FirstOrDefault();
                    if (_item == null)
                    {
                        throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 204, string.Format("{0} not found", navAlias), new Exception());
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
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
            }
        }

        /// <summary>
        /// Get category infor
        /// </summary>
        /// <param name="navigationAlias">Navigation alias</param>
        /// <param name="cateAlias">Category alias</param>
        /// <returns>CategoryViewModel</returns>
        public static async Task<CategoryViewModel> GetCategoryInfor(string navigationAlias, string cateAlias)
        {
            try
            {
                await Task.Yield();
                using (var context = new TDHEntities())
                {
                    var _item = context.PROC_WEB_VIEW_CATEGORY_Info(navigationAlias + "/" + cateAlias).FirstOrDefault();
                    if (_item == null)
                    {
                        throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 204, string.Format("{0}/{1}: not found", navigationAlias, cateAlias), new Exception());
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
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
            }
        }

        /// <summary>
        /// Get post infor which parent is navigation
        /// </summary>
        /// <param name="navAlias">Navigation alias</param>
        /// <param name="postAlias">Post alias</param>
        /// <returns>PostViewModel</returns>
        public static async Task<PostViewModel> GetPostInfor(string navAlias, string postAlias)
        {
            try
            {
                await Task.Yield();
                var _nav = GetNavigationInfor(navAlias);
                using (var context = new TDHEntities())
                {
                    var _item = context.PROC_WEB_VIEW_POST_Info(navAlias, "", postAlias).FirstOrDefault();
                    if (_item == null)
                    {
                        throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 204, string.Format("{0}/{1} not found", navAlias, postAlias), new Exception());
                    }
                    return new PostViewModel()
                    {

                        CategoryID = (Guid)_item.category_id,
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
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
            }
        }

        /// <summary>
        /// Get post infor which parent is category
        /// </summary>
        /// <param name="navAlias">Navigation alias</param>
        /// <param name="cateAlias">Category alias</param>
        /// <param name="postAlias">Post alias</param>
        /// <returns>PostViewModel</returns>
        public static async Task<PostViewModel> GetPostInfor(string navAlias, string cateAlias, string postAlias)
        {
            try
            {
                await Task.Yield();
                using (var context = new TDHEntities())
                {
                    var _item = context.PROC_WEB_VIEW_POST_Info(navAlias, cateAlias, postAlias).FirstOrDefault();
                    if (_item == null)
                    {
                        throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 204, string.Format("{0}/{1}/{2} not found", navAlias, cateAlias, postAlias), new Exception());
                    }
                    return new PostViewModel()
                    {
                        CategoryID = (Guid)_item.category_id,
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
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
            }
        }

        /// <summary>
        /// Get top 4 lasted news
        /// </summary>
        /// <returns>List<PostViewModel></returns>
        public static async Task<List<PostViewModel>> GetTop4LastedNews()
        {
            try
            {
                await Task.Yield();
                using (var _context = new TDHEntities())
                {
                    return _context.PROC_WEB_VIEW_POST_Top4Lasted()
                                             .Select(m => new PostViewModel()
                                             {
                                                 Alias = m.alias,
                                                 Title = m.title,
                                                 Image = m.image,
                                                 CreateDate = m.create_date,
                                                 View = m.view
                                             }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
            }
        }

        /// <summary>
        /// Get 2 news has largest view
        /// </summary>
        /// <returns>List<PostViewModel></returns>
        public static async Task<List<PostViewModel>> GetTop2Views()
        {
            try
            {
                await Task.Yield();
                using (var _context = new TDHEntities())
                {
                    return _context.PROC_WEB_VIEW_POST_Top2View()
                                             .Select(m => new PostViewModel()
                                             {
                                                 Alias = m.alias,
                                                 Title = m.title,
                                                 Image = m.image,
                                                 CreateDate = m.create_date,
                                                 View = m.view
                                             }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
            }
        }

        #endregion

        #region " [ Navigation Page ] "

        /// <summary>
        /// Get list category by navigation alias
        /// </summary>
        /// <param name="navigationAlias"></param>
        /// <returns>List<CategoryViewModel></returns>
        public static async Task<List<CategoryViewModel>> GetListCategoryDataByNavigation(string navigationAlias)
        {
            try
            {
                await Task.Yield();
                using (var context = new TDHEntities())
                {
                    //Return list
                    List<CategoryViewModel> _returnList = new List<CategoryViewModel>();

                    //Get data
                    var _list = context.PROC_WEB_VIEW_CATEGORY_ByNavigation(navigationAlias).ToList();

                    //Get list navigation
                    var _listCate = _list.Where(m => m.title.Length > 0);

                    foreach (var item in _listCate)
                    {
                        _returnList.Add(new CategoryViewModel()
                        {
                            Title = item.title,
                            Alias = item.alias,
                            Posts = _list.Where(m => m.id == item.id && m.title.Length == 0)
                                        .Select(p => new PostViewModel()
                                        {
                                            Title = p.post_title,
                                            Description = p.post_description,
                                            Image = p.post_image,
                                            Alias = p.post_alias,
                                            CreateDate = p.post_create_date
                                        }).ToList()
                        });
                    }

                    //Return list
                    return _returnList;
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
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
        public static async Task<bool> CheckIsCategoryPage(string navigationAlias, string categoryAlias)
        {
            try
            {
                await Task.Yield();
                NavigationViewModel _nav = await GetNavigationInfor(navigationAlias);
                using (var context = new TDHEntities())
                {
                    //Get this is post which parent is navigation
                    var _post = context.WEB_POST.FirstOrDefault(m => m.alias == ("/" + navigationAlias + "/" + categoryAlias) && m.publish && !m.deleted);
                    if (_post != null)
                    {
                        if (!_post.is_navigation || _nav.ID != _post.navigation_id)
                        {
                            throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 204, string.Format("{0}/{1} not found", navigationAlias, categoryAlias), new Exception());
                        }
                        return false;
                    }
                    //Check this is category which parent is navigation
                    var _cate = context.WEB_CATEGORY.FirstOrDefault(m => m.alias == ("/" + navigationAlias + "/" + categoryAlias));
                    if (_cate == null || _cate.navigation_id != _nav.ID)
                    {
                        throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 204, string.Format("{0}/{1} not found", navigationAlias, categoryAlias), new Exception());
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
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
            }
        }

        /// <summary>
        /// Get list post data with has category and navigation
        /// </summary>
        /// <param name="navigationAlias">Navigation alias</param>
        /// <param name="categoryAlias">Category alias</param>
        /// <param name="page">Current page</param>
        /// <returns>List<PostViewModel></returns>
        public static async Task<List<PostViewModel>> GetListPostDataByCategory(string navigationAlias, string categoryAlias, short page = 1)
        {
            try
            {
                await Task.Yield();
                List<PostViewModel> _return = new List<PostViewModel>();
                using (var context = new TDHEntities())
                {
                    return context.PROC_WEB_VIEW_POST_ByCategory(navigationAlias, categoryAlias, page)
                                             .Select(m => new PostViewModel()
                                             {
                                                 Alias = m.alias,
                                                 Title = m.title,
                                                 Description = m.description,
                                                 Image = m.image,
                                                 CreateDate = m.create_date
                                             }).ToList();
                }
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
            }
        }

        #endregion

        #region " [ Post ] "

        /// <summary>
        /// Get top 6 news by navigation identifier
        /// </summary>
        /// <param name="navID">Navigation identifier</param>
        /// <returns>List<PostViewModel></returns>
        public static async Task<List<PostViewModel>> Top6LastedPostByNavigationID(Guid navID)
        {
            try
            {
                await Task.Yield();
                using (var _context = new TDHEntities())
                {
                    return _context.PROC_WEB_VIEW_POST_Top6ByNavigation(navID)
                                             .Select(m => new PostViewModel()
                                             {
                                                 Alias = m.alias,
                                                 Title = m.title,
                                                 Description = m.description,
                                                 Image = m.image,
                                                 CreateDate = m.create_date
                                             }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
            }
        }

        /// <summary>
        /// Get top 6 news by category identifier
        /// </summary>
        /// <param name="cateID">Category identifier</param>
        /// <returns>List<PostViewModel></returns>
        public static async Task<List<PostViewModel>> Top6LastedPostByCategoryID(Guid cateID)
        {
            try
            {
                await Task.Yield();
                List<PostViewModel> _return = new List<PostViewModel>();
                using (var _context = new TDHEntities())
                {
                    return _context.PROC_WEB_VIEW_POST_Top6ByCategory(cateID)
                                             .Select(m => new PostViewModel()
                                             {
                                                 Alias = m.alias,
                                                 Title = m.title,
                                                 Description = m.description,
                                                 Image = m.image,
                                                 CreateDate = m.create_date
                                             }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
            }
        }

        #endregion

        #region " [ Programming ]

        /// <summary>
        /// Get list of 20 items 
        /// </summary>
        /// <param name="navAlias">Navigation alias</param>
        /// <param name="page">currrent page, default: 1</param>
        /// <returns>List<PostViewModel></returns>
        public static async Task<List<PostViewModel>> ProgrammingGetTop20(string navAlias, short page = 1)
        {
            try
            {
                await Task.Yield();
                List<PostViewModel> _return = new List<PostViewModel>();
                using (var context = new TDHEntities())
                {
                    var _list = context.PROC_WEB_VIEW_PROGRAMMING_Top20(navAlias, page).ToList();

                    foreach (var item in _list)
                    {
                        _return.Add(new PostViewModel()
                        {
                            Title = item.title,
                            Alias = item.alias,
                            CategoryAlias = item.cate_alias,
                            CategoryTitle = item.cate_title,
                            Image = item.image
                        });
                    }

                    return _return;
                }
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
            }
        }

        /// <summary>
        /// Get list of category in programming page
        /// </summary>
        /// <param name="navAlias">Navigation alias</param>
        /// <returns>List<CategoryViewModel></returns>
        public static List<CategoryViewModel> ProgrammingCategory(string navAlias)
        {
            try
            {
                List<CategoryViewModel> _return = new List<CategoryViewModel>();
                using (var context = new TDHEntities())
                {
                    var _list = context.PROC_WEB_VIEW_PROGRAMMING_category(navAlias).ToList();

                    foreach (var item in _list)
                    {
                        _return.Add(new CategoryViewModel()
                        {
                            Title = item.title,
                            Alias = item.alias,
                            Count = (int)item.count
                        });
                    }

                    return _return;
                }
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
            }
        }

        /// <summary>
        /// Get top 6 post which has highest viwe
        /// </summary>
        /// <param name="navAlias"></param>
        /// <returns></returns>
        public static List<PostViewModel> ProgrammingGetTop6View(string navAlias)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    return _context.PROC_WEB_VIEW_PROGRAMMING_topview(navAlias)
                                             .Select(m => new PostViewModel()
                                             {
                                                 Alias = m.alias,
                                                 Title = m.title,
                                                 Image = m.image,
                                                 CreateDate = m.create_date,
                                                 View = m.view
                                             }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
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
                    var _list = _context.PROC_WEB_VIEW_INTRO_META().FirstOrDefault();
                    _meta.MetaDescription = _list.description;
                    _meta.MetaImage = _list.image;
                    return _meta;
                }
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
            }
        }

        #region " [ About ] "

        /// <summary>
        /// About infor
        /// </summary>
        /// <returns>PostViewModel</returns>
        public static async Task<PostViewModel> About()
        {
            try
            {
                await Task.Yield();
                using (var _context = new TDHEntities())
                {
                    var _item = _context.PROC_WEB_VIEW_ABOUT().FirstOrDefault();
                    if (_item == null)
                    {
                        throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 204, "", new Exception());
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
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
            }
        }

        #endregion

        #region " [ Portfolio ] "

        /// <summary>
        /// Get portfolio infor
        /// </summary>
        /// <returns></returns>
        public static async Task<List<PortfolioViewModel>> PortfolioInfo()
        {
            try
            {
                await Task.Yield();
                using (var _context = new TDHEntities())
                {
                    return _context.WEB_CONFIGURATION.Where(m => m.key.Contains("portfolio_") || m.key.Contains("social_") || m.key == "name")
                        .Select(m => new PortfolioViewModel() { Key = m.key, Value = m.value }).ToList();

                }
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
            }
        }

        /// <summary>
        /// Get list of skill by user
        /// </summary>
        /// <returns></returns>
        public static async Task<List<SkillViewModel>> GetListSkill()
        {
            await Task.Yield();
            try
            {
                using (var _context = new TDHEntities())
                {
                    return (from m in _context.PN_SKILL_DEFINDED
                            join s in _context.CM_SKILL_DEFINDED on m.defined_id equals s.id
                            where m.point > 0
                            orderby m.skill_id descending
                            select new SkillViewModel()
                            {
                                Type = m.point >= 90 ? "Expert" : m.point >= 70 ? "Advanced" : m.point >= 50 ? "Intermediate" : "Beginner",
                                Name = s.name,
                                Level = m.point
                            }).ToList();
                }
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
            }
        }

        /// <summary>
        /// Get list education, certifacted
        /// </summary>
        /// <returns></returns>
        public static async Task<List<EducationViewModel>> GetEducation()
        {
            try
            {
                await Task.Yield();
                List<EducationViewModel> _return = new List<EducationViewModel>();
                using (var _context = new TDHEntities())
                {
                    var _lEdu = (from m in _context.PN_EDUCATION
                                 where m.publish && !m.deleted
                                 orderby m.ordering descending
                                 select new
                                 {
                                     m.name,
                                     m.school,
                                     m.duration
                                 }).ToList();
                    foreach (var item in _lEdu)
                    {
                        _return.Add(new EducationViewModel() { Name = item.name, School = item.school, Time = item.duration });
                    }
                    var _lCer = (from m in _context.PN_CETIFICATE
                                 where m.publish && !m.deleted
                                 orderby m.ordering descending
                                 select new
                                 {
                                     m.name,
                                     m.school,
                                     m.time
                                 }).ToList();
                    foreach (var item in _lCer)
                    {
                        _return.Add(new EducationViewModel() { Name = item.name, School = item.school, Time = item.time });
                    }
                    return _return;
                }
            }
            catch (UserException uEx)
            {
                throw uEx;
            }
            catch (Exception ex)
            {
                throw new UserException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, 500, ErrorMessage.ErrorService, ex);
            }
        }

        #endregion

    }
}
