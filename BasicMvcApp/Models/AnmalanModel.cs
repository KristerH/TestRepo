using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using BasicMvcApp.PrismaWCF;

namespace BasicMvcApp.Models

{
    public class AnmalanModel
    {
        public IEnumerable<Zone> ZoneList { get; set; }
        public string SelectedZoneCode { get; set; }

        public IEnumerable<Building> BuildingList  { get; set; }
        public string SelectedBuildingCode { get; set; }

        public IEnumerable<Floor> FloorList { get; set; }
        public string SelectedFloorCode { get; set; }

        public IEnumerable<Room> RoomList { get; set; }
        public string SelectedRoomCode { get; set; }

        public IEnumerable<ActionEntity> ActionList { get; set; }
        public string SelectedActionCode { get; set; }

        public string WRDescription { get; set; }

        public string TelephoneNumber { get; set; }
    }
}