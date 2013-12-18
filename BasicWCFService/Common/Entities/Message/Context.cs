using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TwoToWin.Prisma.BasicWCFService.Entities.Message
{
    [DataContract]
    public class Context: EntityBase
    {
        private string contextID;

        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string RequestTransactionID { get; set; }

        [DataMember]
        public string ContextID
        {
            get { return contextID ?? (contextID = Guid.NewGuid().ToString()); }
        }
    }
}