//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BasicWCFService.Datalayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class BLfloor
    {
        public BLfloor()
        {
            this.BLroom = new HashSet<BLroom>();
        }
    
        public string blbuilding_code { get; set; }
        public string blfloor_code { get; set; }
        public string descr { get; set; }
        public System.DateTime UpdateDate { get; set; }
    
        public virtual BLbuilding BLbuilding { get; set; }
        public virtual ICollection<BLroom> BLroom { get; set; }
    }
}