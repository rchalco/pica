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

namespace AgentDevice.Net
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
            //levantamos el servicio de huellas
            ServiceHost serviceHost = new ServiceHost(typeof(FingerPrintServices), new Uri("http://localhost:2525/fingerprint"));
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior() { HttpGetEnabled = true, HttpsGetEnabled = true };
            //liberamos metadata
            serviceHost.Description.Behaviors.Add(smb);
            //liberamos los errores
            serviceHost.Description.Behaviors.Find<ServiceDebugBehavior>().IncludeExceptionDetailInFaults = true;
            //adicionamos endpoint
            WebHttpBinding bindingREST = new WebHttpBinding() { Name = "rest", CrossDomainScriptAccessEnabled = true };

            var endpointREST = serviceHost.AddServiceEndpoint(typeof(IFingerPrintServices), bindingREST, "http://localhost:2525/fingerprint/rest");
            endpointREST.EndpointBehaviors.Add(new WebHttpBehavior
            {
                HelpEnabled = true,
                DefaultBodyStyle = System.ServiceModel.Web.WebMessageBodyStyle.Wrapped,
                DefaultOutgoingRequestFormat = System.ServiceModel.Web.WebMessageFormat.Json,
                DefaultOutgoingResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json
            });
            //configuramos el enpoint
            endpointREST.Binding.CloseTimeout = TimeSpan.Parse("00:10:00");
            endpointREST.Binding.OpenTimeout = TimeSpan.Parse("00:10:00");
            endpointREST.Binding.ReceiveTimeout = TimeSpan.Parse("00:10:00");
            endpointREST.Binding.SendTimeout = TimeSpan.Parse("00:10:00");

            bindingREST.MaxBufferPoolSize = 524288;
            bindingREST.MaxBufferSize = 524288;
            bindingREST.MaxReceivedMessageSize = 524288;
            bindingREST.OpenTimeout = TimeSpan.Parse("00:10:00");
            //bindingREST.ProxyAddress = string.IsNullOrEmpty(x.ProxyAdress) ? null : new Uri(x.ProxyAdress);
            bindingREST.ReceiveTimeout = TimeSpan.Parse("00:10:00");
            bindingREST.SendTimeout = TimeSpan.Parse("00:10:00");
            bindingREST.UseDefaultWebProxy = false;
            serviceHost.Open();
        }

        private void frmMain_MinimumSizeChanged(object sender, EventArgs e)
        {
           
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                this.notifyIcon1.Visible = true;
                this.notifyIcon1.ShowBalloonTip(2000);
            }
        }
    }
}
