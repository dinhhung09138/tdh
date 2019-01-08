using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDH.Model.ViewModel.WebSite
{
    /// <summary>
    /// Project view model
    /// </summary>
    public class ProjectViewModel
    {
        /// <summary>
        /// Image url
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Project name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Duration
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
    }
}
