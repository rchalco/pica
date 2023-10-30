using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NMDManagement
{
    class Environments
    {
    }

    [Serializable]
    public class Parameter
    {
        public int typeReader { get; set; }
    }
    [DataContract]
    [Serializable]
    public enum Comando : int
    {
        [EnumMember]
        ResetLento,
        Reset,
        [EnumMember]
        CerrarBandejas,
        [EnumMember]
        AbrirBandejas,
        [EnumMember]
        LeeIdBandeja,
        [EnumMember]
        Dispensar,
        [EnumMember]
        DispensarATM,
        [EnumMember]
        VerifEntrega,
        [EnumMember]
        Liberar,
        [EnumMember]
        Recojer,
        [EnumMember]
        SelfTest,
        [EnumMember]
        SelfTest0,
        [EnumMember]
        SelfTest1,
        [EnumMember]
        SelfTestA,
        [EnumMember]
        NMDStatus,
        [EnumMember]
        SelfTest9,
        [EnumMember]
        StatusCodeTable,
        [EnumMember]
        TotalNotesDelivered,
        [EnumMember]
        TotalNotesRejected,
        [EnumMember]
        TotalBundleRejects,
        [EnumMember]
        TotalNotesSingleRejected,
        [EnumMember]
        TotalNotesBundleRejected,
        [EnumMember]
        TotalBundlesDelivered,
        [EnumMember]
        TotalRetracts,
        [EnumMember]
        TotalNotesDelivered2,
        [EnumMember]
        TotalBundleRejectsHighPressureFeed,
        [EnumMember]
        TotalNotesDeliveredLifeTime,
        [EnumMember]
        TotalNotesRejectedLifeTime,
        [EnumMember]
        TotalBundleRejectsLifeTime,
        [EnumMember]
        TotalBundlesDeliveredLifeTime,
        [EnumMember]
        TotalRetractsLifeTime,
        [EnumMember]
        AbrirShutter,
        [EnumMember]
        CerrarShutter,
        [EnumMember]
        EmulatorShutter,
        [EnumMember]
        ReadCassette,
        [EnumMember]
        WriteSize,
        [EnumMember]
        ClearTable,
        [EnumMember]
        ReadPromID,
        [EnumMember]
        NumberSerialNMD,
        [EnumMember]
        WriteNumberSerialNMD,
        [EnumMember]
        CheckBundle,
        [EnumMember]
        Fin
    }
    [DataContract]
    [Serializable]
    public class Trace
    {
        [DataMember]
        public Comando IdCommand { get; set; }

        [DataMember]
        public string  CommandName { get; set; }
        [DataMember]
        public string Parameter { get; set; }
        [DataMember]
        public string AdditionalInformation { get; set; }
        [DataMember]
        public StatusDispenser Result { get; set; }
        [DataMember]
        public DateTime DateCreate { get; set; }
        [DataMember]
        public DateTime DateExecution { get; set; }
    }

    [Serializable]
    public class ResponseInit
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult InicializarResult { get; set; }
    }
    [Serializable]
    public class ResponseDelivery
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
        }
        public InitResult DispensarResult { get; set; }
    }

    [Serializable]
    public class ResponseDeliveryForce
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
        }
        public InitResult DispensarForceResult { get; set; }
    }

    [Serializable]
    public enum ResponseType
    {
        Success = 1,
        Warning = 2,
        Error = 3,
        NoData = 4
    }
    [Serializable]
    public class CompositePartResponseStatuc
    {
        public PartRespuesta Key { get; set; }
        public string Value { get; set; }
    }

    [Serializable]
    public class StatusDispenser
    {
        public StatusDispenser()
        {
            Cassettes = new List<CassetteStatus>();
        }

        public string Code { get; set; }
        public string Type { get; set; }
        public ResponseDispensador state { get; set; }
        public string Description { get; set; }
        public List<CompositePartResponseStatuc> PartsResponse { get; set; }
        public List<CassetteStatus> Cassettes { get; set; }
        public string ResponseOriginal { get; set; }
    }

    [Serializable]
    public class CompositePartResponse
    {
        public PartRespuesta Key { get; set; }
        public ResponseComplex Value { get; set; }
    }

    [Serializable]
    public class CassetteStatus
    {
        public int Index { get; set; }
        public string Id { get; set; }
        public List<CompositePartResponse> PartsResponse { get; set; }
        public string StrResponse { get; set; }
    }
    public enum ResponseDispensador : int
    {
        Exito = 0,
        Advertencia = 1,
        ErrorOperativo = 2,
        ErrorSoftware = 3,
        ErrorFatal = 4,
        ErrorHardware = 5
    }
    public enum PartRespuesta : int
    {
        Status = 0, //S
        Header = 1, //H
        StatusHeader = 2, //F
        NumberBill = 3, //N
        IDCassete = 4, //G
        LRC = 5,//L
        CassetteStatus = 6, //A
        StatusRejectVault = 7,//r
        StatusRejectFeeder = 8,//f
        StatusMotorDriver = 9,//t
        StatusDoubleDetect = 10,//q
        StatusNoteDiverter = 11,//d
        StatusNoteTransport = 12,//s
        StatusStackPresenter = 13,//p
        StatusThroat = 14,//o
        StatusDataHandler = 15,//v usado para cada cassette
        Transport = 16, // 0x00000010
        SensorGain = 17, // 0x00000011
        TransportCalibration = 17, // 0x00000011
        Denomination = 18, // 0x00000012
        SensorThickness = 18, // 0x00000012
        Type = 19, // 0x00000013
        NoteSizeVertical = 20, // 0x00000014
        NoteSizeHorizontal = 21, // 0x00000015
        Currency = 22, // 0x00000016
        Denominacion = 23, // 0x00000017
    }

    [Serializable]
    public class ResponseComplex
    {
        public string model { get; set; }
        public string type { get; set; }
        public string value { get; set; }
        public string typeEnum { get; set; }
        public string concreteResponse { get; set; }
        public string description { get; set; }
        public string code { get; set; }
    }


    #region "Read Id cassette"
    [Serializable]
    public class ResponseReadIdCassette
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult VerificaBanejasResult { get; set; }
    }
    #endregion

    #region "Release cassette"
    [Serializable]
    public class ResponseReleaseCassette
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult LiberarBandejaResult { get; set; }
    }
    #endregion

    #region "Close cassette"
    [Serializable]
    public class ResponseCloseCassette
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult AsegurarBandejaResult { get; set; }
    }
    #endregion

    #region "Abrir shutter"
    public class ResponseOpenShutter
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult AbrirShutterResult { get; set; }
    }
    #endregion

    #region "Cerrar Shutter"
    public class ResponseCloseShutter
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult CerrarShutterResult { get; set; }
    }
    public class ResponseReadStatusShutter
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult LeeEstadoShutterResult { get; set; }
    }
    public class ResponseWriteStatusShutter
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult ActualizaEstadoShutterResult { get; set; }
    }

    #endregion


    #region "tabla de estados"
    public class ResponseStateTable
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult TablaDeEstadoResult { get; set; }
    }
    #endregion

    #region "tabla de estados general"
    public class ResponseStateTableGrl
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult TablaDeEstadosGrlResult { get; set; }
    }
    #endregion

    #region "Reset"
    public class ResponseReset
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult ResetResult { get; set; }
    }
    #endregion

    #region "Reset Lento"
    public class ResponseResetLento
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult ResetLentoResult { get; set; }
    }
    #endregion

    #region "Clear table"
    public class ResponseClearTable
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult BorrarTablaResult { get; set; }
    }
    #endregion


    #region "lee id de firmware"
    public class ResponseReadIdProgram
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult LeeIdDeProgramaResult { get; set; }
    }
    #endregion

    #region "lee serie NMD"
    public class ResponseReadSerieNMD
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult LeeNumeroSerieNMDResult { get; set; }
    }
    #endregion


    #region "Actualiza serie NMD"
    public class ResponseUpdateSerieNMD
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult ActualizaNumeroSerieNMDResult { get; set; }
    }
    #endregion
    #region "Habilita detección de bandejas"
    public class ResponseEnableCassettes
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult HabilitaDeteccionDeBandejaResult { get; set; }
    }
    #endregion

    #region "Habilita verificar entrega"
    public class ResponseEnabledCheckNotes
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult HabilitaValidadorDeEntregaResult { get; set; }
    }
    #endregion

    #region "lee Id block"
    public class ResponseReadIdBlock
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult LeeIdBlockResult { get; set; }
    }
    #endregion

    #region "lee Id block"
    public class ResponseDispensarCMD
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult DispensarCmdResult { get; set; }
    }
    #endregion

    #region "NMDStatus"
    public class ResponseNMDStatus
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult DiagnosticarResult { get; set; }
    }
    #endregion

    #region "SelfTest0"
    public class ResponseSelfTest0
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult SelfTest0Result { get; set; }
    }
    #endregion

    #region "SelfTest1"
    public class ResponseSelfTest1
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult SelfTest1Result { get; set; }
    }
    #endregion

    #region "SelfTest9"
    public class ResponseSelfTest9
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult SelfTest9Result { get; set; }
    }
    #endregion

    #region "SelfTestA"
    public class ResponseSelfTestA
    {
        public class InitResult
        {
            public ResponseType State { get; set; }

            public string Message { get; set; }

            public string CodeBase { get; set; }
            public List<Trace> ListEntities { get; set; }
        }
        public InitResult SelfTestAResult { get; set; }
    }
    #endregion

    #region "huella"
    public class DataCaptureFinger
    {
        public class ReadResult
        {
            public string CodeBase { get; set; }
            public string Message { get; set; }
            public ResponseType State { get; set; }
        }

        public ReadResult CaptureFingerResult { get; set; }

    }

    #endregion
    #region "validador"

    public class ResponseValidateFingerClose
    {
        public class ReadResult
        {
            public string CodeBase { get; set; }
            public string Message { get; set; }
            public ResponseType State { get; set; }
            
        }
        public ReadResult ATMAccessHistoryCloseResult { get; set; }

    }

    public class ResponseValidateFinger
    {
        public class ReadResult
        {
            public string CodeBase { get; set; }
            public string Message { get; set; }
            public ResponseType State { get; set; }
            public clObject Object { get; set; }
        }
        public ReadResult VerifyUserResult { get; set; }
        
    }
    public class ResponseValidatePassword
    {
        public class ReadResult
        {
            public string CodeBase { get; set; }
            public string Message { get; set; }
            public ResponseType State { get; set; }
            public VerifyUserResultGrl Object { get; set; }
        }
        public ReadResult VerifyUserResult { get; set; }

    }
    public class VerifyUserResultGrl
    {
        public string Cargo { get; set; }
        public string DI { get; set; }
        public string Email { get; set; }
        public string IdGenero { get; set; }
        public int IdOficina { get; set; }
        public long IdUserRole { get; set; }
        public long IdUsuario { get; set; }
        public string MensajeSalida { get; set; }
        public string NombrePersona { get; set; }
        public string Oficina { get; set; }
        public string UserRole { get; set; }
        public string Usuario { get; set; }
        public Int64 IdATMAccess { get; set; }  
        public ResponseType State { get; set; }
    }
    public class clObject
    {
        public List<AditionalItem> AditionalItems { get; set; }
        public string User { get; set; }
        public string token { get; set; }   
    }
    public class AditionalItem
    {
        public string GroupName { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
    #endregion

    #region "user"
    public class UserData
    {
        public string CodeOffice { get; set; }
        public DateTime CreationDate { get; set; }
        public string Id { get; set; }
        public int IdOffice { get; set; }

        public Int64 IdPerson { get; set; }
        public string UserCI { get; set; }
        public long IdUserRole { get; set; }
        public string UserRole { get; set; }
        public string OfficeName { get; set; }

        public Boolean PasswordNotChange { get; set; }
        public Int64 UserID { get; set; }
        public String UserPersonName { get; set; }
        public Int64 IdATMAccess { get; set; }
    }
    #endregion

    #region "clases para lector de tarjeta"
    public class CardResultIni
    {

        public class InitResult
        {

            public string CodeBase { get; set; }


            public string Message { get; set; }


            public ResponseType State { get; set; }
        }

        public InitResult InitReaderResult { get; set; }

    }

    public class CardResultRead
    {
        public class ReadResult
        {
            public string CodeBase { get; set; }
            public string Message { get; set; }
            public ResponseType State { get; set; }
        }

        public ReadResult ReadCardResult { get; set; }

    }
    public class CardResultEject
    {
        public class ReadResult
        {
            public string CodeBase { get; set; }
            public string Message { get; set; }
            public ResponseType State { get; set; }
        }

        public ReadResult EjectCardResult { get; set; }

    }

    public class CardResultReset
    {
        public class ReadResult
        {
            public string CodeBase { get; set; }
            public string Message { get; set; }
            public ResponseType State { get; set; }
        }

        public ReadResult ResetResult { get; set; }

    }
    #endregion

    #region "Administration"

    public class ResponseObject
    {
        public class Result
        {
            public string CodeBase { get; set; }
            public string Code { get; set; }
            public string Message { get; set; }
            public ResponseType State { get; set; }
            public ReponseCount reponseCount { get; set; }
        }

        public Result InsertAtmCashCountDetailResult { get; set; }  
    }
    public class ReponseCount
    {
        /// <summary>
        /// Indica si la información fue grabada correctamente
        /// </summary>           
        public bool IsSave { get; set; }

        /// <summary>
        /// Indica el tipo de error que ocurrio
        /// </summary>           
        public string ErrorDescription { get; set; }
    }

    public class ResultRecordForBalance
    {
        public class ReadResult
        {
            public string CodeBase { get; set; }
            public string Message { get; set; }
            public ResponseType State { get; set; }
           
            public Coinages Object { get; set; }
           
        }

        public ReadResult GetATMInformationToCashCountByIdATMResult { get; set; }
        
    }
    public class Coinage
    {
        public long IdCoinage { get; set; }
        public long IdMoney { get; set; }

        public long Value { get; set; }
    }

    public class Coinages
    {
        public List<Coinage> ColCoinage { get; set; }
        public string ErrorDescription { get; set; }
        public long IdATMCashCount { get; set; }
        public bool IsCorrect { get; set; }
        public string Observations { get; set; }
    }


    public class ATMCashCountDTO
    {
        public ATMCashCountDTOa objATMCashCountDTO { get; set; }
    }
    public class ATMCashCountDTOa
    {
        /// <summary>
        /// Id Arqueo Atm
        /// </summary>           
        public long IdAtmCashCount
        {
            get;
            set;
        }

        /// <summary>
        /// Observaciones
        /// </summary>           
        public string Observations
        {
            get;
            set;
        }

        /// <summary>
        /// detalle arqueo
        /// </summary>           
        public IList<ATMCashCountDetailDTO> ColAtmCashCountDetail
        {
            get;
            set;
        }
    }

    public class ATMCashCountDetailDTO
    {
        /// <summary>
        /// Cantidad contada
        /// </summary>           
        public int Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// id del corte
        /// </summary>           
        public long IdCoinage
        {
            get;
            set;
        }

        /// <summary>
        /// Id de la moneda
        /// </summary>
        public virtual long IdMoney
        {
            get;
            set;
        }
    }

    public class AllATMResult
    {
        public class ReadResult
        {
            public string CodeBase { get; set; }
            public string Message { get; set; }
            public ResponseType State { get; set; }
            public List< ATMDetalle> ListEntities { get; set; }

        }
        public ReadResult GetAllATMResult { get; set; }
    }
    public class ATMDetalle
    {
        public string Description { get; set; }
        public string IP { get; set; }
        public string name { get; set; }
        public long IdATM { get; set; }
        public long IdOffice { get; set; }
    }
    public class ATMLoadSolicitationConfirmProcess
    {
        public class ReadResult
        {
            public string CodeBase { get; set; }
            public string Message { get; set; }
            public ResponseType State { get; set; }
            public long Object { get; set; }

        }
        public ReadResult ATMLoadSolicitationConfirmProcessResult { get; set; }
    }

    public class RecuestRegisterTicket
    {
        public class ReadResult
        {
            public string CodeBase { get; set; }
            public string Message { get; set; }
            public ResponseType State { get; set; }
            public string  Object { get; set; }

        }

        public ReadResult CreateTicketForOperationExternalServiceResult { get; set; }   
    }

    public class ParameterTicket
    {
        public class Param
        {
            public decimal AmountOperation { get; set; }
            public int IdMoney { get; set; }
            public int IdOffice { get; set; }
            public string Observation { get; set; }
            public string IdentityNumber { get; set; }
            public decimal IdcTranssactionType { get; set; }
            public decimal IdATM { get; set; }
            public string NameATM { get; set; }
            public string DescriptionATM { get; set; }
            public decimal NumberOperation { get; set; }
            public string NewConfiguration { get; set; }
            public string TraceDevice { get; set; }

        }
        //public class Param
        //{
        //    public decimal AmountOperation { get; set; }
        //    public int IdMoney { get; set; }
        //    public int IdOffice { get; set; }
        //    public string Observation { get; set; }
        //    public string IdentityNumber { get; set; }
        //    public decimal IdcTranssactionType { get; set; }
        //    public decimal IdATM { get; set; }
        //    public string NameATM { get; set; }
        //    public string DescriptionATM { get; set; }
        //    public decimal NumberOperation { get; set; }
        //    public string NewConfiguration { get; set; }
        //    public string TraceDevice { get; set; }

        //}
        public  Param objParameters { get; set; }
    }

    public class ATMLoadSolicitationPending
    {
        public class ReadResult
        {
            public string CodeBase { get; set; }
            public string Message { get; set; }
            public ResponseType State { get; set; }
            public List< ListEntity> ListEntities { get; set; }

        }

        public ReadResult ATMLoadSolicitationGetPendingForATMResult { get; set; }
    }
    public class ListEntity
    {
        public long IdATMLoadSolicitation { get; set; }
        public long IdATMEntity { get; set; }
        public long IdVault { get; set; }
        public long IdCoinage { get; set; }
        public string CoinageType { get; set; }
        public int CoinageValue { get; set; }
        public int Quantity { get; set; }
    }



    #endregion
    public class ResultConfgATM
    {
        public class ReadResult
        {
            public string CodeBase { get; set; }
            public string Message { get; set; }
            public ResponseType State { get; set; }

            public GlobalConfigATM Object { get; set; }
        }
        public ReadResult GetHotStateResult { get; set; }
    }

    public class GlobalConfigATM
    {
        public class ConfigReceptor
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
        public class ConfigDispenserStatus
        {
            [DataMember] public int NumBandejas { get; set; }
            [DataMember] public int MaxQuantityBill { get; set; }
            [DataMember] public List<Cassette> Cassettes { get; set; }
            [DataMember] public bool HasDispenser { get; set; }
            [DataMember] public string Tipo { get; set; }
        }
        public ConfigCardReader configCardReader { get; set; }
        public ConfigDispenserCOM configDispenserCOM { get; set; }
        public ConfigDispenserStatus configDispenserStatus { get; set; }
        public ConfigFingerPrintReader configFingerPrintReader { get; set; }
        public ConfigReceptor configReceptorCOM { get; set; }
        public int Id { get; set; }
        public int IdOffice { get; set; }
        public string Name { get; set; }

    }
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

        public class BillDelivered
    {
         public int CurrencyCourt { get; set; }
            
        public int IdMoney { get; set; }
            
        public int Quantity { get; set; }
    }

    public class ConfigurationATM
    {
        public class ReadResult
        {
            public string CodeBase { get; set; }
            public string Message { get; set; }
            public ResponseType State { get; set; }
            
            public ATMDTO Object { get; set; }
        }
        public ReadResult GetATMByIdResult { get; set; }    
    }
    public class ConfigATM
    {
        public long Id { get; set; }
        public long IdOffice { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IP { get; set; }
        public ConfigCardReader configCardReader { get; set; }
        public ConfigDispenserCOM configDispenserCOM { get; set; }
        public ConfigDispenserStatus configDispenserStatus { get; set; }
        public ConfigFingerPrintReader configFingerPrintReader { get; set; }
    }

    public class ConfigCardReader
    {
        bool HasCarReader { get; set; }
        int IdTypeCardReader { get; set; }
        string NameCarderReader { get; set; }
        string SerialNumber { get; set; }
    }

    public class ConfigDispenserCOM
    {
        public long Baudios { get; set; }
        public int Data { get; set; }
        public bool HasDispenser { get; set; }
        public int Paridad { get; set; }
        public string Portname { get; set; }
        public long ReadTimeout { get; set; }
        public string SerialNumber { get; set; }
        public long WriteTimeout { get; set; }

    }

    public class ConfigDispenserStatus
    {
        public bool HasDispenser { get; set; }
        public int MaxQuantityBill { get; set; }
        public int NumBandejas { get; set; }
        public string Tipo { get; set; }

        public List<Cassette> Cassettes { get; set; }
    }

    public class ConfigFingerPrintReader
    {
       public bool HasFingerPrint { get; set; }
        public int IdTypeFingerPrint { get; set; }
        public string NameFingerPrint { get; set; } 
        public string SerialNumber { get; set; }
    }

    public class ResultUpdateATM
    {
        public class ReadResult
        {
            public string CodeBase { get; set; }
            public string Message { get; set; }
            public ResponseType State { get; set; }

      
        }
        public ReadResult UpdateATMResult { get; set; }
    }

    public class RestultIniATM
    {
        public class ReadResult
        {
            public string CodeBase { get; set; }
            public string Message { get; set; }
            public ResponseType State { get; set; }


        }
        public ReadResult InitATMResult { get; set; }
    }

    public class ResultClearNQ
    {
        public class ReadResult
        {
            public string CodeBase { get; set; }
            public string Message { get; set; }
            public ResponseType State { get; set; }

            public List<Trace> ListEntities { get; set; }
        }
        public ReadResult BorrarNQResult { get; set; }
    }
    public class ResultCleanNS
    {
        public class ReadResult
        {
            public string CodeBase { get; set; }
            public string Message { get; set; }
            public ResponseType State { get; set; }

            public List<Trace> ListEntities { get; set; }
        }
        public ReadResult LimpiarNSRollersResult { get; set; }
    }

    public class ResultCleanNF
    {
        public class ReadResult
        {
            public string CodeBase { get; set; }
            public string Message { get; set; }
            public ResponseType State { get; set; }

            public List<Trace> ListEntities { get; set; }
        }
        public ReadResult LimpiarNFResult { get; set; }
    }
}
