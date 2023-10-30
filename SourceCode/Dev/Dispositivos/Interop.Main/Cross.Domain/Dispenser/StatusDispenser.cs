using Interop.Main.Cross.Domain.Dispenser.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Interop.Main.Cross.Domain.Dispenser
{
    [DataContract]
    public class ResponseComplex
    {
        [DataMember]
        public string model { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public string typeEnum { get; set; }
    }


    [DataContract]
    public class CassetteStatus
    {
        [DataMember]
        public int Index { get; set; }
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public System.Collections.Generic.SortedDictionary<PartRespuesta, ResponseComplex> PartsResponse { get; set; }
        [DataMember]
        public string StrResponse { get; set; }
    }

    [DataContract]
    public class StatusDispenser
    {
        public StatusDispenser()
        {
            Cassettes = new List<CassetteStatus>();
        }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public ResponseDispensador state { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public Dictionary<PartRespuesta, string> PartsResponse { get; set; }
        [DataMember]
        public List<CassetteStatus> Cassettes { get; set; }
        [DataMember]
        public string ResponseOriginal { get; set; }
    }
}
