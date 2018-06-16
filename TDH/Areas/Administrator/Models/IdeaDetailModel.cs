using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Areas.Administrator.Models
{
    public class IdeaDetailModel : Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid IdeaID { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "Nội dung không quá 150 ký tự")]
        public string Title { get; set; }

        public string Content { get; set; }
    }
}