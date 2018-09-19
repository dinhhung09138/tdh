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
    /// Category service
    /// </summary>
    public class CategoryService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.Money/CategoryService.cs";

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
                int _year = int.Parse(request.Parameter2.Substring(0, 4));
                int _month = int.Parse(request.Parameter2.Substring(4, 2));
                //Declare response data to json object
                DataTableResponse<CategoryModel> _itemResponse = new DataTableResponse<CategoryModel>();
                //List of data
                List<CategoryModel> _list = new List<CategoryModel>();
                using (var _context = new TDHEntities())
                {
                    var _lData = (from m in _context.V_MN_CATEGORY
                                  where m.create_by == userID && request.Parameter1 == (request.Parameter1.Length == 0 ? request.Parameter1 : m.group_id.ToString()) &&
                                        (m.year == 0 || m.year == _year) && (m.month == 0 || m.month == _month)
                                  select new
                                  {
                                      m.id,
                                      m.name,
                                      m.notes,
                                      m.is_input,
                                      m.group_name,
                                      m.current_income,
                                      m.current_payment,
                                      m.money_setting
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
                    foreach (var item in _lData)
                    {
                        _list.Add(new CategoryModel()
                        {
                            ID = item.id,
                            Name = item.name,
                            GroupName = item.group_name,
                            Notes = item.notes,
                            MoneyCurrent = item.is_input ? item.current_income : item.current_payment,
                            MoneyCurrentString = (item.is_input ? item.current_income : item.current_payment).NumberToString(),
                            MoneySetting = item.money_setting,
                            MoneySettingString = item.money_setting.NumberToString(),
                            IsIncome = item.is_input
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
                throw new ServiceException(FILE_NAME, "List", userID, ex);
            }
            return _return;
        }

        /// <summary>
        /// Get all item without deleted
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<CategoryModel></returns>
        public List<CategoryModel> GetAll(Guid userID)
        {
            try
            {
                List<CategoryModel> _return = new List<CategoryModel>();
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.MN_CATEGORY
                                 join n in _context.MN_GROUP on m.group_id equals n.id
                                 where n.publish && !n.deleted && !m.deleted && m.create_by == userID
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
                throw new ServiceException(FILE_NAME, "GetAll", userID, ex);
            }
        }

        /// <summary>
        /// Get all item without deleted
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <param name="isInput">true: Income, false: Payment</param>
        /// <returns>List<CategoryModel></returns>
        public List<CategoryModel> GetAll(Guid userID, bool isInput)
        {
            try
            {
                List<CategoryModel> _return = new List<CategoryModel>();
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.MN_CATEGORY
                                 join n in _context.MN_GROUP on m.group_id equals n.id
                                 where n.publish && !n.deleted && !m.deleted && n.is_input == isInput && m.create_by == userID
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
                throw new ServiceException(FILE_NAME, "GetAll", userID, ex);
            }
        }

        /// <summary>
        /// Check income, payment history by month, and category id and by type (payment or income)
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
                DataTableResponse<CategoryHistoryModel> _itemResponse = new DataTableResponse<CategoryHistoryModel>();
                //List of data
                List<CategoryHistoryModel> _list = new List<CategoryHistoryModel>();
                using (var _context = new TDHEntities())
                {
                    var _lData = (from m in _context.V_MN_CATEGORY_HISTORY
                                  where m.create_by == userID && request.Parameter2 == (request.Parameter2.Length == 0 ? request.Parameter2 : m.category_id.ToString()) //By account id
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
                throw new ServiceException(FILE_NAME, "GetHistory", userID, ex);
            }
            return _return;
        }

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="model">Category model</param>
        /// <returns>MoneyCategoryModel. Throw exception if not found or get some error</returns>
        public CategoryModel GetItemByID(CategoryModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    List<V_MN_CATEGORY> _list = _context.V_MN_CATEGORY.Where(m => m.id == model.ID && m.create_by == model.CreateBy).ToList();
                    if (_list == null && _list.Count() == 0)
                    {
                        throw new DataAccessException(FILE_NAME, "GetItemByID", model.CreateBy);
                    }

                    CategoryModel _return = new CategoryModel()
                    {
                        ID = _list[0].id,
                        Name = _list[0].name,
                        Notes = _list[0].notes,
                        GroupID = _list[0].group_id,
                        GroupName = _list[0].group_name,
                        MoneyCurrent = _list[0].money_current,
                        MoneySetting = _list[0].money_setting,
                        Ordering = _list[0].ordering,
                        Publish = _list[0].publish
                    };

                    if (_list.Count > 1)
                    {
                        foreach (var item in _list)
                        {
                            _return.Setting.Add(new CategorySettingModel()
                            {
                                Month = item.month.Value,
                                Year = item.year.Value,
                                MoneySetting = item.money_setting,
                                MoneyCurrent = item.is_input ? item.current_income : item.current_payment,
                                YearMonthString = item.year_month.ToString()
                            });
                        }
                    }

                    return _return;
                }
            }
            catch (DataAccessException fieldEx)
            {
                throw fieldEx;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "GetItemByID", model.CreateBy, ex);
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model">Category model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(CategoryModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    using (var trans = _context.Database.BeginTransaction())
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
                                _md = _context.MN_CATEGORY.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.create_by == model.CreateBy);
                                if (_md == null)
                                {
                                    throw new DataAccessException(FILE_NAME, "Save", model.CreateBy);
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
                                _context.MN_CATEGORY.Add(_md);
                                _context.Entry(_md).State = EntityState.Added;
                            }
                            else
                            {
                                _md.update_by = model.UpdateBy;
                                _md.update_date = DateTime.Now;
                                _context.MN_CATEGORY.Attach(_md);
                                _context.Entry(_md).State = EntityState.Modified;
                            }
                            _context.SaveChanges();
                            trans.Commit();
                        }
                        catch (DataAccessException fieldEx)
                        {
                            trans.Rollback();
                            throw fieldEx;
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (DataAccessException fieldEx)
            {
                throw fieldEx;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "Save", model.CreateBy, ex);
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
        /// <param name="model">Category model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Publish(CategoryModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    MN_CATEGORY _md = _context.MN_CATEGORY.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.create_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "Publish", model.CreateBy);
                    }
                    _md.publish = model.Publish;
                    _md.update_by = model.UpdateBy;
                    _md.update_date = DateTime.Now;
                    _context.MN_CATEGORY.Attach(_md);
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
                throw new ServiceException(FILE_NAME, "Publish", model.CreateBy, ex);
            }
            Notifier.Notification(model.CreateBy, Message.UpdateSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model">Category model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(CategoryModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    MN_CATEGORY _md = _context.MN_CATEGORY.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.create_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "Delete", model.CreateBy);
                    }
                    _md.deleted = true;
                    _md.delete_by = model.DeleteBy;
                    _md.delete_date = DateTime.Now;
                    _context.MN_CATEGORY.Attach(_md);
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
                throw new ServiceException(FILE_NAME, "Delete", model.CreateBy, ex);
            }
            Notifier.Notification(model.CreateBy, Message.DeleteSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Check Delete
        /// </summary>
        /// <param name="model">Category model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper CheckDelete(CategoryModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {

                    MN_CATEGORY _md = _context.MN_CATEGORY.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.create_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "CheckDelete", model.CreateBy);
                    }
                    var _payment = _context.MN_PAYMENT.FirstOrDefault(m => m.category_id == model.ID && !m.deleted && m.create_by == model.CreateBy);
                    if (_payment != null)
                    {
                        Notifier.Notification(model.CreateBy, Message.CheckExists, Notifier.TYPE.Warning);
                        return ResponseStatusCodeHelper.NG;
                    }
                    var _income = _context.MN_INCOME.FirstOrDefault(m => m.category_id == model.ID && !m.deleted && m.create_by == model.CreateBy);
                    if (_income != null)
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
                throw new ServiceException(FILE_NAME, "CheckDelete", model.CreateBy, ex);
            }
            return ResponseStatusCodeHelper.OK;
        }
    }
}
