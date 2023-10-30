using Interop.Main.Cross.Domain.Dispenser;
using Interop.Main.Cross.Domain.Dispenser.Enumerators;
using System;
using System.Linq;


namespace RuntimeDispensador.Core
{


    public class ExecutorCommand
    {

        private static ProviderComunication provider = ProviderComunication.GetCurrentInstace();
        public static StatusDispenser ejecutar(Comando pCmd, string parameters = "")
        {
            string respuesta = string.Empty;
            CommandDispensador.FactoryCommand();
            AdapterResponse.FactoryResponse();        
            respuesta = provider.SenMessage(CommandDispensador.Comandos.Find(x => x.ComandSend == pCmd).ComandoExecute + parameters).Result;
            return AdapterResponse.ParseResponse(respuesta, CommandDispensador.Comandos.Find(x => x.ComandSend == pCmd).Parser);
        }
    }
}