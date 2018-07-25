﻿using System;
using System.Collections.Generic;
using System.Linq;
using TDH.Models;
using Utils.JqueryDatatable;
using TDH.Areas.Administrator.Models;
using Utils;
using TDH.Areas.Administrator.Common;
using System.Threading.Tasks;
using TDH.Model.Money;

namespace TDH.Areas.Administrator.Services
{
    public class MoneyReportService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator/Services/MoneyReportService.cs";

        #endregion

        /// <summary>
        /// Total summary
        /// Line chart
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<List<ReportCollectionByYearModel>> Summary(Guid userID)
        {
            Task<List<ReportCollectionByYearModel>> _return = Task.Run(() =>
            {
                try
                {
                    List<ReportCollectionByYearModel> _listResult = new List<ReportCollectionByYearModel>();
                    using (var context = new chacd26d_trandinhhungEntities())
                    {
                        var _list = (from m in context.FNC_REPORT_SUMMARY()
                                     orderby m.year ascending
                                     select m).ToList();
                        foreach (var item in _list)
                        {
                            _listResult.Add(new ReportCollectionByYearModel() { Year = item.year, Income = item.input, Payment = item.output, Total = item.final });
                        }
                    }
                    return _listResult;
                }
                catch (Exception ex)
                {
                    Notifier.Notification(userID, Resources.Message.Error, Notifier.TYPE.Error);
                    TDH.Services.Log.WriteLog(FILE_NAME, "Summaryr", userID, ex);
                    throw new ApplicationException();
                }
            });
            await _return;
            return _return.Result;


            //Task<List<ReportCollectionByYearModel>> _return = Task.Run(() => {
            //    List<ReportCollectionByYearModel> _listResult = new List<ReportCollectionByYearModel>();
            //    using (var context = new chacd26d_trandinhhungEntities())
            //    {
            //        var _list = (from m in context.FNC_REPORT_SUMMARY_BY_YEAR(year)
            //                     orderby m.month ascending
            //                     select m).ToList();
            //        foreach (var item in _list)
            //        {
            //            _listResult.Add(new ReportCollectionByYearModel() { Year = item.year, Month = item.month, Income = item.input, Payment = item.output, Total = item.final });
            //        }
            //    }
            //    return _listResult;
            //});
            //await _return;
            //return _return.Result;
        }

        /// <summary>
        /// Get income, payment, total in a year
        /// line chart
        /// </summary>
        /// <param name="year">year</param>
        /// <param name="userID">user's id</param>
        /// <returns></returns>
        public async Task<List<ReportCollectionByYearModel>> SummaryByYear(int year, Guid userID)
        {
            Task<List<ReportCollectionByYearModel>> _return = Task.Run(() =>
            {
                try
                {
                    List<ReportCollectionByYearModel> _listResult = new List<ReportCollectionByYearModel>();
                    using (var context = new chacd26d_trandinhhungEntities())
                    {
                        var _list = (from m in context.FNC_REPORT_SUMMARY_BY_YEAR(year)
                                     orderby m.month ascending
                                     select m).ToList();
                        foreach (var item in _list)
                        {
                            _listResult.Add(new ReportCollectionByYearModel() { Year = item.year, Month = item.month, Income = item.input, Payment = item.output, Total = item.final });
                        }
                    }
                    return _listResult;
                }
                catch (Exception ex)
                {
                    Notifier.Notification(userID, Resources.Message.Error, Notifier.TYPE.Error);
                    TDH.Services.Log.WriteLog(FILE_NAME, "SummaryByYear", userID, ex);
                    throw new ApplicationException();
                }
            });
            await _return;
            return _return.Result;


            //Task<List<ReportCollectionByYearModel>> _return = Task.Run(() => {
            //    List<ReportCollectionByYearModel> _listResult = new List<ReportCollectionByYearModel>();
            //    using (var context = new chacd26d_trandinhhungEntities())
            //    {
            //        var _list = (from m in context.FNC_REPORT_SUMMARY_BY_YEAR(year)
            //                     orderby m.month ascending
            //                     select m).ToList();
            //        foreach (var item in _list)
            //        {
            //            _listResult.Add(new ReportCollectionByYearModel() { Year = item.year, Month = item.month, Income = item.input, Payment = item.output, Total = item.final });
            //        }
            //    }
            //    return _listResult;
            //});
            //await _return;
            //return _return.Result;
        }

    }
}