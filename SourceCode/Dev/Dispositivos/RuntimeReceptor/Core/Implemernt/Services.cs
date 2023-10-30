using Newtonsoft.Json;
using NMDManagement;
using RegisterLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RuntimeReceptor.Core.Implemernt
{
    internal class Services
    {
        string url_getATM = GetAppSetting.GetSetting("URLServices") + "/rest/ATM.Services.IAtmServices/GetATMById";
        string url_updateATM = GetAppSetting.GetSetting("URLServices") + "/rest/ATM.Services.IAtmServices/UpdateATM";

        public static bool ValidateServerCertificate(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        public ConfigurationATM GetATM(string pToken, int pIdATM)
        {
            dynamic dataParam = new { idATM = pIdATM };
            ConfigurationATM urlResult = new ConfigurationATM();
            string result = "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url_getATM);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Accept", "*/*");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    if (!string.IsNullOrEmpty(pToken))
                    {
                        client.DefaultRequestHeaders.Add("HangarAuthentication", pToken);
                    }
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                    client.Timeout = TimeSpan.FromSeconds(90);
                    var content = new StringContent(JsonConvert.SerializeObject(dataParam), Encoding.UTF8, "application/json");

                    var resultServices = client.PostAsync(url_getATM, content).Result;
                    result = resultServices.Content.ReadAsStringAsync().Result;

                    urlResult = JsonConvert.DeserializeObject<ConfigurationATM>(result);

                }
            }
            catch (Exception error)
            {
                urlResult.GetATMByIdResult = new ConfigurationATM.ReadResult();
                urlResult.GetATMByIdResult.State = ResponseType.Error;
                urlResult.GetATMByIdResult.Message = error.Message;
                //throw;
            }
            return urlResult;
        }

        
        public ResultUpdateATM UpadateATM(ATMDTO pConfigurationATM, string pToken)
        {
            dynamic dataParam = new { atmDTO = pConfigurationATM };
            ResultUpdateATM urlResult = new ResultUpdateATM();
            string result = "";
            try
            {
                pConfigurationATM.Profile.InactivationDate = null;

                loggerATM.PsRegisterLogger("UpadateATM", "Parametro: " + JsonConvert.SerializeObject(pConfigurationATM));
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url_updateATM);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Accept", "*/*");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    if (!string.IsNullOrEmpty(pToken))
                    {
                        client.DefaultRequestHeaders.Add("HangarAuthentication", pToken);
                    }
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                    client.Timeout = TimeSpan.FromSeconds(90);
                    var content = new StringContent(JsonConvert.SerializeObject(dataParam), Encoding.UTF8, "application/json");
                    loggerATM.PsRegisterLogger("UpadateATM", "ante de enviar al servicio: " + JsonConvert.SerializeObject(dataParam));
                    var resultServices = client.PostAsync(url_updateATM, content).Result;
                    result = resultServices.Content.ReadAsStringAsync().Result;
                    loggerATM.PsRegisterLogger("UpadateATM", "Respuesta Servicio:" + resultServices.ToString());
                    urlResult = JsonConvert.DeserializeObject<ResultUpdateATM>(result);
                }
            }
            catch (Exception error)
            {
                urlResult.UpdateATMResult = new ResultUpdateATM.ReadResult();
                urlResult.UpdateATMResult.State = ResponseType.Error;
                urlResult.UpdateATMResult.Message = error.Message;
                //throw;
            }
            return urlResult;
        }
    }
}
