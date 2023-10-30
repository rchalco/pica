using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Interop.Main.Cross.Domain.CardReader
{
    
    [DataContract]
    public class StatusCardReader
    {
        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public int CodeE1 { get; set; }

        [DataMember]
        public int CodeE0 { get; set; }

        [DataMember]
        public string ConcreteResponse { get; set; }
    }
}
