using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDH.Model.Personal
{
    /// <summary>
    /// Experience mode;
    /// </summary>
    public class ExperienceModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "Nội dung không quá 150 ký tự")]
        public string Position { get; set; }

        /// <summary>
        /// Company name
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "Nội dung không quá 150 ký tự")]
        public string CompanyName { get; set; }

        /// <summary>
        /// During time
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string DuringTime { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Nội dung không quá 500 ký tự")]
        public string Description { get; set; }

        /// <summary>
        /// Ordering
        /// </summary>
        public short Ordering { get; set; } = 1;

    }
}
