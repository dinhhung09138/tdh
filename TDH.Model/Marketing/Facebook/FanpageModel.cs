﻿using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Marketing.Facebook
{
    /// <summary>
    /// Fanpage model
    /// </summary>
    public class FanpageModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The fanpage identifier
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
        /// User name
        /// </summary>
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Nội dung không quá 300 ký tự")]
        public string UserName { get; set; }

        /// <summary>
        /// Display name
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Nội dung không quá 300 ký tự")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Phone
        /// </summary>
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Nội dung không quá 20 ký tự")]
        public string Phone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nội dung không quá 50 ký tự")]
        public string Email { get; set; }

        /// <summary>
        /// Website
        /// </summary>
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nội dung không quá 50 ký tự")]
        public string Website { get; set; }

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
