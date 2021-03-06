﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TwoToWin.Prisma.BasicWCFService.Entities
{
    [DataContract]
    public class WorkRequest
    {
        [DataMember]
        public int WORequestCode { get; set; }        

        [DataMember]
        public DateTime? CreatedDate { get; set; }

        [DataMember]
        public string BuildingCode { get; set; }

        [DataMember]
        public string FloorCode { get; set; }

        [DataMember]
        public string RoomCode { get; set; }

        [DataMember]
        public string Description { get;set ;}

        [DataMember]
        public string WOActionCode { get; set; }

        [DataMember]
        public string CreatedBy { get; set; }

        [DataMember]
        public string Telephone { get; set; }

        [DataMember]
        public int? WorkorderCode { get; set; }

        [DataMember]
        public int? WorkorderPartCode { get; set; }
    }
}