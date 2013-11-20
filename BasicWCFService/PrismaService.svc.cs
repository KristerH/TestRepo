using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TwoToWin.Prisma.BasicWCFService.Datalayer;
using TwoToWin.Prisma.BasicWCFService.Entities;

namespace TwoToWin.Prisma.BasicWCFService
{
    public class PrismaService : IPrismaService
    {
        private Prisma_FastighetEntities dbPrisma;

        public PrismaService()
        {
            dbPrisma = new Prisma_FastighetEntities();
        }

        public IEnumerable<Building> GetAllBuildings()
        {
            List<Building> EntitiesBuildingList = new List<Building>();

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

        public IEnumerable<Floor> GetFloors(string buildingCode)
        {
            if (string.IsNullOrEmpty(buildingCode))
                throw new Exception("Building code cannot be empty"); //TODO gör om till ett businessexception

            List<Floor> floorList = new List<Floor>();

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

        public IEnumerable<Room> GetRooms(string buildingCode, string floorCode)
        {
            if (string.IsNullOrEmpty(buildingCode))
                throw new Exception("Building code cannot be empty"); //TODO gör om till ett businessexception

            if (string.IsNullOrEmpty(floorCode))
                throw new Exception("Floor code cannot be empty"); //TODO gör om till ett businessexception

            List<Room> roomList = new List<Room>();

            List<BLroom> blRoomList = dbPrisma.BLroom.Where(x => x.blbuilding_code == buildingCode && x.blfloor_code == floorCode).ToList();

            foreach (var blRoom in blRoomList)
            {
                Room room = new Room();
                room.RoomCode = blRoom.blroom_code;
                room.Description = blRoom.descr;
                roomList.Add(room);
            }

            return roomList;
        }

        public IEnumerable<ActionEntity> GetAllWORequestActions()
        {
            List<ActionEntity> actionList = new List<ActionEntity>();

            IEnumerable<WOaction> wOActionsList = dbPrisma.WOaction.Where(x => x.wr.Equals("1"));

            foreach (var woAction in wOActionsList)
            {
                ActionEntity actionEntity = new ActionEntity();
                actionEntity.ActionCode = woAction.woaction_code;
                actionEntity.Description = woAction.descr;
                actionList.Add(actionEntity);
            }

            return actionList;
        }
    }
}
