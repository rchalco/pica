using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMonitoreo.Util
{
    public class ClientRestHelper
    {
        public async Task<T> Consume<T>(string URI, Object parameter) where T : class, new()
        {
            T resul = new T();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(URI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Accept", "*/*");
                client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                client.Timeout = TimeSpan.FromSeconds(90);

                var content = new StringContent(JsonConvert.SerializeObject(parameter), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(URI, content);
                var data = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    resul = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
                }
                else
                {
                    throw new Exception($"No se puede consumir el servicio. Error: {data}");
                }
            }
            return resul;
        }

        public async Task<T> Consume<T>(string URI, string innerNameFirstChild, string parameter) where T : class, new()
        {
            T resul = new T();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(URI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Accept", "*/*");
                client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                client.Timeout = TimeSpan.FromSeconds(90);

                var content = new StringContent(parameter, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(URI, content);
                var data = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    IDictionary<string, object> responseService = (IDictionary<string, object>)JsonConvert.DeserializeObject<ExpandoObject>(data);
                    resul = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(responseService[innerNameFirstChild]));
                }
                else
                {
                    throw new Exception($"No se puede consumir el servicio. Error: {data}");
                }
            }
            return resul;
        }

        public async Task<string> ConsumeToString(string URI, Object parameter)
        {
            string resul = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(URI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Accept", "*/*");
                client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                client.Timeout = TimeSpan.FromSeconds(90);

                string innerContentParameter = "{}";
                if (parameter != null)
                {
                    innerContentParameter = JsonConvert.SerializeObject(parameter);
                }
                var content = new StringContent(innerContentParameter, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(URI, content);
                var data = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    resul = data;
                }
                else
                {
                    throw new Exception($"No se puede consumir el servicio. Error: {data}");
                }
            }
            return resul;
        }

        public async Task<string> ConsumeToString(string URI, string parameter)
        {
            string resul = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(URI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Accept", "*/*");
                client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                client.Timeout = TimeSpan.FromSeconds(90);

                var content = new StringContent(parameter, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(URI, content);
                var data = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    resul = data;
                }
                else
                {
                    throw new Exception($"No se puede consumir el servicio. Error: {data}");
                }
            }
            return resul;
        }
    }
}



