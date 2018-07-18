using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Money
{
    /// <summary>
    /// 
    /// </summary>
    public class CategorySettingModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Format yyyyMM
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public decimal YearMonth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string YearMonthString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Month { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Year { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid CategoryID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CategoryName { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public short PercentSetting { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public short PercentCurrent { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public decimal MoneySetting { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public decimal MoneyCurrent { get; set; } = 0;
    }
}
