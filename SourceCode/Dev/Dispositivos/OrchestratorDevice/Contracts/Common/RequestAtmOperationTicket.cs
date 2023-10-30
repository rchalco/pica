using OrchestratorDevice.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Contracts.Common
{
    [DataContract]
    public class RequestAtmOperationTicket : BaseRequest
    {
        [DataMember]
        public decimal AmountOperation { get; set; }
        [DataMember]
        public long IdMoney { get; set; }
        [DataMember]
        public long IdOffice { get; set; }
        [DataMember]
        public string Observation { get; set; }
        [DataMember]
        public string IdentityNumber { get; set; }
        [DataMember]
        public long IdcTranssactionType { get; set; }
    }
}
