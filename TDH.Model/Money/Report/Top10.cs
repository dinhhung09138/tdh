using System;

namespace TDH.Model.Money.Report
{
    /// <summary>
    /// Top 10 payment or income
    /// </summary>
    public class Top10
    {
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// Payment date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Payment number
        /// </summary>
        public Decimal Money { get; set; }

        /// <summary>
        /// Setting money number
        /// </summary>
        public decimal Setting { get; set; }
    }
}
