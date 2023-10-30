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
    public class RequestTransferAmount : BaseRequest
    {
        [DataMember]
        public InnerRequestTranserAccount ObjParameter { get; set; }
    }

    [DataContract]
    public class InnerRequestTranserAccount
    {
        /// <summary>
        /// Numero de cuenta 
        /// </summary>
        [DataMember]
        public string CodeSavingAccount { get; set; }
        /// <summary>
        /// Id de moneda de la transaccion
        /// </summary>
        [DataMember]
        public long IdMoneyTrans { get; set; }
        /// <summary>
        /// Monto de la transaccion
        /// </summary>
        [DataMember]
        public decimal AmountTrans { get; set; }

        /// <summary>
        /// Numero de cuenta destino
        /// </summary>
        [DataMember]
        public string CodeSavingAccountTarget { get; set; }
    }
}
