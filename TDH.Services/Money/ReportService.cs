using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDH.Model.Money.Report;
using TDH.DataAccess;
using TDH.Common.UserException;

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
        private readonly string FILE_NAME = "Services.Money/ReportService.cs";

        #endregion

        /// <summary>
        /// Total summary
        /// Get only 10 lasted years
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
                        var _list = (from m in _context.FNC_MN_REPORT_SUMMARY()
                                     orderby m.year ascending
                                     select m).Take(10).ToList();
                        foreach (var item in _list)
                        {
                            _listResult.Add(new CollectionByYearModel() { Year = item.year, Income = item.input, Payment = item.output, Total = item.final });
                        }
                    }
                    return _listResult;
                }
                catch (Exception ex)
                {
                    throw new ServiceException(FILE_NAME, "Summary", userID, ex);
                }
            });
            await _return;
            return _return.Result;
        }

        /// <summary>
        /// Get income, payment, total in a year
        /// line chart
        /// </summary>
        /// <param name="year">Year</param>
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
                        var _list = (from m in _context.FNC_MN_REPORT_SUMMARY_BY_YEAR(year)
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
                    throw new ServiceException(FILE_NAME, "SummaryByYear", userID, ex);
                }
            });
            await _return;
            return _return.Result;
        }

        /// <summary>
        /// Get summary income by month in a year
        /// line chart
        /// </summary>
        /// <param name="year">Year</param>
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
                        var _list = (from m in _context.FNC_MN_REPORT_INCOME_BY_CATEGORY_BY_YEAR(year)
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
                    throw new ServiceException(FILE_NAME, "SummaryIncomeByYear", userID, ex);
                }
            });
            await _return;
            return _return.Result;
        }

        /// <summary>
        /// Get summary payment by month in a year
        /// line chart
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<PaymentByYearModel></returns>
        public async Task<List<PaymentByYearModel>> SummaryPaymentByYear(int year, Guid userID)
        {
            Task<List<PaymentByYearModel>> _return = Task.Run(() =>
            {
                try
                {
                    List<PaymentByYearModel> _listResult = new List<PaymentByYearModel>();
                    using (var _context = new TDHEntities())
                    {
                        var _list = (from m in _context.FNC_MN_REPORT_PAYMENT_BY_GROUP_BY_YEAR(year)
                                     orderby m.name ascending
                                     select m).ToList();
                        foreach (var item in _list)
                        {
                            _listResult.Add(new PaymentByYearModel()
                            {
                                Name = item.name,
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
                    throw new ServiceException(FILE_NAME, "SummaryPaymentByYear", userID, ex);
                }
            });

            await _return;
            return _return.Result;
        }

        /// <summary>
        /// Borrow accocunt status
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<BorrowAccountStatusModel></returns>
        public List<BorrowAccountStatusModel> BorrowAccountStatus(Guid userID)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    return (from m in _context.V_MN_REPORT_BORROW_ACCOUNT_STATUS
                            orderby m.name ascending
                            select new BorrowAccountStatusModel()
                            {
                                ID = m.id,
                                Name = m.name,
                                Max = m.max_payment,
                                Remain = m.remain.Value,
                                Title = m.title,
                                Money = m.money,
                                Date = m.date
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "BorrowAccountStatus", userID, ex);
            }
        }

        /// <summary>
        /// Get top 10 payment in current month
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<Top10></returns>
        public List<Top10> Top10Payment(int year, int month, Guid userID)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    return (from m in _context.MN_PAYMENT
                            join c in _context.MN_CATEGORY on m.category_id equals c.id
                            join g in _context.MN_GROUP on c.group_id equals g.id
                            where g.is_input == false && m.date.Year == year && m.date.Month == month
                            orderby m.money descending
                            select new Top10()
                            {
                                Title = m.title,
                                Money = m.money,
                                Date = m.date
                            }).Take(10).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "Top10", userID, ex);
            }
        }

        /// <summary>
        /// Get top 10 income in current month
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <param name="userID">The user identifier</param>
        /// <returns></returns>
        public List<Top10> Top10Income(int year, int month, Guid userID)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    return (from m in _context.MN_INCOME
                            join c in _context.MN_CATEGORY on m.category_id equals c.id
                            join g in _context.MN_GROUP on c.group_id equals g.id
                            where g.is_input == true && m.date.Year == year && m.date.Month == month
                            orderby m.money descending
                            select new Top10()
                            {
                                Title = m.title,
                                Money = m.money,
                                Date = m.date
                            }).Take(10).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "Top10", userID, ex);
            }
        }

        /// <summary>
        /// Get percent of payment by group
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <param name="userID">The user identifier</param>
        /// <returns>Task<List<Top10>></returns>
        public async Task<List<Top10>> GroupPercent(int year, int month, Guid userID)
        {
            Task<List<Top10>> _return = Task.Run(() =>
            {
                try
                {
                    using (var _context = new TDHEntities())
                    {
                        return (from m in _context.V_MN_CATEGORY
                                where m.is_input == false && m.year == year && m.month == month
                                group m by m.group_name into g
                                select new Top10()
                                {
                                    Title = g.Key,
                                    Money = g.Sum(m => m.current_payment)
                                }).ToList().Where(m => m.Money > 0).ToList();
                    }
                }
                catch (Exception ex)
                {
                    throw new ServiceException(FILE_NAME, "GroupPercent", userID, ex);
                }
            });
            await _return;
            return _return.Result;
        }

        /// <summary>
        /// Get top 10 payment in current month
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<Top10></returns>
        public List<Top10> CategorySettingByMonth(int year, int month, Guid userID)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    return (from m in _context.V_MN_CATEGORY
                            where m.is_input == false && m.year == year && m.month == month && (m.money_setting > 0 || m.current_payment > 0)
                            orderby m.current_payment descending
                            select new Top10()
                            {
                                Title = m.name,
                                Money = m.current_payment,
                                Setting = m.money_setting
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "CategorySettingByMonth", userID, ex);
            }
        }

    }
}
