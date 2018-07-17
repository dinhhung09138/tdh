using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TDH.Areas.Administrator.Models
{
    public class RoleModel : Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Nội dung không quá 300 ký tự")]
        public string Description { get; set; }
        
        /// <summary>
        /// Count number of child item
        /// </summary>
        public int Count { get; set; } = 0;

        public string CountString { get; set; } = "0";

        public List<RoleDetailModel> Detail { get; set; } = new List<RoleDetailModel>();
    }
}