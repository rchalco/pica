using OrchestratorDevice.Contracts.SavingAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Contracts.LoanFlow
{
    [DataContract]
    public class ResulComplexLoanFlow
    {
        [DataMember]
        public string NumberOfTransaction { get; set; }
        [DataMember]
        public string Token { get; set; }

        [DataMember]
        public List<LoanCreditRecoveryDetailComplex> ColLoanFlow { get; set; }
        [DataMember]
        public List<Account> ColAccount { get; set; }
    }
}
