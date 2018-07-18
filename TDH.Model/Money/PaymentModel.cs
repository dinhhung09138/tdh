using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Money
{
    /// <summary>
    /// Payment model
    /// </summary>
    public class PaymentModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Account identifier
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid AccountID { get; set; }

        /// <summary>
        /// Category identifier
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid CategoryID { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(100, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string Title { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Date, format as string
        /// </summary>
        public string DateString { get; set; } = "";

        /// <summary>
        /// Purpose
        /// </summary>
        [StringLength(255, ErrorMessage = "Nội dung không quá 255 ký tự")]
        public string Purpose { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        [StringLength(255, ErrorMessage = "Nội dung không quá 255 ký tự")]
        public string Notes { get; set; }

        /// <summary>
        /// Total money in a transaction
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public decimal Money { get; set; }
    }
}
