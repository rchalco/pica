using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NMDManagement
{
    public class ATMDTO
    {
        [DataMember]
        public int IdATM { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string IP { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public bool State { get; set; }
        [DataMember]
        public ATMConfig Configuration { get; set; }
        public ProfileATM Profile { get; set; }
        [DataMember]
        public int IdOffice { get; set; }
      
    }
    public class ATMConfig
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

        [DataContract]
        public class ConfigReceptorCOM
        { /// <summary>
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

        [DataMember] public ConfigCardReader configCardReader { get; set; }
        [DataMember] public ConfigDispenserCOM configDispenserCOM { get; set; }
        [DataMember] public ConfigDispenserStatus configDispenserStatus { get; set; }
        [DataMember] public ConfigFingerPrintReader configFingerPrintReader { get; set; }
        [DataMember] public ConfigReceptorCOM configReceptorCOM { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int IdOffice { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataContract]
        public class Cassette
        {
            public Cassette()
            {
                Status = "0";
                MinQuantity = 15;
            }
            [DataMember]
            public int Id { get; set; }

            [DataMember]
            public int Sequence { get; set; }

            [DataMember]
            public int CurrencyCourt { get; set; }

            [DataMember]
            public int TotalQuantity { get; set; }

            [DataMember]
            public string Status { get; set; }

            [DataMember]
            public int MinQuantity { get; set; }
        }
    }

    [DataContract]
    public class ProfileATM
    {
        /// <summary>
        /// Codigo referencial MF .Net
        /// </summary>
        [DataMember]
        public string Code { get; set; }
        /// <summary>
        /// Id Office MF .Net
        /// </summary>
        [DataMember]
        public long IdOffice { get; set; }
        /// <summary>
        /// Id Office MF .Net
        /// </summary>
        [DataMember]
        public string NameOffice { get; set; }
        /// <summary>
        /// Id Ref MF .Net
        /// </summary>
        [DataMember]
        public string GeographicLocation { get; set; }
        /// <summary>
        /// Id Ref para carga trasera o delantera
        /// </summary>
        [DataMember]
        public int IdcMode { get; set; }
        /// <summary>
        /// Desc Ref para carga trasera o delantera
        /// </summary>
        [DataMember]
        public int IdcModeDescription { get; set; }
        /// <summary>
        /// Id Ref MF .Net
        /// </summary>
        [DataMember]
        public int IdcTypeATM { get; set; }
        /// <summary>
        /// Desc Ref MF .Net
        /// </summary>
        [DataMember]
        public string DescriptionTypeATM { get; set; }
        /// <summary>
        /// Id Ref MF .Net
        /// </summary>
        [DataMember]
        public string IdSchedule { get; set; }
        /// <summary>
        /// Desc Ref MF .Net
        /// </summary>
        [DataMember]
        public string DescriptionSchedule { get; set; }
        /// <summary>
        /// punto de geolozacion del ATM
        /// </summary>
        [DataMember]
        public string Location { get; set; }
        /// <summary>
        /// DIreccion del ATM 
        /// </summary>
        [DataMember]
        public string Address { get; set; }
        /// <summary>
        /// Codigo para la ASFI
        /// </summary>
        [DataMember]
        public string CodeForASFI { get; set; }
        /// <summary>
        /// Fecha de inactivacion
        /// </summary>
        [DataMember]
        public DateTime? InactivationDate { get; set; }
        /// <summary>
        /// Metodo de entrega del dinero
        /// </summary>
        [DataMember]
        public int IdcDeliveryMethod { get; set; }
        /// <summary>
        /// Metodo de entrega del dinero
        /// </summary>
        [DataMember]
        public string DescDeliveryMethod { get; set; }
        /// <summary>
        /// Id de oficina encargada de la carga operativa de un ATM
        /// </summary>
        [DataMember]
        public int IdOperationalOffice { get; set; }
        /// <summary>
        /// Nombre de oficina encargada de la carga operativa de un ATM
        /// </summary>
        [DataMember]
        public string NameOperationalOffice { get; set; }
    }
}
