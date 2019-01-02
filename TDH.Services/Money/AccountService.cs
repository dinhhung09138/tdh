using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using TDH.Common;
using TDH.Common.UserException;
using TDH.DataAccess;
using TDH.Model.Money;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Services.Money
{
    /// <summary>
    /// Account service
    /// </summary>
    public class AccountService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.Money/AccountService.cs";

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
                DataTableResponse<AccountModel> _itemResponse = new DataTableResponse<AccountModel>();
                //List of data
                List<AccountModel> _list = new List<AccountModel>();
                using (var _context = new TDHEntities())
                {
                    var _lData = (from m in _context.MN_ACCOUNT
                                  join n in _context.MN_ACCOUNT_TYPE on m.account_type_id equals n.id
                                  where !n.deleted && n.publish && !m.deleted && m.create_by == userID
                                  orderby m.name descending
                                  select new
                                  {
                                      m.id,
                                      m.name,
                                      m.input,
                                      m.output,
                                      m.max_payment,
                                      type_name = n.name,
                                      n.type
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
                    decimal _yearMonth = decimal.Parse(DateTime.Now.DateToString("yyyyMM"));
                    foreach (var item in _lData)
                    {
                        _list.Add(new AccountModel()
                        {
                            ID = item.id,
                            Name = item.name,
                            MaxPayment = item.type == (short)AccountType.Debit || item.type == (short)AccountType.Saving || item.type == (short)AccountType.Cash ? 0 :
                                          item.max_payment,
                            MaxPaymentString = item.type == (short)AccountType.Debit || item.type == (short)AccountType.Saving || item.type == (short)AccountType.Cash ? "" :
                                          item.max_payment.NumberToString(),
                            BorrowMoney = item.type == (short)AccountType.Credit ? (item.input > item.output ? 0 : item.output - item.input) : //For credit card, show if income > payment, don't have debt
                                          item.type == (short)AccountType.Borrow ? item.max_payment - item.input : //Can not use for pay bills, only receive money to pay for debt
                                          0,
                            BorrowMoneyString = item.type == (short)AccountType.Credit ? (item.input > item.output ? "" : (item.output - item.input).NumberToString()) :
                                                item.type == (short)AccountType.Borrow ? (item.max_payment - item.input).NumberToString() : //Borrow
                                                "",
                            LoanMoney = item.type == (short)AccountType.Loan ? item.max_payment - item.input : 0,
                            LoanMoneyString = item.type == (short)AccountType.Loan ? (item.max_payment - item.input).NumberToString() : "",
                            AccountType = item.type,
                            Total = item.type == (short)AccountType.Credit ? (item.input - item.output > 0 ? item.input - item.output : 0) : //For credit card, show if income > payment, don't have debt
                                    item.type == (short)AccountType.Borrow ? 0 :
                                    item.type == (short)AccountType.Loan ? 0 :
                                    (item.input - item.output),
                            TotalString = item.type == (short)AccountType.Credit ? (item.input - item.output > 0 ? (item.input - item.output).NumberToString() : "") :
                                          item.type == (short)AccountType.Borrow ? "" :
                                          item.type == (short)AccountType.Loan ? "" :
                                          (item.input - item.output).NumberToString()
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
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, userID, ex);
            }
            return _return;
        }

        /// <summary>
        /// Get all item without deleted
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<AccountModel></returns>
        public List<AccountModel> GetAll(Guid userID)
        {
            try
            {
                List<AccountModel> _return = new List<AccountModel>();
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.MN_ACCOUNT
                                 join n in _context.MN_ACCOUNT_TYPE on m.account_type_id equals n.id
                                 where !m.deleted && !n.deleted && n.publish && m.create_by == userID
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
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, userID, ex);
            }
        }

        /// <summary>
        /// Get all item without deleted
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<AccountModel></returns>
        public List<AccountModel> GetAllWithFullMoney(Guid userID)
        {
            try
            {
                List<AccountModel> _return = new List<AccountModel>();
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.MN_ACCOUNT
                                 join n in _context.MN_ACCOUNT_TYPE on m.account_type_id equals n.id
                                 where !m.deleted && !n.deleted && n.type < (short)AccountType.Borrow && n.publish && m.create_by == userID
                                 orderby m.name descending
                                 select new
                                 {
                                     m.id,
                                     m.name,
                                     m.max_payment,
                                     m.input,
                                     m.output,
                                     n.type
                                 }).ToList();
                    foreach (var item in _list)
                    {
                        if ((item.type == (short)AccountType.Debit || item.type == (short)AccountType.Cash) && (item.input - item.output) <= 0)
                        {
                            continue;
                        }
                        if (item.type == (short)AccountType.Credit && item.output - item.input >= item.max_payment)
                        {
                            continue;
                        }
                        _return.Add(new AccountModel() { ID = item.id, Name = item.name });
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
        /// <param name="model">Account model</param>
        /// <returns>MoneyAccountModel</returns>
        public AccountModel GetItemByID(AccountModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    MN_ACCOUNT _md = _context.MN_ACCOUNT.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.create_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    MN_ACCOUNT_TYPE _type = _context.MN_ACCOUNT_TYPE.FirstOrDefault(m => m.id == _md.account_type_id);

                    var _return = new AccountModel()
                    {
                        ID = _md.id,
                        Name = _md.name,
                        MaxPayment = _md.max_payment,
                        MaxPaymentString = _md.max_payment.NumberToString(),
                        AccountType = _type.type,
                        AccountTypeID = _md.account_type_id,
                        AccountTypeName = _type.name,
                        Total = _type.type == (short)AccountType.Borrow ? _md.input - _md.max_payment :
                                _type.type == (short)AccountType.Loan ? _md.input - _md.max_payment :
                                _md.input - _md.output
                    };
                    return _return;
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
        /// Check income, payment history by month, and account id and by type (payment or income)
        /// Parameter1: by month
        /// Parameter2: by acount id
        /// Parameter3: by type (income or payment)
        /// </summary>
        /// <param name="request">Jquery datatable request</param>
        /// <param name="userID">The user identifier</param>
        /// <returns>Dictionary<string, object></returns>
        public Dictionary<string, object> GetHistory(CustomDataTableRequestHelper request, Guid userID)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                //Declare response data to json object
                DataTableResponse<AccountHistoryModel> _itemResponse = new DataTableResponse<AccountHistoryModel>();
                //List of data
                List<AccountHistoryModel> _list = new List<AccountHistoryModel>();
                using (var _context = new TDHEntities())
                {
                    var _lData = (from m in _context.V_MN_ACCOUNT_HISTORY
                                  where request.Parameter1 == (request.Parameter1.Length == 0 ? request.Parameter1 : m.account_id.ToString()) && //By account id
                                        request.Parameter2 == (request.Parameter2.Length == 0 ? request.Parameter2 : m.type.ToString()) //by type (income or payment)
                                  orderby m.date descending
                                  select new
                                  {
                                      m.title,
                                      m.date,
                                      m.money,
                                      m.type
                                  }).ToList();
                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.title.ToLower().Contains(searchValue) || m.date.Value.ToString().ToLower().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    foreach (var item in _lData)
                    {
                        _list.Add(new AccountHistoryModel()
                        {
                            Title = item.title,
                            Date = item.date.Value,
                            DateString = item.date.Value.DateToString(),
                            Money = item.money.Value,
                            MoneyString = item.money.Value.NumberToString(),
                            Type = item.type
                        });
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<AccountHistoryModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
                                case "DateString":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Date) : _sortList.Sort(col.Dir, m => m.Date);
                                    break;
                                case "MoneyString":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Money) : _sortList.Sort(col.Dir, m => m.Money);
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
        /// Save
        /// </summary>
        /// <param name="model">Account model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(AccountModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    MN_ACCOUNT _md = new MN_ACCOUNT();
                    if (model.Insert)
                    {
                        _md.id = Guid.NewGuid();
                    }
                    else
                    {
                        _md = _context.MN_ACCOUNT.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.create_by == model.CreateBy);
                        if (_md == null)
                        {
                            throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                        }
                    }
                    _md.account_type_id = model.AccountTypeID;
                    _md.name = model.Name;
                    _md.max_payment = model.MaxPayment;
                    //Create or edit, only change the name and type
                    if (model.Insert)
                    {
                        _md.create_by = model.CreateBy;
                        _md.create_date = DateTime.Now;
                        _context.MN_ACCOUNT.Add(_md);
                        _context.Entry(_md).State = EntityState.Added;
                    }
                    else
                    {
                        _md.update_by = model.UpdateBy;
                        _md.update_date = DateTime.Now;
                        _context.MN_ACCOUNT.Attach(_md);
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
        /// <param name="model">Account model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Publish(AccountModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    MN_ACCOUNT _md = _context.MN_ACCOUNT.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.create_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    _md.publish = model.Publish;
                    _md.update_by = model.UpdateBy;
                    _md.update_date = DateTime.Now;
                    _context.MN_ACCOUNT.Attach(_md);
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
        /// <param name="model">Account model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(AccountModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    MN_ACCOUNT _md = _context.MN_ACCOUNT.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.create_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    _md.deleted = true;
                    _md.delete_by = model.DeleteBy;
                    _md.delete_date = DateTime.Now;
                    _context.MN_ACCOUNT.Attach(_md);
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
        /// <param name="model">Account model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper CheckDelete(AccountModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    MN_ACCOUNT _md = _context.MN_ACCOUNT.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.create_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    var _accSetting = _context.MN_ACCOUNT_SETTING.FirstOrDefault(m => m.account_id == model.ID && !m.deleted);
                    if (_accSetting != null)
                    {
                        Notifier.Notification(model.CreateBy, Message.CheckExists, Notifier.TYPE.Warning);
                        return ResponseStatusCodeHelper.NG;
                    }
                    var _payment = _context.MN_PAYMENT.FirstOrDefault(m => m.account_id == model.ID && !m.deleted);
                    if (_payment != null)
                    {
                        Notifier.Notification(model.CreateBy, Message.CheckExists, Notifier.TYPE.Warning);
                        return ResponseStatusCodeHelper.NG;
                    }
                    var _income = _context.MN_INCOME.FirstOrDefault(m => m.account_id == model.ID && !m.deleted);
                    if (_income != null)
                    {
                        Notifier.Notification(model.CreateBy, Message.CheckExists, Notifier.TYPE.Warning);
                        return ResponseStatusCodeHelper.NG;
                    }
                    var _transFrom = _context.MN_TRANSFER.FirstOrDefault(m => m.account_from == model.ID && !m.deleted);
                    if (_transFrom != null)
                    {
                        Notifier.Notification(model.CreateBy, Message.CheckExists, Notifier.TYPE.Warning);
                        return ResponseStatusCodeHelper.NG;
                    }
                    var _transTo = _context.MN_TRANSFER.FirstOrDefault(m => m.account_to == model.ID && !m.deleted);
                    if (_transTo != null)
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
