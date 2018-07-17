using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TDH.Areas.Administrator.Models
{
    public class MoneyAccountTypeModel : Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nội dung không quá 50 ký tự")]
        public string Name { get; set; }

        public short Ordering { get; set; }

        public int Count { get; set; } = 0;

        public string CountString { get; set; } = "";
    }
}