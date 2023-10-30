using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Foundation.Stone.CrossCuting.File;
namespace NMDManagement
{
    public class AdapterResponse
    {
        class ParserSequence
        {
            public int NumberCharacter { get; set; }
            //public int Hopper { get; set; }

            //public char HoperStatus { get; set; }
            public char Character { get; set; }
        }

        private static Dictionary<string, PartRespuesta> PartsEnum;
        static List<ResponseComplex> _LexiconResponse = null;
        public static List<ResponseComplex> LexiconResponse
        {
            get
            {
                if (_LexiconResponse != null)
                {
                    return _LexiconResponse;
                }

                FileUtil file = new FileUtil();
                file.NameFile = "C:\\HLA\\ConfigDispenser\\ConfigResponse.xml";
                string xmlString = file.GetData();
                XDocument xdoc = XDocument.Parse(xmlString);

                //Run query
                var respuestas = from comand in xdoc.Descendants("respose")
                                 //where comand.Attribute("model").Value.Equals("NMD100")
                                 select new ResponseComplex
                                 {
                                     model = comand.Attribute("model").Value,
                                     description = comand.Attribute("concreteResponse").Value,
                                     type = comand.Attribute("type").Value,
                                     code = comand.Attribute("value").Value,
                                     typeEnum = comand.Attribute("typeEnum").Value
                                 };
                _LexiconResponse = respuestas.ToList();

                return _LexiconResponse;
            }
        }

        public static System.Collections.Generic.List<StatusDispenser> Status = new System.Collections.Generic.List<StatusDispenser>();
        
    }
}
