﻿using System;
using System.Collections.Generic;
using System.Linq;
using Utils;
using TDH.Common;
using TDH.DataAccess;
using TDH.Model.Money;

namespace TDH.Services.Money
{
    /// <summary>
    /// Group of money service
    /// </summary>
    public class GroupSettingService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services/GroupSettingService.cs";

        #endregion

        /// <summary>
        /// Get all item without deleted
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<GroupSettingModel></returns>
        public List<GroupSettingModel> GetAll(Guid userID)
        {
            try
            {
                List<GroupSettingModel> _return = new List<GroupSettingModel>();
                using (var context = new TDHEntities())
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
                        _return.Add(new GroupSettingModel()
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
                Notifier.Notification(userID, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "GetAll", userID, ex);
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// Get all item without deleted. By group in a year
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <param name="groupID">The group identifier</param>
        /// <returns>List<GroupSettingModel></returns>
        public List<GroupSettingModel> GetAll(Guid userID, Guid groupID)
        {
            try
            {
                List<GroupSettingModel> _return = new List<GroupSettingModel>();
                using (var context = new TDHEntities())
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
                        _return.Add(new GroupSettingModel()
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
                Notifier.Notification(userID, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "GetAll", userID, ex);
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// Get all item without deleted. By group in a month
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <param name="groupID">The group identifier</param>
        /// <param name="yearMonth">Year month</param>
        /// <returns>List<GroupSettingModel></returns>
        public List<GroupSettingModel> GetAll(Guid userID, Guid groupID, decimal yearMonth)
        {
            try
            {
                List<GroupSettingModel> _return = new List<GroupSettingModel>();
                using (var context = new TDHEntities())
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
                        _return.Add(new GroupSettingModel()
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
                Notifier.Notification(userID, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "GetAll", userID, ex);
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// Get all item without deleted. All group in a month
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <param name="yearMonth">Year month</param>
        /// <returns>List<GroupSettingModel></returns>
        public List<GroupSettingModel> GetAll(Guid userID, decimal yearMonth)
        {
            try
            {
                List<GroupSettingModel> _return = new List<GroupSettingModel>();
                using (var context = new TDHEntities())
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
                        _return.Add(new GroupSettingModel()
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
                Notifier.Notification(userID, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "GetAllGroupByMonth", userID, ex);
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="model">Group setting model</param>
        /// <returns>CategoryModel. Throw exception if not found or get some error</returns>
        public GroupSettingModel GetItemByID(GroupSettingModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    MN_GROUP_SETTING _md = context.MN_GROUP_SETTING.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new FieldAccessException();
                    }
                    var _group = context.MN_GROUP.FirstOrDefault(m => m.id == _md.group_id);
                    return new GroupSettingModel()
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
                Notifier.Notification(model.CreateBy, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "GetItemByID", model.CreateBy, ex);
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model">Group setting model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(List<GroupSettingModel> model)
        {
            try
            {
                using (var context = new TDHEntities())
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
                                if (_md.year_month % 100 == DateTime.Now.Month)
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
                            Notifier.Notification(model[0].CreateBy, Message.Error, Notifier.TYPE.Error);
                            trans.Rollback();
                            Log.WriteLog(FILE_NAME, "Save", model[0].CreateBy, ex);
                            throw new ApplicationException();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model[0].CreateBy, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "Save", model[0].CreateBy, ex);
                throw new ApplicationException();
            }
            Notifier.Notification(model[0].CreateBy, Message.UpdateSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Save all group by month
        /// </summary>
        /// <param name="yearMonth">year month</param>
        /// <param name="userID">The user identifier</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(decimal yearMonth, Guid userID)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var _list = context.MN_GROUP.Where(m => !m.deleted);
                            foreach (var item in _list)
                            {
                                //Check if exists
                                if (context.MN_GROUP_SETTING.FirstOrDefault(m => m.group_id == item.id && m.year_month == yearMonth) != null)
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
                            Notifier.Notification(userID, Message.Error, Notifier.TYPE.Error);
                            trans.Rollback();
                            Log.WriteLog(FILE_NAME, "SaveAllGroupByMonth", userID, ex);
                            throw new ApplicationException();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(userID, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "SaveAllGroupByMonth", userID, ex);
                throw new ApplicationException();
            }
            return ResponseStatusCodeHelper.Success;
        }

    }
}