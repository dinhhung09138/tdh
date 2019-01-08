using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Personal
{
    /// <summary>
    /// Project model
    /// </summary>
    public class ProjectModel : Utils.Database.BaseModel
    {
        /// <summary>
      /// The identifier
      /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Nội dung không quá 250 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Nội dung không quá 500 ký tự")]
        public string Description { get; set; }

        /// <summary>
        /// During time
        /// </summary>
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string DuringTime { get; set; }

        /// <summary>
        /// Image
        /// </summary>
        [StringLength(150, MinimumLength = 1, ErrorMessage = "Nội dung không quá 150 ký tự")]
        public string Image { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Ordering
        /// </summary>
        public short Ordering { get; set; } = 1;
    }
}
