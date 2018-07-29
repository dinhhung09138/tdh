
namespace TDH.Model.Money.Report
{
    /// <summary>
    /// Collection report by year model
    /// </summary>
    public class CollectionByYearModel
    {
        /// <summary>
        /// Year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Month
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Total income
        /// </summary>
        public decimal Income { get; set; }

        /// <summary>
        /// Total payment
        /// </summary>
        public decimal Payment { get; set; }

        /// <summary>
        /// Total money
        /// </summary>
        public decimal Total { get; set; }
    }
}
