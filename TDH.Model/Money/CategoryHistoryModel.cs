using System;

namespace TDH.Model.Money
{
    /// <summary>
    /// Category history model
    /// </summary>
    public class CategoryHistoryModel
    {
        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Date value, format as string
        /// </summary>
        public string DateString { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Total money
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// Total money, format as string
        /// </summary>
        public string MoneyString { get; set; }

        /// <summary>
        /// Type, income or payment
        /// </summary>
        public int Type { get; set; }
    }
}
