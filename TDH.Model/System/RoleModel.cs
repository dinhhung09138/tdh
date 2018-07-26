using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDH.Model.System
{
    /// <summary>
    /// Role model
    /// </summary>
    public class RoleModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Nội dung không quá 300 ký tự")]
        public string Description { get; set; }

        /// <summary>
        /// Count number of child item
        /// </summary>
        public int Count { get; set; } = 0;

        /// <summary>
        /// Count number, format as string
        /// </summary>
        public string CountString { get; set; } = "0";

        /// <summary>
        /// Role detail list
        /// </summary>
        public List<RoleDetailModel> Detail { get; set; } = new List<RoleDetailModel>();
    }
}
