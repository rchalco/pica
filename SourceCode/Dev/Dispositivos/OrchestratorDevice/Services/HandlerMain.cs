using Foundation.Stone.Application.Wrapper;
using Foundation.Stone.CrossCuting.StoneException;
using Interop.Main.Cross.Domain.Dispenser;
using Interop.Main.Cross.Domain.Orchestrator;
using Newtonsoft.Json;
using OrchestratorDevice.Contracts;
using OrchestratorDevice.Contracts.Common;
using OrchestratorDevice.Contracts.SavingAccount;
using OrchestratorDevice.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Orchestrator.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "HandlerMain" en el código y en el archivo de configuración a la vez.
    public partial class HandlerMain : IHandlerMain
    {
        public static DTOCurrencyExchangeRateATM dtoCurrencyExchangeRateATM { get; set; }
        public Response InitATM(bool force = false)
        {
            DeviceManager.InitATM(force);
            return new Response();
        }

        public ResponseObject<ResulAuthencation> Authentication(RequestAuthentication requestAuthentication)
        {
            IntegratorManager integratorManager = new IntegratorManager();
            return integratorManager.Authentication(requestAuthentication);
        }

        public ResponseDebitAccount DebitAccount(RequestDebitAccount requestDebitAccount)
        {
            IntegratorManager integratorManager = new IntegratorManager();
            return integratorManager.DebitAccount(requestDebitAccount);
        }
        public ResponseDepositAccount DepositAccount(RequestCreditAccount requestDepositAccount)
        {
            IntegratorManager integratorManager = new IntegratorManager();
            return integratorManager.DepositAccount(requestDepositAccount);
        }
        public ResponseGetAccountBalance GetAccountBalance(RequestGetAccountBalance requestDebitAccount)
        {
            IntegratorManager integratorManager = new IntegratorManager();
            return integratorManager.GetAccountBalance(requestDebitAccount);
        }
        public Response PrintDocument(ResquesPrintDocument resquesPrintDocument)
        {
            IntegratorManager integratorManager = new IntegratorManager();
            return integratorManager.PrintDocument(resquesPrintDocument.tittle, resquesPrintDocument.content);
        }

        public Response GetPrinterStatus()
        {
            IntegratorManager integratorManager = new IntegratorManager();
            return integratorManager.GetPrinterStatus();
        }
        public ResponseQuery<DTOLightningSpin> GetLightningSpin(RequestGetLightningSpin requestGetLightningSpin)
        {
            IntegratorManager integratorManager = new IntegratorManager();
            return integratorManager.GetLightningSpin(requestGetLightningSpin);
        }

        public ResponseObject<ResponseDebitLightningSpin> DebitLightningSpin(RequestDebitLightningSpin requestDebitLightningSpin)
        {
            IntegratorManager integratorManager = new IntegratorManager();
            return integratorManager.DebitLightningSpin(requestDebitLightningSpin);
        }

        public ResponseObject<GlobalConfigATM> GetHotState()
        {
            ResponseObject<GlobalConfigATM> response = new ResponseObject<GlobalConfigATM>
            {
                Message = "Configuracion obtenida correctamente",
                State = ResponseType.Success,
                Object = DeviceManager.globalConfigATM
            };
            return response;
        }

        public ResponseObject<InnerResponseTransferAmount> TransferAmount(RequestTransferAmount requestTransferAmount)
        {
            TransferAccountManager transferAccountManager = new TransferAccountManager();
            return transferAccountManager.TransferAmount(requestTransferAmount);
        }

        public ResponseObject<DTOCurrencyExchangeRateATM> GetCurrencyExchanceRateByDate(RequestGetCurrencyExchanceRateByDate objParameter)
        {
            ResponseObject<DTOCurrencyExchangeRateATM> response = new ResponseObject<DTOCurrencyExchangeRateATM>
            {
                State = ResponseType.Success,
                Message = "Cambio de moneda obtenido correctamente"
            };
            if (dtoCurrencyExchangeRateATM == null)
            {

                if (DeviceManager.responseGetCurrencyExchanceRateByDate == null)
                {
                    response.State = ResponseType.Warning;
                    response.Message = "No se obtuvo el tipo de cambio en la inicializacion";
                    return response;
                }
                dtoCurrencyExchangeRateATM = response.Object = DeviceManager.responseGetCurrencyExchanceRateByDate;
            }
            else
            {
                response.Object = dtoCurrencyExchangeRateATM;
            }
            return response;
        }

        public ResponseGetHoldersAccount GetHoldersAccount(RequestGetHoldersAccount requestGetHoldersAccount)
        {
            IntegratorManager integratorManager = new IntegratorManager();
            return integratorManager.GetHoldersAccount(requestGetHoldersAccount);
        }

        public ResponseQuery<Court> GetCoutAvailable()
        {
            IntegratorManager integratorManager = new IntegratorManager();
            return integratorManager.GetCoutAvailable();
        }

        public ResponseObject<ResponseGetFrequentAmount> GetFrequentAmounts()
        {
            IntegratorManager integratorManager = new IntegratorManager();
            return integratorManager.GetFrequentAmounts();
        }

        public ResponseObject<string> GetaQRATM(RequestGetaQRATM requestGetaQRATM)
        {
            DebitAccountQRManager debitAccountQRManager = new DebitAccountQRManager();
            return debitAccountQRManager.GetQRATM(requestGetaQRATM);
        }

        public Response EnableReceiver()
        {
            IntegratorManager integrator =new IntegratorManager();
            return integrator.EnableReceiver();
        }
        public Response DisableReceiver()
        {
            IntegratorManager integrator =new IntegratorManager();
            return integrator.DisableReceiver();
        }
        public Response GetStateReceiver()
        {
            IntegratorManager integrator = new IntegratorManager();
            return integrator.GetStateReceiver();
        }
    }
}
