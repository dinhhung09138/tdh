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
    
    public partial class SYS_USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SYS_USER()
        {
            this.SYS_USER_ROLE = new HashSet<SYS_USER_ROLE>();
            this.FB_FANPAGE = new HashSet<FB_FANPAGE>();
            this.FB_GROUP = new HashSet<FB_GROUP>();
            this.FB_USER = new HashSet<FB_USER>();
        }
    
        public System.Guid id { get; set; }
        public string full_name { get; set; }
        public string password { get; set; }
        public bool locked { get; set; }
        public string notes { get; set; }
        public Nullable<System.DateTime> last_login { get; set; }
        public System.Guid create_by { get; set; }
        public System.DateTime create_date { get; set; }
        public Nullable<System.Guid> update_by { get; set; }
        public Nullable<System.DateTime> update_date { get; set; }
        public bool deleted { get; set; }
        public Nullable<System.Guid> delete_by { get; set; }
        public Nullable<System.DateTime> delete_date { get; set; }
        public string user_name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SYS_USER_ROLE> SYS_USER_ROLE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FB_FANPAGE> FB_FANPAGE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FB_GROUP> FB_GROUP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FB_USER> FB_USER { get; set; }
    }
}
