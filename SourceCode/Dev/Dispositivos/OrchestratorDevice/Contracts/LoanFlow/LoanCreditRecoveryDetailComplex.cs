using OrchestratorDevice.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Contracts.LoanFlow
{
    [DataContract]
    public class LoanCreditRecoveryDetailComplex : ExternalEntityBase
    {
        [DataMember]
        public string IdLoanCredit { get; set; }

        [DataMember]
        public string LoanCreditCode { get; set; }

        [DataMember]
        public string LoanCreditState { get; set; }

        [DataMember]
        public string LoanCurrency { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string IdPerson { get; set; }

        [DataMember]
        public string IdentityCardNumber { get; set; }

        [DataMember]
        public decimal AnnuitiesDelayBalance { get; set; }

        [DataMember]
        public decimal CurrentAnnuityBalance { get; set; }

        [DataMember]
        public decimal CreditBalance { get; set; }

        [DataMember]
        public decimal TotalTransitPayments { get; set; }

        [DataMember]
        public decimal TotalCreditToPay { get; set; }

        [DataMember]
        public decimal TotalTax { get; set; }

        [DataMember]
        public decimal TotalToPay { get; set; }

        [DataMember]
        public decimal VoluntarySaving { get; set; }

        [DataMember]
        public bool WithInsuranceReturn { get; set; }

    }
}
