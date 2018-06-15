using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Areas.Administrator.Models
{
    public class UserModel : Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Nội dung không quá 60 ký tự")]
        public string FullName { get; set; } = "";

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nội dung không quá 50 ký tự")]
        public string UserName { get; set; }
        
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Nội dung không quá 255 ký tự")]
        public string Password { get; set; }

        [StringLength(1024, ErrorMessage = "Nội dung không quá 1024 ký tự")]
        public string Notes { get; set; }

        public bool Locked { get; set; } = false;

        public string LastLoginString { get; set; } = "";

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid RoleID { get; set; }

        public string RoleName { get; set; } = "";
        
    }
}