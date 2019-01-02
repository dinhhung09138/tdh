using System;
using System.Collections.Generic;
using System.Linq;
using Utils.JqueryDatatable;
using Utils;
using TDH.Common;
using TDH.DataAccess;
using TDH.Model.Money;
using System.Data.Entity;
using TDH.Common.UserException;
using System.Reflection;

namespace TDH.Services.Money
{
    /// <summary>
    /// Group service
    /// </summary>
    public class GroupService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.Money/GroupService.cs";

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
                DataTableResponse<GroupModel> _itemResponse = new DataTableResponse<GroupModel>();
                //List of data
                List<GroupModel> _list = new List<GroupModel>();
                using (var _context = new TDHEntities())
                {
                    var _lData = (from m in _context.MN_GROUP
                                  where !m.deleted && m.create_by == userID &&
                                        m.is_input == (request.Parameter1 == "" ? m.is_input : request.Parameter1 == "0" ? false : true) //by Type (income or payment)
                                  select new
                                  {
                                      m.id,
                                      m.name,
                                      m.notes,
                                      m.is_input,
                                      m.publish
                                  }).ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.name.ToLower().Contains(searchValue) ||
                                                   m.notes.ToLower().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    int _year = int.Parse(request.Parameter2) / 100;
                    int _month = int.Parse(request.Parameter2) % 100;
                    int _count = 0;
                    byte _percentSet = 0;
                    decimal _moneyCur = 0;
                    foreach (var item in _lData)
                    {
                        _count = _context.MN_CATEGORY.Count(m => m.group_id == item.id && !m.deleted && m.create_by == userID);
                        _percentSet = 0;
                        _moneyCur = 0;
                        var _grSetting = _context.MN_GROUP_SETTING.FirstOrDefault(m => m.group_id == item.id && m.create_by == userID && m.year_month.ToString() == request.Parameter2); //By month year
                        if (_grSetting != null)
                        {
                            _percentSet = _grSetting.percent_setting;
                        }
                        _moneyCur = (from i in _context.V_MN_CATEGORY
                                     where i.group_id == item.id && i.year == _year && i.month == _month && i.create_by == userID
                                     select new { i.current_income, i.current_payment }).ToList().Select(m => (item.is_input ? m.current_income : m.current_payment)).DefaultIfEmpty(0).Sum();
                        //
                        _list.Add(new GroupModel()
                        {
                            ID = item.id,
                            Name = item.name,
                            Notes = item.notes,
                            IsInput = item.is_input,
                            PercentSetting = _percentSet,
                            MoneyCurrent = _moneyCur,
                            MoneyCurrentString = _moneyCur.NumberToString(),
                            Publish = item.publish,
                            Count = _count,
                            CountString = _count.NumberToString()
                        });
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<GroupModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
                                case "Name":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Name) : _sortList.Sort(col.Dir, m => m.Name);
                                    break;
                                case "Notes":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Notes) : _sortList.Sort(col.Dir, m => m.Notes);
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
        /// <returns>List<GroupModel></returns>
        public List<GroupModel> GetAll(Guid userID)
        {
            try
            {
                List<GroupModel> _return = new List<GroupModel>();
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.MN_GROUP
                                 where !m.deleted && m.publish && m.create_by == userID
                                 orderby m.ordering descending
                                 select new
                                 {
                                     m.id,
                                     m.name
                                 }).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new GroupModel() { ID = item.id, Name = item.name });
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
        /// Get all item without deleted
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <param name="IsInput">true: income, false: payment</param>
        /// <returns>List<GroupModel></returns>
        public List<GroupModel> GetAll(Guid userID, bool IsInput)
        {
            try
            {
                List<GroupModel> _return = new List<GroupModel>();
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.MN_GROUP
                                 where !m.deleted && m.publish && m.is_input == IsInput && m.create_by == userID
                                 orderby m.ordering descending
                                 select new
                                 {
                                     m.id,
                                     m.name
                                 }).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new GroupModel() { ID = item.id, Name = item.name });
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
        /// Get item by id
        /// </summary>
        /// <param name="model">Group model</param>
        /// <returns>MoneyGroupModel</returns>
        public GroupModel GetItemByID(GroupModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    MN_GROUP _md = _context.MN_GROUP.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.create_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    return new GroupModel()
                    {
                        ID = _md.id,
                        Name = _md.name,
                        Notes = _md.notes,
                        IsInput = _md.is_input,
                        PercentSetting = _md.percent_setting,
                        PercentCurrent = _md.percent_current,
                        MoneyCurrent = _md.money_current,
                        MoneyCurrentString = _md.money_current.NumberToString(),
                        MoneySetting = _md.money_setting,
                        MoneySettingString = _md.money_setting.NumberToString(),
                        Ordering = _md.ordering,
                        Publish = _md.publish
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
        /// <param name="model">Group model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(GroupModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    MN_GROUP _md = new MN_GROUP();
                    if (model.Insert)
                    {
                        _md.id = Guid.NewGuid();
                        _md.is_input = model.IsInput;
                    }
                    else
                    {
                        _md = _context.MN_GROUP.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.create_by == model.CreateBy);
                        if (_md == null)
                        {
                            throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                        }
                    }
                    _md.name = model.Name;
                    _md.notes = model.Notes;
                    _md.ordering = model.Ordering;
                    _md.publish = model.Publish;
                    //Setting value don't allow change when create or edit
                    if (model.Insert)
                    {
                        _md.create_by = model.CreateBy;
                        _md.create_date = DateTime.Now;
                        _context.MN_GROUP.Add(_md);
                        _context.Entry(_md).State = EntityState.Added;
                    }
                    else
                    {
                        _md.update_by = model.UpdateBy;
                        _md.update_date = DateTime.Now;
                        _context.MN_GROUP.Attach(_md);
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
        /// <param name="model">Group model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Publish(GroupModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    MN_GROUP _md = _context.MN_GROUP.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.create_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    _md.publish = model.Publish;
                    _md.update_by = model.UpdateBy;
                    _md.update_date = DateTime.Now;
                    _context.MN_GROUP.Attach(_md);
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
        /// <param name="model">Group model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(GroupModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    MN_GROUP _md = _context.MN_GROUP.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.create_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    _md.deleted = true;
                    _md.delete_by = model.DeleteBy;
                    _md.delete_date = DateTime.Now;
                    _context.MN_GROUP.Attach(_md);
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
        /// Check Delete
        /// </summary>
        /// <param name="model">Group model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper CheckDelete(GroupModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    MN_GROUP _md = _context.MN_GROUP.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.create_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    var _group = _context.MN_CATEGORY.FirstOrDefault(m => m.group_id == model.ID && !m.deleted && m.create_by == model.CreateBy);
                    if (_group != null)
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
