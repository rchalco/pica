using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMonitoreo.Global
{
    internal class Settings
    {
        static public string LogName = "MonitoreoATM";
        static public string URITemplateCardReader = "http://IP:4000/rest/RuntimeCardReader/";
        static public string URITemplateFingerPrint = "http://IP:4000/rest/RuntimeFingerPrint/";
    }
}
