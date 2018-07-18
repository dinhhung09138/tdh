using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Money
{
    /// <summary>
    /// Group setting model
    /// </summary>
    public class GroupSettingModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The identifier 
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Year
        /// </summary>
        public decimal Year { get; set; } = DateTime.Now.Year;

        /// <summary>
        /// Month
        /// </summary>
        public decimal Month { get; set; } = DateTime.Now.Month;

        /// <summary>
        /// Format yyyyMM
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public decimal YearMonth { get; set; }

        /// <summary>
        /// The group identifier
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid GroupID { get; set; }

        /// <summary>
        /// Group name
        /// </summary>
        public string GroupName { get; set; } = "";

        /// <summary>
        /// Percent setting value
        /// </summary>
        public byte PercentSetting { get; set; } = 0;

        /// <summary>
        /// Percent current value
        /// </summary>
        public byte PercentCurrent { get; set; } = 0;

        /// <summary>
        /// Total money setting value
        /// </summary>
        public decimal MoneySetting { get; set; } = 0;

        /// <summary>
        /// Total money setting value, format as string
        /// </summary>
        public string MoneySettingString { get; set; } = "";

        /// <summary>
        /// Total money current value
        /// </summary>
        public decimal MoneyCurrent { get; set; } = 0;

        /// <summary>
        /// Total money current value, format as string
        /// </summary>
        public string MoneyCurrentString { get; set; } = "";

    }
}
