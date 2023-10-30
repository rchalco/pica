using System;
using Foundation.Stone.CrossCuting.File;
using Foundation.Stone.CrossCuting.Util;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace NMDManagement
{
    public static class ReadConfig
    {
        public static string ConfigBD = Path.Combine(ReadConfig.AssemblyDirectory, "Config\\ConfigBD.xml");
        private static ConfigBD _configBD;

        public static string xmlConfig { get; set; }

        public static string AssemblyDirectory => Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));

        public static ConfigBD GetConfigBD()
        {
            try
            {
                if (!string.IsNullOrEmpty(ReadConfig.xmlConfig))
                    ReadConfig._configBD = ReadConfig.xmlConfig.DeserializeFromXml<ConfigBD>();
                else
                    ReadConfig._configBD = new FileUtil()
                    {
                        NameFile = ReadConfig.ConfigBD
                    }.GetData().DeserializeFromXml<ConfigBD>();
                return ReadConfig._configBD;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
    public static class ReadAdapterResponse
    {
        public static string ConfigBD = "C:\\HLA\\ConfigDispenser\\ConfigResponse.xml";
        private static ResponseComplex adapterResponse;

        //public static string xmlConfig { get; set; }

        //public static string AssemblyDirectory => Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));

        public static ResponseComplex GetConfigResponse(string model, string value)
        {
            ResponseComplex resul = new ResponseComplex();
            try
            {
                XElement docResponse = XElement.Load(ConfigBD);

                List<ResponseComplex> pricesByPartNos = (from item in docResponse.Descendants("respose")
                                                         where item.Attribute("model").Value.Equals(model) && Convert.ToBoolean(item.Attribute("value").Value?.Equals(value))
                                                         select new ResponseComplex
                                                         {
                                                             concreteResponse = Convert.ToString(item.Attribute("concreteResponse").Value),
                                                             model = Convert.ToString(item.Attribute("model").Value),
                                                             type = Convert.ToString(item.Attribute("type").Value),
                                                             typeEnum = Convert.ToString(item.Attribute("typeEnum").Value),
                                                             value = Convert.ToString(item.Attribute("value").Value)
                                                         }).ToList();
                if (pricesByPartNos?.Count > 0)
                {
                    resul = pricesByPartNos.First();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return resul;
        }



        internal static object GetConfigResponse(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
