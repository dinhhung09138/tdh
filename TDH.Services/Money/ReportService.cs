using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDH.Model.Money.Report;
using TDH.DataAccess;
using TDH.Common;

namespace TDH.Services.Money
{
    /// <summary>
    /// Report service
    /// </summary>
    public class ReportService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services/ReportService.cs";

        #endregion

        /// <summary>
        /// Total summary
        /// Line chart
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<ReportCollectionByYearModel></returns>
        public async Task<List<CollectionByYearModel>> Summary(Guid userID)
        {
            Task<List<CollectionByYearModel>> _return = Task.Run(() =>
            {
                try
                {
                    List<CollectionByYearModel> _listResult = new List<CollectionByYearModel>();
                    using (var _context = new TDHEntities())
                    {
                        var _list = (from m in _context.FNC_REPORT_SUMMARY()
                                     orderby m.year ascending
                                     select m).ToList();
                        foreach (var item in _list)
                        {
                            _listResult.Add(new CollectionByYearModel() { Year = item.year, Income = item.input, Payment = item.output, Total = item.final });
                        }
                    }
                    return _listResult;
                }
                catch (Exception ex)
                {
                    Notifier.Notification(userID, Message.Error, Notifier.TYPE.Error);
                    Log.WriteLog(FILE_NAME, "Summary", userID, ex);
                    throw new ApplicationException();
                }
            });
            await _return;
            return _return.Result;
        }

        /// <summary>
        /// Get income, payment, total in a year
        /// line chart
        /// </summary>
        /// <param name="year">year</param>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<ReportCollectionByYearModel></returns>
        public async Task<List<CollectionByYearModel>> SummaryByYear(int year, Guid userID)
        {
            Task<List<CollectionByYearModel>> _return = Task.Run(() =>
            {
                try
                {
                    List<CollectionByYearModel> _listResult = new List<CollectionByYearModel>();
                    using (var _context = new TDHEntities())
                    {
                        var _list = (from m in _context.FNC_REPORT_SUMMARY_BY_YEAR(year)
                                     orderby m.month ascending
                                     select m).ToList();
                        foreach (var item in _list)
                        {
                            _listResult.Add(new CollectionByYearModel() { Year = item.year, Month = item.month, Income = item.input, Payment = item.output, Total = item.final });
                        }
                    }
                    return _listResult;
                }
                catch (Exception ex)
                {
                    Notifier.Notification(userID, Message.Error, Notifier.TYPE.Error);
                    Log.WriteLog(FILE_NAME, "SummaryByYear", userID, ex);
                    throw new ApplicationException();
                }
            });
            await _return;
            return _return.Result;
        }

        /// <summary>
        /// Get summary income by month in a year
        /// line chart
        /// </summary>
        /// <param name="year">year</param>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<ReportCollectionByYearModel></returns>
        public async Task<List<IncomeByYearModel>> SummaryIncomeByYear(int year, Guid userID)
        {
            Task<List<IncomeByYearModel>> _return = Task.Run(() =>
            {
                try
                {
                    List<IncomeByYearModel> _listResult = new List<IncomeByYearModel>();
                    using (var _context = new TDHEntities())
                    {
                        var _list = (from m in _context.FNC_REPORT_INCOME_BY_CATEGORY_BY_YEAR(year)
                                     where m.name == "money_current"
                                     orderby m.cate_name ascending
                                     select m).ToList();
                        foreach (var item in _list)
                        {
                            _listResult.Add(new IncomeByYearModel()
                            {
                                Name = item.cate_name,
                                T01 = item.t1,
                                T02 = item.t2,
                                T03 = item.t3,
                                T04 = item.t4,
                                T05 = item.t5,
                                T06 = item.t6,
                                T07 = item.t7,
                                T08 = item.t8,
                                T09 = item.t9,
                                T10 = item.t10,
                                T11 = item.t11,
                                T12 = item.t12
                            });
                        }
                    }
                    return _listResult;
                }
                catch (Exception ex)
                {
                    Notifier.Notification(userID, Message.Error, Notifier.TYPE.Error);
                    Log.WriteLog(FILE_NAME, "SummaryByYear", userID, ex);
                    throw new ApplicationException();
                }
            });
            await _return;
            return _return.Result;
        }


    }
}
