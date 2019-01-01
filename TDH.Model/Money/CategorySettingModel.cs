using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Money
{
    /// <summary>
    /// Category setting model
    /// </summary>
    public class CategorySettingModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Year month value
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public decimal YearMonth { get; set; }

        /// <summary>
        /// Format yearmonth MM/yyyy
        /// </summary>
        public string YearMonthString { get; set; }

        /// <summary>
        /// Month
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// The category identifier
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid CategoryID { get; set; }

        /// <summary>
        /// The category name
        /// </summary>
        public string CategoryName { get; set; } = "";

        /// <summary>
        /// Total money setting value
        /// </summary>
        public decimal MoneySetting { get; set; } = 0;

        /// <summary>
        /// Total money setting value as string format
        /// </summary>
        public string MoneySettingString { get; set; } = "0";

        /// <summary>
        /// Total money current value
        /// </summary>
        public decimal MoneyCurrent { get; set; } = 0;

        /// <summary>
        /// Total money current value as string format
        /// </summary>
        public string MoneyCurrentString { get; set; } = "0";

    }
}
