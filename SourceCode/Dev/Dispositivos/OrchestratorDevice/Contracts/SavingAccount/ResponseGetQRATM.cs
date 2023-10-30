using Foundation.Stone.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Contracts.SavingAccount
{
    [DataContract]
    public class ResponseGetQRATM
    {
        [DataMember]
        public ResponseObject<string> GetaQRATMResult { get; set; }
    }
}
