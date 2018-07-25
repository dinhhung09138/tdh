using System;
using System.Collections.Generic;
using System.Linq;
using TDH.Models;
using Utils.JqueryDatatable;
using TDH.Areas.Administrator.Models;
using Utils;
using TDH.Areas.Administrator.Common;
using TDH.Model.Money;

namespace TDH.Areas.Administrator.Services
{
    public class MoneyGroupService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator/Services/MoneyGroupService.cs";

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
                DataTableResponse<GroupModel> _itemResponse = new DataTableResponse<GroupModel>();
                //List of data
                List<GroupModel> _list = new List<GroupModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _lData = (from m in context.MN_GROUP
                                  where !m.deleted && 
                                        m.is_input == (request.Parameter1 == "" ? m.is_input : request.Parameter1 == "0" ? false : true) //by Type (income of payment)
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
                    int _count = 0;
                    byte _percentSet = 0;
                    byte _percentCur = 0;
                    decimal _moneySet = 0;
                    decimal _moneyCur = 0;
                    foreach (var item in _lData)
                    {
                        _count = context.MN_CATEGORY.Count(m => m.group_id == item.id && !m.deleted);
                        _percentSet = 0;
                        _percentCur = 0;
                        _moneySet = 0;
                        _moneyCur = 0;
                        var _grSetting = context.MN_GROUP_SETTING.FirstOrDefault(m => m.group_id == item.id && m.year_month.ToString() == request.Parameter2); //By month year
                        if(_grSetting != null)
                        {
                            _percentSet = _grSetting.percent_setting;
                            _percentCur = _grSetting.percent_current;
                            _moneySet = _grSetting.money_setting;
                            _moneyCur = _grSetting.money_current;
                        }
                        //
                        _list.Add(new GroupModel()
                        {
                            ID = item.id,
                            Name = item.name,
                            Notes = item.notes,
                            IsInput = item.is_input,
                            PercentCurrent = _percentCur,
                            PercentSetting = _percentSet,
                            MoneyCurrent = _moneyCur,
                            MoneyCurrentString = _moneyCur.NumberToString(),
                            MoneySetting = _moneySet,
                            MoneySettingString = _moneySet.NumberToString(),
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
                Notifier.Notification(userID, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "List", userID, ex);
                throw new ApplicationException();
            }

            return _return;
        }

        /// <summary>
        /// Get all item without deleted
        /// </summary>
        /// <returns></returns>
        public List<GroupModel> GetAll(Guid userID)
        {
            try
            {
                List<GroupModel> _return = new List<GroupModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _list = (from m in context.MN_GROUP
                                 where !m.deleted && m.publish
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
                Notifier.Notification(userID, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "GetAll", userID, ex);
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// Get all item without deleted
        /// </summary>
        /// <param name="userID">User id</param>
        /// <param name="IsInput">true: input mean payment, false: income money</param>
        /// <returns></returns>
        public List<GroupModel> GetAll(Guid userID, bool IsInput)
        {
            try
            {
                List<GroupModel> _return = new List<GroupModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _list = (from m in context.MN_GROUP
                                 where !m.deleted && m.publish && m.is_input == IsInput
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
                Notifier.Notification(userID, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "GetAll", userID, ex);
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="model"></param>
        /// <returns>GroupModel. Throw exception if not found or get some error</returns>
        public GroupModel GetItemByID(GroupModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    MN_GROUP _md = context.MN_GROUP.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new FieldAccessException();
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
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "GetItemByID", model.CreateBy, ex);
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(GroupModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            MN_GROUP _md = new MN_GROUP();
                            if (model.Insert)
                            {
                                _md.id = Guid.NewGuid();
                                _md.is_input = model.IsInput;
                            }
                            else
                            {
                                _md = context.MN_GROUP.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                                if (_md == null)
                                {
                                    throw new FieldAccessException();
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
                                context.MN_GROUP.Add(_md);
                                context.Entry(_md).State = System.Data.Entity.EntityState.Added;
                            }
                            else
                            {
                                _md.update_by = model.UpdateBy;
                                _md.update_date = DateTime.Now;
                                context.MN_GROUP.Attach(_md);
                                context.Entry(_md).State = System.Data.Entity.EntityState.Modified;
                            }
                            context.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            Notifier.Notification(model.CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                            trans.Rollback();
                            TDH.Services.Log.WriteLog(FILE_NAME, "Save", model.CreateBy, ex);
                            throw new ApplicationException();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "Save", model.CreateBy, ex);
                throw new ApplicationException();
            }
            if (model.Insert)
            {
                Notifier.Notification(model.CreateBy, Resources.Message.InsertSuccess, Notifier.TYPE.Success);
            }
            else
            {
                Notifier.Notification(model.CreateBy, Resources.Message.UpdateSuccess, Notifier.TYPE.Success);
            }
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Publish
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Publish(GroupModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            MN_GROUP _md = context.MN_GROUP.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                            if (_md == null)
                            {
                                throw new FieldAccessException();
                            }
                            _md.publish = model.Publish;
                            _md.update_by = model.UpdateBy;
                            _md.update_date = DateTime.Now;
                            context.MN_GROUP.Attach(_md);
                            context.Entry(_md).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            Notifier.Notification(model.CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                            trans.Rollback();
                            TDH.Services.Log.WriteLog(FILE_NAME, "Publish", model.CreateBy, ex);
                            throw new ApplicationException();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "Publish", model.CreateBy, ex);
                throw new ApplicationException();
            }
            Notifier.Notification(model.CreateBy, Resources.Message.UpdateSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }
        
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(GroupModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            MN_GROUP _md = context.MN_GROUP.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                            if (_md == null)
                            {
                                throw new FieldAccessException();
                            }
                            _md.deleted = true;
                            _md.delete_by = model.DeleteBy;
                            _md.delete_date = DateTime.Now;
                            context.MN_GROUP.Attach(_md);
                            context.Entry(_md).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            Notifier.Notification(model.CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                            trans.Rollback();
                            TDH.Services.Log.WriteLog(FILE_NAME, "Delete", model.CreateBy, ex);
                            throw new ApplicationException();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "Delete", model.CreateBy, ex);
                throw new ApplicationException();
            }
            Notifier.Notification(model.CreateBy, Resources.Message.DeleteSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Check Delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper CheckDelete(GroupModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {

                    MN_GROUP _md = context.MN_GROUP.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new FieldAccessException();
                    }
                    var _group = context.MN_CATEGORY.FirstOrDefault(m => m.group_id == model.ID && !m.deleted);
                    if (_group != null)
                    {
                        Notifier.Notification(model.CreateBy, Resources.Message.CheckExists, Notifier.TYPE.Warning);
                        return ResponseStatusCodeHelper.NG;
                    }
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "CheckDelete", model.CreateBy, ex);
                throw new ApplicationException();
            }
            return ResponseStatusCodeHelper.OK;
        }

    }
}