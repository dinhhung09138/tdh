using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Money
{
    /// <summary>
    /// 
    /// </summary>
    public class IncomeModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid AccountID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid CategoryID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(100, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public DateTime Date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DateString { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [StringLength(255, ErrorMessage = "Nội dung không quá 255 ký tự")]
        public string Purpose { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(255, ErrorMessage = "Nội dung không quá 255 ký tự")]
        public string Notes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public decimal Money { get; set; }
    }
}
