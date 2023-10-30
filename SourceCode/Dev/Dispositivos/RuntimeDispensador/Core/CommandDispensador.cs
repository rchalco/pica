using Foundation.Stone.CrossCuting.File;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System;
using RuntimeDispensador.Structural;
using Interop.Main.Cross.Domain.Dispenser.Enumerators;

namespace RuntimeDispensador.Core
{
    public class CommandDispensador
    {
        public Comando ComandSend { get; set; }
        public string ComandoExecute { get; set; }
        public string Parser { get; set; }

        public static List<CommandDispensador> Comandos { get; set; }

        public static void FactoryCommand()
        {
            if (Comandos == null)
            {
                Comandos = new List<CommandDispensador>();
                FileUtil file = new FileUtil();
                file.NameFile = SetConfiguration.ConfigCommand;
                string xmlString = file.GetData();
                XDocument xdoc = XDocument.Parse(xmlString);

                //Run query
                var comandos = from comand in xdoc.Descendants("comand")
                               where comand.Attribute("model").Value.Equals(Dispenser.CurrentDispenser.Tipo)
                               select new
                               {
                                   model = comand.Attribute("model").Value,
                                   comando = comand.Attribute("concreteComand").Value,
                                   valor = comand.Attribute("value").Value,
                                   parser = comand.Attribute("parser").Value
                               };
                foreach (var item in comandos)
                {
                    Comando cmd = (Comando)Enum.Parse(typeof(Comando), item.comando, true);
                    CommandDispensador comando = new CommandDispensador() { ComandoExecute = item.valor, ComandSend = cmd, Parser = item.parser };
                    Comandos.Add(comando);
                }

            }
        }



    }
}