using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Money
{
    /// <summary>
    /// Transfer model
    /// </summary>
    public class TransferModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid CategoryID { get; set; }

        public string CategoryName { get; set; } = "";

        /// <summary>
        /// Account send money
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid AccountFrom { get; set; }

        /// <summary>
        /// Account receive money
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid AccountTo { get; set; }

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
        /// Money in a transaction
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public decimal Money { get; set; }

        /// <summary>
        /// Fee in a transaction
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public decimal Fee { get; set; }
    }
}
