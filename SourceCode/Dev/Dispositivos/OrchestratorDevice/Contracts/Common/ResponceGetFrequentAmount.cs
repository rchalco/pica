using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Contracts.Common
{
    [DataContract]
    public class ResponseGetFrequentAmount
    {
        [DataMember]
        public List< FrequentAmount> FrequentAmount { get; set; }  
    }
    [DataContract]
    public class FrequentAmount
    {
        [DataMember]
        public int orden { get; set; }
        [DataMember]
        public int valor { get; set; }
        [DataMember]
        public int estado { get; set; }

    }
    [DataContract]
    public class ResponseOffSetMount
    {
        [DataMember]
        public List<ItemOffSet> Detail { get; set; }
        [DataMember]
        public int Mount { get; set; }
        [DataMember]
        public string Comand { get; set; }
    }

    [DataContract]
    public class ItemOffSet
    {
        [DataMember]
        public int Sequence { get; set; }
        [DataMember]
        public int Court { get; set; }
        [DataMember]
        public int TotalNotes { get; set; }
    }
}
