using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TDH.Areas.Administrator.Models
{
    public class MoneyAccountModel : Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nội dung không quá 50 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid AccountTypeID { get; set; }

        public string AccountTypeName { get; set; }

        /// <summary>
        /// Money left in last month
        /// </summary>
        public decimal Start { get; set; } = 0;

        public string StartString { get; set; } = "";

        /// <summary>
        /// Money receive in month
        /// </summary>
        public decimal Input { get; set; } = 0;

        public string InputString { get; set; } = "";

        /// <summary>
        /// Money spend in month
        /// </summary>
        public decimal Output { get; set; } = 0;

        public string OutputString { get; set; } = "";

        /// <summary>
        /// Money end of month
        /// </summary>
        public decimal End { get; set; } = 0;

        public string EndString { get; set; } = "";

        public List<MoneyAccountSettingModel> Setting { get; set; } = new List<MoneyAccountSettingModel>();

    }
}