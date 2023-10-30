using Hangar.ServiceImplement.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuntimeCardReader.Global
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

        public static string URIServerLog
        {
            get
            {
                return externalConfiguration.GetAppSettingsFromCurrentAssembly().Settings["URIServerLog"].Value;
            }
        }

        public static string CurrentVersion
        {
            get
            {
                return externalConfiguration.GetAppSettingsFromCurrentAssembly().Settings["Version"].Value;
            }
        }

        public static string PAN_TAG = "5A";//"57";
        public static List<string> _LIST_ALLOWBIN = new List<string>();
        public static List<string> LIST_ALLOWBIN
        {
            get
            {
                if (_LIST_ALLOWBIN.Count == 0)
                {
                    _LIST_ALLOWBIN.Add("526520");
                    _LIST_ALLOWBIN.Add("52652001");
                    _LIST_ALLOWBIN.Add("72652100");
                }
                return _LIST_ALLOWBIN;
            }
        }
    }
}
