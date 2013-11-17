using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BasicWCFService.Datalayer;
using BasicWCFService.Entities;

namespace BasicWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public List<Building> GetAllBuildings()
        {
            List<Building> EntitiesBuildingList = new List<Building>();

            Prisma_FastighetEntities dbPrisma = new Prisma_FastighetEntities();
            var EFBuildingList = dbPrisma.BLbuilding.ToList();

            foreach (var EFbuilding in EFBuildingList)
            {
                Building building = new Building
                    {
                        BuildingCode = EFbuilding.blbuilding_code,
                        Description = EFbuilding.descr
                    };

                EntitiesBuildingList.Add(building);
            }

            return EntitiesBuildingList;
        }

        public List<Floor> GetFloors(string buildingCode)
        {
            if (string.IsNullOrEmpty(buildingCode))
                throw new Exception("Building code cannot be empty"); //Kan göras om till ett businessexception

            List<Floor> floorList = new List<Floor>();

            Prisma_FastighetEntities dbPrisma = new Prisma_FastighetEntities();
            List<BLfloor> blfloorList = dbPrisma.BLfloor.Where(x => x.blbuilding_code == buildingCode).ToList();

            foreach (var blFloor in blfloorList)
            {
                Floor floor = new Floor();
                floor.BuildingCode = blFloor.blbuilding_code;
                floor.FloorCode = blFloor.blfloor_code;
                floor.Description = blFloor.descr;
                floorList.Add(floor);
            }

            return floorList;
        }
    }
}
