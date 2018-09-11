using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Common
{
    /// <summary>
    /// Skill model
    /// </summary>
    public class SkillModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Nội dung không quá 200 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Nội dung không quá 255 ký tự")]
        public string Notes { get; set; }

        /// <summary>
        /// Group identifier
        /// </summary>
        public Guid GroupID { get; set; }

        /// <summary>
        /// Group name
        /// </summary>
        public string GroupName { get; set; } = "";

        /// <summary>
        /// Ordering
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public short Ordering { get; set; }

        /// <summary>
        /// The number of defiend
        /// </summary>
        public int CountDefiend { get; set; } = 0;

        /// <summary>
        /// List of skill defined
        /// </summary>
        public List<SkillDefinedModel> Defined { get; set; } = new List<SkillDefinedModel>();

    }
}
