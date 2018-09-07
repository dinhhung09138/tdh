using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Common
{
    /// <summary>
    /// Skill defined model
    /// </summary>
    public class SkillDefinedModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The skill identifier
        /// </summary>
        public Guid SkillID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Nội dung không quá 200 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [StringLength(400, MinimumLength = 1, ErrorMessage = "Nội dung không quá 400 ký tự")]
        public string Description { get; set; }

        /// <summary>
        /// Point
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public short Point { get; set; } = 0;

        /// <summary>
        /// Ordering
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public short Ordering { get; set; } = 1;

    }
}
