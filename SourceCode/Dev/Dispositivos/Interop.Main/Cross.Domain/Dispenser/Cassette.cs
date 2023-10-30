using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Interop.Main.Cross.Domain.Dispenser
{
    [DataContract]
    public class Cassette
    {
        public Cassette()
        {
            Status = "0";
            MinQuantity = 15;
        }
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Sequence { get; set; }

        [DataMember]
        public int CurrencyCourt { get; set; }

        [DataMember]
        public int TotalQuantity { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public int MinQuantity { get; set; }
    }
}