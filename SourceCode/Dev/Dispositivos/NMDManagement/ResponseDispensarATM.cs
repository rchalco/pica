using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NMDManagement
{
    [DataContract]
    public class ResponseDispensarATM
    {
        [DataContract]
        public class InitResult
        {
            [DataMember]
            public ResponseType State { get; set; }

            [DataMember]
            public string Message { get; set; }

            [DataMember]
            public string CodeBase { get; set; }
            [DataMember]
            public string Code { get; set; }

            [DataMember]
            public ResponseDispensarATMDetalle Object { get; set; }
        }

        [DataMember]
        public InitResult DispensarATMResult { get; set; }
    }

    [DataContract]
    public class ResponseDispensarATMDetalle
    {
        //[DataMember]
        //public List<TraceDilevery> traces { get; set; }

        [DataMember]
        public BillDeliveredDispenser billDelivered { get; set; }
    }

    [DataContract]
    public class BillDeliveredDispenser
    {
        [DataMember]
        public List<ResultDelivered> Required { get; set; }

        [DataMember]
        public List<ResultDelivered> FirstResult { get; set; }

        [DataMember]
        public List<ResultDelivered> Delivered { get; set; }
    }

    [DataContract]
    public class ResultDelivered
    {
        [DataMember]
        public int CurrencyCourt { get; set; }

        [DataMember]
        public int IdMoney { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public int State { get; set; }
    }

    public class TraceDilevery
    {
        [DataMember]
        public Comando Command { get; set; }
        [DataMember]
        public string Parameter { get; set; }
        [DataMember]
        public string AdditionalInformation { get; set; }
        [DataMember]
        public StatusDispenserDilevery Result { get; set; }
        [DataMember]
        public DateTime DateCreate { get; set; }
        [DataMember]
        public DateTime DateExecution { get; set; }
    }
    public class StatusDispenserDilevery
    {

        public string Code { get; set; }
        public string Type { get; set; }
        public ResponseDispensador state { get; set; }
        public string Description { get; set; }

        public List<CassetteStatus> Cassettes { get; set; }
        public string ResponseOriginal { get; set; }
    }

    public class CasseteDilevery
    {
        public string Id { get; set; }
        public int Index { get; set; }

    }

    public class PartsResponseDilevery
    {

    }
}
