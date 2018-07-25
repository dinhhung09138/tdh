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
    public class MoneyFlowService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator/Services/MoneyFlowService.cs";

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
                DataTableResponse<FlowModel> _itemResponse = new DataTableResponse<FlowModel>();
                //List of data
                List<FlowModel> _list = new List<FlowModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _lData = (from m in context.V_MONEY_FLOW
                                  where request.Parameter1 == (request.Parameter1.Length == 0 ? request.Parameter1 : m.type.ToString())
                                  orderby m.date descending
                                  select new
                                  {
                                      m.id,
                                      m.title,
                                      m.from_name,
                                      m.to_name,
                                      m.category_name,
                                      m.date,
                                      m.money,
                                      m.fee,
                                      m.type
                                  }).ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.title.ToLower().Contains(searchValue) ||
                                                   m.from_name.ToLower().Contains(searchValue) ||
                                                   m.to_name.ToLower().Contains(searchValue) ||
                                                   m.category_name.ToString().Contains(searchValue) ||
                                                   m.money.ToString().Contains(searchValue) ||
                                                   m.fee.ToString().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    foreach (var item in _lData)
                    {
                        _list.Add(new FlowModel()
                        {
                            ID = item.id,
                            Title = item.title,
                            FromName = item.from_name,
                            ToName = item.to_name,
                            CategoryName = item.category_name,
                            DateString = item.date.Value.DateToString(),
                            Money = item.money.NumberToString(),
                            Fee = item.fee.NumberToString(),
                            Type = item.type,
                            TypeName = item.type == -1 ? "Chi tiêu" : item.type == 0 ? "Chuyển khoản" : "Thu nhập"
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
        public ResponseStatusCodeHelper SaveIncome(IncomeModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            decimal _currentYearMonth = decimal.Parse(DateTime.Now.DateToString("yyyyMM"));
                            //
                            MN_INCOME _md = new MN_INCOME();
                            _md.id = Guid.NewGuid();
                            _md.account_id = model.AccountID;
                            _md.category_id = model.CategoryID;
                            _md.title = model.Title;
                            _md.date = model.Date;
                            _md.purpose = model.Purpose;
                            _md.notes = model.Notes;
                            _md.money = model.Money;
                            _md.create_by = model.CreateBy;
                            _md.create_date = DateTime.Now;
                            context.MN_INCOME.Add(_md);
                            context.Entry(_md).State = System.Data.Entity.EntityState.Added;
                            context.SaveChanges();
                            //
                            decimal _yearMonth = decimal.Parse(model.Date.DateToString("yyyyMM"));

                            #region " [ Category ] "

                            MN_CATEGORY _cateMd = context.MN_CATEGORY.FirstOrDefault(m => m.id == model.CategoryID);
                            MN_CATEGORY_SETTING _cateSettingMd = context.MN_CATEGORY_SETTING.FirstOrDefault(m => m.category_id == model.CategoryID && m.year_month == _yearMonth);
                            //Category
                            if(_yearMonth == _currentYearMonth)
                            {
                                _cateMd.money_current += model.Money;
                                _cateMd.update_by = model.UpdateBy;
                                _cateMd.update_date = DateTime.Now;
                                context.MN_CATEGORY.Attach(_cateMd);
                                context.Entry(_cateMd).State = System.Data.Entity.EntityState.Modified;
                            }
                            //Category setting
                            if(_cateSettingMd == null)
                            {
                                _cateSettingMd = new MN_CATEGORY_SETTING()
                                {
                                    id = Guid.NewGuid(),
                                    category_id = model.CategoryID,
                                    percent_setting = 0,
                                    percent_current = 0,
                                    money_setting = 0,
                                    money_current = model.Money,
                                    year_month = _yearMonth,
                                    create_by = model.CreateBy,
                                    create_date = DateTime.Now
                                };
                                context.MN_CATEGORY_SETTING.Add(_cateSettingMd);
                                context.Entry(_cateSettingMd).State = System.Data.Entity.EntityState.Added;
                            }
                            else
                            {
                                _cateSettingMd.money_current += model.Money;
                                _cateSettingMd.update_by = model.UpdateBy;
                                _cateSettingMd.update_date = DateTime.Now;
                                context.MN_CATEGORY_SETTING.Attach(_cateSettingMd);
                                context.Entry(_cateSettingMd).State = System.Data.Entity.EntityState.Modified;
                            }
                            context.SaveChanges();

                            #endregion

                            #region " [ Group ] "

                            MN_GROUP _groupMd = context.MN_GROUP.FirstOrDefault(m => m.id == _cateMd.group_id);
                            MN_GROUP_SETTING _groupSettingMd = context.MN_GROUP_SETTING.FirstOrDefault(m => m.group_id == _groupMd.id && m.year_month == _yearMonth);
                            //Group
                            if (_yearMonth == _currentYearMonth)
                            {
                                _groupMd.money_current += model.Money;
                                _groupMd.update_by = model.UpdateBy;
                                _groupMd.update_date = DateTime.Now;
                                context.MN_GROUP.Attach(_groupMd);
                                context.Entry(_groupMd).State = System.Data.Entity.EntityState.Modified;
                            }
                            //Group setting
                            if (_groupSettingMd == null)
                            {
                                _groupSettingMd = new MN_GROUP_SETTING()
                                {
                                    id = Guid.NewGuid(),
                                    group_id = _groupMd.id,
                                    percent_setting = 0,
                                    percent_current = 0,
                                    money_setting = 0,
                                    money_current = model.Money,
                                    year_month = _yearMonth,
                                    create_by = model.CreateBy,
                                    create_date = DateTime.Now
                                };
                                context.MN_GROUP_SETTING.Add(_groupSettingMd);
                                context.Entry(_groupSettingMd).State = System.Data.Entity.EntityState.Added;
                            }
                            else
                            {
                                _groupSettingMd.money_current += model.Money;
                                _groupSettingMd.update_by = model.UpdateBy;
                                _groupSettingMd.update_date = DateTime.Now;
                                context.MN_GROUP_SETTING.Attach(_groupSettingMd);
                                context.Entry(_groupSettingMd).State = System.Data.Entity.EntityState.Modified;
                            }
                            context.SaveChanges();

                            #endregion

                            #region " [ Account ] "

                            MN_ACCOUNT _accMd = context.MN_ACCOUNT.FirstOrDefault(m => m.id == model.AccountID);
                            MN_ACCOUNT_SETTING _accSettingMd = context.MN_ACCOUNT_SETTING.FirstOrDefault(m => m.account_id == model.AccountID && m.yearmonth == _yearMonth);
                            //
                            _accMd.input += model.Money;
                            _accMd.update_by = model.UpdateBy;
                            _accMd.update_date = DateTime.Now;
                            context.MN_ACCOUNT.Attach(_accMd);
                            context.Entry(_accMd).State = System.Data.Entity.EntityState.Modified;
                            //
                            if(_accSettingMd == null)
                            {
                                _accSettingMd = new MN_ACCOUNT_SETTING()
                                {
                                    id = Guid.NewGuid(),
                                    account_id  = model.AccountID,
                                    yearmonth = _yearMonth,
                                    input = model.Money,
                                    output = 0,
                                    start = 0,
                                    end = 0,
                                    create_by = model.CreateBy,
                                    create_date = DateTime.Now
                                };
                                context.MN_ACCOUNT_SETTING.Add(_accSettingMd);
                                context.Entry(_accSettingMd).State = System.Data.Entity.EntityState.Added;
                            }
                            else
                            {
                                _accSettingMd.input += model.Money;
                                _accSettingMd.update_by = model.UpdateBy;
                                _accSettingMd.update_date = DateTime.Now;
                                context.MN_ACCOUNT_SETTING.Attach(_accSettingMd);
                                context.Entry(_accSettingMd).State = System.Data.Entity.EntityState.Modified;
                            }
                            //
                            context.SaveChanges();

                            #endregion

                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            Notifier.Notification(model.CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                            trans.Rollback();
                            TDH.Services.Log.WriteLog(FILE_NAME, "SaveIncome", model.CreateBy, ex);
                            throw new ApplicationException();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "SaveIncome", model.CreateBy, ex);
                throw new ApplicationException();
            }
            Notifier.Notification(model.CreateBy, Resources.Message.InsertSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper SavePayment(PaymentModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            decimal _currentYearMonth = decimal.Parse(DateTime.Now.DateToString("yyyyMM"));
                            //
                            MN_PAYMENT _md = new MN_PAYMENT();
                            _md.id = Guid.NewGuid();
                            _md.account_id = model.AccountID;
                            _md.category_id = model.CategoryID;
                            _md.title = model.Title;
                            _md.date = model.Date;
                            _md.purpose = model.Purpose;
                            _md.notes = model.Notes;
                            _md.money = model.Money;
                            _md.create_by = model.CreateBy;
                            _md.create_date = DateTime.Now;
                            context.MN_PAYMENT.Add(_md);
                            context.Entry(_md).State = System.Data.Entity.EntityState.Added;
                            context.SaveChanges();
                            //
                            decimal _yearMonth = decimal.Parse(model.Date.DateToString("yyyyMM"));

                            #region " [ Category ] "

                            MN_CATEGORY _cateMd = context.MN_CATEGORY.FirstOrDefault(m => m.id == model.CategoryID);
                            MN_CATEGORY_SETTING _cateSettingMd = context.MN_CATEGORY_SETTING.FirstOrDefault(m => m.category_id == model.CategoryID && m.year_month == _yearMonth);
                            //Category
                            if (_yearMonth == _currentYearMonth)
                            {
                                _cateMd.money_current += model.Money;
                                _cateMd.update_by = model.UpdateBy;
                                _cateMd.update_date = DateTime.Now;
                                context.MN_CATEGORY.Attach(_cateMd);
                                context.Entry(_cateMd).State = System.Data.Entity.EntityState.Modified;
                            }
                            //Category setting
                            if (_cateSettingMd == null)
                            {
                                _cateSettingMd = new MN_CATEGORY_SETTING()
                                {
                                    id = Guid.NewGuid(),
                                    category_id = model.CategoryID,
                                    percent_setting = 0,
                                    percent_current = 0,
                                    money_setting = 0,
                                    money_current = model.Money,
                                    year_month = _yearMonth,
                                    create_by = model.CreateBy,
                                    create_date = DateTime.Now
                                };
                                context.MN_CATEGORY_SETTING.Add(_cateSettingMd);
                                context.Entry(_cateSettingMd).State = System.Data.Entity.EntityState.Added;
                            }
                            else
                            {
                                _cateSettingMd.money_current += model.Money;
                                _cateSettingMd.update_by = model.UpdateBy;
                                _cateSettingMd.update_date = DateTime.Now;
                                context.MN_CATEGORY_SETTING.Attach(_cateSettingMd);
                                context.Entry(_cateSettingMd).State = System.Data.Entity.EntityState.Modified;
                            }
                            context.SaveChanges();

                            #endregion

                            #region " [ Group ] "

                            MN_GROUP _groupMd = context.MN_GROUP.FirstOrDefault(m => m.id == _cateMd.group_id);
                            MN_GROUP_SETTING _groupSettingMd = context.MN_GROUP_SETTING.FirstOrDefault(m => m.group_id == _groupMd.id && m.year_month == _yearMonth);
                            //Group
                            if (_yearMonth == _currentYearMonth)
                            {
                                _groupMd.money_current += model.Money;
                                _groupMd.update_by = model.UpdateBy;
                                _groupMd.update_date = DateTime.Now;
                                context.MN_GROUP.Attach(_groupMd);
                                context.Entry(_groupMd).State = System.Data.Entity.EntityState.Modified;
                            }
                            //Group setting
                            if (_groupSettingMd == null)
                            {
                                _groupSettingMd = new MN_GROUP_SETTING()
                                {
                                    id = Guid.NewGuid(),
                                    group_id = _groupMd.id,
                                    percent_setting = 0,
                                    percent_current = 0,
                                    money_setting = 0,
                                    money_current = model.Money,
                                    year_month = _yearMonth,
                                    create_by = model.CreateBy,
                                    create_date = DateTime.Now
                                };
                                context.MN_GROUP_SETTING.Add(_groupSettingMd);
                                context.Entry(_groupSettingMd).State = System.Data.Entity.EntityState.Added;
                            }
                            else
                            {
                                _groupSettingMd.money_current += model.Money;
                                _groupSettingMd.update_by = model.UpdateBy;
                                _groupSettingMd.update_date = DateTime.Now;
                                context.MN_GROUP_SETTING.Attach(_groupSettingMd);
                                context.Entry(_groupSettingMd).State = System.Data.Entity.EntityState.Modified;
                            }
                            context.SaveChanges();

                            #endregion

                            #region " [ Account ] "

                            MN_ACCOUNT _accMd = context.MN_ACCOUNT.FirstOrDefault(m => m.id == model.AccountID);
                            MN_ACCOUNT_SETTING _accSettingMd = context.MN_ACCOUNT_SETTING.FirstOrDefault(m => m.account_id == model.AccountID && m.yearmonth == _yearMonth);
                            //
                            _accMd.output += model.Money;
                            _accMd.update_by = model.UpdateBy;
                            _accMd.update_date = DateTime.Now;
                            context.MN_ACCOUNT.Attach(_accMd);
                            context.Entry(_accMd).State = System.Data.Entity.EntityState.Modified;
                            //
                            if (_accSettingMd == null)
                            {
                                _accSettingMd = new MN_ACCOUNT_SETTING()
                                {
                                    id = Guid.NewGuid(),
                                    account_id = model.AccountID,
                                    yearmonth = _yearMonth,
                                    input = 0,
                                    output = model.Money,
                                    start = 0,
                                    end = 0,
                                    create_by = model.CreateBy,
                                    create_date = DateTime.Now
                                };
                                context.MN_ACCOUNT_SETTING.Add(_accSettingMd);
                                context.Entry(_accSettingMd).State = System.Data.Entity.EntityState.Added;
                            }
                            else
                            {
                                _accSettingMd.output += model.Money;
                                _accSettingMd.update_by = model.UpdateBy;
                                _accSettingMd.update_date = DateTime.Now;
                                context.MN_ACCOUNT_SETTING.Attach(_accSettingMd);
                                context.Entry(_accSettingMd).State = System.Data.Entity.EntityState.Modified;
                            }
                            //
                            context.SaveChanges();

                            #endregion

                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            Notifier.Notification(model.CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                            trans.Rollback();
                            TDH.Services.Log.WriteLog(FILE_NAME, "SavePayment", model.CreateBy, ex);
                            throw new ApplicationException();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "SavePayment", model.CreateBy, ex);
                throw new ApplicationException();
            }
            Notifier.Notification(model.CreateBy, Resources.Message.InsertSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper SaveTransfer(TransferModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            decimal _currentYearMonth = decimal.Parse(DateTime.Now.DateToString("yyyyMM"));
                            //
                            MN_TRANSFER _md = new MN_TRANSFER();
                            _md.id = Guid.NewGuid();
                            _md.account_from = model.AccountFrom;
                            _md.account_to = model.AccountTo;
                            _md.title = model.Title;
                            _md.date = model.Date;
                            _md.purpose = model.Purpose;
                            _md.notes = model.Notes;
                            _md.money = model.Money;
                            _md.fee = model.Fee;
                            _md.create_by = model.CreateBy;
                            _md.create_date = DateTime.Now;
                            context.MN_TRANSFER.Add(_md);
                            context.Entry(_md).State = System.Data.Entity.EntityState.Added;
                            context.SaveChanges();
                            //
                            decimal _yearMonth = decimal.Parse(model.Date.DateToString("yyyyMM"));

                            #region " [ Account ] "

                            MN_ACCOUNT _accFromMd = context.MN_ACCOUNT.FirstOrDefault(m => m.id == model.AccountFrom);
                            MN_ACCOUNT _accToMd = context.MN_ACCOUNT.FirstOrDefault(m => m.id == model.AccountTo);
                            MN_ACCOUNT_SETTING _accFromSettingMd = context.MN_ACCOUNT_SETTING.FirstOrDefault(m => m.account_id == model.AccountFrom && m.yearmonth == _yearMonth);
                            MN_ACCOUNT_SETTING _accToSettingMd = context.MN_ACCOUNT_SETTING.FirstOrDefault(m => m.account_id == model.AccountTo && m.yearmonth == _yearMonth);
                            //
                            _accFromMd.output += model.Money;
                            _accFromMd.update_by = model.UpdateBy;
                            _accFromMd.update_date = DateTime.Now;
                            context.MN_ACCOUNT.Attach(_accFromMd);
                            context.Entry(_accFromMd).State = System.Data.Entity.EntityState.Modified;
                            //
                            _accToMd.input += model.Money;
                            _accToMd.update_by = model.UpdateBy;
                            _accToMd.update_date = DateTime.Now;
                            context.MN_ACCOUNT.Attach(_accToMd);
                            context.Entry(_accToMd).State = System.Data.Entity.EntityState.Modified;
                            //
                            if (_accFromSettingMd == null)
                            {
                                _accFromSettingMd = new MN_ACCOUNT_SETTING()
                                {
                                    id = Guid.NewGuid(),
                                    account_id = model.AccountFrom,
                                    yearmonth = _yearMonth,
                                    input = 0,
                                    output = model.Money,
                                    start = 0,
                                    end = 0,
                                    create_by = model.CreateBy,
                                    create_date = DateTime.Now
                                };
                                context.MN_ACCOUNT_SETTING.Add(_accFromSettingMd);
                                context.Entry(_accFromSettingMd).State = System.Data.Entity.EntityState.Added;
                            }
                            else
                            {
                                _accFromSettingMd.output += model.Money;
                                _accFromSettingMd.update_by = model.UpdateBy;
                                _accFromSettingMd.update_date = DateTime.Now;
                                context.MN_ACCOUNT_SETTING.Attach(_accFromSettingMd);
                                context.Entry(_accFromSettingMd).State = System.Data.Entity.EntityState.Modified;
                            }
                            //
                            if (_accToSettingMd == null)
                            {
                                _accToSettingMd = new MN_ACCOUNT_SETTING()
                                {
                                    id = Guid.NewGuid(),
                                    account_id = model.AccountTo,
                                    yearmonth = _yearMonth,
                                    input = model.Money,
                                    output = 0,
                                    start = 0,
                                    end = 0,
                                    create_by = model.CreateBy,
                                    create_date = DateTime.Now
                                };
                                context.MN_ACCOUNT_SETTING.Add(_accToSettingMd);
                                context.Entry(_accToSettingMd).State = System.Data.Entity.EntityState.Added;
                            }
                            else
                            {
                                _accToSettingMd.output += model.Money;
                                _accToSettingMd.update_by = model.UpdateBy;
                                _accToSettingMd.update_date = DateTime.Now;
                                context.MN_ACCOUNT_SETTING.Attach(_accToSettingMd);
                                context.Entry(_accToSettingMd).State = System.Data.Entity.EntityState.Modified;
                            }
                            //
                            context.SaveChanges();

                            #endregion


                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            Notifier.Notification(model.CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                            trans.Rollback();
                            TDH.Services.Log.WriteLog(FILE_NAME, "SaveTransfer", model.CreateBy, ex);
                            throw new ApplicationException();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "SavePayment", model.CreateBy, ex);
                throw new ApplicationException();
            }
            Notifier.Notification(model.CreateBy, Resources.Message.InsertSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }



    }
}