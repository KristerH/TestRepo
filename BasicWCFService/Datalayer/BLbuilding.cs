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
    
    public partial class BLbuilding
    {
        public BLbuilding()
        {
            this.BLfloor = new HashSet<BLfloor>();
        }
    
        public string blbuilding_code { get; set; }
        public string descr { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string blproperty_code { get; set; }
        public string blstatus_code { get; set; }
        public string blcategory_code { get; set; }
        public string blzone_code { get; set; }
        public string owner { get; set; }
        public Nullable<System.DateTime> constructdate { get; set; }
        public Nullable<System.DateTime> acquireddate { get; set; }
        public Nullable<decimal> BTA { get; set; }
        public Nullable<decimal> BRA { get; set; }
        public Nullable<decimal> STA { get; set; }
        public Nullable<decimal> MA { get; set; }
        public Nullable<decimal> BTV { get; set; }
        public Nullable<decimal> BTAt { get; set; }
        public Nullable<decimal> NTA { get; set; }
        public Nullable<decimal> BYA { get; set; }
        public Nullable<decimal> BTVt { get; set; }
        public string buaccount1_code { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int tablerecnum { get; set; }
        public string inactive { get; set; }
    
        public virtual ICollection<BLfloor> BLfloor { get; set; }
    }
}