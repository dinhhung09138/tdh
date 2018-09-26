﻿using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using TDH.Common;
using TDH.DataAccess;
using TDH.Model.Website;
using Utils;
using Utils.JqueryDatatable;
using TDH.Common.UserException;

namespace TDH.Services.Website
{
    /// <summary>
    /// Category services class
    /// </summary>
    public class CategoryService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.Website/CategoryService.cs";

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
                DataTableResponse<CategoryModel> _itemResponse = new DataTableResponse<CategoryModel>();
                //List of data
                List<CategoryModel> _list = new List<CategoryModel>();
                using (var _context = new TDHEntities())
                {
                    var _lData = (from m in _context.WEB_CATEGORY
                                  join n in _context.WEB_NAVIGATION on m.navigation_id equals n.id
                                  where !n.deleted && !m.deleted && request.Parameter1 == (request.Parameter1.Length == 0 ? request.Parameter1 : m.navigation_id.ToString())
                                  select new
                                  {
                                      m.id,
                                      m.title,
                                      m.description,
                                      m.image,
                                      m.show_on_nav,
                                      nav_title = n.title,
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
                                                   m.nav_title.ToLower().Contains(searchValue) ||
                                                   m.description.ToLower().Contains(searchValue) ||
                                                   m.ordering.ToString().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    int _count = 0;
                    foreach (var item in _lData)
                    {
                        _count = _context.WEB_POST.Count(m => m.category_id == item.id && !m.deleted);
                        _list.Add(new CategoryModel()
                        {
                            ID = item.id,
                            Title = item.title,
                            Description = item.description,
                            ShowOnNav = item.show_on_nav,
                            Image = item.image ?? "",
                            NavigationTitle = item.nav_title,
                            Ordering = item.ordering,
                            Publish = item.publish,
                            Count = _count,
                            CountString = _count.NumberToString()
                        });
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<CategoryModel> _sortList = null;
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
                                case "NavigationTitle":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.NavigationTitle) : _sortList.Sort(col.Dir, m => m.NavigationTitle);
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
                throw new ServiceException(FILE_NAME, "List", userID, ex);
            }
            return _return;
        }

        /// <summary>
        /// Get all item without deleted
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<CategoryModel></returns>
        public List<CategoryModel> GetAll(Guid userID)
        {
            try
            {
                List<CategoryModel> _return = new List<CategoryModel>();
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.WEB_CATEGORY
                                 join n in _context.WEB_NAVIGATION on m.navigation_id equals n.id
                                 where n.publish && !n.deleted && !m.deleted && m.publish
                                 orderby m.ordering descending
                                 select new
                                 {
                                     m.id,
                                     m.title
                                 }).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new CategoryModel() { ID = item.id, Title = item.title });
                    }
                }
                return _return;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "GetAll", userID, ex);
            }
        }

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="model">Category model</param>
        /// <returns>CategoryModel</returns>
        public CategoryModel GetItemByID(CategoryModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    WEB_CATEGORY _md = _context.WEB_CATEGORY.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "GetItemByID", model.CreateBy);
                    }
                    var _nav = _context.WEB_NAVIGATION.FirstOrDefault(m => m.id == _md.navigation_id);
                    return new CategoryModel()
                    {
                        ID = _md.id,
                        Title = _md.title,
                        Alias = _md.alias,
                        NavigationID = _md.navigation_id,
                        NavigationTitle = _nav.title,
                        Description = _md.description,
                        ShowOnNav = _md.show_on_nav,
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
        /// <param name="model">Category model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(CategoryModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    WEB_CATEGORY _md = new WEB_CATEGORY();
                    if (model.Insert)
                    {
                        _md.id = Guid.NewGuid();
                    }
                    else
                    {
                        _md = _context.WEB_CATEGORY.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                        if (_md == null)
                        {
                            throw new DataAccessException(FILE_NAME, "Save", model.CreateBy);
                        }
                    }

                    var _nav = _context.WEB_NAVIGATION.FirstOrDefault(m => m.id == model.NavigationID);

                    _md.navigation_id = model.NavigationID;
                    _md.title = model.Title;
                    _md.alias = "/" + _nav.alias + "/" + model.MetaTitle.TitleToAlias();
                    _md.description = model.Description;
                    _md.show_on_nav = model.ShowOnNav;
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
                        _context.WEB_CATEGORY.Add(_md);
                        _context.Entry(_md).State = EntityState.Added;
                    }
                    else
                    {
                        _md.update_by = model.UpdateBy;
                        _md.update_date = DateTime.Now;
                        _context.WEB_CATEGORY.Attach(_md);
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
        /// <param name="model">Category model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Publish(CategoryModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    WEB_CATEGORY _md = _context.WEB_CATEGORY.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "Publish", model.CreateBy);
                    }
                    _md.publish = model.Publish;
                    _md.update_by = model.UpdateBy;
                    _md.update_date = DateTime.Now;
                    _context.WEB_CATEGORY.Attach(_md);
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
        /// Publish
        /// </summary>
        /// <param name="model">Category model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper OnNavigation(CategoryModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    WEB_CATEGORY _md = _context.WEB_CATEGORY.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "OnNavigation", model.CreateBy);
                    }
                    _md.show_on_nav = model.ShowOnNav;
                    _md.update_by = model.UpdateBy;
                    _md.update_date = DateTime.Now;
                    _context.WEB_CATEGORY.Attach(_md);
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
                throw new ServiceException(FILE_NAME, "OnNavigation", model.CreateBy, ex);
            }
            Notifier.Notification(model.CreateBy, Message.UpdateSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model">Category model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(CategoryModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    WEB_CATEGORY _md = _context.WEB_CATEGORY.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "Delete", model.CreateBy);
                    }
                    _md.deleted = true;
                    _md.delete_by = model.DeleteBy;
                    _md.delete_date = DateTime.Now;
                    _context.WEB_CATEGORY.Attach(_md);
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

        /// <summary>
        /// Check Delete
        /// </summary>
        /// <param name="model">Category model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper CheckDelete(CategoryModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {

                    WEB_CATEGORY _md = _context.WEB_CATEGORY.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "CheckDelete", model.CreateBy);
                    }
                    var _product = _context.WEB_POST.FirstOrDefault(m => m.category_id == model.ID && !m.deleted);
                    if (_product != null)
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
                throw new ServiceException(FILE_NAME, "CheckDelete", model.CreateBy, ex);
            }
            return ResponseStatusCodeHelper.OK;
        }

    }
}
