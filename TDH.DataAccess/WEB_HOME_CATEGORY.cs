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
    
    public partial class WEB_HOME_CATEGORY
    {
        public System.Guid id { get; set; }
        public System.Guid category_id { get; set; }
        public short ordering { get; set; }
    
        public virtual WEB_CATEGORY WEB_CATEGORY { get; set; }
    }
}
