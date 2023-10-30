using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Interop.Main.Cross.Domain.Dispenser
{
    [DataContract]
    public class ResponseDispensarATM
    {
        [DataMember]
        public List<Trace> traces { get; set; }
        [DataMember]
        public BillDelivered billDelivered { get; set; }
    }

    
    

}
