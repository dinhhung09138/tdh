//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TDH.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TARGET
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TARGET()
        {
            this.TARGET1 = new HashSet<TARGET>();
            this.TARGET_TASK = new HashSet<TARGET_TASK>();
        }
    
        public System.Guid id { get; set; }
        public Nullable<System.Guid> parent_id { get; set; }
        public string title { get; set; }
        public string result { get; set; }
        public bool done { get; set; }
        public System.DateTime estimate_date { get; set; }
        public Nullable<System.DateTime> finish_date { get; set; }
        public short task_count { get; set; }
        public short level { get; set; }
        public System.Guid create_by { get; set; }
        public System.DateTime create_date { get; set; }
        public Nullable<System.Guid> update_by { get; set; }
        public Nullable<System.DateTime> update_date { get; set; }
        public bool deleted { get; set; }
        public Nullable<System.Guid> delete_by { get; set; }
        public Nullable<System.DateTime> delete_date { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TARGET> TARGET1 { get; set; }
        public virtual TARGET TARGET2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TARGET_TASK> TARGET_TASK { get; set; }
    }
}
