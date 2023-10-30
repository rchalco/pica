using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Interop.Main.Cross.Domain.FingerPrint
{
    [DataContract]
    public class FingerContract
    {
        [DataContract]
        public enum TypeEnroll
        {
            [EnumMember]
            ISO = 0,
            [EnumMember]
            DP = 1
        }
    }
}
