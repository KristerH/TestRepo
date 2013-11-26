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
        public virtual ActionResult Index()
        {
            Anmalan anmalan = TempData["anmalan"] as Anmalan;
            if (anmalan == null)
            {
                anmalan = new Anmalan();
                PrismaServiceClient client = new PrismaServiceClient();
                var buildings = client.GetAllBuildings();
                anmalan.BuildingList = new List<Building>(buildings);

                anmalan.FloorList = new List<Floor>();
                anmalan.RoomList = new List<Room>();

                var actions = client.GetAllWORequestActions();
                anmalan.ActionList = new List<ActionEntity>(actions);
                client.Close();

                anmalan.WRDescription = "";
            }

            TempData["anmalan"] = anmalan;
            return View(anmalan);
        }

        // POST: //
        //[HttpPost]
        //public ActionResult Index(Anmalan incomingAnmalan)
        //{
        //    Anmalan anmalan = new Anmalan();
        //    PrismaServiceClient client = new PrismaServiceClient();

        //    //Building
        //    var buildings = client.GetAllBuildings();
        //    anmalan.BuildingList = new List<Building>(buildings);
        //    anmalan.SelectedBuildingCode = incomingAnmalan.SelectedBuildingCode;

        //    //Floor
        //    if (anmalan.SelectedBuildingCode == null)
        //    {
        //        anmalan.FloorList = new List<Floor>();
        //    }
        //    else
        //    {
        //        var floors = client.GetFloors(anmalan.SelectedBuildingCode);
        //        anmalan.FloorList = new List<Floor>(floors);
        //        anmalan.SelectedFloorCode = incomingAnmalan.SelectedFloorCode;
        //    }

        //    //Room
        //    if(anmalan.SelectedFloorCode == null)
        //    {
        //        anmalan.RoomList = new List<Room>();
        //    }
        //    else
        //    {
        //        var rooms = client.GetRooms(anmalan.SelectedBuildingCode, anmalan.SelectedFloorCode);
        //        anmalan.RoomList = new List<Room>(rooms);               
        //    }

        //    //ActionCode
        //    var actions = client.GetAllWORequestActions();
        //    if (anmalan.SelectedActionCode == null)
        //    {
        //        anmalan.ActionList = new List<ActionEntity>(actions);   
        //    }
        //    else
        //    {
        //        anmalan.ActionList = new List<ActionEntity>(actions);
        //        anmalan.SelectedActionCode = incomingAnmalan.SelectedActionCode;
        //    }
        //    client.Close();

        //    anmalan.WRDescription = incomingAnmalan.WRDescription;

        //    return View(anmalan);
        //}

        [HttpPost]
        public ActionResult SelectedBuildingChanged(Anmalan incomingAnmalan)
        {
            Anmalan anmalan = TempData["anmalan"] as Anmalan;

            if (incomingAnmalan.SelectedBuildingCode == null)
            {
                anmalan.SelectedBuildingCode = null;

                anmalan.FloorList = new List<Floor>();
                anmalan.SelectedFloorCode = null;

                anmalan.RoomList = new List<Room>();
                anmalan.SelectedRoomCode = null;
            }
            else
            {
                PrismaServiceClient client = new PrismaServiceClient();

                anmalan.SelectedBuildingCode = incomingAnmalan.SelectedBuildingCode;

                //Floor
                var floors = client.GetFloors(anmalan.SelectedBuildingCode);
                anmalan.FloorList = new List<Floor>(floors);
                anmalan.SelectedFloorCode = null;

                //Room
                anmalan.RoomList = new List<Room>();
                anmalan.SelectedRoomCode = null;

                client.Close();
            }

            TempData["anmalan"] = anmalan;
            return View("Index", anmalan);
        }

        [HttpPost]
        public ActionResult SelectedFloorChanged(Anmalan incomingAnmalan)
        {
            Anmalan anmalan = TempData["anmalan"] as Anmalan;
            anmalan.SelectedFloorCode = incomingAnmalan.SelectedFloorCode;
            
            //Floor
            if (anmalan.SelectedFloorCode == null)
            {
                anmalan.SelectedFloorCode = null;

                anmalan.RoomList = new List<Room>();
                anmalan.SelectedRoomCode = null;
            }
            else
            {
                PrismaServiceClient client = new PrismaServiceClient();

                var floors = client.GetFloors(anmalan.SelectedBuildingCode);
                anmalan.FloorList = new List<Floor>(floors);
                anmalan.SelectedFloorCode = anmalan.SelectedFloorCode;

                //Room
                var rooms = client.GetRooms(anmalan.SelectedBuildingCode, anmalan.SelectedFloorCode);
                anmalan.RoomList = new List<Room>(rooms);
                anmalan.SelectedRoomCode = null;

                client.Close();
            }

            TempData["anmalan"] = anmalan;
            return View("Index", anmalan);
        }

        [HttpPost]
        public ActionResult SelectedRoomChanged(Anmalan incomingAnmalan)
        {
            Anmalan anmalan = TempData["anmalan"] as Anmalan;

            anmalan.SelectedRoomCode = incomingAnmalan.SelectedRoomCode;

            TempData["anmalan"] = anmalan;
            return View("Index", anmalan);
        }

        [HttpPost]
        public ActionResult SelectedActionChanged(Anmalan incomingAnmalan)
        {
            Anmalan anmalan = TempData["anmalan"] as Anmalan;

            anmalan.SelectedActionCode = incomingAnmalan.SelectedActionCode;

            TempData["anmalan"] = anmalan;
            return View("Index", anmalan);
        }

        [HttpPost]
        public ActionResult SelectedDescriptionChanged(Anmalan incomingAnmalan)
        {
            Anmalan anmalan = TempData["anmalan"] as Anmalan;

            anmalan.WRDescription = incomingAnmalan.WRDescription;

            TempData["anmalan"] = anmalan;
            return View("Index", anmalan);
        }

        // POST: //
        [HttpPost]
        public ActionResult IndexSave(Anmalan incomingAnmalan)
        {
            Anmalan anmalan = TempData["anmalan"] as Anmalan;

            PrismaServiceClient client = new PrismaServiceClient();
            WorkRequest workRequest = new WorkRequest
            {
                BuildingCode = anmalan.SelectedBuildingCode,
                CreatedBy = Environment.UserName,
                Description = anmalan.WRDescription,
                FloorCode = anmalan.SelectedFloorCode,
                RoomCode = anmalan.SelectedRoomCode,
                WOActionCode = anmalan.SelectedActionCode
            };

            client.PutWorkRequest(workRequest);
            client.Close();

            return RedirectToAction("Index");
        }
    }
}
