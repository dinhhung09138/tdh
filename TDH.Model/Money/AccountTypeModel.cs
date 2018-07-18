using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Money
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountTypeModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nội dung không quá 50 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short Ordering { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Count { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public string CountString { get; set; } = "";
    }
}
