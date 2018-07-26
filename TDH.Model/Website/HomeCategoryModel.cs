using System;

namespace TDH.Model.Website
{
    /// <summary>
    /// Home category model
    /// </summary>
    public class HomeCategoryModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// The category identifier
        /// </summary>
        public Guid CategoryID { get; set; }

        /// <summary>
        /// Category title
        /// </summary>
        public string CategoryTitle { get; set; } = "";

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
