//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MediaManager
{
    using System;
    using System.Collections.Generic;
    
    public partial class PT_Relation
    {
        public int PartId { get; set; }
        public int TagId { get; set; }
        public bool Value { get; set; }
    
        public virtual Part Part { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
