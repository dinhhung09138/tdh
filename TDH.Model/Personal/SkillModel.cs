using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Personal
{
    /// <summary>
    /// Skill model
    /// </summary>
    public class SkillModel
    {
        /// <summary>
        /// The identifier
        /// Get from CM_SKILL table
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid ID { get; set; }

        public string Name { get; set; } = "";

        /// <summary>
        /// Ordering
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public short Level { get; set; }
        
        /// <summary>
        /// List of skill defined
        /// </summary>
        public List<SkillDefinedModel> Defined { get; set; } = new List<SkillDefinedModel>();

    }
}
