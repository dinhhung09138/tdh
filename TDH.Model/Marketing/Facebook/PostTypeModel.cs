using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Marketing.Facebook
{
    /// <summary>
    /// The post type model
    /// </summary>
    public class PostTypeModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// Code
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Nội dung không quá 30 ký tự")]
        public string Code { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nội dung không quá 50 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// Allow post on profile
        /// </summary>
        public bool OnProfile { get; set; } = false;

        /// <summary>
        /// Allow post on fanpage
        /// </summary>
        public bool OnFanpage { get; set; } = false;

        /// <summary>
        /// Allow post on group
        /// </summary>
        public bool OnGroup { get; set; } = false;

        /// <summary>
        /// Notes
        /// </summary>
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Nội dung không quá 255 ký tự")]
        public string Notes { get; set; }

        /// <summary>
        /// Ordering
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public short Ordering { get; set; } = 1;

    }
}
