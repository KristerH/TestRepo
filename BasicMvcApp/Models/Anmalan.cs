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
        public IEnumerable<Building> BuildingList  { get; set; }
        public string SelectedBuildingCode { get; set; }

        public IEnumerable<Floor> FloorList { get; set; }
        public string SelectedFloorCode { get; set; }    
    }
}