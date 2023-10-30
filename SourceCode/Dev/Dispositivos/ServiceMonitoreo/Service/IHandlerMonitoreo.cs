using Foundation.Stone.Application.Wrapper;
using ServiceMonitoreo.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceMonitoreo.Service
{
    // NOTA: puede usar el comando "Cambiar nombre" del menú "Refactorizar" para cambiar el nombre de interfaz "IHandlerMonitoreo" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IHandlerMonitoreo
    {
        [OperationContract]
        Response RegisterBinnacle(RequestRegisterBinnacle requestRegisterBinnacle);

        [OperationContract]
        ResponseObject<string> GetStateFingerPrint(string IP);

        [OperationContract]
        ResponseQuery<ResulCommandCardReader> GetStateCardReader(string IP, string typeCardReader);

        [OperationContract]
        ResponseQuery<ResulCommandCardReader> ResetCardReader(string IP);

        [OperationContract]
        ResponseQuery<ResulCommandCardReader> ReadCard(string IP);

        [OperationContract]
        ResponseQuery<ResulCommandCardReader> EjectCard(string IP);
    }
}
