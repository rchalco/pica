using Foundation.Stone.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Contracts.Common
{
    [DataContract]
    public class ResponseAtmOperationTicket
    {
        [DataMember]
        public ResponseObject<string> CreateTicketForOperationExternalServiceResult { get; set; }
    }
}
