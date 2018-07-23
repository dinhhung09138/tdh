using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDH.Areas.Administrator.Models
{
    public class CheckListGroup: Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được rỗng")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string title { get; set; }

        public DateTime updateDate { get; set; }
        [Required]
        public Boolean deleted { get; set; }
        public DateTime deleteDate { get; set; }

    }
}