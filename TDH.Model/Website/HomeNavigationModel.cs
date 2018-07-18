using System;

namespace TDH.Model.Website
{
    public class HomeNavigationModel : Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        public Guid NavigationID { get; set; }

        public string NavigationTitle { get; set; } = "";

        public short Ordering { get; set; } = 0;

        public bool Selected { get; set; } = false;
    }
}
