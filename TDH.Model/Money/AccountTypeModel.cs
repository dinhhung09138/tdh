using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Money
{
    /// <summary>
    /// Account type model
    /// </summary>
    public class AccountTypeModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Account's name
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nội dung không quá 50 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// Ordering
        /// </summary>
        public short Ordering { get; set; }

        /// <summary>
        /// Count
        /// </summary>
        public int Count { get; set; } = 0;

        /// <summary>
        /// Count value, format as string
        /// </summary>
        public string CountString { get; set; } = "";
    }
}
