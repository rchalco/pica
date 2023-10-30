using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Contracts
{
    [DataContract]
    public class ResquesPrintDocument
    {
        [DataMember]
        public string tittle { get; set; }
        [DataMember]
        public string content { get; set; }        
    }
}
