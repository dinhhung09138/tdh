using System;

namespace TDH.Areas.Administrator.Models
{
    public class RoleDetailModel : Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        public Guid RoleID { get; set; }

        public string RoleName { get; set; }

        public string FunctionCode { get; set; }

        public string FunctionName { get; set; }

        public bool View { get; set; } = false;

        public bool Add { get; set; } = false;

        public bool Edit { get; set; } = false;

        public bool Delete { get; set; } = false;
    }
}