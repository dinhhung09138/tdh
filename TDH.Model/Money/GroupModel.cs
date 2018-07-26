using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Money
{
    /// <summary>
    /// group model
    /// </summary>
    public class GroupModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The group identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nội dung không quá 50 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Nội dung không quá 255 ký tự")]
        public string Notes { get; set; }

        /// <summary>
        /// Is input (true: income, false: payment)
        /// </summary>
        public bool IsInput { get; set; } = false;

        /// <summary>
        /// Percent setting value
        /// </summary>
        public byte PercentSetting { get; set; } = 0;

        /// <summary>
        /// Percent setting value, format as string
        /// </summary>
        public string PercentSettingString { get; set; } = "";

        /// <summary>
        /// Percent current value
        /// </summary>
        public byte PercentCurrent { get; set; } = 0;

        /// <summary>
        /// Percent current value, format as string
        /// </summary>
        public string PercentCurrentString { get; set; } = "";

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

        /// <summary>
        /// Count
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Count value, format as string
        /// </summary>
        public string CountString { get; set; } = "";

        /// <summary>
        /// Ordering
        /// </summary>
        public short Ordering { get; set; }

        /// <summary>
        /// Total money in start of month
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public decimal StartMonth { get; set; }

        /// <summary>
        /// Total money in start of month, format as string
        /// </summary>
        public string StartMonthString { get; set; } = "";

        /// <summary>
        /// Total money in the end of month
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public decimal EndMonth { get; set; }

        /// <summary>
        /// Total money in the end of month, format as string
        /// </summary>
        public string EndMonthString { get; set; } = "";

        /// <summary>
        /// List of group setting
        /// </summary>
        public List<GroupSettingModel> Setting { get; set; } = new List<GroupSettingModel>();

    }
}
