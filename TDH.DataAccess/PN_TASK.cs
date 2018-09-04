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
    
    public partial class PN_TASK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PN_TASK()
        {
            this.PN_TASK1 = new HashSet<PN_TASK>();
        }
    
        public System.Guid id { get; set; }
        public System.Guid sprint_id { get; set; }
        public System.Guid task_status_id { get; set; }
        public System.Guid task_type_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Nullable<System.DateTime> start_date { get; set; }
        public Nullable<System.DateTime> end_date { get; set; }
        public Nullable<System.Guid> parent_id { get; set; }
        public short ordering { get; set; }
        public bool publish { get; set; }
        public System.Guid created_by { get; set; }
        public System.DateTime created_date { get; set; }
        public Nullable<System.Guid> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public bool deleted { get; set; }
        public Nullable<System.Guid> deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_date { get; set; }
    
        public virtual PN_TARGET_PLANNING_SPRINT PN_TARGET_PLANNING_SPRINT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PN_TASK> PN_TASK1 { get; set; }
        public virtual PN_TASK PN_TASK2 { get; set; }
        public virtual PN_TASK_STATUS PN_TASK_STATUS { get; set; }
        public virtual PN_TASK_TYPE PN_TASK_TYPE { get; set; }
    }
}