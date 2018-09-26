using System;
using System.Collections.Generic;
using System.Linq;
using Utils;
using TDH.Common;
using TDH.DataAccess;
using TDH.Model.Money;
using System.Data.Entity;
using TDH.Common.UserException;

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
        private readonly string FILE_NAME = "Services.Money/GroupSettingService.cs";

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
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.MN_GROUP_SETTING
                                 where !m.deleted && m.create_by == userID
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
                throw new ServiceException(FILE_NAME, "GetAll", userID, ex);
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
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.MN_GROUP_SETTING
                                 where !m.deleted && m.group_id == groupID && m.create_by == userID
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
                throw new ServiceException(FILE_NAME, "GetAll", userID, ex);
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
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.MN_GROUP_SETTING
                                 where !m.deleted && m.group_id == groupID && m.year_month == yearMonth && m.create_by == userID
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
                throw new ServiceException(FILE_NAME, "GetAll", userID, ex);
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
                Save(yearMonth, userID);
                //
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.MN_GROUP_SETTING
                                 join n in _context.MN_GROUP on m.group_id equals n.id
                                 where !m.deleted && m.year_month == yearMonth && !n.deleted && !n.is_input && n.create_by == userID
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
                throw new ServiceException(FILE_NAME, "GetAll", userID, ex);
            }
        }

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="model">Group setting model</param>
        /// <returns>GroupSettingModel</returns>
        public GroupSettingModel GetItemByID(GroupSettingModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    MN_GROUP_SETTING _md = _context.MN_GROUP_SETTING.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.create_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "GetItemByID", model.CreateBy);
                    }
                    var _group = _context.MN_GROUP.FirstOrDefault(m => m.id == _md.group_id);
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
        /// <param name="model">List of group setting model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(List<GroupSettingModel> model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    using (var trans = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (var item in model)
                            {
                                MN_GROUP_SETTING _md = _context.MN_GROUP_SETTING.FirstOrDefault(m => m.id == item.ID && !m.deleted);
                                if (_md == null)
                                {
                                    throw new DataAccessException(FILE_NAME, "Save", item.CreateBy);
                                }
                                _md.percent_setting = item.PercentSetting;
                                _md.update_by = item.UpdateBy;
                                _md.update_date = DateTime.Now;
                                _context.MN_GROUP_SETTING.Attach(_md);
                                _context.Entry(_md).State = EntityState.Modified;
                                //
                                if (_md.year_month % 100 == DateTime.Now.Month)
                                {
                                    var _gr = _context.MN_GROUP.FirstOrDefault(m => m.id == _md.group_id);
                                    _gr.percent_setting = _md.percent_setting;
                                    _context.MN_GROUP.Attach(_gr);
                                    _context.Entry(_gr).State = EntityState.Modified;
                                }
                                _context.SaveChanges();
                            }
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
                throw new ServiceException(FILE_NAME, "Save", model[0].CreateBy, ex);
            }
            Notifier.Notification(model[0].CreateBy, Message.UpdateSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Save all group by month
        /// </summary>
        /// <param name="yearMonth">Year month</param>
        /// <param name="userID">The user identifier</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(decimal yearMonth, Guid userID)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    var _list = _context.MN_GROUP.Where(m => !m.deleted).ToList();
                    using (var trans = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (var item in _list)
                            {
                                //Check if exists
                                var _stItem = _context.MN_GROUP_SETTING.FirstOrDefault(it => it.group_id == item.id && it.year_month == yearMonth && it.create_by == userID);
                                if ( _stItem != null)
                                {
                                    // Don't save new setting items if current is exists
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
                                _context.MN_GROUP_SETTING.Add(_st);
                                _context.Entry(_st).State = EntityState.Added;
                                _context.SaveChanges();
                            }
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "Save", userID, ex);
            }
            return ResponseStatusCodeHelper.Success;
        }

    }
}
