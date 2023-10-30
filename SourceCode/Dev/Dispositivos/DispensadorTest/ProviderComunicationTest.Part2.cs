using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Foundation.Stone.CrossCuting.Util;
using Foundation.Stone.CrossCuting.File;
using Foundation.Stone.Application.Wrapper;
using System.Collections.ObjectModel;
using System.IO;
using RuntimeDispensador.Core;
using Interop.Main.Cross.Domain.Dispenser;
using Interop.Main.Cross.Domain.Dispenser.Enumerators;
using RuntimeDispensador.Structural;
using RuntimeDispensador.Service;
using Interop.Main.Cross.Domain.Orchestrator;

//using RuntimeDispensador.Arrange;

namespace DispensadorTest
{
    public partial class ProviderComunicationTest
    {


        public ProviderComunicationTest()
        {
            //GlobalConfigATM globalConfigATM = new GlobalConfigATM(); 
            //Dispenser.InitDispenser(globalConfigATM);
        }


        [TestMethod]
        [STAThread]

        public void SendMessageCOMTest2()
        {
            /*ProviderComunication provider = ProviderComunication.GetCurrentInstace();

            var s = provider.SenMessage("0");*/

            //var s1 = provider.SenMessage("R");

            /*var s2 = provider.SenMessage("8"
             * );

            var s7 = provider.SenMessage("5");

            provider = ProviderComunication.GetCurrentInstace();
            var s3 = provider.SenMessage("2010012001");*/

            //var s4 = provider.SenMessage("7");


            //var resul = ExecutorCommand.ejecutar(Comando.AbrirBandejas);

            //var resul = ExecutorCommand.ejecutar(Comando.CerrarBandejas );

            //resul = ExecutorCommand.ejecutar(Comando.Reset);
            //resul.Cassettes = null;
            //resul = null;
            /*resul = ExecutorCommand.ejecutar(Comando.LeeIdBandeja);

            

            //var s5 = provider.SenMessage("GA");

            Dispenser disp = new Dispenser();
            Dispenser.Cassette cs = new Dispenser.Cassette() { CurrencyCourt = 100, Id = 1, MinQuantity = 15, Sequence = 2, TotalQuantity = 18  };
            disp.Cassettes.Add(cs);
            //cs = new Dispenser.Cassette() { CurrencyCourt = 50, Id = 1, MinQuantity = 15, Sequence = 3, TotalQuantity = 30 };
            //disp.Cassettes.Add(cs);
            cs = new Dispenser.Cassette() { CurrencyCourt = 20, Id = 1, MinQuantity = 15, Sequence = 1, TotalQuantity = 500 };
            disp.Cassettes.Add(cs);
            //cs = new Dispenser.Cassette() { CurrencyCourt = 10, Id = 1, MinQuantity = 15, Sequence = 1, TotalQuantity = 30 };
            //disp.Cassettes.Add(cs);

            var resulDis = disp.OffSetMountToString(120);

            if (resulDis.Object!=null  )
            { 
                resul = ExecutorCommand.ejecutar(Comando.Dispensar,resulDis.Object );

                resul = ExecutorCommand.ejecutar(Comando.VerifEntrega);
            }*/

            var resul = ExecutorCommand.ejecutar(Comando.CerrarBandejas);
        }

        [TestMethod]
        public void SendBatchInitialize()
        {


            //Creando Batch de comandos para dispensador 
            GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(Comando.AbrirBandejas);
            batch.AddComand((StatusDispenser x) =>
            {
                GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                request.command = (x.state == ResponseDispensador.Exito || x.Code.Equals("1")) ? Comando.ResetLento : Comando.Fin;
                return request;
            });

            batch.AddComand((StatusDispenser x) =>
            {
                GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                request.command = (x.state == ResponseDispensador.Exito || x.Code.Equals("1")) ? Comando.Reset : Comando.Fin;
                return request;
            });

            batch.AddComand((StatusDispenser x) =>
            {
                GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                request.command = (x.state == ResponseDispensador.Exito || x.Code.Equals("1")) ? Comando.LeeIdBandeja : Comando.Fin;
                return request;
            });




            GetwayDispensador.GetInstance().AddBatches(batch);
            var trace = GetwayDispensador.GetInstance().ExecuteBatch();


        }

        [TestMethod]

        public void SendBatchClose()
        {

            //Creando Batch de comandos para dispensador 
            GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(Comando.CerrarBandejas);
            batch.AddComand((StatusDispenser x) =>
            {
                GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                request.command = (x.state == ResponseDispensador.Exito || x.Code.Equals("1")) ? Comando.ResetLento : Comando.Fin;
                return request;
            });

            GetwayDispensador.GetInstance().AddBatches(batch);
            var trace = GetwayDispensador.GetInstance().ExecuteBatch();
            //respuesta
            Response resul = new Response();
            if (trace.All(x => x.Result.state == ResponseDispensador.Exito))
            {
                resul.State = ResponseType.Success;
                resul.Message = "Operacion con exito";
            }
            else
            {
                resul.State = ResponseType.Warning;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = "Operacion fallida: " + mensaje_traza;
            }
        }

        public void SendBatchOpen()
        {


            //Creando Batch de comandos para dispensador 
            GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(Comando.AbrirBandejas);
            batch.AddComand((StatusDispenser x) =>
            {
                GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                request.command = (x.state == ResponseDispensador.Exito || x.Code.Equals("1")) ? Comando.ResetLento : Comando.Fin;
                return request;
            });

            GetwayDispensador.GetInstance().AddBatches(batch);
            var trace = GetwayDispensador.GetInstance().ExecuteBatch();
        }

