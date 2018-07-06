using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TDH.Areas.Administrator.Models
{
    public class MoneyCategoryModel : Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid GroupID { get; set; }

        public string GroupName { get; set; } = "";

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nội dung không quá 50 ký tự")]
        public string Name { get; set; }

        [StringLength(255, MinimumLength = 1, ErrorMessage = "Nội dung không quá 255 ký tự")]
        public string Notes { get; set; }
        
        public byte PercentSetting { get; set; } = 0;
        
        public byte PercentCurrent { get; set; } = 0;
        
        public decimal MoneySetting { get; set; } = 0;

        public string MoneySettingString { get; set; } = "";

        public decimal MoneyCurrent { get; set; } = 0;

        public string MoneyCurrentString { get; set; } = "";

        public string Count { get; set; }

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

        public List<MoneyCategorySettingModel> Setting { get; set; } = new List<MoneyCategorySettingModel>();

    }
}