using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.PersonalWorking
{
    public class CheckListGroupModel : Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được rỗng")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string title { get; set; }

        public DateTime updateDate { get; set; }
        [Required]
        public bool deleted { get; set; }
        public DateTime deleteDate { get; set; }

    }
}
