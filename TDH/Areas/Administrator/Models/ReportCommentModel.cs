using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TDH.Areas.Administrator.Models
{
    public class ReportCommentModel : Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid ReportID { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "Nội dung không quá 150 ký tự")]
        public string Title { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public string Content { get; set; }

        public string UserCreate { get; set; } = "";

        public string CreateDateString { get; set; } = "";

    }
}