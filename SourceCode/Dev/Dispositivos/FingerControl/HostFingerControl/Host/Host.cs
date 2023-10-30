using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Prodem.Fingerprint.FingerPrintControl.Util;
using HostFingerControl.Service;

namespace HostFingerControl.Hosting
{
    public static class Host
    {
        internal static ServiceHost myServiceHost = null;
        public static void Start()
        {
            if (myServiceHost != null)
            {
                myServiceHost.Close();
            }
            myServiceHost = new ServiceHost(typeof(FingerComponentService));
            myServiceHost.Open();
            Logger.Write("Servicio de Captura de Huellas iniciado", System.Diagnostics.EventLogEntryType.Information);
        }
        public static void Stop()
        {
            if (myServiceHost != null)
            {
                myServiceHost.Close();
                myServiceHost = null;
            }
            Logger.Write("Servicio de comparacion de huellas detenido", System.Diagnostics.EventLogEntryType.Information);
        }
    }
}
