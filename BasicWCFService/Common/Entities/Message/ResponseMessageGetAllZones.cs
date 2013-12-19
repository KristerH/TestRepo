using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwoToWin.Prisma.BasicWCFService.Common.Entities.Message;

namespace TwoToWin.Prisma.BasicWCFService.Entities.Message
{
    public class ResponseMessageGetAllZones : ResponseMessageBase
    {
        public List<Zone> Zones { get; set; }
    }
}
