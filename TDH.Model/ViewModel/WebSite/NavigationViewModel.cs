using System;
using System.Collections.Generic;

namespace TDH.Model.ViewModel.WebSite
{
    /// <summary>
    /// Navigation view model
    /// </summary>
    public class NavigationViewModel : MetaViewModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// Alias
        /// </summary>
        public string Alias { get; set; } = "";

        /// <summary>
        /// List category by navigation
        /// </summary>
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

        /// <summary>
        /// List post by navigation
        /// </summary>
        public List<PostViewModel> Posts { get; set; } = new List<PostViewModel>();
    }
}
