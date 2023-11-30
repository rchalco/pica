using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using AgentDevice.Net.Inspectors;
using Foundation.Stone.Application.Wrapper;

namespace Hangar.Core.Inspectors
{
    /// <summary>
    /// Componente que funge con dos funciones:
    /// 1. Verifica las peticiones de dominio cruzado.
    /// 2. Verifica las cabeceras de seguridad de los servicios.
    /// </summary>
    public class AuthenticationMessageInspector : IDispatchMessageInspector
    {

        private string method = string.Empty;

        public AuthenticationMessageInspector()
        {
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            HttpRequestMessageProperty httpProp = (HttpRequestMessageProperty)request.Properties[HttpRequestMessageProperty.Name];

            string operationName = string.Empty;
            string serviceName = string.Empty;

            var action = OperationContext.Current.IncomingMessageHeaders.Action;

            if (!string.IsNullOrEmpty(action))//peticion SOAP
            {
                operationName = action.Substring(action.LastIndexOf("/", StringComparison.OrdinalIgnoreCase) + 1);
                serviceName = request.Properties.Via.AbsolutePath.Split('/')[1];
            }
            else//peticion http
            {
                operationName = OperationContext.Current.IncomingMessageProperties["HttpOperationName"] as string;
                serviceName = request.Properties.Via.AbsolutePath.Split('/')[2];
            }



            if (httpProp != null)
            {
                httpProp.Headers.Add(CorsConstants.AccessControlAllowOrigin, "*");
                method = httpProp.Method;
                if (httpProp.Method == "OPTIONS")
                {
                    httpProp.Headers.Add("Cache-Control", "no-cache");
                    httpProp.Headers.Add(CorsConstants.AccessControlAllowMethods, "GET, POST");
                    httpProp.Headers.Add(CorsConstants.AccessControlAllowHeaders, "Origin, X-Requested-With, Content-Type, Accept, Authorization");
                    httpProp.Headers.Add(CorsConstants.AccessControlMaxAge, "1728000");                    
                    httpProp.Headers.Add(CorsConstants.AccessControlRequestHeaders, "Origin, X-Requested-With, Content-Type, Accept, Authorization");

                    return new
                    {
                        origin = httpProp.Headers["Origin"],
                        handlePreflight = httpProp.Method.Equals("OPTIONS", StringComparison.InvariantCultureIgnoreCase)
                    };
                }


            }

            return null;

        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {

            HttpResponseMessageProperty httpProp = null;

            if (reply.Properties.ContainsKey(HttpResponseMessageProperty.Name))
            {
                httpProp = (HttpResponseMessageProperty)reply.Properties[HttpResponseMessageProperty.Name];
            }
            else
            {
                httpProp = new HttpResponseMessageProperty();
                reply.Properties.Add(HttpResponseMessageProperty.Name, httpProp);
            }

            if (httpProp != null)
            {
                httpProp.Headers.Add(CorsConstants.AccessControlAllowOrigin, "*");
                if (method.Equals("OPTIONS"))
                {
                    httpProp.Headers.Add("Cache-Control", "no-cache");
                    httpProp.Headers.Add(CorsConstants.AccessControlAllowMethods, "GET, POST");
                    httpProp.Headers.Add(CorsConstants.AccessControlAllowHeaders, "Origin, X-Requested-With, Content-Type, Accept, Authorization");
                    httpProp.Headers.Add(CorsConstants.AccessControlMaxAge, "1728000");                    
                    httpProp.Headers.Add(CorsConstants.AccessControlRequestHeaders, "Origin, X-Requested-With, Content-Type, Accept, Authorization");

                }
                reply.Properties[HttpResponseMessageProperty.Name] = httpProp;

                System.ServiceModel.Channels.HttpResponseMessageProperty resProp = reply.Properties.Values.OfType<System.ServiceModel.Channels.HttpResponseMessageProperty>().FirstOrDefault();
                if (resProp.StatusCode == System.Net.HttpStatusCode.MethodNotAllowed && method.Equals("OPTIONS"))
                {
                    resProp.StatusCode = System.Net.HttpStatusCode.OK;
                    resProp.SuppressEntityBody = true;
                }
            }


        }
    }
}
