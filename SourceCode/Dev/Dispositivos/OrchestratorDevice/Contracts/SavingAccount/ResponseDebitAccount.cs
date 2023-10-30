using Foundation.Stone.Application.Wrapper;
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
    public class BaseATMResult
    {
        [DataMember]
        public int CodeError { get; set; }

        [DataMember]
        public string IdTransacPending { get; set; }

        [DataMember]
        public bool IsClosureExecuted { get; set; }

        [DataMember]
        public string MessageError { get; set; }

        [DataMember()]
        public string ATMTransaction { get; set; }
    }

    [DataContract]
    public class ResponseDebitAccount : BaseRequest
    {
        [DataMember]
        public ResponseObject<ResulDebitAccount> DebitAccountResult { get; set; }
    }


    [DataContract]
    public class ResponseDepositAccount : BaseRequest
    {
        [DataMember]
        public ResponseObject<ResulDebitAccount> DepositAccountResult { get; set; }
    }

    [DataContract]
    public class ResulDebitAccount : BaseATMResult
    {
        [DataMember]
        public string IdTransaction { get; set; }
        [DataMember]
        public string Voucher { get; set; }
    }
}
