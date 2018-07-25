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
    public class MoneyAccountService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator/Services/MoneyAccountService.cs";

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
                DataTableResponse<AccountModel> _itemResponse = new DataTableResponse<AccountModel>();
                //List of data
                List<AccountModel> _list = new List<AccountModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _lData = (from m in context.MN_ACCOUNT
                                  join n in context.MN_ACCOUNT_TYPE on m.account_type_id equals n.id
                                  where !n.deleted && n.publish && !m.deleted
                                  orderby m.name descending
                                  select new
                                  {
                                      m.id,
                                      m.name,
                                      m.input,
                                      m.output,
                                      type_name = n.name
                                  }).ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.name.ToLower().Contains(searchValue) ||
                                                   m.type_name.ToLower().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    decimal _input = 0;
                    decimal _out = 0;
                    decimal _yearMonth = decimal.Parse(DateTime.Now.DateToString("yyyyMM"));
                    foreach (var item in _lData)
                    {
                        _input = 0;
                        _out = 0;
                        var _setting = context.MN_ACCOUNT_SETTING.FirstOrDefault(m => m.account_id == item.id && m.yearmonth == _yearMonth);
                        if(_setting != null)
                        {
                            _input = _setting.input;
                            _out = _setting.output;
                        }
                        _list.Add(new AccountModel()
                        {
                            ID = item.id,
                            Name = item.name,
                            AccountTypeName = item.type_name,
                            MonthInput = _input,
                            MonthInputString = _input.NumberToString(),
                            MonthOutput = _out,
                            MonthOutputString = _out.NumberToString(),
                            MonthTotal = (_input - _out),
                            MonthTotalString = (_input - _out).NumberToString(),
                            Total = (item.input - item.output),
                            TotalString = (item.input - item.output).NumberToString()
                        });
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<AccountModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
                                case "Name":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Name) : _sortList.Sort(col.Dir, m => m.Name);
                                    break;
                                case "AccountTypeName":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.AccountTypeName) : _sortList.Sort(col.Dir, m => m.AccountTypeName);
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
        public List<AccountModel> GetAll(Guid userID)
        {
            try
            {
                List<AccountModel> _return = new List<AccountModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _list = (from m in context.MN_ACCOUNT
                                 join n in context.MN_ACCOUNT_TYPE on m.account_type_id equals n.id
                                 where !m.deleted && !n.deleted && n.publish
                                 orderby m.name descending
                                 select new
                                 {
                                     m.id,
                                     m.name
                                 }).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new AccountModel() { ID = item.id, Name = item.name });
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
        public List<AccountModel> GetAllWithFullMoney(Guid userID)
        {
            try
            {
                List<AccountModel> _return = new List<AccountModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _list = (from m in context.MN_ACCOUNT
                                 join n in context.MN_ACCOUNT_TYPE on m.account_type_id equals n.id
                                 where !m.deleted && !n.deleted && n.publish && (m.input - m.output) > 0
                                 orderby m.name descending
                                 select new
                                 {
                                     m.id,
                                     m.name
                                 }).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new AccountModel() { ID = item.id, Name = item.name });
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
        /// <returns>AccountModel. Throw exception if not found or get some error</returns>
        public AccountModel GetItemByID(AccountModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    MN_ACCOUNT _md = context.MN_ACCOUNT.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new FieldAccessException();
                    }
                    MN_ACCOUNT_TYPE _type = context.MN_ACCOUNT_TYPE.FirstOrDefault(m => m.id == _md.account_type_id);
                    var _lSetting = context.MN_ACCOUNT_SETTING.Where(m => m.account_id == model.ID && m.yearmonth.ToString().Contains(DateTime.Now.Year.ToString())).OrderByDescending(m => m.yearmonth);
                    //
                    var _return = new AccountModel()
                    {
                        ID = _md.id,
                        Name = _md.name,
                        AccountTypeID = _md.account_type_id,
                        AccountTypeName = _type.name,
                        Total = _md.input - _md.output
                        //Start = _md.start,
                        //End = _md.input - _md.output,
                        //Input = _md.input,
                        //Output = _md.output
                    };
                    foreach (var item in _lSetting)
                    {
                        _return.Setting.Add(new AccountSettingModel() {
                            Month = item.yearmonth % 100,
                            Year = item.yearmonth / 100,
                            Input = item.input,
                            Output = item.output,
                            YearMonthString = item.yearmonth.ToString()
                        });
                    }
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
        /// Check income, payment history by month, and account id and by type (payment or income)
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
                DataTableResponse<AccountHistoryModel> _itemResponse = new DataTableResponse<AccountHistoryModel>();
                //List of data
                List<AccountHistoryModel> _list = new List<AccountHistoryModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _lData = (from m in context.V_ACCOUNT_HISTORY
                                  where request.Parameter2 == (request.Parameter2.Length == 0 ? request.Parameter2 : m.account_id.ToString()) && //By account id
                                        request.Parameter3 == (request.Parameter3.Length == 0 ? request.Parameter3 : m.type.ToString()) //by type (income or payment)
                                  orderby m.date descending
                                  select new
                                  {
                                      m.title,
                                      m.date,
                                      m.money,
                                      m.type
                                  }).ToList();
                    if(request.Parameter1.Length > 0) //by month
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
                        _list.Add(new AccountHistoryModel()
                        {
                            Title = item.title,
                            Date = item.date.Value,
                            DateString = item.date.Value.DateToString(),
                            MoneyString = item.money.Value.NumberToString(),
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
        /// Save
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(AccountModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            MN_ACCOUNT _md = new MN_ACCOUNT();
                            if (model.Insert)
                            {
                                _md.id = Guid.NewGuid();
                            }
                            else
                            {
                                _md = context.MN_ACCOUNT.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                                if (_md == null)
                                {
                                    throw new FieldAccessException();
                                }
                            }
                            _md.account_type_id = model.AccountTypeID;
                            _md.name = model.Name;
                            //Create or edit, only change the name and type
                            if (model.Insert)
                            {
                                _md.create_by = model.CreateBy;
                                _md.create_date = DateTime.Now;
                                context.MN_ACCOUNT.Add(_md);
                                context.Entry(_md).State = System.Data.Entity.EntityState.Added;
                            }
                            else
                            {
                                _md.update_by = model.UpdateBy;
                                _md.update_date = DateTime.Now;
                                context.MN_ACCOUNT.Attach(_md);
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
        public ResponseStatusCodeHelper Publish(AccountModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            MN_ACCOUNT _md = context.MN_ACCOUNT.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                            if (_md == null)
                            {
                                throw new FieldAccessException();
                            }
                            _md.publish = model.Publish;
                            _md.update_by = model.UpdateBy;
                            _md.update_date = DateTime.Now;
                            context.MN_ACCOUNT.Attach(_md);
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
        public ResponseStatusCodeHelper Delete(AccountModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            MN_ACCOUNT _md = context.MN_ACCOUNT.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                            if (_md == null)
                            {
                                throw new FieldAccessException();
                            }
                            _md.deleted = true;
                            _md.delete_by = model.DeleteBy;
                            _md.delete_date = DateTime.Now;
                            context.MN_ACCOUNT.Attach(_md);
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
        public ResponseStatusCodeHelper CheckDelete(AccountModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {

                    MN_ACCOUNT _md = context.MN_ACCOUNT.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new FieldAccessException();
                    }
                    var _accSetting = context.MN_ACCOUNT_SETTING.FirstOrDefault(m => m.account_id == model.ID && !m.deleted);
                    if (_accSetting != null)
                    {
                        Notifier.Notification(model.CreateBy, Resources.Message.CheckExists, Notifier.TYPE.Warning);
                        return ResponseStatusCodeHelper.NG;
                    }
                    var _payment = context.MN_PAYMENT.FirstOrDefault(m => m.account_id == model.ID && !m.deleted);
                    if (_payment != null)
                    {
                        Notifier.Notification(model.CreateBy, Resources.Message.CheckExists, Notifier.TYPE.Warning);
                        return ResponseStatusCodeHelper.NG;
                    }
                    var _income = context.MN_INCOME.FirstOrDefault(m => m.account_id == model.ID && !m.deleted);
                    if (_income != null)
                    {
                        Notifier.Notification(model.CreateBy, Resources.Message.CheckExists, Notifier.TYPE.Warning);
                        return ResponseStatusCodeHelper.NG;
                    }
                    var _transFrom = context.MN_TRANSFER.FirstOrDefault(m => m.account_from == model.ID && !m.deleted);
                    if (_transFrom != null)
                    {
                        Notifier.Notification(model.CreateBy, Resources.Message.CheckExists, Notifier.TYPE.Warning);
                        return ResponseStatusCodeHelper.NG;
                    }
                    var _transTo = context.MN_TRANSFER.FirstOrDefault(m => m.account_to == model.ID && !m.deleted);
                    if (_transTo != null)
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