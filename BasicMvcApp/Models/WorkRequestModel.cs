using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasicMvcApp.PrismaWCF;

namespace BasicMvcApp.Models
{
    public class WorkRequestModel
    {
        public IEnumerable<WorkRequest> WorkRequestList { get; set; }
    }
}