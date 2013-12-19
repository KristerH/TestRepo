using System;
using System.Collections.Generic;
using System.ServiceModel;
using TwoToWin.Prisma.BasicWCFService.Entities;
using TwoToWin.Prisma.BasicWCFService.Entities.Message;

namespace TwoToWin.Prisma.BasicWCFService
{
    [ServiceContract]
    public interface IPrismaService
    {
        [OperationContract]
        ResponseMessageGetAllZones GetAllZones(RequestMessageGetAllZones request);

        [OperationContract]
        ResponseMessageGetBuildings GetBuildings(RequestMessageGetBuildings request);

        [OperationContract]
        ResponseMessageGetFloors GetFloors(RequestMessageGetFloors request);

        [OperationContract]
        ResponseMessageGetRooms GetRooms(RequestMessageGetRooms request);

        [OperationContract]
        IEnumerable<ActionEntity> GetAllWORequestActions();

        [OperationContract]
        bool PutWorkRequest(WorkRequest workRequest);

        [OperationContract]
        IEnumerable<WorkRequest> GetWorkRequest(string username);
    }
}
