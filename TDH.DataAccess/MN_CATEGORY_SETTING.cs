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
    
    public partial class MN_CATEGORY_SETTING
    {
        public System.Guid id { get; set; }
        public System.Guid category_id { get; set; }
        public decimal year_month { get; set; }
        public byte percent_setting { get; set; }
        public byte percent_current { get; set; }
        public decimal money_setting { get; set; }
        public decimal money_current { get; set; }
        public System.Guid create_by { get; set; }
        public System.DateTime create_date { get; set; }
        public Nullable<System.Guid> update_by { get; set; }
        public Nullable<System.DateTime> update_date { get; set; }
        public bool deleted { get; set; }
        public Nullable<System.Guid> delete_by { get; set; }
        public Nullable<System.DateTime> delete_date { get; set; }
    
        public virtual MN_CATEGORY MN_CATEGORY { get; set; }
    }
}
