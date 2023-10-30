using System.Runtime.Serialization;

namespace Interop.Main.Cross.Domain.Dispenser.Enumerators
{
    [DataContract]
    public enum ModelosDispensador : int
    {
        [EnumMember]
        NMD50 = 50,
        [EnumMember]
        NMD100 = 100

    }

    [DataContract]
    public enum Comando : int
    {
        [EnumMember]
        ResetLento,
        [EnumMember]
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
        ReadEmulatorShutter,
        [EnumMember]
        WriteEmulatorShutter,
        [EnumMember]
        ReadCassette,
        [EnumMember]
        WriteSize,
        [EnumMember]
        ClearTable,
        [EnumMember]
        ReadPromID,
        [EnumMember]
        ProgramIDBlock,
        [EnumMember]
        NumberSerialNMD,
        [EnumMember]
        WriteNumberSerialNMD,
        [EnumMember]
        EnabledCassette,
        [EnumMember]
        EnabledCheckNotes,
        [EnumMember]
        NQInitialisation,
        [EnumMember]
        CleanNSRollers,
        [EnumMember]
        CleanNF,
        [EnumMember]
        CheckBundle,
        [EnumMember]
        Fin
    }

    [DataContract]
    public enum ResponseDispensador : int
    {
        [EnumMember]
        Exito = 0,
        [EnumMember]
        Advertencia = 1,
        [EnumMember]
        ErrorOperativo = 2,
        [EnumMember]
        ErrorSoftware = 3,
        [EnumMember]
        ErrorFatal = 4,
        [EnumMember]
        ErrorHardware = 5
    }

    [DataContract]
    public enum PartRespuesta : int
    {
        [EnumMember]
        Status = 0, //S
        [EnumMember]
        Header = 1, //H
        [EnumMember]
        StatusHeader = 2, //F
        [EnumMember]
        NumberBill = 3, //N
        [EnumMember]
        IDCassete = 4, //G
        [EnumMember]
        LRC = 5,//L
        [EnumMember]
        CassetteStatus = 6, //A
        [EnumMember]
        StatusRejectVault = 7,//r
        [EnumMember]
        StatusRejectFeeder = 8,//f
        [EnumMember]
        StatusMotorDriver = 9,//t
        [EnumMember]
        StatusDoubleDetect = 10,//q
        [EnumMember]
        StatusNoteDiverter = 11,//d
        [EnumMember]
        StatusNoteTransport = 12,//s
        [EnumMember]
        StatusStackPresenter = 13,//p
        [EnumMember]
        StatusThroat = 14,//o
        [EnumMember]
        StatusDataHandler = 15,//v usado para cada cassette
        [EnumMember]
        UnitIdentifier = 16//v usado para cada cassette
            
    }
}