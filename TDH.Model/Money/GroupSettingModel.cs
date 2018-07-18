using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Money
{
    public class GroupSettingModel : Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        public decimal Year { get; set; } = DateTime.Now.Year;

        public decimal Month { get; set; } = DateTime.Now.Month;

        /// <summary>
        /// Format yyyyMM
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public decimal YearMonth { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid GroupID { get; set; }

        public string GroupName { get; set; } = "";

        public byte PercentSetting { get; set; } = 0;

        public byte PercentCurrent { get; set; } = 0;

        public decimal MoneySetting { get; set; } = 0;

        public string MoneySettingString { get; set; } = "";

        public decimal MoneyCurrent { get; set; } = 0;

        public string MoneyCurrentString { get; set; } = "";

    }
}
