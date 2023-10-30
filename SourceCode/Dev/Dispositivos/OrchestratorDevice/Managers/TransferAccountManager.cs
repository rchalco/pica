using Foundation.Stone.Application.Wrapper;
using Foundation.Stone.CrossCuting.StoneException;
using OrchestratorDevice.Global;
using Newtonsoft.Json;
using OrchestratorDevice.Contracts.SavingAccount;
using System;
using System.Net;
using System.ServiceModel;

namespace OrchestratorDevice.Managers
{
    public class TransferAccountManager : CommonManager
    {

        public TransferAccountManager()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }
        public ResponseObject<InnerResponseTransferAmount> TransferAmount(RequestTransferAmount requestTransferAmount)
        {
            ResponseObject<InnerResponseTransferAmount> resul = new ResponseObject<InnerResponseTransferAmount>
            {
                State = ResponseType.Success,
                Message = "Transferencia Realizada de forma exitosa",
            };
            try
            {
                string eventPath = FileHelper.writeEvent("TransferAmount: " + JsonConvert.SerializeObject(requestTransferAmount));
                var resulService = clientRestHelper.Consume<ResponseTransferAmount>(Setttings.uriBaseServices + "/TransferAccounts", requestTransferAmount, requestTransferAmount.Token).Result;
                resul = resulService.TransferAccountsResult;
                ///Quitando mensaje de clase en voucher de impresion                
                FileHelper.deleteEvent(eventPath);
                resul.Object.Voucher = resul.Object?.Voucher?.Replace("<ClosureMessage>", "");
                resul.Object.Voucher = resul.Object?.Voucher?.Replace("</ClosureMessage>", "");
            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
                resul.Message = ex is FaultException ? ex.Message : ExceptionManager.MsgError;
            }
            return resul;
        }
    }
}
