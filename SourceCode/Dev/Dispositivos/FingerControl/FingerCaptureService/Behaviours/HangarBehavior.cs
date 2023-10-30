using Hangar.Core.Inspectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Text;


namespace Hangar.Core.Behaviours
{
    /// <summary>
    /// componente que inyecta el interceptor y los componentes rest a los servicios
    /// </summary>
    public class HangarEndpointBehavior : IEndpointBehavior
    {

        public bool IsSecure { get; set; }
        public HangarEndpointBehavior()
        {
            this.IsSecure = true;
        }

        public HangarEndpointBehavior(bool _IsSecure)
        {
            this.IsSecure = _IsSecure;
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {

        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            AuthenticationMessageInspector inspector = new AuthenticationMessageInspector(IsSecure);
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);
         
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
