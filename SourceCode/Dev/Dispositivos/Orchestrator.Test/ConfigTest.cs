using Interop.Main.Cross.Domain.Orchestrator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrchestratorDevice.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Orchestrator.Test
{
    [TestClass]
    public class ConfigTest
    {
        public ConfigTest()
        {
        }

        [TestMethod]
        public void TestGenerateFileConfig()
        {
            GlobalConfigATM globalConfigATM = new GlobalConfigATM
            {
                configCardReader = new GlobalConfigATM.ConfigCardReader
                {
                    IdTypeCardReader = 1,
                    NameCarderReader = "CREATOR",
                    HasCarReader = true,
                    SerialNumber = ""
                },
                configFingerPrintReader = new GlobalConfigATM.ConfigFingerPrintReader
                {
                    IdTypeFingerPrint = 1,
                    NameFingerPrint = "DIGITAL PERSONA",
                    HasFingerPrint = true,
                    SerialNumber = ""
                },
                configDispenserCOM = new GlobalConfigATM.ConfigDispenserCOM
                {
                    Baudios = 9600,
                    Portname = "COM7",
                    Data = 7,
                    Paridad = 2,
                    ReadTimeout = 180000,
                    WriteTimeout = 180000,
                    HasDispenser = true,
                    SerialNumber = ""
                },
                configDispenserStatus = new GlobalConfigATM.ConfigDispenserStatus
                {
                    Cassettes = new List<Interop.Main.Cross.Domain.Dispenser.Cassette>(),
                    HasDispenser = true,
                    MaxQuantityBill = 30,
                    NumBandejas = 4,
                    Tipo = "NMD50"
                }
            };
            Interop.Main.Cross.Domain.Dispenser.Cassette cassette1 = new Interop.Main.Cross.Domain.Dispenser.Cassette
            {
                Id = 1,
                Sequence = 1,
                CurrencyCourt = 10,
                TotalQuantity = 18,
                MinQuantity = 15,
                Status = "0"
            };
            Interop.Main.Cross.Domain.Dispenser.Cassette cassette2 = new Interop.Main.Cross.Domain.Dispenser.Cassette
            {
                Id = 2,
                Sequence = 2,
                CurrencyCourt = 50,
                TotalQuantity = 500,
                MinQuantity = 15,
                Status = "0"
            };
            Interop.Main.Cross.Domain.Dispenser.Cassette cassette3 = new Interop.Main.Cross.Domain.Dispenser.Cassette
            {
                Id = 3,
                Sequence = 3,
                CurrencyCourt = 50,
                TotalQuantity = 20,
                MinQuantity = 15,
                Status = "0"
            };
            Interop.Main.Cross.Domain.Dispenser.Cassette cassette4 = new Interop.Main.Cross.Domain.Dispenser.Cassette
            {
                Id = 4,
                Sequence = 4,
                CurrencyCourt = 100,
                TotalQuantity = 50,
                MinQuantity = 15,
                Status = "0"
            };
            globalConfigATM.configDispenserStatus.Cassettes.Add(cassette1);
            globalConfigATM.configDispenserStatus.Cassettes.Add(cassette2);
            globalConfigATM.configDispenserStatus.Cassettes.Add(cassette3);
            globalConfigATM.configDispenserStatus.Cassettes.Add(cassette4);
            string resulConfigJSON = Newtonsoft.Json.JsonConvert.SerializeObject(globalConfigATM);
        }


        [TestMethod]
        public void TestDeserealizeFileConfig()
        {
            string jsonContent = File.ReadAllText(@"c:\hla\ATM.json");
            GlobalConfigATM globalConfigATM = Newtonsoft.Json.JsonConvert.DeserializeObject<GlobalConfigATM>(jsonContent);
        }

        [TestMethod]
        public void DeviceManagerTest()
        {
            string jsonContent = File.ReadAllText(@"c:\hla\ATM.json");
            GlobalConfigATM globalConfigATM = Newtonsoft.Json.JsonConvert.DeserializeObject<GlobalConfigATM>(jsonContent);

        }

        [TestMethod]
        public void InitATMTest()
        {
            DeviceManager.InitATM();
        }

        [TestMethod]
        public void VerificacionElectron()
        {
            var procesos = System.Diagnostics.Process.GetProcesses().ToList().Where(x => x.ProcessName.Contains("electron")).ToList();
            if (!System.Diagnostics.Process.GetProcesses().ToList().Any(x => x.ProcessName.Contains("electron")))
            {
                Console.WriteLine("No hay proceso");
            }
        }

        [TestMethod]
        public void OpenUI()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(@"C:\electron-atm\electron.exe", @"c:\atm\index.html");
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            var processs = Process.Start(startInfo);
            System.Threading.Thread.Sleep(5000);
            SendKeys.SendWait("{F11}");

        }

        
    }


}
