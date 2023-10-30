using Foundation.Stone.Application.Wrapper;
using Interop.Main.Cross.Domain.Dispenser;
using Interop.Main.Cross.Domain.Orchestrator;
using System.ServiceModel;

namespace RuntimeDispensador.Service
{
    [ServiceContract]
    public interface IDispensador
    {

        [OperationContract]
        ResponseQuery<Trace> Inicializar(GlobalConfigATM globalConfigATM, bool force);

        [OperationContract]
        ResponseQuery<Trace> AsegurarBandeja();

        [OperationContract]
        ResponseQuery<Trace> LiberarBandeja();

        [OperationContract]
        ResponseQuery<Trace> Dispensar(int pAmount);
        [OperationContract]
        ResponseQuery<Trace> DispensarForce(int pAmount);
        [OperationContract]
        ResponseObject<ResponseDispensarATM> DispensarATM(int pAmount);

        [OperationContract]
        ResponseQuery<Trace> VerificaBanejas();

        [OperationContract]
        ResponseQuery<Trace> Diagnosticar();

        [OperationContract]
        ResponseQuery<Trace> AbrirShutter();

        [OperationContract]
        ResponseQuery<Trace> TablaDeEstadosGrl();

        [OperationContract]
        ResponseQuery<Trace> TablaDeEstado();

        [OperationContract]
        ResponseQuery<Trace> CerrarShutter();

        [OperationContract]
        ResponseQuery<Trace> LeeEstadoShutter();

        [OperationContract]
        ResponseQuery<Trace> ActualizaEstadoShutter(string pEstado);

        [OperationContract]
        ResponseQuery<Trace> Reset();

        [OperationContract]
        ResponseQuery<Trace> ResetLento();

        [OperationContract]
        ResponseQuery<Trace> BorrarTabla();

        [OperationContract]
        ResponseQuery<Trace> LeeIdDePrograma();

        [OperationContract]
        ResponseQuery<Trace> LeeNumeroSerieNMD();

        [OperationContract]
        ResponseQuery<Trace> ActualizaNumeroSerieNMD(string pSerie);

        [OperationContract]
        ResponseQuery<Trace> HabilitaDeteccionDeBandeja();

        [OperationContract]
        ResponseQuery<Trace> HabilitaValidadorDeEntrega();

        [OperationContract]
        ResponseQuery<Trace> LeeIdBlock();

        [OperationContract]
        ResponseQuery<Trace> DispensarCmd(string pParam);

        [OperationContract]
        ResponseQuery<Trace> SelfTest0();

        [OperationContract]
        ResponseQuery<Trace> SelfTest1();

        [OperationContract]
        ResponseQuery<Trace> SelfTest9();

        [OperationContract]
        ResponseQuery<Trace> SelfTestA();
        [OperationContract]
        ResponseQuery<Trace> BorrarNQ();

        [OperationContract]
        int NumBandejas();

        [OperationContract]
        ResponseQuery<Trace> LimpiarNSRollers();

        [OperationContract]
        ResponseQuery<Trace> LimpiarNF(int pNF);

        [OperationContract]
        ResponseObject<ResponseOffSetMount> OffSetMountToString(int mountDispenser);

        [OperationContract]
        ResponseQuery<Trace> VerificaBrazoMecanico();
        [OperationContract]
        ResponseQuery<Court> GetCoutAvailable();
    }

}