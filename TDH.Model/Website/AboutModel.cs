using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Website
{
    /// <summary>
    /// About model
    /// </summary>
    public class AboutModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Link
        /// </summary>
        public string Link { get; set; } = "";

        /// <summary>
        /// Image url
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public string Image { get; set; }

        /// <summary>
        /// Meta title
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string MetaTitle { get; set; }

        /// <summary>
        /// Meta description
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(160, MinimumLength = 1, ErrorMessage = "Nội dung không quá 160 ký tự")]
        public string MetaDescription { get; set; }

        /// <summary>
        /// Meta keyword
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Nội dung không quá 250 ký tự")]
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Meta next
        /// </summary>
        [StringLength(170, ErrorMessage = "Nội dung không quá 170 ký tự")]
        public string MetaNext { get; set; }

        /// <summary>
        /// Meta google site name
        /// </summary>
        [StringLength(100, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string MetaOgSiteName { get; set; }

        /// <summary>
        /// Meta google image url
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Nội dung không quá 200 ký tự")]
        public string MetaOgImage { get; set; }

        /// <summary>
        /// Meta twitter image url
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Nội dung không quá 200 ký tự")]
        public string MetaTwitterImage { get; set; }

        /// <summary>
        /// Meta article name
        /// </summary>
        [StringLength(200, ErrorMessage = "Nội dung không quá 200 ký tự")]
        public string MetaArticleName { get; set; }

        /// <summary>
        /// Meta article section name
        /// </summary>
        [StringLength(200, ErrorMessage = "Nội dung không quá 200 ký tự")]
        public string MetaArticleSection { get; set; }

        /// <summary>
        /// Meta article tags
        /// </summary>
        [StringLength(255, ErrorMessage = "Nội dung không quá 255 ký tự")]
        public string MetaArticleTag { get; set; }

        /// <summary>
        /// Meta article publish
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public DateTime MetaArticlePublish { get; set; }

    }
}
