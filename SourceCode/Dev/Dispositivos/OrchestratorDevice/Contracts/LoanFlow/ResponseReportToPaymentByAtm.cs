using Foundation.Stone.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Contracts.LoanFlow
{
    [DataContract]
    [Serializable]
    public class ResponseReportToPaymentByAtm
    {
        [DataMember]
        public ResponseObject<ReportToPaymentByAtm> LoanFlowCreditRecoveryForExternalChannelResult { get; set; }
    }

   
}
