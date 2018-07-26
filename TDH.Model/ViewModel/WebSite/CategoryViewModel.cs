using System;
using System.Collections.Generic;

namespace TDH.Model.ViewModel.WebSite
{
    /// <summary>
    /// Category view model
    /// </summary>
    public class CategoryViewModel : MetaViewModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Navigation identifier
        /// </summary>
        public Guid NavigationID { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// Alias
        /// </summary>
        public string Alias { get; set; } = "";

        /// <summary>
        /// Number of post
        /// </summary>
        public int Count { get; set; } = 0;

        /// <summary>
        /// List post by category id
        /// </summary>
        public List<PostViewModel> Posts { get; set; } = new List<PostViewModel>();
    }
}
