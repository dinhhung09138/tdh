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
    
    public partial class PN_TARGET
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PN_TARGET()
        {
            this.PN_TARGET_PLANNING = new HashSet<PN_TARGET_PLANNING>();
            this.PN_TARGET_QUESTION = new HashSet<PN_TARGET_QUESTION>();
        }
    
        public System.Guid id { get; set; }
        public string name { get; set; }
        public string content { get; set; }
        public System.Guid target_type_id { get; set; }
        public System.Guid idea_id { get; set; }
        public System.Guid priority_id { get; set; }
        public short ordering { get; set; }
        public bool publish { get; set; }
        public System.Guid created_by { get; set; }
        public System.DateTime created_date { get; set; }
        public Nullable<System.Guid> updated_by { get; set; }
        public Nullable<System.DateTime> updated_date { get; set; }
        public bool deleted { get; set; }
        public Nullable<System.Guid> deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_date { get; set; }
    
        public virtual PN_IDEA PN_IDEA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PN_TARGET_PLANNING> PN_TARGET_PLANNING { get; set; }
        public virtual PN_TARGET_PRIORITY PN_TARGET_PRIORITY { get; set; }
        public virtual PN_TARGET_TYPE PN_TARGET_TYPE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PN_TARGET_QUESTION> PN_TARGET_QUESTION { get; set; }
    }
}