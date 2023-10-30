using Hangar.ServiceImplement.Config;
using Interop.Main.Cross.Domain.Dispenser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Interop.Main.Cross.Domain.Orchestrator
{
    [DataContract]
    public class GlobalConfigATM
    {
        [DataContract]
        public class ConfigCardReader
        {
            /// <summary>
            /// 1. CREATOR
            /// 3. GEMPLUS
            /// </summary>
            [DataMember] public int IdTypeCardReader { get; set; }
            /// <summary>
            /// 1. CREATOR
            /// 3. GEMPLUS
            /// </summary>
            [DataMember] public string NameCarderReader { get; set; }
            /// <summary>
            /// Serial del lector de tarjeta
            /// </summary>
            [DataMember] public string SerialNumber { get; set; }
            /// <summary>
            /// bandera que indica si el ATM tiene este dispositivo
            /// </summary>
            [DataMember] public bool HasCarReader { get; set; }
        }
        [DataContract]
        public class ConfigFingerPrintReader
        {
            /// <summary>
            /// 1. Digital Persona
            /// </summary>
            [DataMember] public int IdTypeFingerPrint { get; set; }
            /// <summary>
            /// 1. Digital Persona
            /// </summary>
            [DataMember] public string NameFingerPrint { get; set; }
            /// <summary>
            /// Serial del lector de tarjeta
            /// </summary>
            [DataMember] public string SerialNumber { get; set; }
            /// <summary>
            /// bandera que indica si el ATM tiene este dispositivo
            /// </summary>
            [DataMember] public bool HasFingerPrint { get; set; }
        }
        [DataContract]
        public class ConfigDispenserCOM
        {
            /// <summary>
            /// Nombre del puerto COM
            /// </summary>
            [DataMember] public string Portname { get; set; }
            /// <summary>
            /// Tiempo de espera de respuesta de lectura del puerto COM
            /// </summary>
            [DataMember] public int ReadTimeout { get; set; }
            /// <summary>
            /// Timepo de espera de respuesta de escritura por el puerto COM
            /// </summary>
            [DataMember] public int WriteTimeout { get; set; }
            /// <summary>
            /// Valor de frecuencia de los Baudios
            /// </summary>
            [DataMember] public int Baudios { get; set; }
            /// <summary>
            /// Valor para comprobacion de paridad
            /// </summary>
            [DataMember] public int Paridad { get; set; }
            /// <summary>
            /// S/A
            /// </summary>
            [DataMember] public int Data { get; set; }
            /// <summary>
            /// Serial del Dispensador
            /// </summary>
            [DataMember] public string SerialNumber { get; set; }
            /// <summary>
            /// bandera que indica si el ATM tiene este dispositivo
            /// </summary>
            [DataMember] public bool HasDispenser { get; set; }
        }
        [DataContract]
        public class ConfigReceptorCOM
        {
            /// <summary>
            /// Nombre del puerto COM
            /// </summary>
            [DataMember] public string Portname { get; set; }
            /// <summary>
            /// Tiempo de espera de respuesta de lectura del puerto COM
            /// </summary>
            [DataMember] public int ReadTimeout { get; set; }
            /// <summary>
            /// Timepo de espera de respuesta de escritura por el puerto COM
            /// </summary>            
            [DataMember] public string SerialNumber { get; set; }
            /// <summary>
            /// bandera que indica si el ATM tiene este dispositivo
            /// </summary>
            [DataMember] public bool HasReceptor { get; set; }
            /// <summary>
            /// Modelo
            /// </summary>
            [DataMember] public string Model { get; set; }
        }
        [DataContract]
        public class ConfigDispenserStatus
        {
            [DataMember] public int NumBandejas { get; set; }
            [DataMember] public int MaxQuantityBill { get; set; }
            [DataMember] public List<Cassette> Cassettes { get; set; }
            [DataMember] public bool HasDispenser { get; set; }
            /// <summary>
            /// Modelo del Dispensador, puede ser NMD50 o NMD100
            /// </summary>
            [DataMember] public string Tipo { get; set; }
        }

        [DataMember] public ConfigCardReader configCardReader { get; set; }
        [DataMember] public ConfigDispenserCOM configDispenserCOM { get; set; }
        [DataMember] public ConfigReceptorCOM configReceptorCOM { get; set; }
        [DataMember] public ConfigDispenserStatus configDispenserStatus { get; set; }
        [DataMember] public ConfigFingerPrintReader configFingerPrintReader { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int IdOffice { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
