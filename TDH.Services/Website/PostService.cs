using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using TDH.Common;
using Utils.JqueryDatatable;
using TDH.Model.Website;
using TDH.DataAccess;
using Utils;
using TDH.Common.UserException;

namespace TDH.Services.Website
{
    /// <summary>
    /// Post service
    /// </summary>
    public class PostService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.Website/PostService.cs";

        #endregion

        /// <summary>
        /// Get list data using jquery datatable
        /// </summary>
        /// <param name="request">Jquery datatable request</param>
        /// <param name="userID">The user identifier</param>
        /// <returns><string, object></returns>
        public Dictionary<string, object> List(CustomDataTableRequestHelper request, Guid userID)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                //Declare response data to json object
                DataTableResponse<PostModel> _itemResponse = new DataTableResponse<PostModel>();
                //List of data
                List<PostModel> _list = new List<PostModel>();
                using (var _context = new TDHEntities())
                {
                    var _lData = (from m in _context.WEB_POST
                                  where !m.deleted
                                  orderby m.create_date descending
                                  select new
                                  {
                                      m.id,
                                      m.category_id,
                                      m.navigation_id,
                                      m.title,
                                      m.description,
                                      m.publish,
                                      m.is_navigation,
                                      m.create_date
                                  }).ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    string _cateTitle = "";
                    foreach (var item in _lData)
                    {
                        _cateTitle = "";
                        if (item.is_navigation)
                        {
                            WEB_NAVIGATION _nav = _context.WEB_NAVIGATION.FirstOrDefault(m => m.id == item.navigation_id);
                            _cateTitle = _nav.title;
                        }
                        else
                        {
                            WEB_CATEGORY _cate = _context.WEB_CATEGORY.FirstOrDefault(m => m.id == item.category_id);
                            _cateTitle = _cate.title;
                        }
                        _list.Add(new PostModel()
                        {
                            ID = item.id,
                            CategoryName = _cateTitle,
                            Description = item.description,
                            Title = item.title,
                            Publish = item.publish,
                            CreateDate = item.create_date,
                            CreateDateString = item.create_date.DateToString()
                        });
                    }
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _list = _list.Where(m => m.CategoryName.ToLower().Contains(searchValue) ||
                                         m.Title.ToLower().Contains(searchValue) ||
                                         m.Description.ToLower().Contains(searchValue) ||
                                         m.CreateDateString.ToString().ToLower().Contains(searchValue)).ToList();
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<PostModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
                                case "CategoryName":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.CategoryName) : _sortList.Sort(col.Dir, m => m.CategoryName);
                                    break;
                                case "Description":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Description) : _sortList.Sort(col.Dir, m => m.Description);
                                    break;
                                case "Title":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Title) : _sortList.Sort(col.Dir, m => m.Title);
                                    break;
                                case "CreateDateString":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.CreateDate) : _sortList.Sort(col.Dir, m => m.CreateDate);
                                    break;
                            }
                        }
                        _itemResponse.data = _sortList.Skip(request.start).Take(request.length).ToList();
                    }
                    else
                    {
                        _itemResponse.data = _list.Skip(request.start).Take(request.length).ToList();
                    }
                    _return.Add(DatatableCommonSetting.Response.DATA, _itemResponse);
                }
                _return.Add(DatatableCommonSetting.Response.STATUS, ResponseStatusCodeHelper.OK);
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "List", userID, ex);
            }
            return _return;
        }

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="model">Post model</param>
        /// <returns>PostModel</returns>
        public PostModel GetItemByID(PostModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    WEB_POST _md = _context.WEB_POST.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "GetItemByID", model.CreateBy);
                    }
                    return new PostModel()
                    {
                        ID = _md.id,
                        IsNavigation = _md.is_navigation,
                        CategoryID = _md.category_id,
                        NavigationID = _md.navigation_id,
                        Title = _md.title,
                        Alias = _md.alias,
                        Description = _md.description,
                        Content = _md.content,
                        Image = _md.image,
                        Ordering = _md.ordering,
                        Publish = _md.publish,
                        MetaTitle = _md.meta_title,
                        MetaDescription = _md.meta_description,
                        MetaKeywords = _md.meta_keywords,
                        MetaNext = _md.meta_next,
                        MetaOgSiteName = _md.meta_og_site_name,
                        MetaOgImage = _md.meta_og_image,
                        MetaTwitterImage = _md.meta_twitter_image,
                        MetaArticleName = _md.meta_article_name,
                        MetaArticleTag = _md.meta_article_tag,
                        MetaArticleSection = _md.meta_article_section,
                        MetaArticlePublish = _md.meta_article_publish
                    };
                }
            }
            catch (DataAccessException fieldEx)
            {
                throw fieldEx;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "GetItemByID", model.CreateBy, ex);
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model">Post model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(PostModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    WEB_POST _md = new WEB_POST();
                    if (model.Insert)
                    {
                        _md.id = Guid.NewGuid();
                    }
                    else
                    {
                        _md = _context.WEB_POST.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                        if (_md == null)
                        {
                            throw new DataAccessException(FILE_NAME, "Save", model.CreateBy);
                        }
                    }
                    _md.is_navigation = model.IsNavigation;
                    if (model.IsNavigation)
                    {
                        var _nav = _context.WEB_NAVIGATION.FirstOrDefault(m => m.id == model.NavigationID);

                        _md.navigation_id = model.NavigationID;
                        _md.category_id = null;
                        _md.alias = "/" + _nav.alias + "/" + model.MetaTitle.TitleToAlias();
                    }
                    else
                    {
                        var _cate = _context.WEB_CATEGORY.FirstOrDefault(m => m.id == model.CategoryID);

                        _md.category_id = model.CategoryID;
                        _md.navigation_id = null;
                        _md.alias = "/" + _cate.alias + "/" + model.MetaTitle.TitleToAlias();
                    }
                    _md.title = model.Title;
                    _md.description = model.Description;
                    _md.content = model.Content;
                    _md.image = model.Image;
                    _md.ordering = model.Ordering;
                    _md.publish = model.Publish;
                    _md.meta_title = model.MetaTitle;
                    _md.meta_description = model.MetaDescription;
                    _md.meta_keywords = model.MetaKeywords;
                    _md.meta_next = model.MetaNext;
                    _md.meta_og_site_name = model.MetaOgSiteName;
                    _md.meta_og_image = model.MetaOgImage;
                    _md.meta_twitter_image = model.MetaTwitterImage;
                    _md.meta_article_name = model.MetaArticleName;
                    _md.meta_article_tag = model.MetaArticleTag;
                    _md.meta_article_section = model.MetaArticleSection;
                    _md.meta_article_publish = DateTime.Now;
                    if (model.Insert)
                    {
                        _md.create_by = model.CreateBy;
                        _md.create_date = DateTime.Now;
                        _context.WEB_POST.Add(_md);
                        _context.Entry(_md).State = EntityState.Added;
                    }
                    else
                    {
                        _md.update_by = model.UpdateBy;
                        _md.update_date = DateTime.Now;
                        _context.WEB_POST.Attach(_md);
                        _context.Entry(_md).State = EntityState.Modified;
                    }
                    _context.SaveChanges();
                }
            }
            catch (DataAccessException fieldEx)
            {
                throw fieldEx;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "Save", model.CreateBy, ex);
            }
            if (model.Insert)
            {
                Notifier.Notification(model.CreateBy, Message.InsertSuccess, Notifier.TYPE.Success);
            }
            else
            {
                Notifier.Notification(model.CreateBy, Message.UpdateSuccess, Notifier.TYPE.Success);
            }
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Publish
        /// </summary>
        /// <param name="model">Post model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Publish(PostModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    WEB_POST _md = _context.WEB_POST.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "Publish", model.CreateBy);
                    }
                    _md.publish = model.Publish;
                    _md.update_by = model.UpdateBy;
                    _md.update_date = DateTime.Now;
                    _context.WEB_POST.Attach(_md);
                    _context.Entry(_md).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            catch (DataAccessException fieldEx)
            {
                throw fieldEx;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "Publish", model.CreateBy, ex);
            }
            Notifier.Notification(model.CreateBy, Message.UpdateSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model">Post model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(PostModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    WEB_POST _md = _context.WEB_POST.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "Delete", model.CreateBy);
                    }
                    _md.deleted = true;
                    _md.delete_by = model.DeleteBy;
                    _md.delete_date = DateTime.Now;
                    _context.WEB_POST.Attach(_md);
                    _context.Entry(_md).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            catch (DataAccessException fieldEx)
            {
                throw fieldEx;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "Delete", model.CreateBy, ex);
            }
            Notifier.Notification(model.CreateBy, Message.DeleteSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

    }
}
