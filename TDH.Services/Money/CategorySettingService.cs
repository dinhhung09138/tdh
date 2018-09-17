﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TDH.Common;
using TDH.DataAccess;
using TDH.Model.Money;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Services.Money
{
    /// <summary>
    /// Category setting service
    /// </summary>
    public class CategorySettingService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services/CategorySettingService.cs";

        #endregion

        /// <summary>
        /// Get all item by category id without deleted
        /// </summary>
        /// <param name="categoryID">The category identifier</param>
        /// <param name="year">Year</param>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<CategoryModel></returns>
        public List<CategorySettingModel> GetAll(Guid categoryID, int year, Guid userID)
        {
            try
            {
                List<CategorySettingModel> _return = new List<CategorySettingModel>();
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.MN_CATEGORY_SETTING
                                 where !m.deleted && m.category_id == categoryID && m.year_month.ToString().Contains(year.ToString())
                                 orderby m.year_month descending
                                 select new
                                 {
                                     m.id,
                                     m.year_month,
                                     m.money_setting,
                                     m.money_current
                                 }).ToList();
                    if(_list.Count() == 0)
                    {
                        //Create if current year doesn't setting
                        if(Create(categoryID, year, userID) == ResponseStatusCodeHelper.Success)
                        {
                            _list = (from m in _context.MN_CATEGORY_SETTING
                                     where !m.deleted && m.category_id == categoryID && m.year_month.ToString().Contains(year.ToString())
                                     orderby m.year_month descending
                                     select new
                                     {
                                         m.id,
                                         m.year_month,
                                         m.money_setting,
                                         m.money_current
                                     }).ToList();
                        }
                    }
                    foreach (var item in _list)
                    {
                        _return.Add(new CategorySettingModel()
                        {
                            ID = item.id,
                            YearMonth = item.year_month,
                            Month = int.Parse(item.year_month.ToString().Substring(4, 2)),
                            Year = int.Parse(item.year_month.ToString().Substring(0, 4)),
                            MoneySetting = item.money_setting,
                            MoneySettingString = item.money_setting.NumberToString(),
                            MoneyCurrent = item.money_current,
                            MoneyCurrentString = item.money_current.NumberToString()
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
        /// Save
        /// </summary>
        /// <param name="categoryID">The category identifier</param>
        /// <param name="year">Year</param>
        /// <param name="userID">The user identifier</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        private ResponseStatusCodeHelper Create(Guid categoryID, int year, Guid userID)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    using (var trans = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            year = year * 100;
                            for (int i = 1; i <= 12; i++)
                            {
                                MN_CATEGORY_SETTING _md = new MN_CATEGORY_SETTING() {
                                    id = Guid.NewGuid(),
                                    category_id = categoryID,
                                    year_month = year + i,
                                    percent_current = 0,
                                    percent_setting = 0,
                                    money_current = 0,
                                    money_setting = 0,
                                    create_by = userID,
                                    create_date = DateTime.Now,
                                    deleted = false
                                };
                                _context.MN_CATEGORY_SETTING.Add(_md);
                                _context.Entry(_md).State = EntityState.Added;
                            }
                            _context.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            Notifier.Notification(userID, Message.Error, Notifier.TYPE.Error);
                            trans.Rollback();
                            Log.WriteLog(FILE_NAME, "Create", userID, ex);
                            throw new ApplicationException();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Create", userID, ex);
                return ResponseStatusCodeHelper.Error;
            }
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model">setting model</param>
        /// <param name="userID">user identifier</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(List<CategorySettingModel> model, Guid userID)
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
                                MN_CATEGORY_SETTING _md = _context.MN_CATEGORY_SETTING.FirstOrDefault(m => m.id == item.ID && m.category_id == item.CategoryID);
                                _md.money_setting = item.MoneySetting;
                                _context.MN_CATEGORY_SETTING.Attach(_md);
                                _context.Entry(_md).State = EntityState.Modified;
                            }
                            _context.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            Notifier.Notification(userID, Message.Error, Notifier.TYPE.Error);
                            trans.Rollback();
                            Log.WriteLog(FILE_NAME, "Save", userID, ex);
                            throw new ApplicationException();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Save", userID, ex);
                Notifier.Notification(userID, Message.Error, Notifier.TYPE.Error);
                return ResponseStatusCodeHelper.Error;
            }
            Notifier.Notification(userID, Message.UpdateSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }
    }
}
