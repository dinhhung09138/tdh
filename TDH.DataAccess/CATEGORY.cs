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
    
    public partial class CATEGORY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CATEGORY()
        {
            this.HOME_CATEGORY = new HashSet<HOME_CATEGORY>();
            this.POSTs = new HashSet<POST>();
        }
    
        public System.Guid id { get; set; }
        public System.Guid navigation_id { get; set; }
        public string title { get; set; }
        public string alias { get; set; }
        public string description { get; set; }
        public bool show_on_nav { get; set; }
        public string image { get; set; }
        public short ordering { get; set; }
        public bool publish { get; set; }
        public string meta_title { get; set; }
        public string meta_description { get; set; }
        public string meta_keywords { get; set; }
        public string meta_next { get; set; }
        public string meta_og_site_name { get; set; }
        public string meta_og_image { get; set; }
        public string meta_twitter_image { get; set; }
        public string meta_article_name { get; set; }
        public string meta_article_tag { get; set; }
        public string meta_article_section { get; set; }
        public System.DateTime meta_article_publish { get; set; }
        public System.Guid create_by { get; set; }
        public System.DateTime create_date { get; set; }
        public Nullable<System.Guid> update_by { get; set; }
        public Nullable<System.DateTime> update_date { get; set; }
        public bool deleted { get; set; }
        public Nullable<System.Guid> delete_by { get; set; }
        public Nullable<System.DateTime> delete_date { get; set; }
    
        public virtual NAVIGATION NAVIGATION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOME_CATEGORY> HOME_CATEGORY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<POST> POSTs { get; set; }
    }
}
