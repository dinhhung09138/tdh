using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Areas.Administrator.Models
{
    public class MoneyCategorySettingModel : Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        /// <summary>
        /// Format yyyyMM
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public decimal YearMonth { get; set; }

        public string YearMonthString { get; set; }

        public decimal Month { get; set; }

        public decimal Year { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid CategoryID { get; set; }

        public string CategoryName { get; set; } = "";

        public short PercentSetting { get; set; } = 0;

        public short PercentCurrent { get; set; } = 0;

        public decimal MoneySetting { get; set; } = 0;

        public decimal MoneyCurrent { get; set; } = 0;

    }
}