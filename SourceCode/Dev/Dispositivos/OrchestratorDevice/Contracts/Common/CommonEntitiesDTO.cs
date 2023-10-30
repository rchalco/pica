using Foundation.Stone.Application.Wrapper;
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
    public class RequestATMConfirm : BaseRequest
    {
        [DataMember]
        public RequestATMConfirmDTO objATMConfirmData { get; set; }
    }

    [DataContract]
    public class RequestATMConfirmDTO
    {
        [DataMember]
        public long IdATMTransaction { get; set; }
        [DataMember]
        public string ATMTransactionCode { get; set; }
        [DataMember]
        public bool WithError { get; set; }
        [DataMember]
        public string ATMCoinageDetail { get; set; }
        [DataMember]
        public string ATMCoinageTray { get; set; }
        [DataMember]
        public string ATMCoinageRejected { get; set; }
        [DataMember]
        public string ATMErrorDetail { get; set; }
    }
    [DataContract]
    public class ResponseATMConfirm : BaseRequest
    {
        [DataMember]
        public Response ATMTransactionConfirmResult { get; set; }
    }

    [DataContract]
    public class ATMTransactionConfirmData
    {
        [DataMember]
        public long IdATMTransaction { get; set; }

        [DataMember]
        public string ATMTransactionCode { get; set; }

        [DataMember]
        public bool WithError { get; set; }

        [DataMember]
        public string ATMCoinageDetail { get; set; }

        [DataMember]
        public string ATMCoinageTray { get; set; }

        [DataMember]
        public string ATMCoinageRejected { get; set; }

        [DataMember]
        public string ATMErrorDetail { get; set; }
    }
}
