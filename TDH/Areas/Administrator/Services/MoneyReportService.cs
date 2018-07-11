using System;
using System.Collections.Generic;
using System.Linq;
using TDH.Models;
using Utils.JqueryDatatable;
using TDH.Areas.Administrator.Models;
using Utils;
using TDH.Areas.Administrator.Common;
using System.Threading.Tasks;

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

        public async Task<List<MoneyReportCollectionByYearModel>> SummaryByYear(int year, Guid userID)
        {
            Task<List<MoneyReportCollectionByYearModel>> _return = Task.Run(() =>
            {
                try
                {
                    List<MoneyReportCollectionByYearModel> _listResult = new List<MoneyReportCollectionByYearModel>();
                    using (var context = new chacd26d_trandinhhungEntities())
                    {
                        var _list = (from m in context.FNC_REPORT_SUMMARY_BY_YEAR(year)
                                     orderby m.month ascending
                                     select m).ToList();
                        foreach (var item in _list)
                        {
                            _listResult.Add(new MoneyReportCollectionByYearModel() { Year = item.year, Month = item.month, Income = item.input, Payment = item.output, Total = item.final });
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


            //Task<List<MoneyReportCollectionByYearModel>> _return = Task.Run(() => {
            //    List<MoneyReportCollectionByYearModel> _listResult = new List<MoneyReportCollectionByYearModel>();
            //    using (var context = new chacd26d_trandinhhungEntities())
            //    {
            //        var _list = (from m in context.FNC_REPORT_SUMMARY_BY_YEAR(year)
            //                     orderby m.month ascending
            //                     select m).ToList();
            //        foreach (var item in _list)
            //        {
            //            _listResult.Add(new MoneyReportCollectionByYearModel() { Year = item.year, Month = item.month, Income = item.input, Payment = item.output, Total = item.final });
            //        }
            //    }
            //    return _listResult;
            //});
            //await _return;
            //return _return.Result;
        }

    }
}