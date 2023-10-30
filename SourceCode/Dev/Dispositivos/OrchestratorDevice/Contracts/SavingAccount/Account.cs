using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Contracts.SavingAccount
{
    [DataContract]
    public class Account
    {

        [DataMember]
        public string IdSavingsAccount
        {
            get;
            set;
        }


        [DataMember]
        public string CodeAccount
        {
            get;
            set;
        }
        /// <summary>
        /// Saldo de la cuenta
        /// </summary>       

        [DataMember]
        public decimal AccountBalance
        {
            get;
            set;
        }

        /// <summary>
        /// Saldo disponible
        /// </summary>        

        [DataMember]
        public decimal AccountAvailableBalance
        {
            get;
            set;
        }


        [DataMember]
        public long IdcPersonType
        {
            get;
            set;
        }


        [DataMember]
        public long IdcManagementWay
        {
            get;
            set;
        }


        [DataMember]
        public string ProductName
        {
            get;
            set;
        }



        [DataMember]
        public long StateReal
        {
            get;
            set;
        }

        /// <summary>
        /// indica si la cuenta pertenece a tarjeta
        /// </summary>

        [DataMember]
        public bool BelongsCard
        {
            get;
            set;
        }


        [DataMember]
        public List<Holder> ColHolders
        {
            get;
            set;
        }

        [DataMember]
        public long vStateAccount
        {
            get;
            set;
        }

        [DataMember]
        public string Message
        {
            get;
            set;
        }
    }

    [DataContract]
    public class Holder
    {

        [DataMember]
        public long IdPerson
        {
            get;
            set;
        }


        [DataMember]
        public string IdentityCardNumber
        {
            get;
            set;
        }


        [DataMember]
        public string Name
        {
            get;
            set;
        }


        [DataMember]
        public string CodeCustomer
        {
            get;
            set;
        }


        [DataMember]
        public bool IsAuthorized
        {
            get;
            set;
        }
    }
}
