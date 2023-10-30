using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.Main.Cross.Domain.CardReader
{
    public class CardReader
    {
        public enum status
        {
            none = -1,
            ready = 0,
            bussy = 1,
            damaged = 2
        }
        public string Name { get; set; }
        public string PortCOM { get; set; }
        public status Status { get; set; }
        public string DescriptionStatus { get; set; }
    }

    public enum TypeReaderCard
    {
        NONE = 0,
        CREATOR = 1,
        ATHENA = 2,
        GENPLUS = 3
    }
}
