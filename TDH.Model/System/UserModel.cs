using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.System
{
    /// <summary>
    /// User model
    /// </summary>
    public class UserModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Full name
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Nội dung không quá 60 ký tự")]
        public string FullName { get; set; } = "";

        /// <summary>
        /// User name
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nội dung không quá 50 ký tự")]
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Nội dung không quá 255 ký tự")]
        public string Password { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        [StringLength(1024, ErrorMessage = "Nội dung không quá 1024 ký tự")]
        public string Notes { get; set; }

        /// <summary>
        /// Locked status
        /// </summary>
        public bool Locked { get; set; } = false;

        /// <summary>
        /// Last login time, format as string
        /// </summary>
        public string LastLoginString { get; set; } = "";

        /// <summary>
        /// Role identifier
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid RoleID { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        public string RoleName { get; set; } = "";

        public string Token { get; set; } = "";

    }
}
