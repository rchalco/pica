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
    public class ReportToPaymentByAtm
    {
        [DataMember]
        public long IdTransaction { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public string ReportString { get; set; }

    }
}
