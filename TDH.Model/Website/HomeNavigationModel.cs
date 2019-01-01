using System;

namespace TDH.Model.Website
{
    /// <summary>
    /// Home navigation model
    /// </summary>
    public class HomeNavigationModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// The navigation identifier
        /// </summary>
        public Guid NavigationID { get; set; }

        /// <summary>
        /// Navigation title
        /// </summary>
        public string NavigationTitle { get; set; } = "";

        /// <summary>
        /// Ordering
        /// </summary>
        public short Ordering { get; set; } = 0;

        /// <summary>
        /// Selected status
        /// </summary>
        public bool Selected { get; set; } = false;

    }
}
