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
    
    public partial class V_MN_ACCOUNT_HISTORY
    {
        public System.Guid account_id { get; set; }
        public string title { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<decimal> money { get; set; }
        public int type { get; set; }
        public System.Guid create_by { get; set; }
    }
}
