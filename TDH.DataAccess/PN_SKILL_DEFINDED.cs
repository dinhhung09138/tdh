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
    
    public partial class PN_SKILL_DEFINDED
    {
        public System.Guid defined_id { get; set; }
        public System.Guid skill_id { get; set; }
        public short point { get; set; }
    
        public virtual PN_SKILL PN_SKILL { get; set; }
    }
}