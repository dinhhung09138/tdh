using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDH.Areas.Administrator.Models
{
    public class CheckListItemModel: Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "GroupID không được rỗng")]
        public Guid GroupID { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được rỗng")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string title { get; set; }

        [Required(ErrorMessage = "Mô tả không được rỗng")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Nội dung không quá 250 ký tự")]
        public string description { get; set; }

        [Required(ErrorMessage = "Ngày tạo không được rỗng")]
        public DateTime createDate { get; set; }

        public DateTime updateDate { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public bool deleted { get; set; }

        
        public DateTime deleteDate { get; set; }

    }
}