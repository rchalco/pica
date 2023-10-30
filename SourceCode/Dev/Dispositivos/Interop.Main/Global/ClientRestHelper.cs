using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Interop.Main.Global
{
    public class ClientRestHelper
    {
        public async Task<T> Consume<T>(string URI, Object parameter, string innerParameter = "", string token = "") where T : class, new()
        {
            T resul = new T();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(URI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Accept", "*/*");
                client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Add("HangarAuthentication", token);
                }
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                string contentJson = string.Empty;
                if (!string.IsNullOrEmpty(innerParameter) && parameter != null)
                {
                    contentJson = "{ \"" + innerParameter + "\" :{ " + JsonConvert.SerializeObject(parameter) + " } }";
                }
                else
                {
                    contentJson = JsonConvert.SerializeObject(parameter);
                }

                client.Timeout = TimeSpan.FromSeconds(90);
                var content = parameter == null ? new StringContent("{}", Encoding.UTF8, "application/json") : new StringContent(contentJson, Encoding.UTF8, "application/json");


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



