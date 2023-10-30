using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMDManagement
{
    public class GetAppSetting
    {
        public static  string GetSetting(string pKey)
        {
            return ConfigurationManager.AppSettings.Get(pKey);
        }
    }
}
