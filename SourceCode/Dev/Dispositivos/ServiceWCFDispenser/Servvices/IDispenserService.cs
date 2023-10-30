using Foundation.Stone.Application.Wrapper;
using RuntimeDispensador.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceWCFDispenser.Servvices
{
    [ServiceContract]
    public interface IDispenserService
    {
        [OperationContract]
        ResponseQuery<GetwayDispensador.Trace> Dispensar(int Monto);
        [OperationContract]
        ResponseQuery<GetwayDispensador.Trace> ReiniciarDispensador();
        [OperationContract]
        ResponseQuery<GetwayDispensador.Trace> IncializarDispensador();
        [OperationContract]
        ResponseQuery<GetwayDispensador.Trace> AbrirBandejas();
        [OperationContract]
        ResponseQuery<GetwayDispensador.Trace> AsegurarBandejas();

    }
}
