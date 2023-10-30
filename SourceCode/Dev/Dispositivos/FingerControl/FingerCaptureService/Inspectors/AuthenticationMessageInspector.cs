using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using Hangar.Core.General;


namespace Hangar.Core.Inspectors
{
    /// <summary>
    /// Componente que funge con dos funciones:
    /// 1. Verifica las peticiones de dominio cruzado.
    /// 2. Verifica las cabeceras de seguridad de los servicios.
    /// </summary>
    public class AuthenticationMessageInspector : IDispatchMessageInspector
    {
        private const string HeaderKey = "HangarAuthentication";
        private string method = string.Empty;
        private bool enabledSecurity = true;

        public AuthenticationMessageInspector()
        {
        }
        public AuthenticationMessageInspector(bool _enabledSecurity)
        {
            enabledSecurity = _enabledSecurity;
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            HttpRequestMessageProperty httpProp = (HttpRequestMessageProperty)request.Properties[HttpRequestMessageProperty.Name];
            if (httpProp != null)
            {
                httpProp.Headers.Add(CorsConstants.AccessControlAllowOrigin, "*");
                method = httpProp.Method;
                if (httpProp.Method == "OPTIONS")
                {
                    httpProp.Headers.Add("Cache-Control", "no-cache");
                    httpProp.Headers.Add(CorsConstants.AccessControlAllowMethods, "GET, POST");
                    httpProp.Headers.Add(CorsConstants.AccessControlMaxAge, "1728000");

                    return new
                    {
                        origin = httpProp.Headers["Origin"],
                        handlePreflight = httpProp.Method.Equals("OPTIONS",
                        StringComparison.InvariantCultureIgnoreCase)
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
                    httpProp.Headers.Add(CorsConstants.AccessControlAllowHeaders, "Origin, X-Requested-With, Content-Type, Accept, Authorization, HangarAuthentication");
                    httpProp.Headers.Add(CorsConstants.AccessControlMaxAge, "1728000");
                    httpProp.Headers.Add(CorsConstants.AccessControlRequestHeaders, "Origin, X-Requested-With, Content-Type, Accept, Authorization, HangarAuthentication");

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
