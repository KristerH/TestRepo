using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace TwoToWin.Prisma.BasicWCFService.Entities.Message
{
    [DataContract]
    [HasSelfValidation]
    public abstract class RequestMessageBase: IRequestMessage
    {
        private ChannelType channel = ChannelType.Intranet;

        [DataMember] public Context Context { get; set; }
        [DataMember] public RequestMessageBase.ChannelType Channel { get; set; }
        [DataMember] public DateTime TimeStamp { get; set; }
        [DataMember] public string SystemCode { get; set; }        
        [DataMember] public string ServerName{ get; set; }
        [DataMember] public RequestMessageBase.Environment EnvironmentId{ get; set; }
        [DataMember] public string ProgramName { get; set; }
        [DataMember] public string TransactionId { get; set; }
        [DataMember] public string Version { get; set; }
        [DataMember] public string MessageId { get; set; }
 
        [DataContract]
        public enum Environment
        {
            [EnumMember]
            NotSet = 0,
            [EnumMember]
            PT = 10,
            [EnumMember]
            ST = 20,
            [EnumMember]
            SST = 30,
            [EnumMember]
            AT = 40,
            [EnumMember]
            PROD = 20
        };

        [DataContract]
        public enum ChannelType
        {
            [EnumMember]
            NotSet = 0,
            [EnumMember]
            Intranet = 10,
            [EnumMember]
            Internet = 20
        };

        [SelfValidation]
        public abstract void Validate(ValidationResults results);

        [SelfValidation]
        public void ValidateBase(ValidationResults results)
        {
            string msg = string.Empty;
            if (Context == null)
            {
                msg = "Context cannot be empty";
                results.AddResult(new ValidationResult(msg, this, "ContextSelfValidation", "", null));
            }
        }

        //    Dim classname As String = "RequestMessage"

        //    If Me.Context Is Nothing Then
        //        results.AddResult(New ValidationResult("Context måste anges", Me, "Context", classname, Nothing))
        //    End If

        //    If Not Me.Context Is Nothing Then
        //        Dim r As ValidationResults = Validation.Validate(Me.Context)
        //        If r.IsValid = False Then results.AddAllResults(r)
        //    End If

        //    If Me.Channel = Nothing Then
        //        results.AddResult(New ValidationResult("Channel måste anges", Me, "Channel", classname, Nothing))
        //    End If

        //    If Me.TimeStamp = Nothing Then
        //        results.AddResult(New ValidationResult("Timestamp måste anges", Me, "TimeStamp", classname, Nothing))
        //    End If

        //    If Me.SystemCode = Nothing Then
        //        results.AddResult(New ValidationResult("SystemCode måste anges", Me, "SystemCode", classname, Nothing))
        //    End If

        //    If Me.ServerName = Nothing Then
        //        results.AddResult(New ValidationResult("ServerName måste anges", Me, "ServerName", classname, Nothing))
        //    End If

        //    If Me.EnvironmentId = Nothing Then
        //        results.AddResult(New ValidationResult("EnvironmentId måste anges", Me, "EnvironmentId", classname, Nothing))
        //    End If

        //    If Me.ProgramName = Nothing Then
        //        results.AddResult(New ValidationResult("Programname måste anges", Me, "ProgramName", classname, Nothing))
        //    End If
        //End Sub
    }
}