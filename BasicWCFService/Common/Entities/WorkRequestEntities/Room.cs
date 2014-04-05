using System.Runtime.Serialization;

namespace TwoToWin.Prisma.BasicWCFService.Entities
{
    [DataContract]
    public class Room
    {
        [DataMember]
        public string RoomCode { get; set; }

        [DataMember]
        public string Description;
    }
}