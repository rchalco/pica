using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DebugLoger;
using RegisterLogger;

namespace NMDManagement
{
    
    public partial class FrmSelfTestData : Form
    {
        List<DebugLoger.LogDTO> logDTOs = new List<LogDTO>();
        Interop.Main.Cross.Domain.Orchestrator.GlobalConfigATM globalConfigATM;

        public FrmSelfTestData()
        {
            InitializeComponent();
            //Dispenser.InitDispenser();
            string pathConfigATM = ConfigurationManager.AppSettings["pathConfigATM"];
            globalConfigATM = Newtonsoft.Json.JsonConvert.DeserializeObject<Interop.Main.Cross.Domain.Orchestrator.GlobalConfigATM>(File.ReadAllText(pathConfigATM));
            LsCargarDatos();

        }
        private void LsCargarDatos()
        {
            loggerATM.PsRegisterLogger("LsCargarDatos", "Ingreso a cargar datos. SelfTestA");
            //primera pestaña
            List<SelfTestA> selfTestAs = new List<SelfTestA>();

            var LvVarRespuesta = new StatusDispenser();// ExecutorCommand.ejecutar(Comando.SelfTestA);
            if (LvVarRespuesta.state == ResponseDispensador.Exito)
            {
                try
                {
                    lblSensor.Text = "Transport Clock Sensor";
                    if (LvVarRespuesta.ResponseOriginal.Substring(1, 1) == "0")
                    {
                        lblEstado.Text = "Sensor not obstructed";
                    }
                    else
                    {
                        lblEstado.Text = " Sensor obstructed ";
                    }
                    lblCalibracion.Text = LvVarRespuesta.ResponseOriginal.Substring(2, 3) + " (0-200)";
                    //
                    for (int i = 0; i < (LvVarRespuesta.ResponseOriginal.Length / 21); i++)
                    {
                        SelfTestA selfTest = new SelfTestA();
                        string LvStrParaValidar = LvVarRespuesta.ResponseOriginal.Substring((i * 21) + 5, 3);
                        if (LvStrParaValidar != "---")
                        {
                            selfTest.denomination = LvVarRespuesta.ResponseOriginal.Substring((i * 21) + 5, 3) + ' ' + LvVarRespuesta.ResponseOriginal.Substring((i * 21) + 9, 1) + new string('0', Convert.ToInt32(LvVarRespuesta.ResponseOriginal.Substring((i * 21) + 10, 1)));
                            selfTest.offsetA = LvVarRespuesta.ResponseOriginal.Substring((i * 21) + 12, 3);
                            selfTest.gainA = LvVarRespuesta.ResponseOriginal.Substring((i * 21) + 15, 1);
                            selfTest.thicknessA = LvVarRespuesta.ResponseOriginal.Substring((i * 21) + 16, 3);

                            selfTest.offsetB = LvVarRespuesta.ResponseOriginal.Substring((i * 21) + 19, 3);
                            selfTest.gainB = LvVarRespuesta.ResponseOriginal.Substring((i * 21) + 22, 1);
                            selfTest.thicknessB = LvVarRespuesta.ResponseOriginal.Substring((i * 21) + 23, 3);

                            selfTestAs.Add(selfTest);
                        }
                    }
                    dgvSelfTestA.DataSource = selfTestAs;
                    //Segunda pestaña

                }
                catch (Exception)
                {

                    throw;
                }

            }
            //tercera pestaña
            List<SelftestFeeders> noteTransports = new List<SelftestFeeders>();
            
            if (globalConfigATM.configDispenserStatus.Tipo == "NMD50")
            {
                ////loggerATM.PsRegisterLogger("LsCargarDatos", "Tipo dispensador NMD50. SelfTest9");
                ////LvVarRespuesta = ExecutorCommand.ejecutar(Comando.SelfTest9);
                ////loggerATM.PsRegisterLogger("LsCargarDatos", "respuesta:" + LvVarRespuesta.ResponseOriginal);
                ////if (LvVarRespuesta.state == ResponseDispensador.Exito)
                ////{
                ////    SelftestFeeders noteTranport = new SelftestFeeders();
                ////    noteTranport.sensor = "Note Transport  sensor";
                ////    if (LvVarRespuesta.ResponseOriginal.Substring(1, 1) == "0")
                ////    {
                ////        noteTranport.state = "Sensor not obstructed";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(1, 1) == "1")
                ////    {
                ////        noteTranport.state = "Sensor obstructed";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(1, 1) == "2")
                ////    {
                ////        noteTranport.state = "Sensor error";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(1, 1) == "3")
                ////    {
                ////        noteTranport.state = " Sensor warning";
                ////    }
                ////    noteTranport.calibration = LvVarRespuesta.ResponseOriginal.Substring(4, 1);
                ////    noteTranport.rango = "0-7";
                ////    noteTransports.Add(noteTranport);
                ////    //*
                ////    noteTranport = new SelftestFeeders();
                ////    noteTranport.sensor = "Throat sensor";
                ////    if (LvVarRespuesta.ResponseOriginal.Substring(2, 1) == "0")
                ////    {
                ////        noteTranport.state = "Sensor not obstructed";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(2, 1) == "1")
                ////    {
                ////        noteTranport.state = "Sensor obstructed";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(2, 1) == "2")
                ////    {
                ////        noteTranport.state = "Sensor error";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(2, 1) == "3")
                ////    {
                ////        noteTranport.state = " Sensor warning";
                ////    }
                ////    noteTranport.calibration = LvVarRespuesta.ResponseOriginal.Substring(5, 1);
                ////    noteTranport.rango = "0-7";
                ////    noteTransports.Add(noteTranport);


                ////    ////
                ////    noteTranport = new SelftestFeeders();
                ////    noteTranport.sensor = "Reject Vault present sensor";
                ////    if (LvVarRespuesta.ResponseOriginal.Substring(3, 1) == "0")
                ////    {
                ////        noteTranport.state = "Cassette not present";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(3, 1) == "1")
                ////    {
                ////        noteTranport.state = " Cassette present";
                ////    }

                ////    noteTranport.calibration = "";
                ////    noteTranport.rango = "";
                ////    noteTransports.Add(noteTranport);


                ////    dtgNoteTransport.DataSource = noteTransports;
                ////}
            }

            //Cuata pestaña
            List<SelftestFeeders> stackPresenters = new List<SelftestFeeders>();

            if (globalConfigATM.configDispenserStatus.Tipo == "NMD100")
            {
                ////loggerATM.PsRegisterLogger("LsCargarDatos", "Tipo dispensador NMD100. SelfTestsactk0");
                ////LvVarRespuesta = ExecutorCommand.ejecutar(Comando.SelfTest0 );
                ////loggerATM.PsRegisterLogger("LsCargarDatos", "respuesta:" + LvVarRespuesta.ResponseOriginal);
                ////if (LvVarRespuesta.state == ResponseDispensador.Exito)
                ////{
                ////    SelftestFeeders stackPresenter = new SelftestFeeders();
                ////    stackPresenter.sensor = "BOU Exit Sensor";
                ////    if (LvVarRespuesta.ResponseOriginal.Substring(1, 1) == "0")
                ////    {
                ////        stackPresenter.state = "Sensor not obstructed";
                ////    }
                ////    else
                ////    {
                ////        stackPresenter.state = "Sensor obstructed";
                ////    }
                ////    stackPresenter.calibration = LvVarRespuesta.ResponseOriginal.Substring(10,2);
                ////    stackPresenter.rango = "(0-31)";
                ////    stackPresenters.Add(stackPresenter);
                ////    //2
                ////    stackPresenter = new SelftestFeeders();
                ////    stackPresenter.sensor = "BOU Empty sensor";
                ////    if (LvVarRespuesta.ResponseOriginal.Substring(2, 1) == "0")
                ////    {
                ////        stackPresenter.state = "Sensor not obstructed";
                ////    }
                ////    else
                ////    {
                ////        stackPresenter.state = "Sensor obstructed";
                ////    }
                ////    stackPresenter.calibration = LvVarRespuesta.ResponseOriginal.Substring(12, 2);
                ////    stackPresenter.rango = "(0-31)";
                ////    stackPresenters.Add(stackPresenter);
                ////    //3
                ////    stackPresenter = new SelftestFeeders();
                ////    stackPresenter.sensor = "BCU Front service deliver position sensor";
                ////    if (LvVarRespuesta.ResponseOriginal.Substring(3, 1) == "0")
                ////    {
                ////        stackPresenter.state = "BCU not at sensor";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(3, 1) == "1")
                ////    {
                ////        stackPresenter.state = "BCU at sensor";
                ////    }
                ////    else { stackPresenter.state = "sensor not present"; }
                ////    stackPresenter.calibration = "";
                ////    stackPresenter.rango = "";
                ////    stackPresenters.Add(stackPresenter);
                ////    //4
                ////    stackPresenter = new SelftestFeeders();
                ////    stackPresenter.sensor = "BCU Rear service deliver position sensor";
                ////    if (LvVarRespuesta.ResponseOriginal.Substring(4, 1) == "0")
                ////    {
                ////        stackPresenter.state = "BCU not at sensor";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(4, 1) == "1")
                ////    {
                ////        stackPresenter.state = "BCU at sensor";
                ////    }
                ////    else { stackPresenter.state = "sensor not present"; }
                ////    stackPresenter.calibration = "";
                ////    stackPresenter.rango = "";
                ////    stackPresenters.Add(stackPresenter);
                ////    //5
                ////    stackPresenter = new SelftestFeeders();
                ////    stackPresenter.sensor = "BCU home sensor";
                ////    if (LvVarRespuesta.ResponseOriginal.Substring(5, 1) == "0")
                ////    {
                ////        stackPresenter.state = "BCU not at sensor";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(5, 1) == "1")
                ////    {
                ////        stackPresenter.state = "BCU at sensor";
                ////    }
                ////    else { stackPresenter.state = "sensor not present"; }
                ////    stackPresenter.calibration = "";
                ////    stackPresenter.rango = "";
                ////    stackPresenters.Add(stackPresenter);
                ////    //6
                ////    stackPresenter = new SelftestFeeders();
                ////    stackPresenter.sensor = "BCU reject sensor";
                ////    if (LvVarRespuesta.ResponseOriginal.Substring(6, 1) == "0")
                ////    {
                ////        stackPresenter.state = "BCU not at sensor";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(6, 1) == "1")
                ////    {
                ////        stackPresenter.state = "BCU at sensor";
                ////    }
                ////    else { stackPresenter.state = "sensor not present"; }
                ////    stackPresenter.calibration = "";
                ////    stackPresenter.rango = "";
                ////    stackPresenters.Add(stackPresenter);
                ////    //7
                ////    stackPresenter = new SelftestFeeders();
                ////    stackPresenter.sensor = "Spare";
                ////    if (LvVarRespuesta.ResponseOriginal.Substring(7, 1) == "0")
                ////    {
                ////        stackPresenter.state = "Sensor not active";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(7, 1) == "1")
                ////    {
                ////        stackPresenter.state = "Sensor active";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(7, 1) == "2")
                ////    {
                ////        stackPresenter.state = "Sensor error";
                ////    }
                ////    else { stackPresenter.state = "sensor not present"; }
                ////    stackPresenter.calibration = "";
                ////    stackPresenter.rango = "";
                ////    stackPresenters.Add(stackPresenter);
                ////    //8
                ////    stackPresenter = new SelftestFeeders();
                ////    stackPresenter.sensor = "Shutter open sensor";
                ////    if (LvVarRespuesta.ResponseOriginal.Substring(8, 1) == "0")
                ////    {
                ////        stackPresenter.state = "Shutter not open";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(8, 1) == "1")
                ////    {
                ////        stackPresenter.state = "Shutter open";
                ////    }
                ////    else
                ////    {
                ////        stackPresenter.state = "Sensor not present";
                ////    }
                ////    stackPresenter.calibration = "";
                ////    stackPresenter.rango = "";
                ////    stackPresenters.Add(stackPresenter);
                ////    //9
                ////    stackPresenter = new SelftestFeeders();
                ////    stackPresenter.sensor = "Shutter close sensor";
                ////    if (LvVarRespuesta.ResponseOriginal.Substring(9, 1) == "0")
                ////    {
                ////        stackPresenter.state = "Shutter not closed";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(9, 1) == "1")
                ////    {
                ////        stackPresenter.state = "Shutter closed";
                ////    }
                ////    else
                ////    {
                ////        stackPresenter.state = "Sensor not present";
                ////    }
                ////    stackPresenter.calibration = "";
                ////    stackPresenter.rango = "";
                ////    stackPresenters.Add(stackPresenter);
                ////    //10
                ////    stackPresenter = new SelftestFeeders();
                ////    stackPresenter.sensor = "Note Stacker Entry sensor";
                ////    if (LvVarRespuesta.ResponseOriginal.Substring(14, 1) == "0")
                ////    {
                ////        stackPresenter.state = "Sensor not obstructed";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(14, 1) == "1")
                ////    {
                ////        stackPresenter.state = "Sensor obstructed";
                ////    }
                ////    else
                ////    {
                ////        stackPresenter.state = "Sensor not present";
                ////    }
                ////    stackPresenter.calibration = LvVarRespuesta.ResponseOriginal.Substring(15, 1);
                ////    stackPresenter.rango = "(0-7)";
                ////    stackPresenters.Add(stackPresenter);

                ////    //11
                ////    stackPresenter = new SelftestFeeders();
                ////    stackPresenter.sensor = "Reject Vault present sensor";
                ////    if (LvVarRespuesta.ResponseOriginal.Substring(16, 1) == "0")
                ////    {
                ////        stackPresenter.state = "Cassette not present";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(16, 1) == "1")
                ////    {
                ////        stackPresenter.state = "Cassette present";
                ////    }
                ////    else
                ////    {
                ////        stackPresenter.state = "Sensor not present";
                ////    }
                ////    stackPresenter.calibration = "";
                ////    stackPresenter.rango = "";
                ////    stackPresenters.Add(stackPresenter);
                ////    //12
                ////    stackPresenter = new SelftestFeeders();
                ////    stackPresenter.sensor = "RV Shutter sensor Closed position";
                ////    if (LvVarRespuesta.ResponseOriginal.Substring(17, 1) == "0")
                ////    {
                ////        stackPresenter.state = "Sensor not obstructed";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(17, 1) == "1")
                ////    {
                ////        stackPresenter.state = "Sensor obstructed";
                ////    }
                ////    else
                ////    {
                ////        stackPresenter.state = "Sensor not present";
                ////    }
                ////    stackPresenter.calibration = "";
                ////    stackPresenter.rango = "";
                ////    stackPresenters.Add(stackPresenter);
                ////    //13
                ////    stackPresenter = new SelftestFeeders();
                ////    stackPresenter.sensor = "RV Shutter sensor Single accept pos";
                ////    if (LvVarRespuesta.ResponseOriginal.Substring(18, 1) == "0")
                ////    {
                ////        stackPresenter.state = "Sensor not obstructed";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(18, 1) == "1")
                ////    {
                ////        stackPresenter.state = "Sensor obstructed";
                ////    }
                ////    else
                ////    {
                ////        stackPresenter.state = "Sensor not present";
                ////    }
                ////    stackPresenter.calibration = "";
                ////    stackPresenter.rango = "";
                ////    stackPresenters.Add(stackPresenter);
                ////    //14
                ////    stackPresenter = new SelftestFeeders();
                ////    stackPresenter.sensor = "RV Shutter sensor Stack accept position";
                ////    if (LvVarRespuesta.ResponseOriginal.Substring(19, 1) == "0")
                ////    {
                ////        stackPresenter.state = "Sensor not obstructed";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(19, 1) == "1")
                ////    {
                ////        stackPresenter.state = "Sensor obstructed";
                ////    }
                ////    else
                ////    {
                ////        stackPresenter.state = "Sensor not present";
                ////    }
                ////    stackPresenter.calibration = "";
                ////    stackPresenter.rango = "";
                ////    stackPresenters.Add(stackPresenter);
                ////    //15
                ////    stackPresenter = new SelftestFeeders();
                ////    stackPresenter.sensor = "RV Shutter sensor Stack retract pos";
                ////    if (LvVarRespuesta.ResponseOriginal.Substring(20, 1) == "0")
                ////    {
                ////        stackPresenter.state = "Sensor not obstructed";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(20, 1) == "1")
                ////    {
                ////        stackPresenter.state = "Sensor obstructed";
                ////    }
                ////    else
                ////    {
                ////        stackPresenter.state = "Sensor not present";
                ////    }
                ////    stackPresenter.calibration = "";
                ////    stackPresenter.rango = "";
                ////    stackPresenters.Add(stackPresenter);
                ////    //16
                ////    stackPresenter = new SelftestFeeders();
                ////    stackPresenter.sensor = "Cassette lid/lock sensor";
                ////    if (LvVarRespuesta.ResponseOriginal.Substring(21, 1) == "0")
                ////    {
                ////        stackPresenter.state = "Lid closed and locked";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(21, 1) == "1")
                ////    {
                ////        stackPresenter.state = "Lid not closed";
                ////    }
                ////    else
                ////    {
                ////        stackPresenter.state = "Sensor not present";
                ////    }
                ////    stackPresenter.calibration = "";
                ////    stackPresenter.rango = "";
                ////    stackPresenters.Add(stackPresenter);
                ////    //17
                ////    stackPresenter = new SelftestFeeders();
                ////    stackPresenter.sensor = "Lid solenoid status sensor";
                ////    if (LvVarRespuesta.ResponseOriginal.Substring(22, 1) == "0")
                ////    {
                ////        stackPresenter.state = "Solenoid inactive";
                ////    }
                ////    else if (LvVarRespuesta.ResponseOriginal.Substring(22, 1) == "1")
                ////    {
                ////        stackPresenter.state = "Solenoid active";
                ////    }
                ////    else
                ////    {
                ////        stackPresenter.state = "Sensor not present";
                ////    }
                ////    stackPresenter.calibration = "";
                ////    stackPresenter.rango = "";
                ////    stackPresenters.Add(stackPresenter);
                ////    dgvSctackPresenter.DataSource = stackPresenters;
                ////}
            }
        }
        private string LfStrObtieneDenominacion(string pCadDenominacion)
        {
            return pCadDenominacion.Substring(1, 1) + new string('0', Convert.ToInt32(pCadDenominacion.Substring(2, 1)));
        }

