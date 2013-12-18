using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TwoToWin.Prisma.BasicWCFService.Entities.Message
{

    public interface ICloneable
    {
        object Clone();
    }

    [DataContract]
    public abstract class EntityBase: ICloneable
    {
        public object Clone()
        {
            throw new NotImplementedException();
        }

        //Titta i Sally projektet för fler metoder
    }
}