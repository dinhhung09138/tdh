using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TDH.Common;
using TDH.Common.UserException;
using TDH.DataAccess;
using TDH.Model.Money;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Services.Money
{
    /// <summary>
    /// Flow service
    /// </summary>
    public class FlowService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.Money/FlowService.cs";

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
                int _year = int.Parse(request.Parameter2) / 100;
                int _month = int.Parse(request.Parameter2) % 100;
                DateTime _from = new DateTime(_year, _month, 1, 0, 0, 0);
                DateTime _to = _from.AddMonths(1).AddDays(-1);
                //Declare response data to json object
                DataTableResponse<FlowModel> _itemResponse = new DataTableResponse<FlowModel>();
                //List of data
                List<FlowModel> _list = new List<FlowModel>();
                using (var _context = new TDHEntities())
                {
                    var _lData = (from m in _context.V_MN_MONEY_FLOW
                                  where m.create_by == userID && request.Parameter1 == (request.Parameter1.Length == 0 ? request.Parameter1 : m.type.ToString()) && m.date.Value >= _from && m.date.Value <= _to
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
                throw new ServiceException(FILE_NAME, "List", userID, ex);
            }
            return _return;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model">Income model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper SaveIncome(IncomeModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    using (var trans = _context.Database.BeginTransaction())
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
                            _context.MN_INCOME.Add(_md);
                            _context.Entry(_md).State = EntityState.Added;
                            _context.SaveChanges();
                            //MN_ACCOUNT
                            MN_ACCOUNT _acc = _context.MN_ACCOUNT.FirstOrDefault(m => m.id == _md.account_id);
                            _acc.input += model.Money;
                            _context.MN_ACCOUNT.Attach(_acc);
                            _context.Entry(_acc).State = EntityState.Modified;
                            _context.SaveChanges();

                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw new ServiceException(FILE_NAME, "SaveIncome", model.CreateBy, ex);
                        }
                    }
                }
            }
            catch (ServiceException serviceEx)
            {
                throw serviceEx;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "SaveIncome", model.CreateBy, ex);
            }
            Notifier.Notification(model.CreateBy, Message.InsertSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model">Payment model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper SavePayment(PaymentModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    using (var trans = _context.Database.BeginTransaction())
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
                            _context.MN_PAYMENT.Add(_md);
                            _context.Entry(_md).State = EntityState.Added;
                            _context.SaveChanges();
                            //MN_ACCOUNT
                            MN_ACCOUNT _acc = _context.MN_ACCOUNT.FirstOrDefault(m => m.id == _md.account_id);
                            _acc.output += model.Money;
                            _context.MN_ACCOUNT.Attach(_acc);
                            _context.Entry(_acc).State = EntityState.Modified;
                            _context.SaveChanges();

                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw new ServiceException(FILE_NAME, "SavePayment", model.CreateBy, ex);
                        }
                    }
                }
            }
            catch (ServiceException serviceEx)
            {
                throw serviceEx;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "SavePayment", model.CreateBy, ex);
            }
            Notifier.Notification(model.CreateBy, Message.InsertSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model">Transfer model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper SaveTransfer(TransferModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    using (var trans = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            decimal _currentYearMonth = decimal.Parse(DateTime.Now.DateToString("yyyyMM"));
                            
                            MN_TRANSFER _md = new MN_TRANSFER();
                            _md.id = Guid.NewGuid();
                            _md.category_id = model.CategoryID;
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
                            _context.MN_TRANSFER.Add(_md);
                            _context.Entry(_md).State = EntityState.Added;
                            _context.SaveChanges();
                            //MN_ACCOUNT receive 
                            MN_ACCOUNT _accReceive = _context.MN_ACCOUNT.FirstOrDefault(m => m.id == _md.account_to);
                            _accReceive.input += model.Money;
                            _context.MN_ACCOUNT.Attach(_accReceive);
                            _context.Entry(_accReceive).State = EntityState.Modified;
                            _context.SaveChanges();
                            //MN_ACCOUNT spend
                            MN_ACCOUNT _accSpend = _context.MN_ACCOUNT.FirstOrDefault(m => m.id == _md.account_from);
                            _accSpend.input -= (model.Money + model.Fee);
                            _context.MN_ACCOUNT.Attach(_accSpend);
                            _context.Entry(_accSpend).State = EntityState.Modified;
                            _context.SaveChanges();
                            //
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw new ServiceException(FILE_NAME, "SaveTransfer", model.CreateBy, ex);
                        }
                    }
                }
            }
            catch (ServiceException serviceEx)
            {
                throw serviceEx;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "SaveTransfer", model.CreateBy, ex);
            }
            Notifier.Notification(model.CreateBy, Message.InsertSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

    }
}
