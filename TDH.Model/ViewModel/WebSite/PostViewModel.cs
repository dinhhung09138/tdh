using System;

namespace TDH.Model.ViewModel.WebSite
{
    /// <summary>
    /// Post view model
    /// </summary>
    public class PostViewModel : MetaViewModel
    {
        /// <summary>
        /// The post identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// The category identifier
        /// </summary>
        public Guid CategoryID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CategoryTitle { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string CategoryAlias { get; set; } = "";

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// Alias
        /// </summary>
        public string Alias { get; set; } = "";

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// Content
        /// </summary>
        public string Content { get; set; } = "";

        /// <summary>
        /// Image url
        /// </summary>
        public string Image { get; set; } = "";

        /// <summary>
        /// Create date
        /// </summary>
        public string CreateDate { get; set; } = "";
    }
}
