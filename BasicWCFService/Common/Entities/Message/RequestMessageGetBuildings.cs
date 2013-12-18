using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using TwoToWin.Prisma.BasicWCFService.Entities.Message;

namespace TwoToWin.Prisma.BasicWCFService
{
    [DataContract]
    [HasSelfValidation]
    public class RequestMessageGetBuildings : RequestMessageBase
    {
        [DataMember]
        public string ZoneCode { get; set; }

        //[SelfValidation]
        //public void Validate(ValidationResults results)
        //{
        //    string msg = string.Empty;
        //    if (String.IsNullOrEmpty(ZoneCode))
        //    {
        //        msg = "ZoneCode cannot be empty";
        //        results.AddResult(new ValidationResult(msg, this, "ZoneCodeSelfValidation", "", null));
        //    }
        //}

        [SelfValidation]
        public override void Validate(ValidationResults results)
        {
            string msg = string.Empty;
            if (String.IsNullOrEmpty(ZoneCode))
            {
                msg = "ZoneCode cannot be empty";
                results.AddResult(new ValidationResult(msg, this, "ZoneCodeSelfValidation", "", null));
            }
        }
    }
}
