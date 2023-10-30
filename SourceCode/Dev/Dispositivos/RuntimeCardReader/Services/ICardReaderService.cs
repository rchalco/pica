using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Foundation.Stone.Application.Wrapper;
using Interop.Main.Cross.Domain.CardReader;
using RuntimeCardReader.Domain;

namespace RuntimeCardReader.Services
{
    // NOTA: puede usar el comando "Cambiar nombre" del menú "Refactorizar" para cambiar el nombre de interfaz "ICardReaderService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ICardReaderService
    {
        [OperationContract]
        ResponseQuery<CommandCardReader> InitReader(TypeReaderCard typeReader);
        [OperationContract]
        ResponseQuery<CommandCardReader> ReadCard();
        [OperationContract]
        ResponseQuery<CommandCardReader> EjectCard();
        [OperationContract]
        ResponseQuery<CommandCardReader> Reset();

    }
}
