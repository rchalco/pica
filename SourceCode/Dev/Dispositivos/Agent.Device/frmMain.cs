using RuntimeFingerPrint.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Agent.Device
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnHuella_Click(object sender, EventArgs e)
        {
            FingerPrintServices fingerPrintServices = new FingerPrintServices();
            var resulFinger = fingerPrintServices.CaptureFinger(5000);
            MessageBox.Show(resulFinger.Message);
            if (resulFinger.State == Foundation.Stone.Application.Wrapper.ResponseType.Success)
            {

            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            ServiceHost serviceHost = new ServiceHost(typeof(FingerPrintServices), new Uri("http://localhost:2525/fingerprint"));
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior() { HttpGetEnabled = true, HttpsGetEnabled = true };
            //liberamos metadata
            serviceHost.Description.Behaviors.Add(smb);
            //liberamos los errores
            serviceHost.Description.Behaviors.Find<ServiceDebugBehavior>().IncludeExceptionDetailInFaults = true;
            //adicionamos endpoint
            WebHttpBinding bindingREST = new WebHttpBinding() { Name = "rest", CrossDomainScriptAccessEnabled = true };

            var endpointREST = sh.AddServiceEndpoint(x.TypeContractInstance, bindingREST, URIService);
            endpointREST.EndpointBehaviors.Add(new WebHttpBehavior
            {
                HelpEnabled = true,
                DefaultBodyStyle = System.ServiceModel.Web.WebMessageBodyStyle.Wrapped,
                DefaultOutgoingRequestFormat = System.ServiceModel.Web.WebMessageFormat.Json,
                DefaultOutgoingResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json
            });
            //configuramos el enpoint
            endpointREST.Binding.CloseTimeout = TimeSpan.Parse(x.CloseTimeout);
            endpointREST.Binding.OpenTimeout = TimeSpan.Parse(x.OpenTimeOut);
            endpointREST.Binding.ReceiveTimeout = TimeSpan.Parse(x.ReceiveTimeout);
            endpointREST.Binding.SendTimeout = TimeSpan.Parse(x.SendTimeout);

            bindingREST.MaxBufferPoolSize = long.Parse(x.MaxBufferPoolSize);
            bindingREST.MaxBufferSize = int.Parse(x.MaxBufferSize);
            bindingREST.MaxReceivedMessageSize = long.Parse(x.MaxReceivedMessageSize);
            bindingREST.OpenTimeout = TimeSpan.Parse(x.OpenTimeOut);
            bindingREST.ProxyAddress = string.IsNullOrEmpty(x.ProxyAdress) ? null : new Uri(x.ProxyAdress);
            bindingREST.ReceiveTimeout = TimeSpan.Parse(x.ReceiveTimeout);
            bindingREST.SendTimeout = TimeSpan.Parse(x.SendTimeout);
            bindingREST.UseDefaultWebProxy = bool.Parse(x.UseDeftaultWebProxy);
            endpointREST.EndpointBehaviors.Add(new HangarEndpointBehavior(item.IsSecure, _ValidateToken, customInspector));

        }
    }
}
