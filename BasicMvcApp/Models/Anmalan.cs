using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using BasicMvcApp.PrismaWCF;

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

        public IEnumerable<Room> RoomList { get; set; }
        public string SelectedRoomCode { get; set; }   
    }
}