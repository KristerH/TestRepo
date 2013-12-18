using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TwoToWin.Prisma.BasicWCFService.Common.Entities.Message
{
    public class ResponseMessageBase //Implements IResponseMessage
    {
        //private MessageErrorList m_Errors;
        private string m_transactionId;

        [DataMember]
        public string TransactionId
        {
            get { return m_transactionId; }
            set { m_transactionId = value; }
        }

        [DataMember(IsRequired = true)]
        public MessageStatus Status { get; set; }

        [DataMember(IsRequired = true)]
        public MessageErrorList Errors { get; set; }

        [DataContract]
        public enum MessageStatus
        {
            [EnumMember] SUCCESS = 0,
            [EnumMember] FAILURE = -1,
            [EnumMember] WARNING = -2
        };
    }

    public class MessageErrorList : EntityListBase<MessageError>
    {

    }

    [DataContract]
    public abstract class EntityListBase<T> : System.Collections.Generic.List<T>, ICloneable//, ICloneableList
    {
        //''' <summary>
        //''' kopierar allt i en klass till en anna som har matchande propertys, kravet är
        //''' att source och destination klassen har samma namn vilket löses genom att Serializera klassen
        //''' till en xml och replace namnet på gamla klassen till nya sen Deserializera klassen tillbaka till
        //''' ett objekt och casta den till det nya objektet.
        //''' 
        //''' den tar även underliggande objekt om den hittar klasser som har matchande namn
        //''' </summary>
        //''' <typeparam name="T2"></typeparam>
        //''' <returns></returns>
        //''' <remarks>Varning den är case sensitive vilket betyder att DU måste se till att innehållet i klasserna har exakt
        //samma Case på namnen.</remarks>
        //public Function DeserializeTo(Of T2)() As T2
        //Dim Serializer As New Xml.Serialization.XmlSerializer(Me.GetType())
        //Dim DataFile As New StringWriter()
        //Serializer.Serialize(DataFile, Me)

        //Dim Deserializer As New Xml.Serialization.XmlSerializer(GetType(T2))

        //'byter namn på start och slut elementet i klassen till den nya klassens namn.
        //Dim strXml As String = DataFile.ToString
        //strXml = strXml.Replace("<" & Me.GetType().Name & " ", "<" & GetType(T2).Name & " ")
        //strXml = strXml.Replace("</" & Me.GetType().Name & ">", "</" & GetType(T2).Name & ">")

        //Dim Data = CType(Deserializer.Deserialize(New StringReader(strXml)), T2)

        //Return Data
        //End Function

        //public Overridable Function Clone() As Object Implements System.ICloneable.Clone
        //Return CloneHelper.CloneList(Me)
        //End Function

        //public Function Clone(excludeProperties As System.Collections.ArrayList) As Object Implements ICloneableList.Clone
        //Return CloneHelper.CloneList(Me, excludeProperties)
        //End Function
        public object Clone()
        {
            throw new NotImplementedException();
        }
    }

    [DataContract]
    public class MessageError
    {
        private ErrorSeverity severity = ErrorSeverity.Error;

        [DataMember]
        public string ErrorNumber {get;set;}

        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public string AdditionInfo { get; set; }

        [DataMember]
        public ErrorSeverity Severity 
        { 
            get { return severity;}
            set { severity = value; }
        }

        [DataMember]
        public ErrorType Type { get; set; } 

        [DataContract]
        public enum ErrorSeverity
        {
            [EnumMember]
            NotSet= 0,
            [EnumMember]
            Information= 10,
            [EnumMember]
            Warning = 20,
            [EnumMember]
            Error = 30
        };

        [DataContract]
        public enum ErrorType
        {
            [EnumMember] NotSet = 0,
            [EnumMember] BusinessError = 10,
            [EnumMember] ValidationError = 20
        };
    }
}