using System;

namespace TDH.Model.System
{
    /// <summary>
    /// Role detail model
    /// </summary>
    public class RoleDetailModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Role identifier
        /// </summary>
        public Guid RoleID { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Function code
        /// </summary>
        public string FunctionCode { get; set; }

        /// <summary>
        /// Function name
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// View
        /// </summary>
        public bool View { get; set; } = false;

        /// <summary>
        /// Create
        /// </summary>
        public bool Add { get; set; } = false;

        /// <summary>
        /// Edit
        /// </summary>
        public bool Edit { get; set; } = false;

        /// <summary>
        /// Delete
        /// </summary>
        public bool Delete { get; set; } = false;
    }
}
