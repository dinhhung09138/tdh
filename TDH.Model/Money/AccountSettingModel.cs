using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Money
{
    /// <summary>
    /// Account type setting
    /// </summary>
    public class AccountSettingModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Format yyyyMM
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public decimal YearMonth { get; set; }

        /// <summary>
        /// YearMonth value, Format as string
        /// </summary>
        public string YearMonthString { get; set; }

        /// <summary>
        /// Year
        /// </summary>
        public decimal Year { get; set; }

        /// <summary>
        /// Month
        /// </summary>
        public decimal Month { get; set; }

        /// <summary>
        /// Account's id
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid AccountID { get; set; }

        /// <summary>
        /// Account's name
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Total money begin
        /// </summary>
        public decimal Start { get; set; } = 0;

        /// <summary>
        /// Total income
        /// </summary>
        public decimal Input { get; set; } = 0;

        /// <summary>
        /// Total payment
        /// </summary>
        public decimal Output { get; set; } = 0;

        /// <summary>
        /// Total money left
        /// </summary>
        public decimal End { get; set; } = 0;
    }
}
