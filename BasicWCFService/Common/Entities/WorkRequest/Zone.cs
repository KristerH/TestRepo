using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TwoToWin.Prisma.BasicWCFService.Entities.Message;

namespace TwoToWin.Prisma.BasicWCFService.Entities
{
    public class Zone : EntityBase
    {
        public string ZoneCode { get; set; }
        public string Description { get; set; }
    }
}