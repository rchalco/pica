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
    public class RequestGetaQRATM : BaseRequest
    {
        [DataMember]
        public InnerRequestGetaQRATM requestGetaQRATM { get; set; }
    }

    [DataContract]
    public class InnerRequestGetaQRATM
    {
        [DataMember]
        public string uuid { get; set; }
    }

}
