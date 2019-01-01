using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TDH.Model.Personal
{
    /// <summary>
    /// Event model
    /// </summary>
    public class EventModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string Title { get; set; }

        /// <summary>
        /// Duration
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string Duration { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Nội dung không quá 255 ký tự")]
        public string Description { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        [AllowHtml]
        public string Content { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; } = new DateTime();

        /// <summary>
        /// Date as format string
        /// </summary>
        public string DateString { get; set; } = "";

        /// <summary>
        /// Is planning status
        /// Default is true
        /// </summary>
        public bool IsPlan { get; set; } = true;

        /// <summary>
        /// Is finish status
        /// Default is false
        /// </summary>
        public bool IsFinish { get; set; } = false;

        /// <summary>
        /// Is cancel status
        /// Default is false
        /// </summary>
        public bool IsCancel { get; set; } = false;

        /// <summary>
        /// Event type code
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public string TypeCode { get; set; }

        /// <summary>
        /// Event type name
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Ordering
        /// </summary>
        public short Ordering { get; set; } = 0;
    }
}
