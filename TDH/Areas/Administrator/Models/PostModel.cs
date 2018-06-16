using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TDH.Areas.Administrator.Models
{
    public class PostModel : Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        public bool IsNavigation { get; set; } = false;
        
        public Guid? NavigationID { get; set; }

        public Guid? CategoryID { get; set; }

        public string CategoryName { get; set; } = "";

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(130, MinimumLength = 1, ErrorMessage = "Nội dung không quá 130 ký tự")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "Nội dung không quá 150 ký tự")]
        public string Alias { get; set; }

        [StringLength(250, ErrorMessage = "Nội dung không quá 250 ký tự")]
        public string Description { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public short Ordering { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string MetaTitle { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(160, MinimumLength = 1, ErrorMessage = "Nội dung không quá 160 ký tự")]
        public string MetaDescription { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Nội dung không quá 250 ký tự")]
        public string MetaKeywords { get; set; }

        [StringLength(170, ErrorMessage = "Nội dung không quá 170 ký tự")]
        public string MetaNext { get; set; }

        [StringLength(100, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string MetaOgSiteName { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Nội dung không quá 200 ký tự")]
        public string MetaOgImage { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Nội dung không quá 200 ký tự")]
        public string MetaTwitterImage { get; set; }

        [StringLength(200, ErrorMessage = "Nội dung không quá 200 ký tự")]
        public string MetaArticleName { get; set; }

        [StringLength(200, ErrorMessage = "Nội dung không quá 200 ký tự")]
        public string MetaArticleSection { get; set; }

        [StringLength(255, ErrorMessage = "Nội dung không quá 255 ký tự")]
        public string MetaArticleTag { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public DateTime MetaArticlePublish { get; set; }

        public string CreateDateString { get; set; } = "";
    }
}