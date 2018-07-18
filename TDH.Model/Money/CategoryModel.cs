using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Money
{
    /// <summary>
    /// Category model
    /// </summary>
    public class CategoryModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

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
        /// The category's name
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nội dung không quá 50 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Nội dung không quá 255 ký tự")]
        public string Notes { get; set; }

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
        /// Total money as current
        /// </summary>
        public decimal MoneyCurrent { get; set; } = 0;

        /// <summary>
        /// Total money as current, format as string
        /// </summary>
        public string MoneyCurrentString { get; set; } = "";

        /// <summary>
        /// Count
        /// </summary>
        public string Count { get; set; }

        /// <summary>
        /// Ordering
        /// </summary>
        public short Ordering { get; set; }

        /// <summary>
        /// Total money from the start of month
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public decimal StartMonth { get; set; }

        /// <summary>
        /// Total money from the start of month, format as string
        /// </summary>
        public string StartMonthString { get; set; } = "";

        /// <summary>
        /// Total money at the end of month
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public decimal EndMonth { get; set; }

        /// <summary>
        /// Total money at the end of month, format as string
        /// </summary>
        public string EndMonthString { get; set; } = "";

        /// <summary>
        /// List of setting
        /// </summary>
        public List<CategorySettingModel> Setting { get; set; } = new List<CategorySettingModel>();
    }
}
