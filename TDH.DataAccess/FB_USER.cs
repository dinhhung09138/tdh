//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TDH.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class FB_USER
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string auth_token { get; set; }
        public System.DateTime start_on { get; set; }
        public System.DateTime last_execute { get; set; }
        public System.DateTime expires_on { get; set; }
        public short ordering { get; set; }
        public bool publish { get; set; }
        public System.Guid created_by { get; set; }
        public System.DateTime created_date { get; set; }
        public Nullable<System.Guid> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public bool deleted { get; set; }
        public Nullable<System.Guid> deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_date { get; set; }
    
        public virtual SYS_USER SYS_USER { get; set; }
    }
}
