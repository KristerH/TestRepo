using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using TwoToWin.Prisma.BasicWCFService.Entities.Message;

namespace TwoToWin.Prisma.BasicWCFService
{
    public class RequestMessageGetFloors : RequestMessageBase
    {
        public string BuildingCode { get; set; }

        public override void Validate(Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResults results)
        {
            base.ValidateBase(results);

            string msg = string.Empty;
            if (String.IsNullOrEmpty(BuildingCode))
            {
                msg = "BuildingCode cannot be empty";
                results.AddResult(new ValidationResult(msg, this, "BuildingCodeSelfValidation", "", null));
            }
        }
    }
}
