﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using TwoToWin.Prisma.BasicWCFService.Datalayer;
using TwoToWin.Prisma.BasicWCFService.Entities;
using TwoToWin.Prisma.BasicWCFService.Entities.Message;

namespace TwoToWin.Prisma.BasicWCFService
{
    public class PrismaService : IPrismaService
    {
        private Prisma_FastighetEntities dbPrisma;

        public PrismaService()
        {
            dbPrisma = new Prisma_FastighetEntities();
        }

        public ResponseMessageGetAllZones GetAllZones(RequestMessageGetAllZones request)
        {
            //TODO Validate request

            ResponseMessageGetAllZones response = new ResponseMessageGetAllZones();
            response.Zones = new List<Zone>();

            foreach (var item in dbPrisma.BLzone.ToList())
            {
                Zone zone = new Zone();
                zone.Description = item.descr;
                zone.ZoneCode = item.blzone_code;

                response.Zones.Add(zone);
            }

            return response;
        }

        public ResponseMessageGetBuildings GetBuildings(RequestMessageGetBuildings request)
        {
            //TODO oavsett resultat logga request-objektet

            //TODO Det här borde brytas ut till en generell valideringsfunktion
            ValidationResults results = Validation.Validate(request);

            if (!results.IsValid)
            {
                StringBuilder errorString = new StringBuilder();
                errorString.AppendLine(String.Format("The following {0} validation errors were detected:", results.Count));

                foreach (Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult item in results)
                {
                    errorString.AppendLine(String.Format("Target:'{0}' Key:'{1}' Tag:'{2}' Message:'{3}'", item.Target, item.Key, item.Tag, item.Message));
                }

                throw new ValidationException(errorString.ToString());
            }

            ResponseMessageGetBuildings response = new ResponseMessageGetBuildings();
            response.BuildingList = new List<Building>();

            var EFBuildingList = dbPrisma.BLbuilding.Where(x => x.blzone_code.Equals(request.ZoneCode)).ToList();

            foreach (var item in EFBuildingList)
            {
                Building building = new Building
                {
                    BuildingCode = item.blbuilding_code,
                    Description = item.descr
                };

                response.BuildingList.Add(building);
            }

            //TODO oavsett resultat logga response-objektet
            return response;
        }

        public IEnumerable<Floor> GetFloors(string buildingCode)
        {
            if (string.IsNullOrEmpty(buildingCode))
                throw new ValidationException("Building code cannot be empty"); //TODO gör om till en validator

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


        public bool PutWorkRequest(WorkRequest workRequest)
        {
            var maxWOCode = dbPrisma.WOrequest.Max(x => x.worequest_code);

            //TODO: Entitytranslator
            var woRequest = new WOrequest
                {
                    worequest_code = maxWOCode + 1,
                    blbuilding_code = workRequest.BuildingCode,
                    blfloor_code = workRequest.FloorCode,
                    blroom_code = workRequest.RoomCode,
                    createdby = workRequest.CreatedBy,
                    createddate = DateTime.Now,
                    descr = workRequest.Description,
                    partwo_code = 1,
                    status = "1",
                    woaction_code = workRequest.WOActionCode,
                    reqphone = workRequest.Telephone
                };

            dbPrisma.WOrequest.Add(woRequest);
            dbPrisma.SaveChanges();

            return true;
        }

        public IEnumerable<WorkRequest> GetWorkRequest(string username)
        {
            List<WorkRequest> workRequestList = new List<WorkRequest>();

            IEnumerable<WOrequest> woRequestList = dbPrisma.WOrequest.Where(x => x.createdby.Equals(username));
            //TODO: Entitytranslator
            foreach (var req in woRequestList)
            {
                WorkRequest workRequest = new WorkRequest();
                workRequest.BuildingCode = req.blbuilding_code;
                workRequest.CreatedBy = req.createdby;
                workRequest.Description = req.descr;
                workRequest.FloorCode = req.blfloor_code;
                workRequest.RoomCode = req.blroom_code;
                workRequest.WOActionCode = req.woaction_code;

                workRequestList.Add(workRequest);
            }

            return workRequestList;
        }
    }
}
