using Foundation.Stone.Application.Wrapper;
using Interop.Main.Cross.Domain.Dispenser;
using Interop.Main.Cross.Domain.Orchestrator;
using OrchestratorDevice.Contracts;
using OrchestratorDevice.Contracts.Common;
using OrchestratorDevice.Contracts.SavingAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Orchestrator.Services
{
    // NOTA: puede usar el comando "Cambiar nombre" del menú "Refactorizar" para cambiar el nombre de interfaz "IHandlerMain" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public partial interface IHandlerMain
    {

        [OperationContract]
        Response InitATM(bool force = false);

        [OperationContract]
        ResponseObject<ResulAuthencation> Authentication(RequestAuthentication requestAuthentication);

        [OperationContract]
        ResponseDebitAccount DebitAccount(RequestDebitAccount requestDebitAccount);

        [OperationContract]
        ResponseDepositAccount DepositAccount(RequestCreditAccount requestDepositAccount);

        [OperationContract]
        ResponseGetAccountBalance GetAccountBalance(RequestGetAccountBalance requestGetAccountBalance);

        [OperationContract]
        ResponseQuery<DTOLightningSpin> GetLightningSpin(RequestGetLightningSpin requestGetLightningSpin);

        [OperationContract]
        ResponseObject<ResponseDebitLightningSpin> DebitLightningSpin(RequestDebitLightningSpin requestDebitLightningSpin);

        [OperationContract]
        Response PrintDocument(ResquesPrintDocument resquesPrintDocument);

        [OperationContract]
        Response GetPrinterStatus();

        [OperationContract]
        ResponseObject<GlobalConfigATM> GetHotState();

        [OperationContract]
        ResponseObject<InnerResponseTransferAmount> TransferAmount(RequestTransferAmount requestTransferAmount);

        [OperationContract]
        ResponseObject<DTOCurrencyExchangeRateATM> GetCurrencyExchanceRateByDate(RequestGetCurrencyExchanceRateByDate objParameter);

        [OperationContract]
        ResponseGetHoldersAccount GetHoldersAccount(RequestGetHoldersAccount requestGetHoldersAccount);
        [OperationContract]
        ResponseQuery<Court> GetCoutAvailable();

        [OperationContract]
        ResponseObject<ResponseGetFrequentAmount> GetFrequentAmounts();
        [OperationContract]
        ResponseObject<string> GetaQRATM(RequestGetaQRATM requestGetaQRATM);

        [OperationContract]
        Response DisableReceiver();

        [OperationContract]
        Response EnableReceiver();

        [OperationContract]
        Response GetStateReceiver();
    }
}
