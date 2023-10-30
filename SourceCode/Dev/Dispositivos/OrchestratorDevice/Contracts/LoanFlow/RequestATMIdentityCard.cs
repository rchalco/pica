using OrchestratorDevice.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Contracts.LoanFlow
{
    [DataContract]
    [Serializable]
    public class RequestATMIdentityCard : BaseRequest
    {
        [DataMember]
        public virtual string IdentityCardNumber { get; set; }
    }

    [DataContract]
    public class RequestATMIdentityCardParameter 
    {
        [DataMember]
        public RequestATMIdentityCard objParameter { get; set; }
    }    
}
