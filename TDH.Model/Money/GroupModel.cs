using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Money
{
    /// <summary>
    /// 
    /// </summary>
    public class GroupModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nội dung không quá 50 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Nội dung không quá 255 ký tự")]
        public string Notes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsInput { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public byte PercentSetting { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public string PercentSettingString { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public byte PercentCurrent { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public string PercentCurrentString { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public decimal MoneySetting { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public string MoneySettingString { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public decimal MoneyCurrent { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public string MoneyCurrentString { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CountString { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public short Ordering { get; set; }

        /// <summary>
        /// Format yyyyMM
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public decimal StartMonth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StartMonthString { get; set; } = "";

        /// <summary>
        /// Format yyyyMM
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public decimal EndMonth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EndMonthString { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public List<GroupSettingModel> Setting { get; set; } = new List<GroupSettingModel>();

    }
}