        private void btnCerrarSelf_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void LsCargaDatosDeBandeja(int pBandeja)
        {
            //logDTOs.Add(RegisterInDB.AddLogDTO(System.DateTime.Now, "", eTypes.secuential, "LsCargaDatosDeBandeja", "Ingreso a funcion cargar datos debandejas " + pBandeja.ToString()));

            List<SelftestFeeders> selftestFeeders = new List<SelftestFeeders>();
            
            //var LvVarRespuesta = ExecutorCommand.ejecutar(Comando.SelfTest1, pBandeja.ToString());
            //if (LvVarRespuesta.state == ResponseDispensador.Exito)
            //{
            //    SelftestFeeders selftestFeeder = new SelftestFeeders();
            //    selftestFeeder.sensor = "Exit sensor";
            //    if (LvVarRespuesta.ResponseOriginal.Substring(1, 1) == "0")
            //    {
            //        selftestFeeder.state = "Sensor not obstructed";
            //    }
            //    else if (LvVarRespuesta.ResponseOriginal.Substring(1, 1) == "1")
            //    {
            //        selftestFeeder.state = "Sensor obstructed ";
            //    }
            //    else if (LvVarRespuesta.ResponseOriginal.Substring(1, 1) == "2")
            //    {
            //        selftestFeeder.state = "Sensor Error";
            //    }
            //    else if (LvVarRespuesta.ResponseOriginal.Substring(1, 1) == "3")
            //    {
            //        selftestFeeder.state = "Sensor Warning";
            //    }
            //    selftestFeeder.calibration = LvVarRespuesta.ResponseOriginal.Substring(12, 1);
            //    selftestFeeder.rango = "0-7";
            //    selftestFeeders.Add(selftestFeeder);

            //    //logDTOs.Add(RegisterInDB.AddLogDTO(System.DateTime.Now, "", eTypes.secuential, "LsCargaDatosDeBandeja", "Empty sensor" ));
                
            //    ///
            //    selftestFeeder = new SelftestFeeders();
            //    selftestFeeder.sensor = "Empty senso";
            //    if (LvVarRespuesta.ResponseOriginal.Substring(2, 1) == "0")
            //    {
            //        selftestFeeder.state = "Sensor not obstructed";
            //    }
            //    else if (LvVarRespuesta.ResponseOriginal.Substring(2, 1) == "1")
            //    {
            //        selftestFeeder.state = "Sensor obstructed ";
            //    }
            //    else if (LvVarRespuesta.ResponseOriginal.Substring(2, 1) == "2")
            //    {
            //        selftestFeeder.state = "Sensor Error";
            //    }
            //    else if (LvVarRespuesta.ResponseOriginal.Substring(2, 1) == "3")
            //    {
            //        selftestFeeder.state = "Sensor Warning";
            //    }
            //    selftestFeeder.calibration = LvVarRespuesta.ResponseOriginal.Substring(13, 1);
            //    selftestFeeder.rango = "0-7";
            //    selftestFeeders.Add(selftestFeeder);

            //    //logDTOs.Add(RegisterInDB.AddLogDTO(System.DateTime.Now, "", eTypes.secuential, "LsCargaDatosDeBandeja", "Pressure sensor"));
            //    ///
            //    selftestFeeder = new SelftestFeeders();
            //    selftestFeeder.sensor = "Pressure sensor";
            //    if (LvVarRespuesta.ResponseOriginal.Substring(3, 1) == "0")
            //    {
            //        selftestFeeder.state = "Low feed pressure";
            //    }
            //    else if (LvVarRespuesta.ResponseOriginal.Substring(3, 1) == "1")
            //    {
            //        selftestFeeder.state = "Normal feed pressure";
            //    }
            //    else if (LvVarRespuesta.ResponseOriginal.Substring(3, 1) == "2")
            //    {
            //        selftestFeeder.state = "Sensor Error";
            //    }
            //    else if (LvVarRespuesta.ResponseOriginal.Substring(3, 1) == "3")
            //    {
            //        selftestFeeder.state = "Sensor Warning";
            //    }
            //    selftestFeeder.calibration = LvVarRespuesta.ResponseOriginal.Substring(14, 1);
            //    selftestFeeder.rango = "0-3";
            //    selftestFeeders.Add(selftestFeeder);

            //    //logDTOs.Add(RegisterInDB.AddLogDTO(System.DateTime.Now, "", eTypes.secuential, "LsCargaDatosDeBandeja", "NC Shutter sensor 1"));
            //    ///
            //    selftestFeeder = new SelftestFeeders();
            //    selftestFeeder.sensor = "NC Shutter sensor 1";
            //    if (LvVarRespuesta.ResponseOriginal.Substring(4, 1) == "0")
            //    {
            //        selftestFeeder.state = " Sensor not obstructed";
            //    }
            //    else if (LvVarRespuesta.ResponseOriginal.Substring(4, 1) == "1")
            //    {
            //        selftestFeeder.state = "Sensor obstructed ";
            //    }
                
            //    selftestFeeder.calibration = LvVarRespuesta.ResponseOriginal.Substring(15, 1);
            //    selftestFeeder.rango = "0-3";
            //    selftestFeeders.Add(selftestFeeder);

            //    //logDTOs.Add(RegisterInDB.AddLogDTO(System.DateTime.Now, "", eTypes.secuential, "LsCargaDatosDeBandeja", "NC Shutter sensor 2"));
            //    ///
            //    selftestFeeder = new SelftestFeeders();
            //    selftestFeeder.sensor = "NC Shutter sensor 2";
            //    if (LvVarRespuesta.ResponseOriginal.Substring(5, 1) == "0")
            //    {
            //        selftestFeeder.state = " Sensor not obstructed";
            //    }
            //    else if (LvVarRespuesta.ResponseOriginal.Substring(5, 1) == "1")
            //    {
            //        selftestFeeder.state = "Sensor obstructed ";
            //    }

            //    selftestFeeder.calibration = "";
            //    selftestFeeder.rango = "";
            //    selftestFeeders.Add(selftestFeeder);

            //    //logDTOs.Add(RegisterInDB.AddLogDTO(System.DateTime.Now, "", eTypes.secuential, "LsCargaDatosDeBandeja", "NC Shutter sensor 3"));
            //    ///
            //    selftestFeeder = new SelftestFeeders();
            //    selftestFeeder.sensor = "NC Shutter sensor 3";
            //    if (LvVarRespuesta.ResponseOriginal.Substring(6, 1) == "0")
            //    {
            //        selftestFeeder.state = " Sensor not obstructed";
            //    }
            //    else if (LvVarRespuesta.ResponseOriginal.Substring(6, 1) == "1")
            //    {
            //        selftestFeeder.state = "Sensor obstructed ";
            //    }

            //    selftestFeeder.calibration = "";
            //    selftestFeeder.rango = "";
            //    selftestFeeders.Add(selftestFeeder);

            //    //logDTOs.Add(RegisterInDB.AddLogDTO(System.DateTime.Now, "", eTypes.secuential, "LsCargaDatosDeBandeja", "Cassette present sensor"));
            //    ///
            //    selftestFeeder = new SelftestFeeders();
            //    selftestFeeder.sensor = "Cassette present sensor";
            //    if (LvVarRespuesta.ResponseOriginal.Substring(7, 1) == "0")
            //    {
            //        selftestFeeder.state = "Cassette not present";
            //    }
            //    else if (LvVarRespuesta.ResponseOriginal.Substring(7, 1) == "1")
            //    {
            //        selftestFeeder.state = "Cassette present";
            //    }

            //    selftestFeeder.calibration = "";
            //    selftestFeeder.rango = "";
            //    selftestFeeders.Add(selftestFeeder);

            //    //logDTOs.Add(RegisterInDB.AddLogDTO(System.DateTime.Now, "", eTypes.secuential, "LsCargaDatosDeBandeja", "Cassette low level sensor"));
            //    ///
            //    selftestFeeder = new SelftestFeeders();
            //    selftestFeeder.sensor = "Cassette low level sensor";
            //    if (LvVarRespuesta.ResponseOriginal.Substring(8, 1) == "0")
            //    {
            //        selftestFeeder.state = "Low level";
            //    }
            //    else if (LvVarRespuesta.ResponseOriginal.Substring(8, 1) == "1")
            //    {
            //        selftestFeeder.state = "Not low level";
            //    }

            //    selftestFeeder.calibration = "";
            //    selftestFeeder.rango = "";
            //    selftestFeeders.Add(selftestFeeder);

            //    //logDTOs.Add(RegisterInDB.AddLogDTO(System.DateTime.Now, "", eTypes.secuential, "LsCargaDatosDeBandeja", "Cassette lid/lock sensor"));
            //    ///
            //    selftestFeeder = new SelftestFeeders();
            //    selftestFeeder.sensor = "Cassette lid/lock sensor";
            //    if (LvVarRespuesta.ResponseOriginal.Substring(9, 1) == "0")
            //    {
            //        selftestFeeder.state = " Lid closed and locked";
            //    }
            //    else if (LvVarRespuesta.ResponseOriginal.Substring(9, 1) == "1")
            //    {
            //        selftestFeeder.state = "Lid not close";
            //    }
            //    else if (LvVarRespuesta.ResponseOriginal.Substring(9, 1) == "-")
            //    {
            //        selftestFeeder.state = "Sensor not present";
            //    }

            //    selftestFeeder.calibration = "";
            //    selftestFeeder.rango = "";
            //    selftestFeeders.Add(selftestFeeder);

            //    //logDTOs.Add(RegisterInDB.AddLogDTO(System.DateTime.Now, "", eTypes.secuential, "LsCargaDatosDeBandeja", "NC Shutter solenoid status sensor"));

            //    ///
            //    selftestFeeder = new SelftestFeeders();
            //    selftestFeeder.sensor = "NC Shutter solenoid status sensor";
            //    if (LvVarRespuesta.ResponseOriginal.Substring(10, 1) == "0")
            //    {
            //        selftestFeeder.state = "Solenoide inactive";
            //    }
            //    else if (LvVarRespuesta.ResponseOriginal.Substring(10, 1) == "1")
            //    {
            //        selftestFeeder.state = "Solenoide activated ";
            //    }
            //    else if (LvVarRespuesta.ResponseOriginal.Substring(10, 1) == "2")
            //    {
            //        selftestFeeder.state = "Solenoide sensor error";
            //    }

            //    selftestFeeder.calibration = "";
            //    selftestFeeder.rango = "";
            //    selftestFeeders.Add(selftestFeeder);

            //    //logDTOs.Add(RegisterInDB.AddLogDTO(System.DateTime.Now, "", eTypes.secuential, "LsCargaDatosDeBandeja", "NC Lid solenoid status sensor"));
            //    ///
            //    selftestFeeder = new SelftestFeeders();
            //    selftestFeeder.sensor = "NC Lid solenoid status sensor";
            //    if (LvVarRespuesta.ResponseOriginal.Substring(11, 1) == "0")
            //    {
            //        selftestFeeder.state = "Solenoide inactive";
            //    }
            //    else if (LvVarRespuesta.ResponseOriginal.Substring(11, 1) == "1")
            //    {
            //        selftestFeeder.state = "Solenoide activated ";
            //    }

            //    //logDTOs.Add(RegisterInDB.AddLogDTO(System.DateTime.Now, "", eTypes.secuential, "LsCargaDatosDeBandeja", "Finalizar llenado de datos"));
            //    selftestFeeder.calibration = "";
            //    selftestFeeder.rango = "";
            //    selftestFeeders.Add(selftestFeeder);
            //    //logDTOs.Add(RegisterInDB.AddLogDTO(System.DateTime.Now, "", eTypes.secuential, "LsCargaDatosDeBandeja", "datos a equiquetas"));
            //    /////////
            //    lblTamaño.Text = LvVarRespuesta.ResponseOriginal.Substring(16, 3) +  " x " + LvVarRespuesta.ResponseOriginal.Substring(19, 3);
            //    //lblDenominacion.Text = LvVarRespuesta.ResponseOriginal.Substring(22, 3) +' ' + LvVarRespuesta.ResponseOriginal.Substring(26, 1) + new string('0', Convert.ToInt32(LvVarRespuesta.ResponseOriginal.Substring(27, 1)));
            //    if (IsNumeric(LvVarRespuesta.ResponseOriginal.Substring(27, 1)))
            //    {
            //        lblDenominacion.Text = LvVarRespuesta.ResponseOriginal.Substring(22, 3) + ' ' + LvVarRespuesta.ResponseOriginal.Substring(26, 1) + new string('0', Convert.ToInt32(LvVarRespuesta.ResponseOriginal.Substring(27, 1)));
            //    }
            //    else
            //    {
            //        lblDenominacion.Text = LvVarRespuesta.ResponseOriginal.Substring(22, 3) + ' ' + LvVarRespuesta.ResponseOriginal.Substring(26, 1) + new string('0', 1);
            //    }
            //    //lblDenominacion.Text = LvVarRespuesta.ResponseOriginal.Substring(22, 3) + ' ' + LvVarRespuesta.ResponseOriginal.Substring(26, 1) + new string('0' , LvVarRespuesta.ResponseOriginal.Substring(27, 1));

            //    ///
            //    //logDTOs.Add(RegisterInDB.AddLogDTO(System.DateTime.Now, "", eTypes.secuential, "LsCargaDatosDeBandeja", "llenar grid"));
            //    dgvFeeders.DataSource = selftestFeeders;
            //    //logDTOs.Add(RegisterInDB.AddLogDTO(System.DateTime.Now, "", eTypes.secuential, "LsCargaDatosDeBandeja", "Salir"));
            //}

            //bool IsNumeric(string value)
            //{
            //    bool isNumeric = true;
            //    char[] digits = "0123456789".ToCharArray();
            //    char[] letters = value.ToCharArray();
            //    for (int k = 0; k < letters.Length; k++)
            //    {
            //        for (int i = 0; i < digits.Length; i++)
            //        {
            //            if (letters[k] != digits[i])
            //            {
            //                isNumeric = false;
            //                break;
            //            }
            //        }
            //    }
            //    return isNumeric;
            //}
        }
        public class SelfTestA
        {
            public string denomination { get; set; }
            public string offsetA { get; set; }
            public string gainA { get; set; }
            public string thicknessA { get; set; }
            public string offsetB { get; set; }
            public string gainB { get; set; }
            public string thicknessB { get; set; }
        }
        public class SelftestFeeders
        {
            public string sensor { get; set; }
            public string state { get; set; }
            public string calibration { get; set; }
            public string rango { get; set; }
        }

        private void btnNF1_Click(object sender, EventArgs e)
        {
            LsCargaDatosDeBandeja(1);

        }

        private void btnNF2_Click(object sender, EventArgs e)
        {
            LsCargaDatosDeBandeja(2);
        }

        private void btnNF3_Click(object sender, EventArgs e)
        {
            
            LsCargaDatosDeBandeja(3);
        }

        private void btnNF4_Click(object sender, EventArgs e)
        {
            LsCargaDatosDeBandeja(4);
        }
    }
}

