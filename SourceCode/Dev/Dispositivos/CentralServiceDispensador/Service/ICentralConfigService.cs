using CentralServiceDispensador.Wraper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CentralServiceDispensador.Service
{
    [ServiceContract]
    public interface ICentralConfigService
    {
        [OperationContract]
        ResponseCentral GetConfig(RequestConfig req);        

    }
}
