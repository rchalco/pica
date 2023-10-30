using Foundation.Stone.Application.Module;
using Foundation.Stone.Application.Wrapper;
using Interop.Main.Cross.Domain.Dispenser;
using Interop.Main.Cross.Domain.Orchestrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.Main.Service.Interface
{
    [ModuleStone(FullName = "RuntimeDispensador.Service.Dispensador")]
    public interface IDispenserInterop
    {
        ResponseQuery<Trace> Inicializar(GlobalConfigATM globalConfigATM, bool force);
        ResponseQuery<Trace> AsegurarBandeja();
        ResponseQuery<Trace> LiberarBandeja();
        ResponseQuery<Trace> Dispensar(int pAmount);
        ResponseObject<ResponseDispensarATM> DispensarATM(int pAmount);
        ResponseQuery<Trace> VerificaBanejas();
        ResponseQuery<Trace> Diagnosticar();
        ResponseQuery<Trace> TablaDeEstadosGrl();
        ResponseQuery<Trace> AbrirShutter();

        ResponseQuery<Trace> TablaDeEstado();
        ResponseQuery<Trace> CerrarShutter();

        ResponseQuery<Trace> Reset();

        ResponseQuery<Trace> ResetLento();

        ResponseQuery<Trace> BorrarTabla();
        ResponseQuery<Trace> LeeIdDePrograma();

        ResponseQuery<Trace> LeeNumeroSerieNMD();

        ResponseQuery<Trace> ActualizaNumeroSerieNMD(string pSerie);
        ResponseQuery<Trace> HabilitaDeteccionDeBandeja();
        ResponseQuery<Trace> HabilitaValidadorDeEntrega();
        ResponseQuery<Trace> LeeIdBlock();

        ResponseQuery<Trace> SelfTest0();

        ResponseQuery<Trace> SelfTest1();

        ResponseQuery<Trace> SelfTest9();

        ResponseQuery<Trace> SelfTestA();

        ResponseQuery<Trace> BorrarNQ();

        int NumBandejas();

        ResponseQuery<Trace> LimpiarNSRollers();

        ResponseQuery<Trace> LimpiarNF(int pNF);

        ResponseObject<ResponseOffSetMount> OffSetMountToString(int mountDispenser);

        ResponseQuery<Trace> VerificaBrazoMecanico();
        ResponseQuery<Court> GetCoutAvailable();
    }
}
