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
    public class ResponseAuthentication
    {
        [DataMember]
        public ResponseObject<ResulAuthencation> VerifyUserResult { get; set; }
    }

    [DataContract]
    public class ResulAuthencation
    {
        [DataMember]
        public List<AditionalItem> AditionalItems { get; set; }
        [DataMember]
        public string User { get; set; }
        [DataMember]
        public string token { get; set; }
    }
}
