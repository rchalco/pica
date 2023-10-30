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
    public class ResposeLoanFlowGetLoanCreditByIdentityCard
    {
        [DataMember]
        public ResponseQuery<LoanCreditRecoveryDetailComplex> LoanFlowGetLoanCreditByIdentityCardResult { get; set; }

    }

    public class ResposeLoanFlowGetLoanCreditByCreditCode
    {
        [DataMember]
        public ResponseQuery<LoanCreditRecoveryDetailComplex> LoanFlowGetLoanCreditByCreditCodeResult { get; set; }

    }


}
