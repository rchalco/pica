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
    public class ResponseGetHoldersAccount
    {
        [DataMember]
        public ResponseQuery<HolderAccount> GetHoldersAccountResult { get; set; }
    }

    [DataContract]
    public class HolderAccount
    {
        [DataMember]
        public virtual long IdPerson { get; set; }
        [DataMember]
        public virtual string IdentityCardNumber { get; set; }
        [DataMember]
        public virtual string PersonName { get; set; }
        [DataMember]
        public string StateDescription { get; set; }
        [DataMember]
        public string CodeSavingsAccount { get; set; }
        [DataMember]
        public int IdcState { get; set; }
        [DataMember]
        public string IdSavingsAccount { get; set; }
    }
}
