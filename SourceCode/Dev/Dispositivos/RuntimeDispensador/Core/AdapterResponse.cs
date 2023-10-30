using System;
using System.Linq;
using Foundation.Stone.CrossCuting.File;
using System.Xml.Linq;
using System.Collections.Generic;
using RuntimeDispensador.Structural;
using Interop.Main.Cross.Domain.Dispenser;
using Interop.Main.Cross.Domain.Dispenser.Enumerators;

namespace RuntimeDispensador.Core
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
                file.NameFile = SetConfiguration.ConfigResponse;
                string xmlString = file.GetData();
                XDocument xdoc = XDocument.Parse(xmlString);

                //Run query
                var respuestas = from comand in xdoc.Descendants("respose")
                                 where comand.Attribute("model").Value.Equals(Dispenser.CurrentDispenser.Tipo)
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
        private static ProviderComunication Provider;
        public static void FactoryResponse()
        {
            if (PartsEnum == null)
            {
                PartsEnum = new Dictionary<string, PartRespuesta>();
                PartsEnum.Add("S", PartRespuesta.Status);
                PartsEnum.Add("H", PartRespuesta.Header);
                PartsEnum.Add("F", PartRespuesta.StatusHeader);
                PartsEnum.Add("N", PartRespuesta.NumberBill);
                PartsEnum.Add("M", PartRespuesta.UnitIdentifier);
                PartsEnum.Add("G", PartRespuesta.IDCassete);
                PartsEnum.Add("L", PartRespuesta.LRC);
                PartsEnum.Add("A", PartRespuesta.CassetteStatus);
                PartsEnum.Add("r", PartRespuesta.StatusRejectVault);
                PartsEnum.Add("f", PartRespuesta.StatusRejectFeeder);
                PartsEnum.Add("t", PartRespuesta.StatusMotorDriver);
                PartsEnum.Add("q", PartRespuesta.StatusDoubleDetect);
                PartsEnum.Add("d", PartRespuesta.StatusNoteDiverter);
                PartsEnum.Add("s", PartRespuesta.StatusNoteTransport);
                PartsEnum.Add("p", PartRespuesta.StatusStackPresenter);
                PartsEnum.Add("o", PartRespuesta.StatusThroat);
                PartsEnum.Add("v", PartRespuesta.StatusDataHandler);
            }

            if (Status.Count == 0)
            {

                FileUtil file = new FileUtil();
                file.NameFile = SetConfiguration.ConfigResponse;
                string xmlString = file.GetData();
                XDocument xdoc = XDocument.Parse(xmlString);

                //Run query
                var respuestas = from comand in xdoc.Descendants("respose")
                                 where comand.Attribute("model").Value.Equals(Dispenser.CurrentDispenser.Tipo)
                                 select new
                                 {
                                     model = comand.Attribute("model").Value,
                                     description = comand.Attribute("concreteResponse").Value,
                                     type = comand.Attribute("type").Value,
                                     code = comand.Attribute("value").Value,
                                     typeEnum = comand.Attribute("typeEnum").Value
                                 };
                foreach (var item in respuestas)
                {
                    ResponseDispensador vtypeEnum = (ResponseDispensador)Enum.Parse(typeof(ResponseDispensador), item.typeEnum, true);
                    StatusDispenser resp = new StatusDispenser();
                    resp.Code = item.code;
                    resp.Description = item.description;
                    resp.Type = item.type;
                    resp.state = vtypeEnum;
                    Status.Add(resp);
                }

            }

        }

        public static StatusDispenser ParseResponse(string pResp, string patron)
        {
            StatusDispenser response = new StatusDispenser();
            ///En caso de que no se desee el parser de la resuesta el status dispenser sera vacio
            if (patron.Equals("{NP}"))
            {
                response.ResponseOriginal = pResp;
                return response;   
            }

            if (!Status.Any(x => x.Code.Equals(pResp.Substring(0, 1))))
            {
                throw new Exception("Respuesta no registrada en la configuracion!");
            }
            
            response = Status.Where(x => x.Code.Equals(pResp.Substring(0, 1))).First();
            response.PartsResponse = new Dictionary<PartRespuesta, string>();
            List<ParserSequence> lSequence = new List<ParserSequence>();
            int count = 0;

            bool ignoreSequence = false;
            string casseteSequence = string.Empty;
            int totalCaracteres = 0;
            int indiceInicial = -1;
            int totalCssetes = 0;

            patron.ToCharArray().ToList().ForEach(p =>
            {

                if (ignoreSequence)
                {
                    casseteSequence = casseteSequence + (p.ToString());
                    ignoreSequence = p != ']';
                    if ((int)p >= 49 && (int)p <= 57)
                    {
                        totalCaracteres += Convert.ToInt32(p.ToString());
                    }
                }
                else
                {
                    ignoreSequence = p == '[';
                    if (ignoreSequence)
                    {
                        indiceInicial = lSequence.Sum(x => x.NumberCharacter);
                    }

                    if ((int)p >= 49 && (int)p <= 57 && !ignoreSequence)
                    {
                        ParserSequence pS = new ParserSequence();
                        pS.NumberCharacter = Convert.ToInt32(p.ToString());
                        pS.Character = patron.ToCharArray()[count + 1];
                        lSequence.Add(pS);
                    }

                }

                count++;
            });
            int counterResponse = 0;

            lSequence.ForEach(x =>
            {
                if (x.Character != '[')
                {
                    response.PartsResponse.Add(PartsEnum[x.Character.ToString()], pResp.Substring(counterResponse, x.NumberCharacter));
                }
                counterResponse += x.NumberCharacter;
            });
            response.ResponseOriginal = pResp;
            if (pResp.Length > 4)
            {
                totalCssetes = Dispenser.CurrentDispenser.Cassettes.Count+1;
            }
            else
            { totalCssetes = 0; }

            response.Cassettes = new List<CassetteStatus>();
            response.Cassettes.Clear();

            //se debe modificar el for por while, para recorrer las secuencias debido a que no todos estan de acuerdo a la cantidad de bandejas
            for (int i = 0; i < totalCssetes && indiceInicial >= 0; i++)
            {
                CassetteStatus cassete = new CassetteStatus()
                {
                    Index = i,
                    Id = string.Empty,
                    PartsResponse = new SortedDictionary<PartRespuesta, ResponseComplex>(),
                    StrResponse = string.Empty
                };
                int ini = indiceInicial + totalCaracteres * i;
                int fin = totalCaracteres;
                if (!(pResp.Length > ini && pResp.Length >= fin + ini))
                {
                    continue;
                }

                cassete.StrResponse = pResp.Substring(ini, fin);
                string partResponseCassette = pResp.Substring(ini, fin);

                List<ParserSequence> lSequenceCassette = new List<ParserSequence>();
                count = 0;
                counterResponse = 0;
                casseteSequence.ToCharArray().ToList().ForEach(p =>
                {
                    if ((int)p >= 49 && (int)p <= 57)
                    {
                        ParserSequence pS = new ParserSequence();
                        pS.NumberCharacter = Convert.ToInt32(p.ToString());
                        pS.Character = casseteSequence.ToCharArray()[count + 1];
                        lSequenceCassette.Add(pS);
                    }
                    count++;
                });

                lSequenceCassette.ForEach(x =>
                {
                    ResponseComplex responseComplex = new ResponseComplex();
                    responseComplex.code = partResponseCassette.Substring(counterResponse, x.NumberCharacter);

                    if (LexiconResponse.Any(x1 => x1 != null && x1.code != null && x1.code.Equals(responseComplex.code)))
                    {
                        responseComplex = LexiconResponse.First(x1 => x1.code.Equals(responseComplex.code));
                    }
                    cassete.PartsResponse.Add(PartsEnum[x.Character.ToString()], responseComplex);
                    counterResponse += x.NumberCharacter;
                });

                response.Cassettes.Add(cassete);
            }
            return response;

        }
    }



}
