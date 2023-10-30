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
    public class ResponseGetLightningSpin
    {
        [DataMember]
        public ResponseQuery<DTOLightningSpin> GetLightningSpinResult { get; set; }
    }

    [DataContract]
    public class DTOLightningSpin
    {
        /// <summary>
        /// Id de la clase
        /// </summary>
        /// <change who="Edson Fernández" when="20150810" what="Creación"/>
        [DataMember]
        public virtual string IdProdemExpressSolicitation
        {
            get;
            set;
        }


        /// <summary>
        /// Fecha y hora en la que se realizo la solicitud
        /// </summary>
        /// <change who="Edson Fernández" when="20150810" what="Creación"/>
        [DataMember]
        public virtual DateTime SolicitationDate
        {
            get;
            set;
        }

        /// <summary>
        /// nombre de la oficina
        /// </summary>
        /// <change who="Edson Fernández" when="20150810" what="Creación"/>
        [DataMember]
        public virtual string OfficeName
        {
            get;
            set;
        }

        /// <summary>
        /// monto del envio
        /// </summary>
        /// <change who="Edson Fernández" when="20150810" what="Creación"/>
        [DataMember]
        public virtual decimal AmountTransaction
        {
            get;
            set;
        }

        /// <summary>
        /// moneda de la solicitud 
        /// </summary>
        /// <change who="Edson Fernández" when="20150810" what="Creación"/>
        [DataMember]
        public virtual string MoneyDescription
        {
            get;
            set;
        }


        /// <summary>
        /// moneda de la solicitud 
        /// </summary>
        /// <change who="Edson Fernández" when="20150810" what="Creación"/>
        [DataMember]
        public virtual string BeneficiaryName
        {
            get;
            set;
        }


        /// <summary>
        /// codigo de cuenta de ahorro origen.
        /// </summary>
        /// <change who="Edson Fernández" when="20150810" what="Creación"/>
        [DataMember]
        public virtual string CodeSavingsAccount
        {
            get;
            set;
        }

        /// <summary>
        /// monto total que se debitara de la cuenta de ahorro incluira comisones e impuestos
        /// </summary>
        /// <change who="Edson Fernandez" when="20150811" what="Creacion"/>        
        [DataMember]
        public virtual decimal AmountTotalDebit { get; set; }
    }
}
