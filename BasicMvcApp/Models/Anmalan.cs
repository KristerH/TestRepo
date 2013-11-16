using BasicMvcApp.BasicWCF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BasicMvcApp.Models

{

    public class Contrib
    {
        public int ContribId { get; set; }
        public string Value { get; set; }
    }




    public class Anmalan
    {

        public class Contrib
        {
            public int ContribId { get; set; }
            public string Value { get; set; }
        }

        public IEnumerable<Contrib> ContribTypeOptions =
            new List<Contrib>
        {
            new Contrib {ContribId = 0, Value = "Payroll Deduction"},
            new Contrib {ContribId = 1, Value = "Bill Me"}
        };

        [DisplayName("Contribution Type")]
        public string ContribType { get; set; }

        //public IEnumerable<Building> BuildinListOptions = new List<Building>
        //{
        //    new Building {Building_Code = "0", Description = "Payroll Deduction"},
        //    new Building {Building_Code = "1", Description = "Bill Me"}
        //};

            
            //BuildingList;

        public List<Building> BuildingList  { get; set; } 
        
    }
}