using System;
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
        /// Total income
        /// </summary>
        public decimal Input { get; set; } = 0;

        /// <summary>
        /// Total payment
        /// </summary>
        public decimal Output { get; set; } = 0;

        /// <summary>
        /// Maximum payment for credit card
        /// </summary>
        public decimal MaxPayment { get; set; } = 0;

        /// <summary>
        /// Maximum payment for credit card
        /// Format as tring
        /// </summary>
        public string MaxPaymentString { get; set; } = "";

        /// <summary>
        /// Total money account borrow or spend from credit card
        /// </summary>
        public decimal BorrowMoney { get; set; } = 0;

        /// <summary>
        /// Total money account borrow or spend from credit card
        /// Format as string
        /// </summary>
        public string BorrowMoneyString { get; set; } = "";

        /// <summary>
        /// Total loan money
        /// </summary>
        public decimal LoanMoney { get; set; } = 0;

        /// <summary>
        /// Total loan money
        /// Format as string
        /// </summary>
        public string LoanMoneyString { get; set; } = "";

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
        /// Account type
        /// cash,debit, credit, borrow, loan, saving,..
        /// </summary>
        public short AccountType { get; set; } = 0;

        /// <summary>
        /// List of detail setting
        /// </summary>
        public List<AccountSettingModel> Setting { get; set; } = new List<AccountSettingModel>();
    }
}
