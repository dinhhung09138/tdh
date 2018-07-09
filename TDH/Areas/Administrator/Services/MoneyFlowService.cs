using System;
using System.Collections.Generic;
using System.Linq;
using TDH.Models;
using Utils.JqueryDatatable;
using TDH.Areas.Administrator.Models;
using Utils;
using TDH.Areas.Administrator.Common;

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
                DataTableResponse<MoneyFlowModel> _itemResponse = new DataTableResponse<MoneyFlowModel>();
                //List of data
                List<MoneyFlowModel> _list = new List<MoneyFlowModel>();
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
                        _list.Add(new MoneyFlowModel()
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
        public ResponseStatusCodeHelper SaveIncome(MoneyIncomeModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
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
        public ResponseStatusCodeHelper SavePayment(MoneyPaymentModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
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
        public ResponseStatusCodeHelper SaveTransfer(MoneyTransferModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
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