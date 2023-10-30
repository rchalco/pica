using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation.Stone.Application.Wrapper;
using RuntimeDispensador.Core;
using System.ServiceModel;

namespace ServiceWCFDispenser.Servvices
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DispenserService : IDispenserService
    {
        ResponseQuery<GetwayDispensador.Trace> AbrirBandejas()
        {
            throw new NotImplementedException();
        }

        ResponseQuery<GetwayDispensador.Trace> Dispensar(int Monto)
        {
            throw new NotImplementedException();
        }

        ResponseQuery<GetwayDispensador.Trace> IncializarDispensador()
        {
            throw new NotImplementedException();
        }

        ResponseQuery<GetwayDispensador.Trace> ReiniciarDispensador()
        {
            throw new NotImplementedException();
        }

        ResponseQuery<GetwayDispensador.Trace> ResetearDispensador()
        {
            throw new NotImplementedException();
        }
    }
}
