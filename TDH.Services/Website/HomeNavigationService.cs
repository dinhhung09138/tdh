using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TDH.Common;
using TDH.Common.UserException;
using TDH.DataAccess;
using TDH.Model.Website;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Services.Website
{
    /// <summary>
    /// Home navigation service
    /// </summary>
    public class HomeNavigationService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.Website/HomeNavigationService.cs";

        #endregion

        /// <summary>
        /// Get list data using jquery datatable
        /// </summary>
        /// <param name="request">Jquery datatable request</param>
        /// <param name="userID">The user identififer</param>
        /// <returns><string, object></returns>
        public Dictionary<string, object> List(CustomDataTableRequestHelper request, Guid userID)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                //Declare response data to json object
                DataTableResponse<HomeNavigationModel> _itemResponse = new DataTableResponse<HomeNavigationModel>();
                //List of data
                List<HomeNavigationModel> _list = new List<HomeNavigationModel>();
                using (var _context = new TDHEntities())
                {
                    var _lData = (from m in _context.WEB_NAVIGATION
                                  where !m.deleted && m.publish
                                  select new
                                  {
                                      m.id,
                                      m.title,
                                  }).ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.title.ToLower().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    short _ordering = 0;
                    bool _selected = false;
                    foreach (var item in _lData)
                    {
                        _selected = false;
                        _ordering = 0;
                        var _cate = _context.WEB_HOME_NAVIGATION.FirstOrDefault(m => m.navigation_id == item.id);
                        if (_cate != null)
                        {
                            _selected = true;
                            _ordering = _cate.ordering;
                        }
                        _list.Add(new HomeNavigationModel()
                        {
                            ID = item.id,
                            NavigationID = item.id,
                            NavigationTitle = item.title,
                            Ordering = _ordering,
                            Selected = _selected
                        });
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<HomeNavigationModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
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
        /// <param name="model">Home navigation model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(HomeNavigationModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    WEB_HOME_NAVIGATION _md = _md = _context.WEB_HOME_NAVIGATION.FirstOrDefault(m => m.navigation_id == model.NavigationID);
                    if (!model.Selected)
                    {
                        return Delete(model);
                    }
                    if (_md == null)
                    {
                        _md = new WEB_HOME_NAVIGATION()
                        {
                            id = Guid.NewGuid(),
                            navigation_id = model.NavigationID,
                            ordering = 1
                        };
                        _context.WEB_HOME_NAVIGATION.Add(_md);
                        _context.Entry(_md).State = EntityState.Added;
                    }
                    else
                    {
                        _md.ordering = model.Ordering;
                        _context.WEB_HOME_NAVIGATION.Attach(_md);
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
        /// <param name="model">Home navigation model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(HomeNavigationModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    WEB_HOME_NAVIGATION _md = _context.WEB_HOME_NAVIGATION.FirstOrDefault(m => m.navigation_id == model.NavigationID);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "Delete", model.CreateBy);
                    }
                    _context.WEB_HOME_NAVIGATION.Remove(_md);
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
