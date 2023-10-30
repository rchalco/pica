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
    public class RequestDebitAccount : BaseRequest
    {
        [DataMember]
        public ParameterDebitAccount ObjParameter { get; set; }
    }

    [DataContract]
    public class ParameterDebitAccount : BaseRequest
    {
        [DataMember]
        public string CodeSavingAccount { get; set; }
        [DataMember]
        public int IdMoneyTrans { get; set; }
        [DataMember]
        public decimal AmountTrans { get; set; }
        [DataMember]
        public bool ForceTransaction { get; set; }
    }
    

}
