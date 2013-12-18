using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TwoToWin.Prisma.BasicWCFService.Entities.Message
{

    [DataContract]
    public class RequestMessageGetAllZones : RequestMessageBase
    {
        public override void Validate(Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResults results)
        {
            throw new NotImplementedException();
        }
    }
}
