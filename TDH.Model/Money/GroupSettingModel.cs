using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Money
{
    /// <summary>
    /// 
    /// </summary>
    public class GroupSettingModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Year { get; set; } = DateTime.Now.Year;

        /// <summary>
        /// 
        /// </summary>
        public decimal Month { get; set; } = DateTime.Now.Month;

        /// <summary>
        /// Format yyyyMM
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public decimal YearMonth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid GroupID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GroupName { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public byte PercentSetting { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public byte PercentCurrent { get; set; } = 0;

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

    }
}
