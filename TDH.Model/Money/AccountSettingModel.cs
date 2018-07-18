using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Money
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountSettingModel : Utils.Database.BaseModel
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
        public decimal Year { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Month { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid AccountID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Start { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public decimal Input { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public decimal Output { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public decimal End { get; set; } = 0;
    }
}
