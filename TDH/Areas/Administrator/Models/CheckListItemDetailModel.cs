using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDH.Areas.Administrator.Models
{
    public class CheckListItemDetailModel: Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "GroupID không được rỗng")]
        public Guid CheckID { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được rỗng")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string title { get; set; }

        [Required(ErrorMessage = "Ngày tạo không được rỗng")]
        public DateTime createDate { get; set; }

        public DateTime updateDate { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public bool deleted { get; set; }
        
        public DateTime deleteDate { get; set; }
    }
}