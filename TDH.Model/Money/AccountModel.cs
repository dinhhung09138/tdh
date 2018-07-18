using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Money
{
    public class AccountModel : Utils.Database.BaseModel
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
        public decimal MonthStart { get; set; } = 0;

        public string MonthStartString { get; set; } = "";

        /// <summary>
        /// Money receive in month
        /// </summary>
        public decimal MonthInput { get; set; } = 0;

        public string MonthInputString { get; set; } = "";

        /// <summary>
        /// Money spend in month
        /// </summary>
        public decimal MonthOutput { get; set; } = 0;

        public string MonthOutputString { get; set; } = "";

        /// <summary>
        /// Money end of month
        /// </summary>
        public decimal MonthEnd { get; set; } = 0;

        public string MonthEndString { get; set; } = "";

        /// <summary>
        /// Money spend in month
        /// </summary>
        public decimal MonthTotal { get; set; } = 0;

        public string MonthTotalString { get; set; } = "";

        /// <summary>
        /// Money spend in month
        /// </summary>
        public decimal Total { get; set; } = 0;

        public string TotalString { get; set; } = "";

        public List<AccountSettingModel> Setting { get; set; } = new List<AccountSettingModel>();
    }
}
