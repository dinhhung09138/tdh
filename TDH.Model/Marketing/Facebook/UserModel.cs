using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Marketing.Facebook
{
    /// <summary>
    /// The facebook user model
    /// </summary>
    public class UserModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The user identifier
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string UID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Nội dung không quá 300 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// Mobile
        /// </summary>
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Nội dung không quá 20 ký tự")]
        public string Mobile { get; set; } = "";

        /// <summary>
        /// Email
        /// </summary>
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nội dung không quá 50 ký tự")]
        public string Email { get; set; } = "";
        
        /// <summary>
        /// Authentication token string
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Nội dung không quá 1000 ký tự")]
        public string AuthToken { get; set; }

        /// <summary>
        /// Token start from
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public DateTime StartOn { get; set; }

        /// <summary>
        /// Last execute
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public DateTime LastExecute { get; set; }

        /// <summary>
        /// Token expired
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public DateTime ExpiresOn { get; set; }

        /// <summary>
        /// Ordering
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public short Ordering { get; set; } = 1;

    }
}
