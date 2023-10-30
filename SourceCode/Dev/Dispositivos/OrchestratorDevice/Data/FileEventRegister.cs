using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchestrator.Data
{

    internal class TraceTransaction
    {
        public string Action { get; set; }
        public string Componentes { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public decimal TimeSeconds { get; set; }
        public bool IsComplete { get; set; }
    }

    internal class FileEventRegister
    {
        public bool RegisterEvent(TraceTransaction traceTransaction)
        {
            return true;
        }
    }
}
