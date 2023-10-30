using Foundation.Stone.Application.Wrapper;
using Foundation.Stone.Business.Core;
using Foundation.Stone.CrossCuting.StoneException;
using Hangar.ServiceImplement.Config;
using Hangar.ServiceImplement.Factory;
using Interop.Main.Cross.Domain.Orchestrator;
using Interop.Main.Service.Interface;
using Newtonsoft.Json;
using Orchestrator.Global;
using OrchestratorDevice.Contracts;
using OrchestratorDevice.Contracts.Base;
using OrchestratorDevice.Contracts.Common;
using OrchestratorDevice.Contracts.LoanFlow;
using OrchestratorDevice.Global;
using OrchestratorDevice.Util;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Managers
{
    public class CommonManager : BaseManager
    {
        internal ClientRestHelper clientRestHelper = new ClientRestHelper();
        internal static ExternalConfigurartion externalConfiguration = new ExternalConfigurartion();
        internal static string loggerName = externalConfiguration.GetAppSettingsFromCurrentAssembly().Settings["logConfig"].Value;

        public CommonManager()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }

        protected static void ProcessError(Response response, Exception exception, System.Diagnostics.EventLogEntryType eventLogEntryType = System.Diagnostics.EventLogEntryType.Error)
        {
            response.State = ResponseType.Error;
            response.Message = exception.GetAllErrorMessage();
            if (response.Message?.Contains("8700 - Estado Inactivo ATM") == true)
            {
                response.CodeBase = "8700";
                response.Message = "ATM Inactivo";
            }
            else if (exception is FaultException)
            {
                response.Message = exception.Message;
            }
            else
            {
                StoneException stoneException = new StoneException(exception);
                ExceptionManager.ProcessError(stoneException);
                response.Message = ExceptionManager.MsgError;
            }
            Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO(loggerName, response.Message + " . StackTrace: " + exception.StackTrace, eventLogEntryType);
        }

        internal bool ATMTransactionConfirm(BaseRequest objRequest, ATMTransactionConfirmData objConfirmData)
        {
            var result = false;

            RequestATMConfirm objParamData = new RequestATMConfirm
            {
                objATMConfirmData = new RequestATMConfirmDTO
                {
                    ATMCoinageDetail = objConfirmData.ATMCoinageDetail,
                    ATMCoinageRejected = objConfirmData.ATMCoinageRejected,
                    ATMCoinageTray = objConfirmData.ATMCoinageTray,
                    ATMErrorDetail = objConfirmData.ATMErrorDetail,
                    ATMTransactionCode = objConfirmData.ATMTransactionCode,
                    IdATMTransaction = 0,
                    WithError = objConfirmData.WithError
                },
                //StateATM = DeviceManager.globalConfigATM != null ? JsonConvert.SerializeObject(DeviceManager.globalConfigATM) : "",
                Token = objRequest.Token
            };

            var resMFConfirm = clientRestHelper.Consume<ResponseATMConfirm>(Setttings.uriBaseServices + "/ATMTransactionConfirm", objParamData, objParamData.Token).Result;

            if (resMFConfirm != null && resMFConfirm.ATMTransactionConfirmResult != null
                && resMFConfirm.ATMTransactionConfirmResult.State == ResponseType.Success)
            {
                result = true;
            }

            return result;
        }

        internal Response PrintDocument(string tittle, string content)
        {
            Response resul = new Response { State = ResponseType.Success, Message = "Impresion correcta" };
            try
            {
                PrinterUtil.PrintVoucher(content, tittle);
            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
                resul.Message = ExceptionManager.MsgError;
            }
            return resul;
        }
        internal Response GetPrinterStatus()
        {
            Response response = new Response();
            PrinterUtil printerUtil = new PrinterUtil();
            Response resul = new Response { State = ResponseType.Success, Message = "Impresora correcta" };
            try
            {
                PrinterStatus printerStatus = printerUtil.GetPrinterStatus();
                if (printerStatus.IdStatus == 1)
                {
                    response.State = ResponseType.Success;
                }
                else
                {
                    response.State = ResponseType.Error;
                }
                response.Message = printerStatus.DescStatus;
                response.CodeBase = printerStatus.PrinterName;
            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
                resul.Message = ExceptionManager.MsgError;
            }
            return response;
        }

        internal ResponseObject<string> CreateTicketForOperationExternalService(RequestAtmOperationTicket objParameter)
        {
            ResponseObject<string> objResult = new ResponseObject<string> { };
            try
            {
                string eventPath = FileHelper.writeEvent("Authentication: " + JsonConvert.SerializeObject(objParameter));

                dynamic request = new ExpandoObject();
                request.objParameters = objParameter;

                objResult = clientRestHelper.Consume<ResponseAtmOperationTicket>(Setttings.uriBaseServices + "/CreateTicketForOperationExternalService", request, objParameter.Token).Result.CreateTicketForOperationExternalServiceResult;

                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(objResult, ex);
                objResult.Message = ex is FaultException ? ex.Message : ExceptionManager.MsgError;
            }
            return objResult;
        }

        public ResponseObject<DTOCurrencyExchangeRateATM> GetCurrencyExchanceRateByDate(RequestGetCurrencyExchanceRateByDate objParameter)
        {
            ResponseObject<DTOCurrencyExchangeRateATM> objResult = new ResponseObject<DTOCurrencyExchangeRateATM> { };
            var CardReaderInterop = MicroService.CreateInstance<ICardReaderInterop>();
            try
            {
                string eventPath = FileHelper.writeEvent($"GetCurrencyExchanceRateByDate Id Money: {objParameter.requestCurrencyExchanceRateByDate.IdMoney}");
                objResult = clientRestHelper.Consume<ResponseGetCurrencyExchanceRateByDate>(Setttings.uriBaseServices + "/GetCurrencyExchanceRateByDate", objParameter, objParameter.Token).Result.GetCurrencyExchanceRateByDateResult;
                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(objResult, ex);
                objResult.Message = ex is FaultException ? ex.Message : ExceptionManager.MsgError;
            }
            finally
            {
                CardReaderInterop.EjectCard();
            }
            return objResult;
        }
        public ResponseObject<ResponseGetFrequentAmount> GetFrequentAmounts()
        {
            int[] amounts;

            ResponseATMParameter responseATM = new ResponseATMParameter();


            ResponseObject<ResponseGetFrequentAmount> objResult = new ResponseObject<ResponseGetFrequentAmount> { };
            ResponseObject<ResponseOffSetMount> responseObject = new ResponseObject<ResponseOffSetMount> { };

            if (DeviceManager.globalConfigATM.configDispenserStatus.NumBandejas == 2)
            {
                amounts = new int[8] { 20, 60, 80, 100, 200, 300, 500, 1000 };
            }
            else
            {
                amounts = new int[8] { 20, 50, 80, 100, 200, 300, 500, 100 };
            }
            int orden;
            try
            {
                objResult.State = Foundation.Stone.Application.Wrapper.ResponseType.Success;
                orden = 0;

                foreach (int amount in amounts)
                {
                    var respObject = clientRestHelper.Consume<ResponseOffSetMount>(Setttings.uriBaseServices + "/OffSetMountToString", amount).Result;

                    if (respObject?.Comand != "")
                    {
                        objResult.Object.FrequentAmount.Add(new FrequentAmount { estado = 1, valor = amount, orden = orden });
                    }
                    else
                    {
                        objResult.Object.FrequentAmount.Add(new FrequentAmount { estado = 0, valor = amount, orden = orden });
                    }
                    orden++;
                }
            }
            catch (Exception ex)
            {
                ProcessError(objResult, ex);
                objResult.Message = ex is FaultException ? ex.Message : ExceptionManager.MsgError;
            }
            return objResult;
        }

    }
}
