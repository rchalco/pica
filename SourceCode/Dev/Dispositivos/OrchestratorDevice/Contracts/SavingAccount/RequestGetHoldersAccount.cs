using OrchestratorDevice.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Contracts.SavingAccount
{
    [DataContract]
    public class RequestGetHoldersAccount: BaseRequest
    {
        [DataMember]
        public RequestGetHoldersAccountInner ObjParameter { get; set; }
    }

    [DataContract]
    public class RequestGetHoldersAccountInner
    {
        [DataMember]
        public string CodeSavingAccount { get; set; }
    }
}
