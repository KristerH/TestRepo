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
        //
        // GET: //
        [HttpGet]
        public ActionResult Index()
        {
            BasicWCF.Service1Client client = new BasicWCF.Service1Client();
            var buildings = client.GetAllBuildings();
            client.Close();
            
            Anmalan anmalan = new Anmalan();
//            anmalan.BuildingList = new List<Building>(buildings);
        
            return View(anmalan);
        }

    }
}
