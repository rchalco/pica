using OrchestratorDevice.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Contracts.Common
{
    [DataContract]
    public class RequestGetCurrencyExchanceRateByDate : BaseRequest
    {
        [DataMember]
        public RequestGetCurrencyExchanceRateByDateInner requestCurrencyExchanceRateByDate { get; set; }
    }

    [DataContract]
    public class RequestGetCurrencyExchanceRateByDateInner
    {
        [DataMember]
        public int IdMoney { get; set; }
    }
}
