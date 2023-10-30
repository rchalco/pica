using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Hangar.ServiceImplement.Config;

namespace RuntimeDispensador.Structural
{
    public static class SetConfiguration
    {
        public static ExternalConfigurartion externalConfiguration = new ExternalConfigurartion();
        public static string pathMainDispenserConfig = externalConfiguration.GetAppSettingsFromCurrentAssembly().Settings["pathMainDispenserConfig"].Value;

        public static string ConfigCommand = Path.Combine(pathMainDispenserConfig, "ConfigComand.xml");
        public static string ConfigResponse = Path.Combine(pathMainDispenserConfig, "ConfigResponse.xml");

    }
}
