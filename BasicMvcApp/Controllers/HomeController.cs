using BasicMvcApp.BasicWCF;
using BasicMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicMvcApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: //
        [HttpGet]
        public ActionResult Index()
        {
            BasicWCF.Service1Client client = new BasicWCF.Service1Client();
            var buildings = client.GetAllBuildings();
            client.Close();
            
            Anmalan anmalan = new Anmalan();
            anmalan.BuildingList = new List<Building>(buildings);

            anmalan.FloorList = new List<Floor>();

            return View(anmalan);
        }

        // GET: //
        [HttpPost]
        public ActionResult Index(Anmalan incomingAnmalan)
        {
            Anmalan anmalan = new Anmalan();

            //Building
            BasicWCF.Service1Client client = new BasicWCF.Service1Client();
            var buildings = client.GetAllBuildings();
            anmalan.BuildingList = new List<Building>(buildings);
            anmalan.SelectedBuildingCode = incomingAnmalan.SelectedBuildingCode;

            //Floor
            var floors = client.GetFloors(anmalan.SelectedBuildingCode);
            anmalan.FloorList = new List<Floor>(floors);

            //TODO processa floorcode här och hämta alla rum.

            client.Close();
            return View(anmalan);
        }
    }
}
