using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Contracts
{
    [DataContract]
    public class DTOCurrencyExchangeRateATM
    {
        [DataMember]
        public virtual Nullable<decimal> NominalBuyExchangeRate
        {
            get;
            set;
        }


        [DataMember]
        public virtual Nullable<decimal> NominalSellExchangeRate
        {
            get;
            set;
        }


        [DataMember]
        public virtual Nullable<decimal> RealBuyExchangeRate
        {
            get;
            set;
        }

        [DataMember]
        public virtual Nullable<decimal> RealSellExchangeRate
        {
            get;
            set;
        }


        [DataMember]
        public virtual Nullable<decimal> AccountancyExchangeRate
        {
            get;
            set;
        }


        [DataMember]
        public virtual Nullable<DateTime> ValidSince
        {
            get;
            set;
        }


        [DataMember]
        public virtual Nullable<DateTime> ValidUntil
        {
            get;
            set;
        }
    }
}
