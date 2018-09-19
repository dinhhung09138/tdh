using System;
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
    /// Home category service
    /// </summary>
    public class HomeCategoryService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.Website/HomeCategoryService.cs";

        #endregion

        /// <summary>
        /// Get list data using jquery datatable
        /// </summary>
        /// <param name="request">Jquery datatable request</param>
        /// <param name="userID">User identififer</param>
        /// <returns><string, object></returns>
        public Dictionary<string, object> List(CustomDataTableRequestHelper request, Guid userID)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                //Declare response data to json object
                DataTableResponse<HomeCategoryModel> _itemResponse = new DataTableResponse<HomeCategoryModel>();
                //List of data
                List<HomeCategoryModel> _list = new List<HomeCategoryModel>();
                using (var _context = new TDHEntities())
                {
                    var _lData = (from m in _context.WEB_CATEGORY
                                  join n in _context.WEB_NAVIGATION on m.navigation_id equals n.id
                                  where !n.deleted && !m.deleted && n.publish
                                  select new
                                  {
                                      m.id,
                                      m.title,
                                      nav_title = n.title
                                  }).ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.title.ToLower().Contains(searchValue) ||
                                                   m.nav_title.ToLower().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    short _ordering = 0;
                    bool _selected = false;
                    foreach (var item in _lData)
                    {
                        _selected = false;
                        _ordering = 0;
                        var _cate = _context.WEB_HOME_CATEGORY.FirstOrDefault(m => m.category_id == item.id);
                        if (_cate != null)
                        {
                            _selected = true;
                            _ordering = _cate.ordering;
                        }
                        _list.Add(new HomeCategoryModel()
                        {
                            ID = item.id,
                            CategoryID = item.id,
                            CategoryTitle = item.title,
                            NavigationTitle = item.nav_title,
                            Ordering = _ordering,
                            Selected = _selected
                        });
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<HomeCategoryModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
                                case "CategoryTitle":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.CategoryTitle) : _sortList.Sort(col.Dir, m => m.CategoryTitle);
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
        /// Save
        /// </summary>
        /// <param name="model">Home category model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(HomeCategoryModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    WEB_HOME_CATEGORY _md = _md = _context.WEB_HOME_CATEGORY.FirstOrDefault(m => m.category_id == model.CategoryID);
                    if (!model.Selected)
                    {
                        return Delete(model);
                    }
                    if (_md == null)
                    {
                        _md = new WEB_HOME_CATEGORY()
                        {
                            id = Guid.NewGuid(),
                            category_id = model.CategoryID,
                            ordering = 1
                        };
                        _context.WEB_HOME_CATEGORY.Add(_md);
                        _context.Entry(_md).State = EntityState.Added;
                    }
                    else
                    {
                        _md.ordering = model.Ordering;
                        _context.WEB_HOME_CATEGORY.Attach(_md);
                        _context.Entry(_md).State = EntityState.Modified;
                    }
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "Save", model.CreateBy, ex);
            }
            Notifier.Notification(model.CreateBy, Message.UpdateSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model">Home category model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(HomeCategoryModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    WEB_HOME_CATEGORY _md = _context.WEB_HOME_CATEGORY.FirstOrDefault(m => m.category_id == model.CategoryID);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "Delete", model.CreateBy);
                    }
                    _context.WEB_HOME_CATEGORY.Remove(_md);
                    _context.Entry(_md).State = EntityState.Deleted;
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
