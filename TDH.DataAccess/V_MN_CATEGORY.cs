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
    
    public partial class V_MN_CATEGORY
    {
        public System.Guid id { get; set; }
        public string name { get; set; }
        public string notes { get; set; }
        public System.Guid group_id { get; set; }
        public string group_name { get; set; }
        public bool is_input { get; set; }
        public Nullable<System.Guid> setting_id { get; set; }
        public decimal year_month { get; set; }
        public Nullable<int> year { get; set; }
        public Nullable<int> month { get; set; }
        public byte percent_setting { get; set; }
        public byte percent_current { get; set; }
        public decimal money_setting { get; set; }
        public decimal money_current { get; set; }
        public decimal current_income { get; set; }
        public decimal current_payment { get; set; }
        public short ordering { get; set; }
        public bool publish { get; set; }
        public System.Guid create_by { get; set; }
    }
}
