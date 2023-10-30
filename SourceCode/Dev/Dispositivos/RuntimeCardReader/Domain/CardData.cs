using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuntimeCardReader.Domain
{
    public class CardData
    {
        public enum type
        {
            MPCOS = 0,
            EMV = 1
        }
        public string Account { get; set; }
        public string PAN { get; set; }
        public string DataComplete { get; set; }
        public type EMV { get; set; }
    }
}
