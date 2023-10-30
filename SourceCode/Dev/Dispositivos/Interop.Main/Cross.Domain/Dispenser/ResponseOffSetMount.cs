using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.Main.Cross.Domain.Dispenser
{
    public class ResponseOffSetMount
    {
        public List<ItemOffSet> Detail { get; set; }
        public int Mount { get; set; }
        public string Comand { get; set; }
    }

    public class ItemOffSet
    {
        public int Sequence { get; set; }
        public int Court { get; set; }
        public int TotalNotes { get; set; }
    }
}
