using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwoToWin.Prisma.BasicWCFService.Common.Entities.Message;
using TwoToWin.Prisma.BasicWCFService.Entities;

namespace TwoToWin.Prisma.BasicWCFService
{
    public class ResponseMessageGetBuildings : ResponseMessageBase
    {
        public List<Building> BuildingList { get; set; }
    }
}
