using OrchestratorDevice.Contracts.Base;
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
    [Serializable]
    public class RequestATMLoanCreditByAccount: BaseRequest
    {
        [DataMember]
        public virtual long IdOperationSavingsAccount { get; set; }
        [DataMember]
        public virtual string CodeSavingsAccount { get; set; }
        //[DataMember]
        //public virtual decimal AmountDebit { get; set; }
        [DataMember]
        public virtual string CodeLoanCredit { get; set; }
        [DataMember]
        public virtual long IdMoneyLoanCredit { get; set; }
        //[DataMember]
        //public virtual decimal AmountLoanCredit { get; set; }
        //[DataMember]
        //public virtual decimal TotalAmountRecovery { get; set; }
        [DataMember]
        public virtual string NumberTranATM { get; set; }        
        [DataMember]
        public virtual bool WithInsuranceReturn { get; set; }
        [DataMember]
        public virtual long IdCustomerAffiliation { get; set; }
        [DataMember]
        public virtual string UseCode { get; set; }
    }

    [DataContract]
    [Serializable]
    public class ExternalChannelRecoveryData: BaseRequest
    {
        [DataMember]
        public string IdLoanCredit { get; set; }

        [DataMember]
        public string LoanCreditCode { get; set; }

        [DataMember]
        public decimal DebitAmount { get; set; }

        [DataMember]
        public decimal AmountToPay { get; set; }

        [DataMember]
        public decimal TaxAmount { get; set; }

        [DataMember]
        public long IdLoanCurrency { get; set; }

        [DataMember]
        public bool WithInsuranceReturn { get; set; }

        [DataMember]
        public bool IsNaturalCustomer { get; set; }

        [DataMember]
        public string CodeAuthentication { get; set; }

        [DataMember]
        public long? IdSavingAccount { get; set; }

        [DataMember]
        public string IdOperationSavingsAccount { get; set; }

        [DataMember]
        public string NumberTranATM { get; set; }

        [DataMember]
        public bool VerifyClosure { get; set; }

        [DataMember]
        public string IdUser { get; set; }

        [DataMember]
        public string IdCustomer { get; set; }

        [DataMember]
        public string IdWebClient { get; set; }

        [DataMember]
        public List<AdittedBills> ColAdittedBills { get; set; }

        [DataMember]
        public string CashDetail { get; set; }
    }



    public class RequestATMLoanCreditByAccountParameter 
    {
        [DataMember]
        public ExternalChannelRecoveryData objParameter { get; set; }
    }


    [DataContract]
    [Serializable]
    public class RequestATMLoanCreditByLoanCreditCode : BaseRequest
    {
        [DataMember]
        public virtual string LoanCreditCode { get; set; }
       
    }

    public class RequestATMLoanCreditByLoanCreditCodeParameter
    {
        [DataMember]
        public RequestATMLoanCreditByLoanCreditCode objParameter { get; set; }
    }
}
