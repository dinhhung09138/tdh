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
    
    public partial class MN_TRANSFER
    {
        public System.Guid id { get; set; }
        public System.Guid account_from { get; set; }
        public System.Guid account_to { get; set; }
        public string title { get; set; }
        public string purpose { get; set; }
        public string notes { get; set; }
        public decimal money { get; set; }
        public decimal fee { get; set; }
        public System.Guid create_by { get; set; }
        public System.DateTime create_date { get; set; }
        public Nullable<System.Guid> update_by { get; set; }
        public Nullable<System.DateTime> update_date { get; set; }
        public bool deleted { get; set; }
        public Nullable<System.Guid> delete_by { get; set; }
        public Nullable<System.DateTime> delete_date { get; set; }
        public System.DateTime date { get; set; }
    
        public virtual MN_ACCOUNT MN_ACCOUNT { get; set; }
        public virtual MN_ACCOUNT MN_ACCOUNT1 { get; set; }
    }
}
