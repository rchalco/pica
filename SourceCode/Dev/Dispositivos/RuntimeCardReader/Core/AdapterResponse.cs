using Interop.Main.Cross.Domain.CardReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RuntimeCardReader.Core
{
  
    public static class ReadAdapterResponse
    {
        public static string ConfigCR = "C:\\HLA\\ConfigDispenser\\ConfigResponseCardReader.xml";
        

        //public static string xmlConfig { get; set; }

        //public static string AssemblyDirectory => Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));

        public static StatusCardReader GetConfigResponse(string pType, string pValue)
        {
            StatusCardReader resul = new StatusCardReader();
            
            try
            {
                XElement docResponse = XElement.Load(ConfigCR);

                List<StatusCardReader> pricesByPartNos = (from item in docResponse.Descendants("respose")
                                                          where item.Attribute("type").Value.Equals(pType ) && Convert.ToBoolean(item.Attribute("value").Value?.Equals(pValue))
                                                         select new StatusCardReader
                                                         {
                                                             Type = Convert.ToString(item.Attribute("type").Value),
                                                             Code = Convert.ToString(item.Attribute("code").Value),
                                                             CodeE1 = Convert.ToInt32 (item.Attribute("codeE1").Value),
                                                             CodeE0= Convert.ToInt32(item.Attribute("codeE0").Value),
                                                             ConcreteResponse = Convert.ToString(item.Attribute("concreteResponse ").Value)
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
