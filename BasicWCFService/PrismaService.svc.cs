

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using TwoToWin.Prisma.BasicWCFService.Common.Entities.Message;
using TwoToWin.Prisma.BasicWCFService.Common.Logging;
using TwoToWin.Prisma.BasicWCFService.Common.Serialize;
using TwoToWin.Prisma.BasicWCFService.Datalayer;
using TwoToWin.Prisma.BasicWCFService.Entities;
using TwoToWin.Prisma.BasicWCFService.Entities.Message;

namespace TwoToWin.Prisma.BasicWCFService
{
    /// <summary>
    /// En testkommentar
    /// </summary>
    public class PrismaService : IPrismaService
    {
        private Prisma_FastighetEntities dbPrisma;

        public PrismaService()
        {
            dbPrisma = new Prisma_FastighetEntities();

            //var listener = new ObservableEventListener();
            //listener.EnableEvents(BasicLogger.Log, System.Diagnostics.Tracing.EventLevel.LogAlways, Keywords.All);
            //listener.LogToFlatFile(@"c:\temp\log2.txt");
            
            //var connStr = System.Configuration.ConfigurationManager.ConnectionStrings["LoggingDB"].ConnectionString;
            //listener.LogToSqlDatabase("Prisma",connStr);
            
            var sqlListener = SqlDatabaseLog.CreateListener("PrismaWCF", "Data Source=.\\sqlexpress;Initial Catalog=Logging;Integrated Security=True");
            sqlListener.EnableEvents(BasicLogger.Log, System.Diagnostics.Tracing.EventLevel.Informational);
        }

        public ResponseMessageGetAllZones GetAllZones(RequestMessageGetAllZones request)
        {
            //Create request
            ResponseMessageGetAllZones response = new ResponseMessageGetAllZones();
            response.Zones = new List<Zone>();

            //Log request
            var str = ObjectSerializer.SerializeObject(request);
            BasicLogger.Log.Info(str);

            //Validate request workflow
            ValidationResults validationResults = Validation.Validate(request);

            if (!validationResults.IsValid)
            {
                StringBuilder errorString = new StringBuilder();
                errorString.AppendLine(String.Format("The following {0} validation errors were detected:", validationResults.Count));

                foreach (Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult item in validationResults)
                {
                    errorString.AppendLine(String.Format("Target:'{0}' Key:'{1}' Tag:'{2}' Message:'{3}'", item.Target, item.Key, item.Tag, item.Message));
                }

                BasicLogger.Log.Critical(errorString.ToString());

                response.Status = ResponseMessageBase.MessageStatus.FAILURE;
                response.Errors = new MessageErrorList();
                foreach (var validationResult in validationResults)
                {
                    //TODO skapa en constructor i MessageError som tar in ett validationresult objekt
                    response.Errors.Add(new MessageError
                        {
                            ErrorMessage = validationResult.Message,
                            ErrorNumber = validationResult.Key,
                            Severity = MessageError.ErrorSeverity.Error,
                            Type = MessageError.ErrorType.ValidationError
                        } );
                }

                return response;
            }

            //TODO flytta till ett busineslayer
            //Execute get building zones workflow
            foreach (var item in dbPrisma.BLzone.ToList())
            {
                Zone zone = new Zone();
                zone.Description = item.descr;
                zone.ZoneCode = item.blzone_code;

                response.Zones.Add(zone);
            }

            //Log response
            str = ObjectSerializer.SerializeObject(response);
            BasicLogger.Log.Info(str);

            return response;
        }

        public ResponseMessageGetBuildings GetBuildings(RequestMessageGetBuildings request)
        {
            var str = ObjectSerializer.SerializeObject(request);           
            BasicLogger.Log.Info(str);

            ValidationResults results = Validation.Validate(request);

            if (!results.IsValid)
            {
                StringBuilder errorString = new StringBuilder();
                errorString.AppendLine(String.Format("The following {0} validation errors were detected:", results.Count));

                foreach (Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult item in results)
                {
                    errorString.AppendLine(String.Format("Target:'{0}' Key:'{1}' Tag:'{2}' Message:'{3}'", item.Target, item.Key, item.Tag, item.Message));
                }

                BasicLogger.Log.Critical(errorString.ToString());

                //TODO sätt i responsen att det inte gick bra och retunera responsen
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

            str = ObjectSerializer.SerializeObject(response);
            BasicLogger.Log.Info(str);

            return response;
        }

        public ResponseMessageGetFloors GetFloors(RequestMessageGetFloors request)
        {
            ResponseMessageGetFloors response = new ResponseMessageGetFloors();

            //TODO gör om till en validator
            if (string.IsNullOrEmpty(request.BuildingCode))
                throw new ValidationException("Building code cannot be empty"); 

            List<Floor> floorList = new List<Floor>();

            List<BLfloor> blfloorList = dbPrisma.BLfloor.Where(x => x.blbuilding_code == request.BuildingCode).ToList();

            foreach (var blFloor in blfloorList)
            {
                Floor floor = new Floor();
                floor.BuildingCode = blFloor.blbuilding_code;
                floor.FloorCode = blFloor.blfloor_code;
                floor.Description = blFloor.descr;
                floorList.Add(floor);
            }

            response.Floors = floorList;
            return response;
        }

        public ResponseMessageGetRooms GetRooms(RequestMessageGetRooms request)
        {
            ResponseMessageGetRooms response = new ResponseMessageGetRooms();

            if (string.IsNullOrEmpty(request.BuildingCode))
                throw new Exception("Building code cannot be empty"); //TODO gör om till ett businessexception

            if (string.IsNullOrEmpty(request.FloorCode))
                throw new Exception("Floor code cannot be empty"); //TODO gör om till ett businessexception

            List<Room> roomList = new List<Room>();

            List<BLroom> blRoomList = dbPrisma.BLroom.Where(x => x.blbuilding_code == request.BuildingCode && x.blfloor_code == request.FloorCode).ToList();

            foreach (var blRoom in blRoomList)
            {
                Room room = new Room();
                room.RoomCode = blRoom.blroom_code;
                room.Description = blRoom.descr;
                roomList.Add(room);
            }

            response.Rooms = roomList;

            return response;
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

        public ResponseMessageGetWorkRequest GetWorkRequest(RequestMessageGetWorkRequest request)
        {
            List<WorkRequest> workRequestList = new List<WorkRequest>();

            IEnumerable<WOrequest> woRequestList = dbPrisma.WOrequest.Where(x => x.createdby.Equals(request.Username));
            //TODO: Entitytranslator
            foreach (var req in woRequestList)
            {
                WorkRequest workRequest = new WorkRequest();
                workRequest.BuildingCode = req.blbuilding_code;
                workRequest.CreatedBy = req.createdby;
                workRequest.CreatedDate = req.createddate;
                workRequest.Description = req.descr;
                workRequest.FloorCode = req.blfloor_code;
                workRequest.RoomCode = req.blroom_code;
                workRequest.WOActionCode = req.woaction_code;
                workRequest.WORequestCode = req.worequest_code;
                workRequest.WorkorderCode = req.wo_code;
                workRequest.WorkorderPartCode = req.partwo_code;
                workRequestList.Add(workRequest);
            }

            ResponseMessageGetWorkRequest response = new ResponseMessageGetWorkRequest();
            response.WorkRequestList = workRequestList;

            return response;
        }
    }
}
