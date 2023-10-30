using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuntimeDispensador.Core;
using System.Threading.Tasks;
using Interop.Main.Cross.Domain.Orchestrator;
using RuntimeDispensador.Structural;
using Newtonsoft.Json;
using System.IO;
using RuntimeDispensador.Service;

namespace DispensadorTest
{
    [TestClass]
    public partial class ProviderComunicationTest
    {
        [TestMethod]
        [STAThread]
        public void SendMessageCOMTest()
        {

            GlobalConfigATM globalConfigATM = new GlobalConfigATM
            {
                configDispenserCOM = new GlobalConfigATM.ConfigDispenserCOM
                {
                    Baudios = 9600,
                    Portname = "COM3",
                    Data = 7,
                    HasDispenser = true,
                    Paridad = 2,
                    ReadTimeout = 180000,
                    SerialNumber = "",
                    WriteTimeout = 180000
                }
            };
            //var resul = ExecutorCommand.ejecutar(Comando.LeeIdBandeja);

            ProviderComunication.InitProviderCominication(globalConfigATM);
            ProviderComunication provider = ProviderComunication.GetCurrentInstace();

            try
            {
                var s = provider.SenMessage("0").Result;

                var s1 = provider.SenMessage("R");
                var s7 = provider.SenMessage("5");
            }
            catch (Exception ex)
            {

                throw ex;
            }


            /*var s2 = provider.SenMessage("8");
            provider = ProviderComunication.GetCurrentInstace();
            var s3 = provider.SenMessage("2010012001");
            var s4 = provider.SenMessage("7");*/
            //var s5 = provider.SenMessage("GA");

        }

        [TestMethod]
        [STAThread]
        public void OffSetMountToStringTest()
        {
            GlobalConfigATM _globalConfigATM = JsonConvert.DeserializeObject<GlobalConfigATM>(File.ReadAllText(@"C:\HLA\ATM.json"));
            Dispenser.InitDispenser(_globalConfigATM);
            var resul = Dispenser.CurrentDispenser.OffSetMountToString(200);
            //var resul2 = Dispenser.CurrentDispenser.OffSetMountToStringBalance  (200);
            
        }

        [TestMethod]
        [STAThread]
        public void VerificaBrazoMecanicoTest()
        {
            GlobalConfigATM _globalConfigATM = JsonConvert.DeserializeObject<GlobalConfigATM>(File.ReadAllText(@"C:\HLA\ATM.json"));

            Dispensador dispensador = new Dispensador();
            
            var resulInit = dispensador.Inicializar(_globalConfigATM, true);
            var resul = dispensador.Dispensar(100);
        }
    }
}
