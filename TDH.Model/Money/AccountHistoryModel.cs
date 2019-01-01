using System;

namespace TDH.Model.Money
{
    /// <summary>
    /// Account history model
    /// </summary>
    public class AccountHistoryModel
    {
        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Date format as string
        /// </summary>
        public string DateString { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Money
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// Money format as string
        /// </summary>
        public string MoneyString { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public int Type { get; set; }

    }
}
