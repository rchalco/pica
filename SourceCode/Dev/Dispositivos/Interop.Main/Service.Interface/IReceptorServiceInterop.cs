using Foundation.Stone.Application.Module;
using Foundation.Stone.Application.Wrapper;
using Interop.Main.Cross.Domain.Receptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.Main.Service.Interface
{
    [ModuleStone(FullName = "RuntimeReceptor.Service.ReceptorService")]
    public interface IReceptorServiceInterop
    {

        Response ActivarReceptor(RequestMaxAmount pMaxAmount);

        Response InitReceptor(string _portCOM);

        ResponseQuery<ItemReceptor> DesactivarReceptor();

        ResponseQuery<ItemReceptor> GetBills();
    }
}
