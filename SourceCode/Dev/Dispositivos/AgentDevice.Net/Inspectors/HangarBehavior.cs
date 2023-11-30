using Hangar.Core.Inspectors;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;

namespace Hangar.Core.Behaviours
{
    /// <summary>
    /// componente que inyecta el interceptor y los componentes rest a los servicios
    /// </summary>
    public class HangarEndpointBehavior : IEndpointBehavior
    {

        public HangarEndpointBehavior()
        {
            
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {

        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            AuthenticationMessageInspector inspector = new AuthenticationMessageInspector();            
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);
         

            //agregamos el comportamiento para salida por JSON
            foreach (OperationDescription od in endpoint.Contract.Operations)
            {
                if (od.Behaviors.Find<WebInvokeAttribute>() != null)
                    continue;

                WebInvokeAttribute wgop = new WebInvokeAttribute();
                wgop.UriTemplate = "/" + od.Name;
                wgop.RequestFormat = WebMessageFormat.Json;
                wgop.ResponseFormat = WebMessageFormat.Json;
                wgop.Method = "POST";
                wgop.BodyStyle = WebMessageBodyStyle.Wrapped;
                od.Behaviors.Add(wgop);
            }

        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
