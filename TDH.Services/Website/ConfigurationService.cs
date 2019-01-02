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
    public class ConfigurationService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.Website/ConfigurationService.cs";

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
                DataTableResponse<ConfigurationModel> _itemResponse = new DataTableResponse<ConfigurationModel>();
                //List of data
                List<ConfigurationModel> _list = new List<ConfigurationModel>();
                using (var _context = new TDHEntities())
                {
                    var _lData = _context.WEB_CONFIGURATION.ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.key.ToLower().Contains(searchValue) ||
                                                   m.description.ToString().Contains(searchValue) ||
                                                   m.value.ToString().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    foreach (var item in _lData)
                    {
                        _list.Add(new ConfigurationModel()
                        {
                            Key = item.key,
                            Value = item.value,
                            Description = item.description
                        });
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<ConfigurationModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
                                case "Key":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Key) : _sortList.Sort(col.Dir, m => m.Key);
                                    break;
                                case "Value":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Value) : _sortList.Sort(col.Dir, m => m.Value);
                                    break;
                                case "Description":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Description) : _sortList.Sort(col.Dir, m => m.Description);
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
        /// Get item
        /// </summary>
        /// <param name="model">Configuration model</param>
        /// <returns>ConfigurationModel</returns>
        public ConfigurationModel GetItemByID(ConfigurationModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    WEB_CONFIGURATION _md = _context.WEB_CONFIGURATION.FirstOrDefault(m => m.key == model.Key);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    return new ConfigurationModel()
                    {
                        Key = _md.key,
                        Description = _md.description,
                        Value = _md.value
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
        /// <param name="model">Configuration model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(ConfigurationModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    WEB_CONFIGURATION _md = _context.WEB_CONFIGURATION.FirstOrDefault(m => m.key == model.Key);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    _md.key = model.Key;
                    _md.description = model.Description;
                    _md.value = model.Value;
                    _context.WEB_CONFIGURATION.Attach(_md);
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

    }
}
