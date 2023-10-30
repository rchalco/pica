using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Interop.Main.Cross.Domain.CardReader
{
    [DataContract]
    [Serializable]
    public class CommandCardReader
    {
        [DataMember]
        public string NameCommand { get; set; }
        [DataMember]
        public string Cm { get; set; }
        [DataMember]
        public string Pm { get; set; }
        [DataMember]
        public int TxDataLen { get; set; }
        [DataMember]
        public int RxDataLen { get; set; }
        [DataMember]
        public string TxData { get; set; }
        [DataMember]
        public string RxData { get; set; }
        [DataMember]
        public string ReType { get; set; }
        [DataMember]
        public string St0 { get; set; }
        [DataMember]
        public string St1 { get; set; }
    }
}
