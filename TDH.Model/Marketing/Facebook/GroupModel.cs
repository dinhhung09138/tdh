using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Marketing.Facebook
{
    /// <summary>
    /// The group model
    /// </summary>
    public class GroupModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The group identifier
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string UID { get; set; }

        /// <summary>
        /// Link
        /// </summary>
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Nội dung không quá 300 ký tự")]
        public string Link { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Nội dung không quá 300 ký tự")]
        public string Name { get; set; }
        
        /// <summary>
        /// Ordering
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public short Ordering { get; set; } = 1;

    }
}
