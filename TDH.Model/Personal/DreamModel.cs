using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Personal
{
    public class DreamModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Title of idea
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Nội dung không quá 250 ký tự")]
        public string Title { get; set; }

        /// <summary>
        /// Ordering
        /// </summary>
        public short Ordering { get; set; } = 0;

        /// <summary>
        /// dream is finish
        /// default: false
        /// </summary>
        public bool Finish { get; set; } = false;

        /// <summary>
        /// Finish time
        /// </summary>
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// Finish time as string
        /// </summary>
        public string FinishTimeString { get; set; } = "";

        /// <summary>
        /// Notes
        /// </summary>
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Nội dung không quá 250 ký tự")]
        public string Notes { get; set; } = "";

    }
}
