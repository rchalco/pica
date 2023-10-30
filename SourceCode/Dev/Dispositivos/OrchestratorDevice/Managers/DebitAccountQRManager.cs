using Foundation.Stone.Application.Wrapper;
using Foundation.Stone.Business.Core;
using Foundation.Stone.CrossCuting.Util;
using Interop.Main.Global;
using Newtonsoft.Json;
using OrchestratorDevice.Contracts.SavingAccount;
using System;
using System.Net;

namespace OrchestratorDevice.Managers
{
    public class DebitAccountQRManager : CommonManager
    {

        public DebitAccountQRManager()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }

        public ResponseObject<string> GetQRATM(RequestGetaQRATM requestGetaQRATM)
        {
            ResponseObject<string> resul = new ResponseObject<string>
            {
                State = ResponseType.Success,
                Message = ""
            };
            try
            {
                string _uuid = CustomGuid.GetGuid();
                requestGetaQRATM.requestGetaQRATM.uuid = _uuid;

                ///TODO creamos el archivo de evidencia
                string eventPath = FileHelper.writeEvent("GetQRATM: " + JsonConvert.SerializeObject(_uuid));
                resul = clientRestHelper.Consume<ResponseGetQRATM>(OrchestratorDevice.Global.Setttings.uriBaseServices + "/GetaQRATM", requestGetaQRATM, DeviceManager.token).Result.GetaQRATMResult;

                ///TODO eliminamos el archivo de evidencia
                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }
            return resul;
        }
    }
}
