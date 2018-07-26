using System.Web.Mvc;

namespace TDH.Model.Website
{
    public class ConfigurationModel : Utils.Database.BaseModel
    {
        public string Key { get; set; } = "";

        public string Description { get; set; } = "";

        [AllowHtml]
        public string Value { get; set; } = "";
    }
}
