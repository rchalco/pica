using System;
using System.Linq;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Text;
using Foundation.Stone.CrossCuting.Util;
using Foundation.Stone.CrossCuting.File;
using System.Collections.Generic;
using RuntimeDispensador.Structural;
using Interop.Main.Cross.Domain.Orchestrator;

namespace RuntimeDispensador.Core
{
    public class ProviderComunication
    {
        public class ConfigCOM
        {
            public string Portname { get; set; }
            public int ReadTimeout { get; set; }
            public int WriteTimeout { get; set; }
            public int Baudios { get; set; }
            public int Paridad { get; set; }
            public int Data { get; set; }
            public int NumBandejas { get; set; }
            public string Tipo { get; set; }
        }   

        public static ConfigCOM Configuration
        {
            get
            {
                return _configCOM;
            }
        }

        private ProviderComunication()
        {

        }

        private static ProviderComunication instance = null;
        private SerialPort _serialPort;
        public string MesssagePort { get; set; }

        private static ConfigCOM _configCOM;
        private static bool portAvailable = true;
        private DateTime fechaInicio = DateTime.Now;
        private bool IsTimeExceeded = false;


        public static void InitProviderCominication(GlobalConfigATM globalConfigATM)
        {

            if (globalConfigATM == null)
            {
                throw new ArgumentException($"el parametro  {globalConfigATM} es nulo, imposible crear un Provider Comunication");
            }
            _configCOM = new ConfigCOM
            {
                Baudios = globalConfigATM.configDispenserCOM.Baudios,
                Data = globalConfigATM.configDispenserCOM.Data,
                Paridad = globalConfigATM.configDispenserCOM.Paridad,
                Portname = globalConfigATM.configDispenserCOM.Portname,
                ReadTimeout = globalConfigATM.configDispenserCOM.ReadTimeout,
                WriteTimeout = globalConfigATM.configDispenserCOM.ReadTimeout
            };

        }
        public static ProviderComunication GetCurrentInstace()
        {

            if (instance == null)
            {
                if (_configCOM == null)
                {
                    throw new ArgumentNullException("no se inicializo el provider comunication, imposible obtener una instancia");
                }
                instance = new ProviderComunication();
                instance._serialPort = new SerialPort()
                {
                    PortName = _configCOM.Portname,
                    ReadTimeout = _configCOM.ReadTimeout,
                    WriteTimeout = _configCOM.WriteTimeout,
                    BaudRate = _configCOM.Baudios,
                    Parity = (Parity)_configCOM.Paridad,
                    DataBits = _configCOM.Data
                };
                instance._serialPort.DataReceived += _serialPort_DataReceived;
            }
            return instance;
        }

        public async Task<string> SenMessage(string message)
        {
            await Task.Factory.StartNew(IsAvalaiblePort);
            portAvailable = false;
            IsTimeExceeded = false;
            fechaInicio = DateTime.Now;
            string resul = string.Empty;
            instance._serialPort.Open();
            instance._serialPort.DiscardOutBuffer();
            instance._serialPort.DiscardInBuffer();
            instance._serialPort.WriteLine(message + CalculoLCR(message) + "\r");
            await Task.Factory.StartNew(IsAvalaiblePort);
            if (instance.IsTimeExceeded)
            {
                instance._serialPort.Close();
                throw new Exception("Tiempo de espera excedido");
            }

            resul = MesssagePort;
            return resul;
        }

        /// <summary>
        /// Calculo de redundancia ciclica
        /// </summary>
        /// <param name="message">comando</param>
        /// <returns></returns>
        private string CalculoLCR(string message)
        {
            string resul = string.Empty;
            int V = 0;
            message.ToCharArray().ToList().ForEach(x =>
            {
                V = (V ^ (int)x);
            });
            int Y = (V / 16);
            int Z = (V & 15);
            int L1 = Y | 48;
            int L2 = Z | 48;

            resul = Convert.ToString((char)L1) + Convert.ToString((char)L2);
            return resul;
        }

        private static void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            lock (instance)
            {
                instance.MesssagePort = instance._serialPort.ReadTo("\r");
                instance._serialPort.Close();
                portAvailable = true;
            }

        }
        private static void IsAvalaiblePort()
        {
            while (!portAvailable)
            {
                if (DateTime.Now > instance.fechaInicio.AddMilliseconds(_configCOM.ReadTimeout))
                {
                    instance.IsTimeExceeded = true;
                    break;
                }
            }
        }
    }

}