using Hangar.ServiceImplement.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuntimeFingerPrint.Global
{
    public static class Setings
    {
        static ExternalConfigurartion externalConfiguration = new ExternalConfigurartion();

        public static string LogConfigName
        {
            get
            {
                return externalConfiguration.GetAppSettingsFromCurrentAssembly().Settings["LogConfigName"].Value;
            }
        }
    }
}
