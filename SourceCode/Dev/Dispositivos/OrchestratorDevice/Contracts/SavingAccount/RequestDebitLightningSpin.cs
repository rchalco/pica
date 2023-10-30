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
    public class RequestDebitLightningSpin : BaseRequest
    {
        [DataMember]
        public StructDebitLightningSpin ObjParameter { get; set; }
    }

    [DataContract]
    public class StructDebitLightningSpin
    {
        [DataMember]
        public string IdProdemExpressSolicitation { get; set; }
        [DataMember]
        public string DIBeneficiary { get; set; }
        [DataMember]
        public string SMSSender { get; set; }
        [DataMember]
        public string SMSBeneficiary { get; set; }
        [DataMember]
        public decimal AmountDispenser { get; set; }
        [DataMember]
        public decimal AmountDebit { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
    }
}

