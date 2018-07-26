using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDH.Model
{
    /// <summary>
    /// Sidebar view model
    /// </summary>
    public class SideBarViewModel
    {
        /// <summary>
        /// Function code
        /// </summary>
        public string Code { get; set; } = "";

        /// <summary>
        /// Function title
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// Area name
        /// </summary>
        public string Area { get; set; } = "";

        /// <summary>
        /// Controller name
        /// </summary>
        public string Controller { get; set; } = "";

        /// <summary>
        /// Action name
        /// </summary>
        public string Action { get; set; } = "";
    }
}
