using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Interop.Main.Cross.Domain.Dispenser
{

    [DataContract]
    public class BillDelivered
    {
        [DataMember]
        public List<StateCassette> Required { get; set; }

        [DataMember]
        public List<StateCassette> FirstResult { get; set; }

        [DataMember]
        public List<StateCassette> Delivered { get; set; }
    }

    [DataContract]
    public class StateCassette
    {
        [DataMember]
        public string State { get; set; }

        [DataMember]
        public int CurrencyCourt { get; set; }

        [DataMember]
        public int IdMoney { get; set; }

        [DataMember]
        public int Quantity { get; set; }
    }
}
