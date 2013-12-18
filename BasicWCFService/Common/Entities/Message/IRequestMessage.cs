using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoToWin.Prisma.BasicWCFService.Entities.Message
{
    interface IRequestMessage
    {
        Context Context { get; set; }
        RequestMessageBase.ChannelType Channel { get; set; }
        DateTime TimeStamp { get; set; }
        string SystemCode { get; set; }
        string ServerName { get; set; }
        RequestMessageBase.Environment EnvironmentId { get; set; }
        string ProgramName { get; set; }
        string TransactionId { get; set; }
        string Version { get; set; }
        string MessageId { get; set; }
    }
}
