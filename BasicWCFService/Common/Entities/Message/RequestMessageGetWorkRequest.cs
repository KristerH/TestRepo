using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwoToWin.Prisma.BasicWCFService.Entities.Message
{
    public class RequestMessageGetWorkRequest : RequestMessageBase
    {
        public string Username { get; set; }

        public override void Validate(Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResults results)
        {
            //TODO
        }
    }
}
