using Foundation.Stone.Application.Wrapper;
using Foundation.Stone.Business.Core;
using Foundation.Stone.CrossCuting.StoneException;
using Hangar.ServiceImplement.Config;
using Hangar.ServiceImplement.Factory;
using Interop.Main.Cross.Domain.Orchestrator;
using Interop.Main.Service.Interface;
using Newtonsoft.Json;
using OrchestratorDevice.Contracts;
using OrchestratorDevice.Contracts.Common;
using OrchestratorDevice.Contracts.SavingAccount;
using OrchestratorDevice.Global;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrchestratorDevice.Managers
{
    public class DeviceManager : BaseManager
    {
        public static ExternalConfigurartion externalConfiguration = new ExternalConfigurartion();
        public static string loggerName = externalConfiguration.GetAppSettingsFromCurrentAssembly().Settings["logConfig"].Value;
        public static GlobalConfigATM globalConfigATM;
        private static System.Timers.Timer timer = new System.Timers.Timer(10000);
        static ResponseObject<ResulAuthencation> resAuth;
        public static DTOCurrencyExchangeRateATM responseGetCurrencyExchanceRateByDate { get; set; }
        public static string token = "";


        protected static void ProcessError(Response response, Exception exception, System.Diagnostics.EventLogEntryType eventLogEntryType = System.Diagnostics.EventLogEntryType.Error)
        {
            response.State = ResponseType.Error;
            response.Message = exception.GetAllErrorMessage();
            Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO(loggerName, response.Message + " . StackTrace: " + exception.StackTrace, eventLogEntryType);
        }

        private static List<string> IPs
        {
            get
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                List<string> LocalIps = new List<string>();
                string ips = string.Empty;
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        LocalIps.Add(ip.ToString());
                        ips += ip.ToString() + " | ";
                    }
                }
                if (string.IsNullOrEmpty(ips))
                {
                    Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO("HangarService", $"no se configuro ninguna IP de tipo IPv4 en el equipo!");
                    throw new Exception("no se configuro ninguna IP de tipo IPv4 en el equipo!");

                }
                return LocalIps;
            }
        }


        public static void InitATMInterceptor()
        {
            InitATM(false);
        }

        public static void InitATM(bool force = false)
        {
            try
            {
                IntegratorManager integratorManager = new IntegratorManager();
                bool isAtuh = false;

                IPs.ForEach(xx =>
                {
                    if (!isAtuh)
                    {
                        Contracts.SavingAccount.Credential _credential = new Contracts.SavingAccount.Credential
                        {
                            AditionalItems = new List<Contracts.SavingAccount.AditionalItem>(),
                            Channel = 3,
                            Password = string.Empty,
                            User = xx
                        };
                        _credential.AditionalItems.Add(new AditionalItem
                        {
                            Key = "IdATM",
                            Value = "0"
                        });

                        _credential.AditionalItems.Add(new AditionalItem
                        {
                            Key = "TypeAuthentication",
                            Value = "ATMIP"
                        });

                        resAuth = integratorManager.Authentication(new Contracts.SavingAccount.RequestAuthentication
                        {
                            credential = _credential
                        });
                        isAtuh = resAuth.State == ResponseType.Success;
                    }
                });

                if (!isAtuh)
                {
                    Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO(loggerName, $"No se puede encontrar un registro de ATM con el IP configurado en el PC", System.Diagnostics.EventLogEntryType.Error);
                    throw new Exception("Imposible inicializar el ATM, no se puede autenticar con el IP actual!!!!");
                }

                string jsonConfigurationATM = resAuth.Object.AditionalItems.Where(z => z.Key == "ATMInfo").First().Value;
                token = resAuth.Object.token;
                ATMDTO aTMDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<ATMDTO>(jsonConfigurationATM);
                globalConfigATM = Newtonsoft.Json.JsonConvert.DeserializeObject<GlobalConfigATM>(aTMDTO.State);
                globalConfigATM.Id = aTMDTO.IdATM;
                globalConfigATM.Name = aTMDTO.Name;
                globalConfigATM.IdOffice = aTMDTO.IdOffice;

                if (globalConfigATM == null)
                {
                    Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO(loggerName, $"Imposible inicializar el ATM, no se cuenta con informacion de la configuracion global del ATM en el registro del la Central ATM. Imposible deserealizar el objeto!!!!", System.Diagnostics.EventLogEntryType.Error);
                    throw new Exception("Imposible inicializar el ATM, no se cuenta con informacion de la configuracion global del ATM en el registro del la Central ATM. Imposible deserealizar el objeto!!!!");
                }

                ///**************************TODO obtenemos el tipo de cambio oficial***********************
                CommonManager commonManager = new CommonManager();
                var resulTipoCambio = commonManager.GetCurrencyExchanceRateByDate(new RequestGetCurrencyExchanceRateByDate
                {
                    requestCurrencyExchanceRateByDate = new RequestGetCurrencyExchanceRateByDateInner { IdMoney = 2 },
                    Token = resAuth.Object.token
                   
                });
                if (resulTipoCambio.State == ResponseType.Success)
                {
                    responseGetCurrencyExchanceRateByDate = resulTipoCambio.Object;
                }
                else
                {
                    Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO(loggerName, $"Imposible obtener el tipo de cambio: {resulTipoCambio.Message}", System.Diagnostics.EventLogEntryType.Error);
                }

                ///**************************TODO inicimos los dispositivos******************************

                ///Inicializamos el Dispensador
                if (globalConfigATM.configDispenserCOM.HasDispenser && globalConfigATM.configDispenserStatus.HasDispenser)
                {
                    Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO(loggerName, $"Ejecutando la incializacion del dispensador", System.Diagnostics.EventLogEntryType.Warning);
                    var DispenserInterop = MicroService.CreateInstance<IDispenserInterop>();
                    var resulDispenser = DispenserInterop.Inicializar(globalConfigATM, force);
                    Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO(loggerName, $"Resulado de la inicializacion: {resulDispenser.Message}", System.Diagnostics.EventLogEntryType.Warning);

                    if (resulDispenser.State == ResponseType.Error)
                    {
                        Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO(loggerName, $"No se puede inicalizar el DISPENSADOR del ATM:{resulDispenser.Message}", System.Diagnostics.EventLogEntryType.Error);
                        ProcessError(new Response(), new Exception($"No se puede inicalizar el DISPENSADOR del ATM:{resulDispenser.Message}"));
                    }
                }

                ///Inicializamos el lector tarjeta
                if (globalConfigATM.configCardReader.HasCarReader)
                {
                    var CardReaderInterop = MicroService.CreateInstance<ICardReaderInterop>();
                    var resulInitCard = CardReaderInterop.InitReader((Interop.Main.Cross.Domain.CardReader.TypeReaderCard)globalConfigATM.configCardReader.IdTypeCardReader);
                    if (resulInitCard.State != Foundation.Stone.Application.Wrapper.ResponseType.Success)
                    {
                        Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO(loggerName, $"No se puede inicalizar el lector de tarjeta del ATM:{resulInitCard.Message}", System.Diagnostics.EventLogEntryType.Error);
                        ProcessError(new Response(), new Exception($"No se puede inicalizar el lector de tarjeta del ATM:{resulInitCard.Message}"));
                    }
                }

                ///Inicializamos el lector huella
                if (globalConfigATM.configFingerPrintReader.HasFingerPrint)
                {
                    var FingerPrintInterop = MicroService.CreateInstance<IFingerPrintInterop>();
                    var resulInitFingerPrint = FingerPrintInterop.GetStatus();
                    if (resulInitFingerPrint.State != ResponseType.Success)
                    {
                        Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO(loggerName, $"No se puede inicalizar el lector de huella del ATM:{resulInitFingerPrint.Message}", System.Diagnostics.EventLogEntryType.Error);
                        ProcessError(new Response(), new Exception($"No se puede inicalizar el lector de huella del ATM:{resulInitFingerPrint.Message}"));
                    }
                }

                ///Inicializamos el Receptor de billetes
                if (globalConfigATM?.configReceptorCOM?.HasReceptor == true)
                {
                    var receptorServiceInterop = MicroService.CreateInstance<IReceptorServiceInterop>();
                    var resulInitReceptor = receptorServiceInterop.InitReceptor(globalConfigATM.configReceptorCOM.Portname);
                    if (resulInitReceptor.State != ResponseType.Success)
                    {
                        Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO(loggerName, $"No se puede inicalizar el receptor del ATM:{resulInitReceptor.Message}", System.Diagnostics.EventLogEntryType.Error);
                        ProcessError(new Response(), new Exception($"No se puede inicalizar el receptor del ATM:{resulInitReceptor.Message}"));
                    }
                }

                /////TODO arrancamos monitor de hotkey
                //InitHotKey();

                ///TODO arrancamos el ATM
                InitUIAtm();

                ///TODO configuramos timer
                timer.Elapsed += Timer_Elapsed;
                timer.Start();


            }
            catch (Exception ex)
            {
                ProcessError(new Response(), ex);
            }

        }

        //private static void InitHotKey()
        //{
        //    IHotKeyServices deamonHotKey = MicroService.CreateInstance<IHotKeyServices>();
        //}

        private static void InitUIAtm()
        {
            if (!System.Diagnostics.Process.GetProcesses().ToList().Any(x => x.ProcessName.Equals("ATM")))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(@"C:\atm\atm.exe");
                startInfo.WindowStyle = ProcessWindowStyle.Maximized;
                var processs = Process.Start(startInfo);
            }
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //verificamos que exista el proceso del ATM UI activo
            if (!System.Diagnostics.Process.GetProcesses().ToList().Any(x => x.ProcessName.Equals("ATM")))
            {
                Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO(loggerName, $"No se tiene levantado el ATM", System.Diagnostics.EventLogEntryType.Error);
#if !DEBUG
                System.Diagnostics.Process.Start(@"c:\ShutDown.exe", @"/L /R /T:1 /Y /C");
#endif
            }

            //Reiniciamos la PC a las 5AM 
            TimeSpan timeSpanIni = new TimeSpan(5, 0, 0);
            TimeSpan timeSpanFin = new TimeSpan(5, 1, 0);
            if (DateTime.Now.TimeOfDay >= timeSpanIni && DateTime.Now.TimeOfDay <= timeSpanFin)
            {
                Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO(loggerName, $"Reiniciando ATM", System.Diagnostics.EventLogEntryType.Warning);
                System.Diagnostics.Process.Start(@"c:\ShutDown.exe", @"/L /R /T:1 /Y /C");
            }

            ///TODO llamamos al MF para que no se duerma
            try
            {
                Interop.Main.Global.ClientRestHelper clientRestHelper = new Interop.Main.Global.ClientRestHelper();
                var responseGetLightningSpin = clientRestHelper.Consume<Response>(Setttings.uriBaseServices + "/RiceMFNet", null, "", resAuth.Object.token).Result;
            }
            catch
            {

            }
        }

       

    }
}
