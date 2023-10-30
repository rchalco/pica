﻿using Newtonsoft.Json;
using OrchestratorDevice.Managers;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Orchestrator.Global
{
    public class ClientRestHelper
    {
        public async Task<T> Consume<T>(string URI, Object parameter, string token = "") where T : class, new()
        {
            T resul = new T();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(URI);
                client.Timeout = TimeSpan.FromMinutes(3);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Accept", "*/*");
                client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Add("HangarAuthentication", token);
                }
                if (DeviceManager.globalConfigATM != null)
                {
                    client.DefaultRequestHeaders.Add("NewStateATM", JsonConvert.SerializeObject(DeviceManager.globalConfigATM));
                }

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);

                client.Timeout = TimeSpan.FromSeconds(90);
                var content = parameter == null ? new StringContent("{}", Encoding.UTF8, "application/json") : new StringContent(JsonConvert.SerializeObject(parameter), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(URI, content);
                var data = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    resul = JsonConvert.DeserializeObject<T>(data);
                }
                else
                {
                    throw new Exception($"No se puede consumir el servicio. Error: {data}");
                }
            }
            return resul;
        }

        public static bool ValidateServerCertificate(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}



