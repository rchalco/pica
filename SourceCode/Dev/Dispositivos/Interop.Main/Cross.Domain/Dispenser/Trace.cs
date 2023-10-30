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
    public class Trace
    {
        [DataMember]
        public Comando IdCommand { get; set; }
        [DataMember]
        public string CommandName { get; set; }
        [DataMember]
        public string Parameter { get; set; }
        [DataMember]
        public string AdditionalInformation { get; set; }
        [DataMember]
        public StatusDispenser Result { get; set; }
        [DataMember]
        public DateTime DateCreate { get; set; }
        [DataMember]
        public DateTime DateExecution { get; set; }
    }
}
