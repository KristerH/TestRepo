using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TwoToWin.Prisma.BasicWCFService.Entities
{
    [DataContract]
    public class ActionEntity
    {
        [DataMember]
        public string ActionCode { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}