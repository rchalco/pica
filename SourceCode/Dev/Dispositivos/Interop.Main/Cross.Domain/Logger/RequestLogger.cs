using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.Main.Cross.Domain.Logger
{
    public class RequestLogger
    {
        public string IP { get; set; }
        public string Evento { get; set; }
        public string Device { get; set; }
        public string IdTransacction { get; set; }
        public string Operation { get; set; }
        public string Trace { get; set; }
        public string Level { get; set; }
        public string StateDevice { get; set; }
    }
}
