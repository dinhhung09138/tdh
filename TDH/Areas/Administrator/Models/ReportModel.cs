using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TDH.Areas.Administrator.Models
{
    public class ReportModel : Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "Nội dung không quá 150 ký tự")]
        public string Title { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        public string Count { get; set; } = "0";

        public string UserCreate { get; set; } = "";

        public string CreateDateString { get; set; } = "";
    }
}