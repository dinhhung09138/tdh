using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Personal
{
    /// <summary>
    /// Cetificate model
    /// </summary>
    public class CetificateModel : Utils.Database.BaseModel
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
        public string Name { get; set; }

        /// <summary>
        /// School
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string School { get; set; }

        /// <summary>
        /// Duration
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string Time { get; set; }
        
        /// <summary>
        /// Ordering
        /// </summary>
        public short Ordering { get; set; } = 0;

    }
}
