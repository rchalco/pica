using Foundation.Stone.Application.Wrapper;
using Interop.Main.Cross.Domain.CardReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ATM.ControlPanel.Services
{
    [ServiceContract]
    public interface IHotKeyServices
    {
        [OperationContract]
        Response InitHotKey();

        [OperationContract]
        Response ShowFormMain();
    }
}
