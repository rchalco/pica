using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMonitoreo.Data
{
    public partial class MonitoreoCENTRAL_ATMEntities
    {
        public MonitoreoCENTRAL_ATMEntities(string dbConfig) : base($"{dbConfig}")
        {

        }
    }
}
