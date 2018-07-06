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
    public class MoneyGroupSettingService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator/Services/MoneyGroupSettingService.cs";

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
                    var _lData = (from m in context.CATEGORies
                                  join n in context.NAVIGATIONs on m.navigation_id equals n.id
                                  where !n.deleted && !m.deleted && request.Parameter1 == (request.Parameter1.Length == 0 ? request.Parameter1 : m.navigation_id.ToString())
                                  select new
                                  {
                                      m.id,
                                      m.title,
                                      m.description,
                                      m.image,
                                      m.show_on_nav,
                                      nav_title = n.title,
                                      m.ordering,
                                      m.publish
                                  }).ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.title.ToLower().Contains(searchValue) ||
                                                   m.nav_title.ToLower().Contains(searchValue) ||
                                                   m.description.ToLower().Contains(searchValue) ||
                                                   m.ordering.ToString().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    int _count = 0;
                    foreach (var item in _lData)
                    {
                        _count = context.POSTs.Count(m => m.category_id == item.id && !m.deleted);
                        _list.Add(new CategoryModel()
                        {
                            ID = item.id,
                            Title = item.title,
                            Description = item.description,
                            ShowOnNav = item.show_on_nav,
                            Image = item.image ?? "",
                            NavigationTitle = item.nav_title,
                            Ordering = item.ordering,
                            Publish = item.publish,
                            Count = _count.NumberToString()
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
                                case "Title":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Title) : _sortList.Sort(col.Dir, m => m.Title);
                                    break;
                                case "Description":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Description) : _sortList.Sort(col.Dir, m => m.Description);
                                    break;
                                case "NavigationTitle":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.NavigationTitle) : _sortList.Sort(col.Dir, m => m.NavigationTitle);
                                    break;
                                case "Ordering":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Ordering) : _sortList.Sort(col.Dir, m => m.Ordering);
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
        /// <param name="userID">User id</param>
        /// <returns></returns>
        public List<MoneyGroupSettingModel> GetAll(Guid userID)
        {
            try
            {
                List<MoneyGroupSettingModel> _return = new List<MoneyGroupSettingModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _list = (from m in context.MN_GROUP_SETTING
                                 where !m.deleted
                                 orderby m.group_id, m.year_month ascending
                                 select new
                                 {
                                     m.id,
                                     m.group_id,
                                     m.year_month,
                                     m.percent_current,
                                     m.percent_setting,
                                     m.money_current,
                                     m.money_setting
                                 }).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new MoneyGroupSettingModel()
                        {
                            ID = item.id,
                            GroupID = item.group_id,
                            YearMonth = item.year_month,
                            PercentCurrent = item.percent_current,
                            PercentSetting = item.percent_setting,
                            MoneyCurrent = item.money_current,
                            MoneyCurrentString = item.money_current.NumberToString(),
                            MoneySetting = item.money_setting,
                            MoneySettingString = item.money_setting.NumberToString()
                        });
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
        /// Get all item without deleted. By group in a year
        /// </summary>
        /// <param name="userID">User id</param>
        /// <param name="groupID">Group id</param>
        /// <returns></returns>
        public List<MoneyGroupSettingModel> GetAll(Guid userID, Guid groupID)
        {
            try
            {
                List<MoneyGroupSettingModel> _return = new List<MoneyGroupSettingModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _list = (from m in context.MN_GROUP_SETTING
                                 where !m.deleted && m.group_id == groupID
                                 orderby m.group_id, m.year_month ascending
                                 select new
                                 {
                                     m.id,
                                     m.group_id,
                                     m.year_month,
                                     m.percent_current,
                                     m.percent_setting,
                                     m.money_current,
                                     m.money_setting
                                 }).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new MoneyGroupSettingModel()
                        {
                            ID = item.id,
                            GroupID = item.group_id,
                            YearMonth = item.year_month,
                            PercentCurrent = item.percent_current,
                            PercentSetting = item.percent_setting,
                            MoneyCurrent = item.money_current,
                            MoneyCurrentString = item.money_current.NumberToString(),
                            MoneySetting = item.money_setting,
                            MoneySettingString = item.money_setting.NumberToString()
                        });
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
        /// Get all item without deleted. By group in a month
        /// </summary>
        /// <param name="userID">User id</param>
        /// <param name="groupID">Group id</param>
        /// <param name="yearMonth">Year month</param>
        /// <returns></returns>
        public List<MoneyGroupSettingModel> GetAll(Guid userID, Guid groupID, decimal yearMonth)
        {
            try
            {
                List<MoneyGroupSettingModel> _return = new List<MoneyGroupSettingModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _list = (from m in context.MN_GROUP_SETTING
                                 where !m.deleted && m.group_id == groupID && m.year_month == yearMonth
                                 orderby m.group_id, m.year_month ascending
                                 select new
                                 {
                                     m.id,
                                     m.group_id,
                                     m.year_month,
                                     m.percent_current,
                                     m.percent_setting,
                                     m.money_current,
                                     m.money_setting
                                 }).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new MoneyGroupSettingModel()
                        {
                            ID = item.id,
                            GroupID = item.group_id,
                            YearMonth = item.year_month,
                            PercentCurrent = item.percent_current,
                            PercentSetting = item.percent_setting,
                            MoneyCurrent = item.money_current,
                            MoneyCurrentString = item.money_current.NumberToString(),
                            MoneySetting = item.money_setting,
                            MoneySettingString = item.money_setting.NumberToString()
                        });
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
        /// Get all item without deleted. All group in a month
        /// </summary>
        /// <param name="userID">User id</param>
        /// <param name="yearMonth">Year month</param>
        /// <returns></returns>
        public List<MoneyGroupSettingModel> GetAll(Guid userID, decimal yearMonth)
        {
            try
            {
                List<MoneyGroupSettingModel> _return = new List<MoneyGroupSettingModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _count = context.MN_GROUP_SETTING.Count(m => !m.deleted && m.year_month == yearMonth);
                    if(_count == 0)
                    {
                        Save(yearMonth, userID);
                    }
                    var _list = (from m in context.MN_GROUP_SETTING
                                 join n in context.MN_GROUP on m.group_id equals n.id
                                 where !m.deleted && m.year_month == yearMonth && !n.deleted && n.is_input
                                 orderby m.group_id, m.year_month ascending
                                 select new
                                 {
                                     m.id,
                                     m.group_id,
                                     n.name,
                                     m.year_month,
                                     m.percent_current,
                                     m.percent_setting,
                                     m.money_current,
                                     m.money_setting
                                 }).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new MoneyGroupSettingModel()
                        {
                            ID = item.id,
                            GroupID = item.group_id,
                            GroupName = item.name,
                            YearMonth = item.year_month,
                            Year = item.year_month / 100,
                            Month = item.year_month % 100,
                            PercentCurrent = item.percent_current,
                            PercentSetting = item.percent_setting,
                            MoneyCurrent = item.money_current,
                            MoneyCurrentString = item.money_current.NumberToString(),
                            MoneySetting = item.money_setting,
                            MoneySettingString = item.money_setting.NumberToString()
                        });
                    }
                }
                return _return;
            }
            catch (Exception ex)
            {
                Notifier.Notification(userID, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "GetAllGroupByMonth", userID, ex);
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="model"></param>
        /// <returns>CategoryModel. Throw exception if not found or get some error</returns>
        public MoneyGroupSettingModel GetItemByID(MoneyGroupSettingModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    MN_GROUP_SETTING _md = context.MN_GROUP_SETTING.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new FieldAccessException();
                    }
                    var _group = context.MN_GROUP.FirstOrDefault(m => m.id == _md.group_id);
                    return new MoneyGroupSettingModel()
                    {
                        ID = _md.id,
                        GroupID = _md.group_id,
                        GroupName = _group.name,
                        YearMonth = _md.year_month,
                        PercentCurrent = _md.percent_current,
                        PercentSetting = _md.percent_setting,
                        MoneyCurrent = _md.money_current,
                        MoneyCurrentString = _md.money_current.NumberToString(),
                        MoneySetting = _md.money_setting,
                        MoneySettingString = _md.money_setting.NumberToString()
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
        public ResponseStatusCodeHelper Save(List<MoneyGroupSettingModel> model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (var item in model)
                            {
                                MN_GROUP_SETTING _md = context.MN_GROUP_SETTING.FirstOrDefault(m => m.id == item.ID && !m.deleted);
                                if (_md == null)
                                {
                                    throw new FieldAccessException();
                                }
                                _md.percent_setting = item.PercentSetting;
                                _md.update_by = item.UpdateBy;
                                _md.update_date = DateTime.Now;
                                context.MN_GROUP_SETTING.Attach(_md);
                                context.Entry(_md).State = System.Data.Entity.EntityState.Modified;
                                //
                                if(_md.year_month % 100 == DateTime.Now.Month)
                                {
                                    var _gr = context.MN_GROUP.FirstOrDefault(m => m.id == _md.group_id);
                                    _gr.percent_setting = _md.percent_setting;
                                    context.MN_GROUP.Attach(_gr);
                                    context.Entry(_gr).State = System.Data.Entity.EntityState.Modified;
                                }
                                context.SaveChanges();
                            }                            
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            Notifier.Notification(model[0].CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                            trans.Rollback();
                            TDH.Services.Log.WriteLog(FILE_NAME, "Save", model[0].CreateBy, ex);
                            throw new ApplicationException();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model[0].CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "Save", model[0].CreateBy, ex);
                throw new ApplicationException();
            }
            Notifier.Notification(model[0].CreateBy, Resources.Message.UpdateSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Save all group by month
        /// </summary>
        /// <param name="yearMonth"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public ResponseStatusCodeHelper Save(decimal yearMonth, Guid userID)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var _list = context.MN_GROUP.Where(m => !m.deleted);
                            foreach (var item in _list)
                            {
                                MN_GROUP_SETTING _st = new MN_GROUP_SETTING()
                                {
                                    id = Guid.NewGuid(),
                                    group_id = item.id,
                                    year_month = yearMonth,
                                    percent_current = 0,
                                    percent_setting = 0,
                                    money_current = 0,
                                    money_setting = 0,
                                    create_by = userID,
                                    create_date = DateTime.Now
                                };
                                context.MN_GROUP_SETTING.Add(_st);
                                context.Entry(_st).State = System.Data.Entity.EntityState.Added;
                                context.SaveChanges();
                            }
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            Notifier.Notification(userID, Resources.Message.Error, Notifier.TYPE.Error);
                            trans.Rollback();
                            TDH.Services.Log.WriteLog(FILE_NAME, "SaveAllGroupByMonth", userID, ex);
                            throw new ApplicationException();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(userID, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "SaveAllGroupByMonth", userID, ex);
                throw new ApplicationException();
            }
            return ResponseStatusCodeHelper.Success;
        }
        
    }
}