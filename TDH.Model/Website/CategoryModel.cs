using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Model.Website
{
    /// <summary>
    /// Category model
    /// </summary>
    public class CategoryModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Navigation identifier
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid NavigationID { get; set; }

        /// <summary>
        /// Navigation title
        /// </summary>
        public string NavigationTitle { get; set; } = "";

        /// <summary>
        /// Title
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(70, MinimumLength = 1, ErrorMessage = "Nội dung không quá 70 ký tự")]
        public string Title { get; set; }

        /// <summary>
        /// Title alias
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string Alias { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [StringLength(250, ErrorMessage = "Nội dung không quá 250 ký tự")]
        public string Description { get; set; }

        /// <summary>
        /// Show on navigation status
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public bool ShowOnNav { get; set; }

        /// <summary>
        /// Image url
        /// </summary>
        [StringLength(200, ErrorMessage = "Nội dung không quá 200 ký tự")]
        public string Image { get; set; }

        /// <summary>
        /// Ordering
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public short Ordering { get; set; }

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

        /// <summary>
        /// Count number of child item
        /// </summary>
        public int Count { get; set; } = 0;

        /// <summary>
        /// Count number of child item. Format as string
        /// </summary>
        public string CountString { get; set; } = "0";

    }
}
