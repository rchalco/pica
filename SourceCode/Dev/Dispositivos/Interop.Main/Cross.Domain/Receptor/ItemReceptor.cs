using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Interop.Main.Cross.Domain.Receptor
{
    [DataContract]
    public class ItemReceptor
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
        [DataMember]
        public DateTime FechaHora { get; set; }
    }

    
}
