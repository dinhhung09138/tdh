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
    
    public partial class PN_REPORT
    {
        public System.Guid id { get; set; }
        public System.DateTime date { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public System.Guid kind_id { get; set; }
        public short ordering { get; set; }
        public bool publish { get; set; }
        public System.Guid created_by { get; set; }
        public System.DateTime created_date { get; set; }
        public Nullable<System.Guid> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public bool deleted { get; set; }
        public Nullable<System.Guid> deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_date { get; set; }
    
        public virtual PN_REPORT_KIND PN_REPORT_KIND { get; set; }
    }
}
