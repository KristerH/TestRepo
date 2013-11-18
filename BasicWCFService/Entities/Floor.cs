using System.Runtime.Serialization;

namespace TwoToWin.Prisma.BasicWCFService.Entities
{
    [DataContract]
    public class Floor
    {
        [DataMember]
        public string BuildingCode { get; set; }

        [DataMember]
        public string FloorCode { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}