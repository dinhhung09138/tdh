using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using TDH.Common;
using TDH.Common.UserException;
using TDH.DataAccess;
using TDH.Model.Website;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Services.Website
{
    /// <summary>
    /// Navigation service
    /// </summary>
    public class NavigationService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "AServices.Website/NavigationService.cs";

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
                DataTableResponse<NavigationModel> _itemResponse = new DataTableResponse<NavigationModel>();
                //List of data
                List<NavigationModel> _list = new List<NavigationModel>();
                using (var _context = new TDHEntities())
                {
                    var _lData = _context.WEB_NAVIGATION.Where(m => !m.deleted).OrderBy(m => m.ordering).Select(m => new
                    {
                        m.id,
                        m.title,
                        m.description,
                        m.image,
                        m.no_child,
                        m.ordering,
                        m.publish
                    }).ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.title.ToLower().Contains(searchValue) ||
                                                   m.description.Contains(searchValue) ||
                                                   m.ordering.ToString().Contains(searchValue)).ToList();
                    }
                    int _count = 0;
                    foreach (var item in _lData)
                    {
                        _count = _context.WEB_CATEGORY.Count(m => m.navigation_id == item.id && !m.deleted);
                        _list.Add(new NavigationModel()
                        {
                            ID = item.id,
                            Title = item.title,
                            Description = item.description,
                            Ordering = item.ordering,
                            Image = item.image ?? "",
                            Publish = item.publish,
                            NoChild = item.no_child,
                            Count = _count,
                            CountString = _count.NumberToString()
                        });
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<NavigationModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
                                case "Title":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Title) : _sortList.Sort(col.Dir, m => m.Title);
                                    break;
                                case "Description":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Description) : _sortList.Sort(col.Dir, m => m.Description);
                                    break;
                                case "Ordering":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Ordering) : _sortList.Sort(col.Dir, m => m.Ordering);
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
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, userID, ex);
            }
            return _return;
        }

        /// <summary>
        /// Get all item without deleted
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<NavigationModel></returns>
        public List<NavigationModel> GetAll(Guid userID)
        {
            try
            {
                List<NavigationModel> _return = new List<NavigationModel>();
                using (var _context = new TDHEntities())
                {
                    var _list = _context.WEB_NAVIGATION.Where(m => !m.deleted).OrderByDescending(m => m.ordering).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new NavigationModel() { ID = item.id, Title = item.title });
                    }
                }
                return _return;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, userID, ex);
            }
        }

        /// <summary>
        /// Get all item with no child item and without deleted
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<NavigationModel></returns>
        public List<NavigationModel> GetAllWithChild(Guid userID)
        {
            try
            {
                List<NavigationModel> _return = new List<NavigationModel>();
                using (var _context = new TDHEntities())
                {
                    var _list = _context.WEB_NAVIGATION.Where(m => !m.deleted && m.no_child).OrderByDescending(m => m.ordering).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new NavigationModel() { ID = item.id, Title = item.title });
                    }
                }
                return _return;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, userID, ex);
            }
        }

        /// <summary>
        /// Get all item with no child item and without deleted
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<NavigationModel></returns>
        public List<NavigationModel> GetAllWithNoChild(Guid userID)
        {
            try
            {
                List<NavigationModel> _return = new List<NavigationModel>();
                using (var _context = new TDHEntities())
                {
                    var _list = _context.WEB_NAVIGATION.Where(m => !m.deleted && !m.no_child).OrderByDescending(m => m.ordering).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new NavigationModel() { ID = item.id, Title = item.title });
                    }
                }
                return _return;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, userID, ex);
            }
        }

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="model">Navigation model</param>
        /// <returns>NavigationModel</returns>
        public NavigationModel GetItemByID(NavigationModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    WEB_NAVIGATION _md = _context.WEB_NAVIGATION.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    return new NavigationModel()
                    {
                        ID = _md.id,
                        Title = _md.title,
                        Alias = _md.alias,
                        NoChild = _md.no_child,
                        Description = _md.description,
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
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy, ex);
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model">Navigation model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(NavigationModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    WEB_NAVIGATION _md = new WEB_NAVIGATION();
                    if (model.Insert)
                    {
                        _md.id = Guid.NewGuid();
                    }
                    else
                    {
                        _md = _context.WEB_NAVIGATION.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                        if (_md == null)
                        {
                            throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                        }
                    }
                    _md.title = model.Title;
                    _md.alias = "/" + model.MetaTitle.TitleToAlias();
                    _md.description = model.Description;
                    _md.no_child = model.NoChild;
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
                        _context.WEB_NAVIGATION.Add(_md);
                        _context.Entry(_md).State = EntityState.Added;
                    }
                    else
                    {
                        _md.update_by = model.UpdateBy;
                        _md.update_date = DateTime.Now;
                        _context.WEB_NAVIGATION.Attach(_md);
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
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy, ex);
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
        /// <param name="model">Navigation model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Publish(NavigationModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    WEB_NAVIGATION _md = _context.WEB_NAVIGATION.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    _md.publish = model.Publish;
                    _md.update_by = model.UpdateBy;
                    _md.update_date = DateTime.Now;
                    _context.WEB_NAVIGATION.Attach(_md);
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
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy, ex);
            }
            Notifier.Notification(model.CreateBy, Message.UpdateSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model">Navigation model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(NavigationModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    WEB_NAVIGATION _md = _context.WEB_NAVIGATION.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    _md.deleted = true;
                    _md.delete_by = model.DeleteBy;
                    _md.delete_date = DateTime.Now;
                    _context.WEB_NAVIGATION.Attach(_md);
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
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy, ex);
            }
            Notifier.Notification(model.CreateBy, Message.DeleteSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Check Delete item
        /// </summary>
        /// <param name="model">Navigation model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper CheckDelete(NavigationModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    WEB_NAVIGATION _md = _context.WEB_NAVIGATION.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    var _cate = _context.WEB_CATEGORY.FirstOrDefault(m => m.navigation_id == model.ID && !m.deleted);
                    if (_cate != null)
                    {
                        Notifier.Notification(model.CreateBy, Message.CheckExists, Notifier.TYPE.Warning);
                        return ResponseStatusCodeHelper.NG;
                    }
                }
            }
            catch (DataAccessException fieldEx)
            {
                throw fieldEx;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy, ex);
            }
            return ResponseStatusCodeHelper.OK;
        }

    }
}
