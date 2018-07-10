using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TDH.Areas.Administrator.Models
{
    public class MoneyGroupModel : Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nội dung không quá 50 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Nội dung không quá 255 ký tự")]
        public string Notes { get; set; }

        public bool IsInput { get; set; } = false;

        public byte PercentSetting { get; set; } = 0;

        public string PercentSettingString { get; set; } = "";

        public byte PercentCurrent { get; set; } = 0;

        public string PercentCurrentString { get; set; } = "";

        public decimal MoneySetting { get; set; } = 0;

        public string MoneySettingString { get; set; } = "";

        public decimal MoneyCurrent { get; set; } = 0;

        public string MoneyCurrentString { get; set; } = "";

        public int Count { get; set; }

        public string CountString { get; set; } = "";

        public short Ordering { get; set; }

        /// <summary>
        /// Format yyyyMM
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public decimal StartMonth { get; set; }

        public string StartMonthString { get; set; } = "";

        /// <summary>
        /// Format yyyyMM
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public decimal EndMonth { get; set; }

        public string EndMonthString { get; set; } = "";

        public List<MoneyGroupSettingModel> Setting { get; set; } = new List<MoneyGroupSettingModel>();

    }
}