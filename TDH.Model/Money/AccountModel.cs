﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Money
{
    /// <summary>
    /// Account model
    /// </summary>
    public class AccountModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Name of account
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nội dung không quá 50 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// Account's type
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid AccountTypeID { get; set; }

        /// <summary>
        /// Account's type name
        /// </summary>
        public string AccountTypeName { get; set; }

        /// <summary>
        /// Money left in last month
        /// </summary>
        public decimal MonthStart { get; set; } = 0;

        /// <summary>
        /// Money left in last month, format as string
        /// </summary>
        public string MonthStartString { get; set; } = "";

        /// <summary>
        /// Money receive in month
        /// </summary>
        public decimal MonthInput { get; set; } = 0;

        /// <summary>
        /// Money receive in a month, format as string
        /// </summary>
        public string MonthInputString { get; set; } = "";

        /// <summary>
        /// Money spend in month
        /// </summary>
        public decimal MonthOutput { get; set; } = 0;

        /// <summary>
        /// Money spend in a month, format as string
        /// </summary>
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

        /// <summary>
        /// Money spend in a month, format as string
        /// </summary>
        public string MonthTotalString { get; set; } = "";

        /// <summary>
        /// Total money spend
        /// </summary>
        public decimal Total { get; set; } = 0;

        /// <summary>
        /// Total money spend, format as string
        /// </summary>
        public string TotalString { get; set; } = "";

        /// <summary>
        /// List of detail setting
        /// </summary>
        public List<AccountSettingModel> Setting { get; set; } = new List<AccountSettingModel>();
    }
}
