using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Personal
{
    /// <summary>
    /// Education type model
    /// </summary>
    public class EducationTypeModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// Code
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Nội dung không quá 20 ký tự")]
        public string Code { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// Ordering
        /// </summary>
        public short Ordering { get; set; }
    }
}
