using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace TDH.Areas.Administrator.Models
{
    public class TargetModel : Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        public Guid? ParentID { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Nội dung không quá 250 ký tự")]
        public string Title { get; set; }

        public string Result { get; set; }

        public bool Done { get; set; } = false;

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public DateTime EstimateDate { get; set; }

        public string EstimateDateString { get; set; } = "";

        public DateTime? FinishDate { get; set; }

        public string FinishDateString { get; set; } = "";

        public short TaskCount { get; set; } = 0;

        public int TaskDone { get; set; } = 0;

        public short Level { get; set; } = 1;
    }
}