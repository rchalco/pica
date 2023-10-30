using Foundation.Stone.Application.Wrapper;
using Hangar.ServiceImplement.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Global
{
    public static class Setttings
    {
        public static ExternalConfigurartion externalConfiguration = new ExternalConfigurartion();
        public static string ConfigurationLooger = externalConfiguration.GetAppSettingsFromCurrentAssembly().Settings["logConfig"].Value;
        public static string uriSecurity = externalConfiguration.GetAppSettingsFromCurrentAssembly().Settings["urlServiceBaseSecurity"].Value;
        public static string uriBaseServices = externalConfiguration.GetAppSettingsFromCurrentAssembly().Settings["urlServiceBase"].Value;
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
    }
}
