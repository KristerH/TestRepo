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
            AnmalanModel anmalan = TempData["anmalan"] as AnmalanModel;
            if (anmalan == null)
            {
                anmalan = new AnmalanModel();
                PrismaServiceClient client = new PrismaServiceClient();

                var request = new RequestMessageGetAllZones();
                var response = client.GetAllZones(request);
                anmalan.ZoneList = new List<Zone>(response.Zones);

                anmalan.BuildingList = new List<Building>();

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
        public ActionResult SelectedZoneChanged(AnmalanModel incomingAnmalan)
        {
            AnmalanModel anmalan = TempData["anmalan"] as AnmalanModel;

            if (incomingAnmalan.SelectedZoneCode == null)
            {
                anmalan.BuildingList = new List<Building>();
                anmalan.SelectedBuildingCode = null;
            }
            else
            {
                anmalan.SelectedZoneCode = incomingAnmalan.SelectedZoneCode;

                PrismaServiceClient client = new PrismaServiceClient();

                var request = new RequestMessageGetBuildings();
                request.ZoneCode = anmalan.SelectedZoneCode;

                var response = client.GetBuildings(request);

                anmalan.BuildingList = response.BuildingList;
                client.Close();
            }

            anmalan.FloorList = new List<Floor>();
            anmalan.SelectedFloorCode = null;

            anmalan.RoomList = new List<Room>();
            anmalan.SelectedRoomCode = null;

            TempData["anmalan"] = anmalan;
            return View("Index", anmalan);
        }

        [HttpPost]
        public ActionResult SelectedBuildingChanged(AnmalanModel incomingAnmalan)
        {
            AnmalanModel anmalan = TempData["anmalan"] as AnmalanModel;

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
                anmalan.SelectedBuildingCode = incomingAnmalan.SelectedBuildingCode;

                PrismaServiceClient client = new PrismaServiceClient();
                RequestMessageGetFloors request = new RequestMessageGetFloors();
                request.BuildingCode = anmalan.SelectedBuildingCode;

                //Floor
                var response = client.GetFloors(request);
                var floors = response.Floors;
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
        public ActionResult SelectedFloorChanged(AnmalanModel incomingAnmalan)
        {
            AnmalanModel anmalan = TempData["anmalan"] as AnmalanModel;
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

                RequestMessageGetFloors requestMessageGetFloors = new RequestMessageGetFloors();
                requestMessageGetFloors.BuildingCode = anmalan.SelectedBuildingCode;

                var responseMessageGetFloors = client.GetFloors(requestMessageGetFloors);
                var floors = responseMessageGetFloors.Floors;
                anmalan.FloorList = new List<Floor>(floors);
                anmalan.SelectedFloorCode = anmalan.SelectedFloorCode;

                //Room
                RequestMessageGetRooms requestMessageGetRooms = new RequestMessageGetRooms();
                requestMessageGetRooms.BuildingCode = anmalan.SelectedBuildingCode;
                requestMessageGetRooms.FloorCode = anmalan.SelectedFloorCode;
                var responseMessageGetRooms = client.GetRooms(requestMessageGetRooms);
                var rooms = responseMessageGetRooms.Rooms;
                anmalan.RoomList = new List<Room>(rooms);
                anmalan.SelectedRoomCode = null;

                client.Close();
            }

            TempData["anmalan"] = anmalan;
            return View("Index", anmalan);
        }

        [HttpPost]
        public ActionResult SelectedRoomChanged(AnmalanModel incomingAnmalan)
        {
            AnmalanModel anmalan = TempData["anmalan"] as AnmalanModel;

            anmalan.SelectedRoomCode = incomingAnmalan.SelectedRoomCode;

            TempData["anmalan"] = anmalan;
            return View("Index", anmalan);
        }

        [HttpPost]
        public ActionResult SelectedActionChanged(AnmalanModel incomingAnmalan)
        {
            AnmalanModel anmalan = TempData["anmalan"] as AnmalanModel;

            anmalan.SelectedActionCode = incomingAnmalan.SelectedActionCode;

            TempData["anmalan"] = anmalan;
            return View("Index", anmalan);
        }

        [HttpPost]
        public ActionResult DescriptionChanged(AnmalanModel incomingAnmalan)
        {
            AnmalanModel anmalan = TempData["anmalan"] as AnmalanModel;

            anmalan.WRDescription = incomingAnmalan.WRDescription;

            TempData["anmalan"] = anmalan;
            return View("Index", anmalan);
        }

        [HttpPost]
        public ActionResult TelephoneChanged(AnmalanModel incomingAnmalan)
        {
            AnmalanModel anmalan = TempData["anmalan"] as AnmalanModel;

            anmalan.TelephoneNumber = incomingAnmalan.TelephoneNumber;

            TempData["anmalan"] = anmalan;
            return View("Index", anmalan);
        }

        [HttpPost]
        public ActionResult IndexSave(AnmalanModel incomingAnmalan)
        {
            AnmalanModel anmalan = TempData["anmalan"] as AnmalanModel;

            PrismaServiceClient client = new PrismaServiceClient();
            WorkRequest workRequest = new WorkRequest
            {
                BuildingCode = anmalan.SelectedBuildingCode,
                CreatedBy = Environment.UserName,
                Description = anmalan.WRDescription,
                FloorCode = anmalan.SelectedFloorCode,
                RoomCode = anmalan.SelectedRoomCode,
                WOActionCode = anmalan.SelectedActionCode,
                Telephone = anmalan.TelephoneNumber               
            };

            client.PutWorkRequest(workRequest);
            client.Close();

            return RedirectToAction("WorkRequestView");
        }


        // GET: //
        [HttpGet]
        public virtual ActionResult WorkRequestView()
        {
            PrismaServiceClient client = new PrismaServiceClient();

            RequestMessageGetWorkRequest request = new RequestMessageGetWorkRequest();
            request.Username = Environment.UserName;
            var response = client.GetWorkRequest(request);
            WorkRequest[] arr = response.WorkRequestList;

            WorkRequestModel model = new WorkRequestModel();
            model.WorkRequestList = new List<WorkRequest>(arr);

            return View(model);
        }

        //// POST: //
        public virtual ActionResult WorkRequestRowSelected(int? wrNumber)
        {
         //if (User.Identity.IsAuthenticated)
         //{

             //TODO Här ska jag skicka användaren till en sida som visar detaljer om workrequesten
             return View("WorkRequestView");
         //}
         //else
         //{
         //    return View("Index");
         //}
        }
    }
}
