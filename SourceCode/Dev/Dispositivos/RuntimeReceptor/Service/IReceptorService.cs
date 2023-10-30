using Foundation.Stone.Application.Wrapper;
using Interop.Main.Cross.Domain.Receptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RuntimeReceptor.Service
{
    [ServiceContract]
    public interface IReceptorService
    {
        [OperationContract]
        Response ActivarReceptor(RequestMaxAmount pMaxAmount);
        [OperationContract]
        Response InitReceptor(string _portCOM);
        [OperationContract]
        ResponseQuery<ItemReceptor> DesactivarReceptor();
        [OperationContract]
        ResponseQuery<ItemReceptor> GetBills();
        
    }
}
