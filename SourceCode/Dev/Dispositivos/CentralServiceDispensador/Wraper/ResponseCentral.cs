using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CentralServiceDispensador.Wraper
{

    [DataContract]
    public class RequestConfig
    {
        [DataMember]
        public int IdATM { get; set; }
        [DataMember]
        public string PortName { get; set; }

    }




    [DataContract]
    public class ResponseCentral
    {
        [DataMember]
        public StateCentral State { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public byte[] ConfigSource { get; set; }
    }

    [DataContract]
    public enum StateCentral
    {
        [EnumMember]
        Sucess =0,
        [EnumMember]
        Warning = 1,
        [EnumMember]
        Error = 2,
    }

    
}
