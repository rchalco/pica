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
    public class ResponseTransferAmount
    {
        [DataMember]
        public ResponseObject<InnerResponseTransferAmount> TransferAccountsResult { get; set; }
    }
    
    [DataContract]
    public class InnerResponseTransferAmount
    {
        /// <summary>
        /// codigo de error valores del enumerador CodeErrorCAIResult
        /// </summary>
        [DataMember]
        public virtual long CodeError { get; set; }

        /// <summary>
        /// codigo de error específico
        /// </summary>
        [DataMember]
        public virtual string CodeErrorSpecific { get; set; }

        /// <summary>
        /// mensaje de error
        /// </summary>
        [DataMember]
        public virtual string MessageError { get; set; }

        /// <summary>
        /// Indica si el cierre se esta ejecutando
        /// </summary>        
        [DataMember()]
        public bool IsClosureExecuted { get; set; }

        /// <summary>
        /// id de la transaccion pendiente (llenada solo cuando este en modo cierre)
        /// </summary>        
        [DataMember()]
        public string IdTransacPending { get; set; }

        /// <summary>
        /// id de la transaccion generada en el ATM
        /// </summary>        
        [DataMember()]
        public string ATMTransaction { get; set; }

        /// <summary>
        /// Voucher generado.
        /// </summary>        
        [DataMember()]
        public virtual string Voucher { get; set; }

        /// <summary>
        /// Id de la transacción
        /// </summary>
        [DataMember()]
        public virtual long IdTransaction { get; set; }
    }
}
