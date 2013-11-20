using System;
using System.Collections.Generic;
using System.ServiceModel;
using TwoToWin.Prisma.BasicWCFService.Entities;

namespace TwoToWin.Prisma.BasicWCFService
{
    [ServiceContract]
    public interface IPrismaService
    {
        // TODO: Add your service operations here
        [OperationContract]
        IEnumerable<Building> GetAllBuildings();

        [OperationContract]
        IEnumerable<Floor> GetFloors(String buildingCode);

        [OperationContract]
        IEnumerable<Room> GetRooms(string buildingCode, string floorCode);

        [OperationContract]
        IEnumerable<ActionEntity> GetAllWORequestActions();
    }
}
