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
    public class RequestCreditAccount : BaseRequest
    {
        [DataMember]
        public ParmeterCreditAccount ObjParameter { get; set; }
    }

    [DataContract]
    public class ParmeterCreditAccount : BaseRequest
    {
        [DataMember]
        public string CodeSavingAccount { get; set; }
        [DataMember]
        public int IdMoneyTrans { get; set; }
        [DataMember]
        public decimal AmountTrans { get; set; }
        [DataMember]
        public bool ForceTransaction { get; set; }

        [DataMember]
        public RequestDepositAccount requestDepositAccount { get; set; }

        [DataMember]
        public List<AdittedBills> adittedBills { get; set; }

        /// <summary>
        ///Id del NO CLIENTE / cliente de prodem que realiza la operacion de terceros
        /// </summary>

        [DataMember]
        public virtual long? IdExternalPerson
        {
            get;
            set;
        }

        /// <summary>
        ///indica si es  NO CLIENTE    
        /// </summary>        
        [DataMember]
        public virtual bool? IsExternalPerson
        {
            get;
            set;
        }

        /// <summary>
        /// Nombre de la tercera person que realizo la transaccion
        /// </summary>        
        [DataMember]
        public virtual string ThirdPersonName
        {
            get;
            set;
        }


        /// <summary>
        /// Documento de identidad de la tercera person que realizo la transaccion
        /// </summary>        
        [DataMember]
        public virtual string ThirdPersonDI
        {
            get;
            set;
        }
        /// <summary>
        /// Detalle del billetaje dispensado por el ATM
        /// </summary>
        [DataMember]
        public virtual string ATMCashDetail { get; set; }
    }
    [DataContract]
    public class AdittedBills
    {
        [DataMember]
        public string Detalle { get; set; }
        [DataMember]
        public int Index { get; set; }
        [DataMember]
        public string Moneda { get; set; }
        [DataMember]
        public int Monto { get; set; }
        [DataMember]
        public bool Almacenado { get; set; }
    }

    [DataContract]
    public class RequestDepositAccount
    {
        [DataMember]
        public string ThirdPersonName { get; set; }
        [DataMember]
        public long ThirdPersonDI { get; set; }
    }
}
