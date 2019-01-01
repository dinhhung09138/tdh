using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TDH.Model.Personal
{
    /// <summary>
    /// IDea model
    /// </summary>
    public class IdeaModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Title of idea
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Nội dung không quá 200 ký tự")]
        public string Title { get; set; }

        /// <summary>
        /// Content of idea
        /// </summary>
        [AllowHtml]
        public string Content { get; set; }

        /// <summary>
        /// Create date as string
        /// </summary>
        public string CreateDateString { get; set; } = "";

        /// <summary>
        /// Number of target by idea
        /// </summary>
        public string Targets { get; set; } = "0";

    }
}
