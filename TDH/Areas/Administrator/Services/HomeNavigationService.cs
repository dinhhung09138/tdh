using System;
using System.Collections.Generic;
using System.Linq;
using TDH.Models;
using Utils.JqueryDatatable;
using TDH.Areas.Administrator.Models;
using Utils;

namespace TDH.Areas.Administrator.Services
{
    public class HomeNavigationService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator/Services/HomeNavigationService.cs";

        #endregion

        /// <summary>
        /// Get list data using jquery datatable
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userID">User id</param>
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
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _lData = (from m in context.NAVIGATIONs
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
                        var _cate = context.HOME_NAVIGATION.FirstOrDefault(m => m.navigation_id == item.id);
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
                TDH.Services.Log.WriteLog(FILE_NAME, "List", userID, ex);
                throw new ApplicationException();
            }

            return _return;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(HomeNavigationModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            HOME_NAVIGATION _md = _md = context.HOME_NAVIGATION.FirstOrDefault(m => m.navigation_id == model.NavigationID);
                            if (!model.Selected)
                            {
                                return Delete(model);
                            }
                            if (_md == null)
                            {
                                _md = new HOME_NAVIGATION()
                                {
                                    id = Guid.NewGuid(),
                                    navigation_id = model.NavigationID,
                                    ordering = 1
                                };
                                context.HOME_NAVIGATION.Add(_md);
                                context.Entry(_md).State = System.Data.Entity.EntityState.Added;
                            }
                            else
                            {
                                _md.ordering = model.Ordering;
                                context.HOME_NAVIGATION.Attach(_md);
                                context.Entry(_md).State = System.Data.Entity.EntityState.Modified;
                            }
                            context.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            TDH.Services.Log.WriteLog(FILE_NAME, "Save", model.CreateBy, ex);
                            throw new ApplicationException();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Save", model.CreateBy, ex);
                throw new ApplicationException();
            }
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(HomeNavigationModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            HOME_NAVIGATION _md = context.HOME_NAVIGATION.FirstOrDefault(m => m.navigation_id == model.NavigationID);
                            if (_md == null)
                            {
                                throw new FieldAccessException();
                            }
                            context.HOME_NAVIGATION.Remove(_md);
                            context.Entry(_md).State = System.Data.Entity.EntityState.Deleted;
                            context.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            TDH.Services.Log.WriteLog(FILE_NAME, "Delete", model.CreateBy, ex);
                            throw new ApplicationException();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Delete", model.CreateBy, ex);
                throw new ApplicationException();
            }
            return ResponseStatusCodeHelper.Success;
        }

    }
}