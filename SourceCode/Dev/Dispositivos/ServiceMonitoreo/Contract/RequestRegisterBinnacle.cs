using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMonitoreo.Contract
{
    [DataContract]
    public class RequestRegisterBinnacle
    {
        [DataMember]
        public string IP { get; set; }
        [DataMember]
        public string Evento { get; set; }
        [DataMember]
        public string Device { get; set; }
        [DataMember]
        public int? IdTransacction { get; set; }
        [DataMember]
        public string Operation { get; set; }
        [DataMember]
        public string Trace { get; set; }
        [DataMember]
        public string Level { get; set; }
        [DataMember]
        public string StateDevice { get; set; }
    }
}
