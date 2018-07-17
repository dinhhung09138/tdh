﻿using System;
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
                    Save(yearMonth, userID);
                    //
                    var _list = (from m in context.MN_GROUP_SETTING
                                 join n in context.MN_GROUP on m.group_id equals n.id
                                 where !m.deleted && m.year_month == yearMonth && !n.deleted && !n.is_input
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
                                //Check if exists
                                if(context.MN_GROUP_SETTING.FirstOrDefault(m => m.group_id == item.id && m.year_month == yearMonth) != null)
                                {
                                    continue;
                                }
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