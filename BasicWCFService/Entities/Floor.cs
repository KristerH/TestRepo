using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicWCFService.Entities
{
    public class Floor
    {
        public string BuildingCode { get; set; }
        public string FloorCode { get; set; }
        public string Description { get; set; }
    }
}