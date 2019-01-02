using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Personal
{
    /// <summary>
    /// Skill defined model
    /// </summary>
    public class SkillDefinedModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The skill defined identifier
        /// Get from CM_SKILL_DEFINED
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid ID { get; set; }

        public string Name { get; set; } = "";

        /// <summary>
        /// The skill identifier
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid SkillID { get; set; }
        
        /// <summary>
        /// Point
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public short Level { get; set; } = 0;
        
    }
}
