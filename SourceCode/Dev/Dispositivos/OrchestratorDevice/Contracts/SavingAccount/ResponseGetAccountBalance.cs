using Foundation.Stone.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Contracts.SavingAccount
{
    [DataContract]
    public class ResponseGetAccountBalance
    {
        [DataMember]
        public ResponseObject<ResulGetAccountBalance> GetAccountBalanceResult { get; set; }

    }
    [DataContract]
    public class ResulGetAccountBalance
    {
        [DataMember]
        public decimal AccountAvailableBalance { get; set; }
        [DataMember]
        public decimal AccountBalance { get; set; }
        [DataMember]
        public decimal BlockAmount { get; set; }
        [DataMember]
        public string CodeSavingsAccount { get; set; }
        [DataMember]
        public string HolderName { get; set; }
        [DataMember]
        public string IdentityCardNumber { get; set; }
        [DataMember]
        public string MoneyCode { get; set; }
        [DataMember]
        public string NameOffice { get; set; }
        [DataMember]
        public DateTime ProcessDate { get; set; }
        [DataMember]
        public string ProductName { get; set; }
    }
}
