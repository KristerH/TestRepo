using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace TwoToWin.Prisma.BasicWCFService.Common.Serialize
{
    public class ObjectSerializer
    {
        public static string SerializeObject(object o)
        {
            var serializer = new XmlSerializer(o.GetType());
            var dataFile = new StringWriter();
            serializer.Serialize(dataFile, o);
            return dataFile.ToString();

            //XmlSerializer xmlSerializer = new XmlSerializer(o.GetType());
            //StringWriter textWriter = new StringWriter();

            //xmlSerializer.Serialize(textWriter, o);
            //return textWriter.ToString();
        }
    }
}