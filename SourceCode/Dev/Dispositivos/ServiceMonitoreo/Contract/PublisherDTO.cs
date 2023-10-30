using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMonitoreo.Contract
{

    [DataContract]
    public class ATMDTO
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int IdATM { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public bool State { get; set; }
        [DataMember]
        public string Configuration { get; set; }
        [DataMember]
        public string Profile { get; set; }
        [DataMember]
        public int IdOffice { get; set; }
    }
}
