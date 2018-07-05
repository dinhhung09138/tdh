using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Areas.Administrator.Models
{
    public class MoneyAccountSettingModel : Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        /// <summary>
        /// Format yyyyMM
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public float YearMonth { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid AccountID { get; set; }

        public string AccountName { get; set; }

        public decimal Start { get; set; } = 0;

        public decimal Input { get; set; } = 0;

        public decimal Output { get; set; } = 0;

        public decimal End { get; set; } = 0;
    }
}