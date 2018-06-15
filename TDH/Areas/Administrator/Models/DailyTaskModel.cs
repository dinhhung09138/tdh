using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Areas.Administrator.Models
{
    public class DailyTaskModel : Utils.Database.BaseModel
    {
        public Guid ID { get; set; }
        
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Nội dung không quá 250 ký tự")]
        public string Title { get; set; }

        public string Notes { get; set; }

        public bool Done { get; set; } = false;

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public DateTime Date { get; set; }
        
    }
}