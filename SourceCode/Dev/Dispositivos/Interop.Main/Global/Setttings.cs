using Foundation.Stone.Application.Wrapper;
using Foundation.Stone.Data.Core;
using Hangar.ServiceImplement.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Interop.Main.Global
{
    public static class Setttings
    {
        public static ExternalConfigurartion externalConfiguration = new ExternalConfigurartion();
        public static string ConfigurationLooger = "InteopMainATM";//ConfigurationManager.AppSettings["LogConfigName"];
        //public static string uriSecurity = externalConfiguration.GetAppSettingsFromCurrentAssembly().Settings["urlServiceBaseSecurity"].Value;
        //public static string uriBaseServices = externalConfiguration.GetAppSettingsFromCurrentAssembly().Settings["urlServiceBase"].Value;
        public static void LoggerResponse(Response response)
        {
            System.Diagnostics.EventLogEntryType eventLogEntryType = System.Diagnostics.EventLogEntryType.Information;
            switch (response.State)
            {
                case ResponseType.Success:
                    eventLogEntryType = System.Diagnostics.EventLogEntryType.Information;
                    break;
                case ResponseType.Warning:
                    eventLogEntryType = System.Diagnostics.EventLogEntryType.Warning;
                    break;
                case ResponseType.Error:
                    eventLogEntryType = System.Diagnostics.EventLogEntryType.Error;
                    break;
                case ResponseType.NoData:
                    eventLogEntryType = System.Diagnostics.EventLogEntryType.Information;
                    break;
                default:
                    eventLogEntryType = System.Diagnostics.EventLogEntryType.Error;
                    break;
            }
            Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO(ConfigurationLooger, response.Message, eventLogEntryType);
        }

        public static void LoggerEvent(string message, System.Diagnostics.EventLogEntryType eventLogEntryType)
        {
            Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO(ConfigurationLooger, message, eventLogEntryType);
        }

        public static string CurrentIp
        {
            get
            {
                List<string> LocalIps = new List<string>();
                string ips = string.Empty;
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        LocalIps.Add(ip.ToString());
                        ips += ip.ToString() + " | ";
                    }
                }
                return ips;
            }
        }
    }
}
