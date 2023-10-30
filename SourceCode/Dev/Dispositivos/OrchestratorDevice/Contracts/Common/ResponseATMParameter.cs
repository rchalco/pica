using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Contracts.Common
{
    [DataContract]
    public class ResponseATMParameter
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int IdOffice { get; set; }

        [DataMember]
        public string Name { get; set; }
    }


}
