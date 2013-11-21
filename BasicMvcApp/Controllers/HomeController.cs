using System.Reflection;
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
            Anmalan anmalan = new Anmalan();
            PrismaServiceClient client = new PrismaServiceClient();
            var buildings = client.GetAllBuildings();           
            anmalan.BuildingList = new List<Building>(buildings);

            anmalan.FloorList = new List<Floor>();
            anmalan.RoomList = new List<Room>();

            var actions = client.GetAllWORequestActions();
            anmalan.ActionList = new List<ActionEntity>(actions);
            client.Close();

            anmalan.WRDescription = "";
            
            return View(anmalan);
        }

        // POST: //
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
            if (anmalan.SelectedBuildingCode == null)
            {
                anmalan.FloorList = new List<Floor>();
            }
            else
            {
                var floors = client.GetFloors(anmalan.SelectedBuildingCode);
                anmalan.FloorList = new List<Floor>(floors);
                anmalan.SelectedFloorCode = incomingAnmalan.SelectedFloorCode;
            }

            //Room
            if(anmalan.SelectedFloorCode == null)
            {
                anmalan.RoomList = new List<Room>();
            }
            else
            {
                var rooms = client.GetRooms(anmalan.SelectedBuildingCode, anmalan.SelectedFloorCode);
                anmalan.RoomList = new List<Room>(rooms);               
            }

            //ActionCode
            var actions = client.GetAllWORequestActions();
            if (anmalan.SelectedActionCode == null)
            {
                anmalan.ActionList = new List<ActionEntity>(actions);   
            }
            else
            {
                anmalan.ActionList = new List<ActionEntity>(actions);
                anmalan.SelectedActionCode = incomingAnmalan.SelectedActionCode;
            }
            client.Close();

            anmalan.WRDescription = incomingAnmalan.WRDescription;

            return View(anmalan);
        }

        // POST: //
        [HttpPost]
        public ActionResult IndexSave(Anmalan incomingAnmalan)
        {
            string user = Environment.UserName;

            PrismaServiceClient client = new PrismaServiceClient();
            WorkRequest workRequest = new WorkRequest
                {
                  BuildingCode = incomingAnmalan.SelectedBuildingCode,
                  CreatedBy =  user,
                  Description = incomingAnmalan.WRDescription,
                  FloorCode = incomingAnmalan.SelectedFloorCode,
                  RoomCode = incomingAnmalan.SelectedRoomCode,
                  WOActionCode = incomingAnmalan.SelectedActionCode
                };

            client.PutWorkRequest(workRequest);
            client.Close();

            return RedirectToAction("Index");
        }
    }
}