        [TestMethod]
        public void SendBatchCheckTray()
        {
            GetwayDispensador.IdATM = 1;
            GetwayDispensador.Port = "COM3";
            //Initializer.Init();

            Dispenser disp = Dispenser.CurrentDispenser;

            //Creando Batch de comandos para dispensador 
            GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(Comando.AbrirBandejas);

            batch.AddComand((StatusDispenser x) =>
            {
                GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                request.command = (x.state == ResponseDispensador.Exito || x.Code.Equals("1")) ? Comando.LeeIdBandeja : Comando.Fin;
                return request;
            });

            GetwayDispensador.GetInstance().AddBatches(batch);
            var trace = GetwayDispensador.GetInstance().ExecuteBatch();
            if (!trace.Any(x => x.IdCommand == Comando.LeeIdBandeja && (x.Result.state == ResponseDispensador.Exito || x.Result.Code.Equals("1"))))
            {
                throw new Exception("No se pudo leer las bandejas del dispensador");
            }
            var trazaLeerBandeja = trace.First(x => x.IdCommand == Comando.LeeIdBandeja);

            disp.Cassettes.ForEach(
                x =>
                {
                    x.Id = Convert.ToInt32(trazaLeerBandeja.Result.Cassettes[x.Sequence].PartsResponse[PartRespuesta.IDCassete]);
                }
                );


            Assert.AreEqual(12471, disp.Cassettes[0].Id);
            Assert.AreEqual(12352, disp.Cassettes[1].Id);
        }

        [TestMethod]
        public void SendBatchEffectiveDelivery(int pRodo)
        {
            Dispenser disp = Dispenser.CurrentDispenser;

            GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(Comando.LeeIdBandeja);
            batch.AddComand((StatusDispenser x) =>
            {
                GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();

                var responeDisp = disp.OffSetMountToString(pRodo);
                if (responeDisp.State != ResponseType.Success)
                {
                    request.command = Comando.Fin;
                    request.AdditionalInformation = responeDisp.Message;
                    return request;
                }

                if (x.state == ResponseDispensador.Exito)
                {
                    request.command = Comando.Dispensar;
                    request.parameter = responeDisp.Object.Comand;
                }
                else if (x.Code.Equals("1") && x.state == ResponseDispensador.Advertencia)
                {
                    request.command = Comando.Dispensar;
                    request.parameter = responeDisp.Object.Comand;
                }
                else
                {
                    request.command = Comando.Fin;
                }
                return request;
            });
            batch.AddComand((StatusDispenser x) =>
            {
                GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                request.command = Comando.VerifEntrega;
                return request;
            });

            GetwayDispensador.GetInstance().AddBatches(batch);
            var trace = GetwayDispensador.GetInstance().ExecuteBatch();
        }

        [TestMethod]
        public void SendBatchTest()
        {
            GetwayDispensador.IdATM = 1;
            GetwayDispensador.Port = "COM3";
            //Initializer.Init();
            Dispenser disp = Dispenser.CurrentDispenser;

            GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(Comando.AbrirBandejas);
            batch.AddComand((StatusDispenser x) =>
            {
                GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                request.command = x.state == ResponseDispensador.Exito ? Comando.Reset : Comando.Fin;
                return request;
            });
            batch.AddComand((StatusDispenser x) =>
            {
                GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                request.command = (x.state == ResponseDispensador.Exito || x.Code.Equals("1")) ? Comando.LeeIdBandeja : Comando.Fin;
                return request;
            });

            batch.AddComand((StatusDispenser x) =>
            {
                GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();

                var responeDisp = disp.OffSetMountToString(120);
                if (responeDisp.State != ResponseType.Success)
                {
                    request.command = Comando.Fin;
                    request.AdditionalInformation = responeDisp.Message;
                    return request;
                }

                if (x.state == ResponseDispensador.Exito)
                {
                    request.command = Comando.Dispensar;
                    request.parameter = responeDisp.Object.Comand;
                }
                else if (x.Code.Equals("1") && x.state == ResponseDispensador.Advertencia)
                {
                    request.command = Comando.Dispensar;
                    request.parameter = responeDisp.Object.Comand;
                }
                else
                {
                    request.command = Comando.Fin;
                }
                return request;
            });
            batch.AddComand((StatusDispenser x) =>
            {
                GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                request.command = Comando.VerifEntrega;
                return request;
            });


            GetwayDispensador.GetInstance().AddBatches(batch);
            var trace = GetwayDispensador.GetInstance().ExecuteBatch();
        }

        [TestMethod]
        public void GenerateXMLDispenser()
        {
            /*Dispenser disp = new Dispenser();
            Dispenser.Cassette cs = new Dispenser.Cassette() { CurrencyCourt = 100, Id = 1, MinQuantity = 15, Sequence = 2, TotalQuantity = 18 };
            disp.Cassettes.Add(cs);
            //cs = new Dispenser.Cassette() { CurrencyCourt = 50, Id = 1, MinQuantity = 15, Sequence = 3, TotalQuantity = 30 };
            //disp.Cassettes.Add(cs);
            cs = new Dispenser.Cassette() { CurrencyCourt = 20, Id = 1, MinQuantity = 15, Sequence = 1, TotalQuantity = 500 };
            disp.Cassettes.Add(cs);
            FileUtil fu = new FileUtil("Dispenser.xml");
            fu.writeFile(disp.SerializarToXml());*/
        }

        [TestMethod]
        public void ServiceTestDispenser()
        {
            string pathConfigATM = @"c:\hla\ATM.json";
            GlobalConfigATM globalConfigATM = Newtonsoft.Json.JsonConvert.DeserializeObject<GlobalConfigATM>(File.ReadAllText(pathConfigATM));
            Dispensador dispensador = new Dispensador();
            var resulDispenser = dispensador.Inicializar(globalConfigATM, false);
            var resulDispenser1 = dispensador.DispensarATM(520);
        }
    }
}