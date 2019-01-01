using System;

namespace TDH.Model.Money.Report
{
    /// <summary>
    /// Borror account status model
    /// </summary>
    public class BorrowAccountStatusModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Max of money
        /// </summary>
        public decimal Max { get; set; } = 0;

        /// <summary>
        /// Remain
        /// </summary>
        public decimal Remain { get; set; } = 0;

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// Money
        /// </summary>
        public decimal Money { get; set; } = 0;

        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; } = DateTime.Now;

    }
}
