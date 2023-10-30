using RuntimeDispensador.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDispensador
{
    partial class DispenserDaemon : ServiceBase
    {
        internal static ServiceHost myServiceHost = null;
        public DispenserDaemon()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (myServiceHost != null && myServiceHost.State == CommunicationState.Opened)
                {
                    myServiceHost.Close();
                }
                //FingerprintComparerServiceLibrary.FingerprintCompService.LogError = escribirLog;
                myServiceHost = new ServiceHost(typeof(Dispensador));
                myServiceHost.Open();
                escribirLog("Servicio Dispensador iniciado");
            }
            catch (Exception ex)
            {
                escribirLog($"Error al iniciar el servicio:  {ex.Message}");
            }
            
        }

        protected override void OnStop()
        {
            try
            {
                if (myServiceHost != null)
                {
                    myServiceHost.Close();
                    myServiceHost = null;
                }
                escribirLog("Servicio Dispensador detenido");
            }
            catch (Exception ex)
            {
                escribirLog($"Error al detener el servicio: {ex.Message}");
            }

        }

        public void escribirLog(string mensaje)
        {
            string sSource;
            string sLog;

            sSource = "LogDispensador";
            sLog = "LogDispensador";

            if (!EventLog.SourceExists(sSource))
                EventLog.CreateEventSource(sSource, sLog);

            EventLog.WriteEntry(sSource, mensaje, EventLogEntryType.Warning, 234);
        }
    }
}
