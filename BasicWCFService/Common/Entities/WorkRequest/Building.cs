using System.Runtime.Serialization;

namespace TwoToWin.Prisma.BasicWCFService.Entities
{
    [DataContract]
    public class Building
    {
        [DataMember]
        public string BuildingCode { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}