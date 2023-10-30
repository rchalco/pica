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
    public class DTODebitLightningSpinResult
    {
        [DataMember]
        public ResponseObject<ResponseDebitLightningSpin> DebitLightningSpinResult { get; set; }
    }

    [DataContract]
    public class ResponseDebitLightningSpin : BaseATMResult
    {
        [DataMember]
        public string IdTransaction { get; set; }
        [DataMember]
        public string Voucher { get; set; }
    }
}
