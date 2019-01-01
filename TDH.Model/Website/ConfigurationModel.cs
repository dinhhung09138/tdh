using System.Web.Mvc;

namespace TDH.Model.Website
{
    /// <summary>
    /// Configuration model
    /// </summary>
    public class ConfigurationModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// Key name
        /// </summary>
        public string Key { get; set; } = "";

        /// <summary>
        /// Key description
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// Value
        /// </summary>
        [AllowHtml]
        public string Value { get; set; } = "";

    }
}
