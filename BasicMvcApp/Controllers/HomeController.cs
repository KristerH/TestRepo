using BasicMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicMvcApp.PrismaWCF;

namespace BasicMvcApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: //
        [HttpGet]
        public ActionResult Index()
        {

            PrismaServiceClient client = new PrismaServiceClient();
            var buildings = client.GetAllBuildings();
            client.Close();
            
            Anmalan anmalan = new Anmalan();
            anmalan.BuildingList = new List<Building>(buildings);

            anmalan.FloorList = new List<Floor>();
            anmalan.RoomList = new List<Room>();

            return View(anmalan);
        }

        // GET: //
        [HttpPost]
        public ActionResult Index(Anmalan incomingAnmalan)
        {
            Anmalan anmalan = new Anmalan();

            //Building
            PrismaServiceClient client = new PrismaServiceClient();
            var buildings = client.GetAllBuildings();
            anmalan.BuildingList = new List<Building>(buildings);
            anmalan.SelectedBuildingCode = incomingAnmalan.SelectedBuildingCode;

            //Floor
            if (anmalan.SelectedBuildingCode != null)
            {
                var floors = client.GetFloors(anmalan.SelectedBuildingCode);
                anmalan.FloorList = new List<Floor>(floors);
                anmalan.SelectedFloorCode = incomingAnmalan.SelectedFloorCode;
            }
            else
            {
                anmalan.FloorList = new List<Floor>();
            }

            if(anmalan.SelectedFloorCode != null)
            {
                var rooms = client.GetRooms(anmalan.SelectedBuildingCode, anmalan.SelectedFloorCode);
                anmalan.RoomList = new List<Room>(rooms);
            }
            else
            {
                anmalan.RoomList = new List<Room>();
            }

            client.Close();
            return View(anmalan);
        }
    }
}
