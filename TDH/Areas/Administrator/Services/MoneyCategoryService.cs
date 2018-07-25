using System;
using System.Collections.Generic;
using System.Linq;
using TDH.Models;
using Utils.JqueryDatatable;
using Utils;
using TDH.Areas.Administrator.Common;
using TDH.Model.Money;

namespace TDH.Areas.Administrator.Services
{
    public class MoneyCategoryService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator/Services/CategoryService.cs";

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
                DataTableResponse<CategoryModel> _itemResponse = new DataTableResponse<CategoryModel>();
                //List of data
                List<CategoryModel> _list = new List<CategoryModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _lData = (from m in context.MN_CATEGORY
                                  join n in context.MN_GROUP on m.group_id equals n.id
                                  where !n.deleted && !m.deleted && request.Parameter1 == (request.Parameter1.Length == 0 ? request.Parameter1 : m.group_id.ToString())
                                  select new
                                  {
                                      m.id,
                                      m.name,
                                      m.notes,
                                      group_name = n.name
                                  }).ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.name.ToLower().Contains(searchValue) ||
                                                   m.notes.ToLower().Contains(searchValue) ||
                                                   m.group_name.ToLower().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    byte _percentSet = 0;
                    byte _percentCur = 0;
                    decimal _moneySet = 0;
                    decimal _moneyCur = 0;
                    foreach (var item in _lData)
                    {
                        _percentSet = 0;
                        _percentCur = 0;
                        _moneySet = 0;
                        _moneyCur = 0;
                        var _cateSetting = context.MN_CATEGORY_SETTING.FirstOrDefault(m => m.category_id == item.id && m.year_month.ToString() == request.Parameter2); //By month year
                        if (_cateSetting != null)
                        {
                            _percentSet = _cateSetting.percent_setting;
                            _percentCur = _cateSetting.percent_current;
                            _moneySet = _cateSetting.money_setting;
                            _moneyCur = _cateSetting.money_current;
                        }
                        _list.Add(new CategoryModel()
                        {
                            ID = item.id,
                            Name = item.name,
                            GroupName = item.group_name,
                            Notes = item.notes,
                            PercentCurrent = _percentCur,
                            PercentSetting = _percentSet,
                            MoneyCurrent = _moneyCur,
                            MoneyCurrentString = _moneyCur.NumberToString(),
                            MoneySetting = _moneySet,
                            MoneySettingString = _moneySet.NumberToString(),
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
                                case "Name":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Name) : _sortList.Sort(col.Dir, m => m.Name);
                                    break;
                                case "Notes":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Notes) : _sortList.Sort(col.Dir, m => m.Notes);
                                    break;
                                case "GroupName":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.GroupName) : _sortList.Sort(col.Dir, m => m.GroupName);
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
        public List<CategoryModel> GetAll(Guid userID)
        {
            try
            {
                List<CategoryModel> _return = new List<CategoryModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _list = (from m in context.MN_CATEGORY
                                 join n in context.MN_GROUP on m.group_id equals n.id
                                 where n.publish && !n.deleted && !m.deleted
                                 orderby m.ordering descending
                                 select new
                                 {
                                     m.id,
                                     m.name
                                 }).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new CategoryModel() { ID = item.id, Name = item.name });
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
        /// <returns></returns>
        public List<CategoryModel> GetAll(Guid userID, bool isInput)
        {
            try
            {
                List<CategoryModel> _return = new List<CategoryModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _list = (from m in context.MN_CATEGORY
                                 join n in context.MN_GROUP on m.group_id equals n.id
                                 where n.publish && !n.deleted && !m.deleted && n.is_input == isInput
                                 orderby m.ordering descending
                                 select new
                                 {
                                     m.id,
                                     m.name
                                 }).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new CategoryModel() { ID = item.id, Name = item.name });
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
        /// Check income, payment history by month, and category id and by type (payment or income)
        /// Parameter1: by month
        /// Parameter2: by acount id
        /// Parameter3: by type (income or payment)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetHistory(CustomDataTableRequestHelper request, Guid userID)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                //Declare response data to json object
                DataTableResponse<CategoryHistoryModel> _itemResponse = new DataTableResponse<CategoryHistoryModel>();
                //List of data
                List<CategoryHistoryModel> _list = new List<CategoryHistoryModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _lData = (from m in context.V_CATEGORY_HISTORY
                                  where request.Parameter2 == (request.Parameter2.Length == 0 ? request.Parameter2 : m.category_id.ToString()) //By account id
                                  orderby m.date descending
                                  select new
                                  {
                                      m.title,
                                      m.date,
                                      m.money,
                                      m.type
                                  }).ToList();
                    if (request.Parameter1.Length > 0) //by month
                    {
                        _lData = _lData.Where(m => m.date.Value.DateToString("yyyyMM") == request.Parameter1).ToList();
                    }
                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.title.ToLower().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    foreach (var item in _lData)
                    {
                        _list.Add(new CategoryHistoryModel()
                        {
                            Title = item.title,
                            Date = item.date.Value,
                            DateString = item.date.Value.DateToString(),
                            MoneyString = item.money.NumberToString(),
                            Type = item.type
                        });
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    _itemResponse.data = _list.Skip(request.start).Take(request.length).ToList();
                    _return.Add(DatatableCommonSetting.Response.DATA, _itemResponse);
                }
                _return.Add(DatatableCommonSetting.Response.STATUS, ResponseStatusCodeHelper.OK);
            }
            catch (Exception ex)
            {
                Notifier.Notification(userID, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "GetHistory", userID, ex);
                throw new ApplicationException();
            }

            return _return;
        }

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="model"></param>
        /// <returns>CategoryModel. Throw exception if not found or get some error</returns>
        public CategoryModel GetItemByID(CategoryModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    MN_CATEGORY _md = context.MN_CATEGORY.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new FieldAccessException();
                    }
                    var _gr = context.MN_GROUP.FirstOrDefault(m => m.id == _md.group_id);
                    var _lSetting = context.MN_CATEGORY_SETTING.Where(m => m.category_id == model.ID && m.year_month.ToString().Contains(DateTime.Now.Year.ToString())).OrderByDescending(m => m.year_month);
                    //
                    CategoryModel _return = new CategoryModel()
                    {
                        ID = _md.id,
                        Name = _md.name,
                        Notes = _md.notes,
                        GroupID = _md.group_id,
                        GroupName = _gr.name,
                        PercentSetting = _md.percent_setting,
                        PercentCurrent = _md.percent_current,
                        MoneyCurrent = _md.money_current,
                        MoneySetting = _md.money_setting,
                        Ordering = _md.ordering,
                        Publish = _md.publish
                    };
                    //
                    foreach (var item in _lSetting)
                    {
                        _return.Setting.Add(new CategorySettingModel()
                        {
                            Month = item.year_month % 100,
                            Year = item.year_month / 100,
                            PercentSetting = item.percent_setting,
                            PercentCurrent = item.percent_current,
                            MoneySetting = item.money_setting,
                            MoneyCurrent = item.money_current,
                            YearMonthString = item.year_month.ToString()
                        });
                    }
                    //
                    return _return;
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
        public ResponseStatusCodeHelper Save(CategoryModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            MN_CATEGORY _md = new MN_CATEGORY();
                            if (model.Insert)
                            {
                                _md.id = Guid.NewGuid();
                            }
                            else
                            {
                                _md = context.MN_CATEGORY.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                                if (_md == null)
                                {
                                    throw new FieldAccessException();
                                }
                            }
                            _md.group_id = model.GroupID;
                            _md.name = model.Name;
                            _md.notes = model.Notes;
                            //Setting doesn't allow set in create or update
                            if (model.Insert)
                            {
                                _md.create_by = model.CreateBy;
                                _md.create_date = DateTime.Now;
                                context.MN_CATEGORY.Add(_md);
                                context.Entry(_md).State = System.Data.Entity.EntityState.Added;
                            }
                            else
                            {
                                _md.update_by = model.UpdateBy;
                                _md.update_date = DateTime.Now;
                                context.MN_CATEGORY.Attach(_md);
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
        public ResponseStatusCodeHelper Publish(CategoryModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            MN_CATEGORY _md = context.MN_CATEGORY.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                            if (_md == null)
                            {
                                throw new FieldAccessException();
                            }
                            _md.publish = model.Publish;
                            _md.update_by = model.UpdateBy;
                            _md.update_date = DateTime.Now;
                            context.MN_CATEGORY.Attach(_md);
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
        public ResponseStatusCodeHelper Delete(CategoryModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            MN_CATEGORY _md = context.MN_CATEGORY.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                            if (_md == null)
                            {
                                throw new FieldAccessException();
                            }
                            _md.deleted = true;
                            _md.delete_by = model.DeleteBy;
                            _md.delete_date = DateTime.Now;
                            context.MN_CATEGORY.Attach(_md);
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
        public ResponseStatusCodeHelper CheckDelete(CategoryModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {

                    MN_CATEGORY _md = context.MN_CATEGORY.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new FieldAccessException();
                    }
                    var _payment = context.MN_PAYMENT.FirstOrDefault(m => m.category_id == model.ID && !m.deleted);
                    if (_payment != null)
                    {
                        Notifier.Notification(model.CreateBy, Resources.Message.CheckExists, Notifier.TYPE.Warning);
                        return ResponseStatusCodeHelper.NG;
                    }
                    var _income = context.MN_INCOME.FirstOrDefault(m => m.category_id == model.ID && !m.deleted);
                    if (_income != null)
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