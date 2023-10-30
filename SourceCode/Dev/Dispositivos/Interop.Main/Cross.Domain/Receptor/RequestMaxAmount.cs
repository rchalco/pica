using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Interop.Main.Cross.Domain.Receptor
{
    [DataContract]
    public class RequestMaxAmount
    {
        [DataMember]
        public int MaxAmount { get; set; }

        [DataMember]
        public int IdATM { get; set; }

        [DataMember]
        public string Token { get; set; }
    }
}
