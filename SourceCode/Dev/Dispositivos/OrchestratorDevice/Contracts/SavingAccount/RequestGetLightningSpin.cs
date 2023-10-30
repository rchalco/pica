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
    public class RequestGetLightningSpin : BaseRequest
    {
        [DataMember]
        public StructGetLightningSpin ObjParameter { get; set; }
    }
    [DataContract]
    public class StructGetLightningSpin
    {
        [DataMember]
        public string BeneficiaryDI { get; set; }
    }
}
