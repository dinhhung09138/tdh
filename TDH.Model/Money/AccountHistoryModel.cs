using System;

namespace TDH.Model.Money
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountHistoryModel
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DateString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MoneyString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Type { get; set; }
    }
}
