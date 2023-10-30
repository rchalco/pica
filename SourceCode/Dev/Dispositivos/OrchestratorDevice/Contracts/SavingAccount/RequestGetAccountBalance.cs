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
    public class RequestGetAccountBalance : BaseRequest
    {
        [DataMember]
        public StructGetAccountBalance requestAccountBalance { get; set; }
    }

    [DataContract]
    public class StructGetAccountBalance
    {
        [DataMember]
        public string CodeSavingsAccount { get; set; }
        [DataMember]
        public string IdPerson { get; set; }
    }
}
