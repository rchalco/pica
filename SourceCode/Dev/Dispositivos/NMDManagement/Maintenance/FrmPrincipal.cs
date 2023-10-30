using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;
using RegisterLogger;
using System.Configuration;
using RuntimeDispensador.Core;

namespace NMDManagement
{
    public partial class FrmPrincipal : Form
    {
        Interop.Main.Cross.Domain.Orchestrator.GlobalConfigATM globalConfigATM;
        string token=string.Empty;
        Int64 idAccess = 0;
        public FrmPrincipal(UserData userData, string pToken)
        {

            InitializeComponent();
            lblCargp.Text = userData.UserRole;
            lblusuario.Text = userData.UserPersonName;
            
            //UpdateConfigCOM();
            //Dispenser.InitDispenser();
            //ProviderComunication.GetCurrentInstace();
            string pathConfigATM = ConfigurationManager.AppSettings["pathConfigATM"];
            //globalConfigATM = Newtonsoft.Json.JsonConvert.DeserializeObject<Interop.Main.Cross.Domain.Orchestrator.GlobalConfigATM>(File.ReadAllText(pathConfigATM));
            CargaArbol();
            this.token = pToken;
            this.idAccess = userData.IdATMAccess;
        }

        private void CargaArbol()
        {
            cMenu cmenu =new cMenu();
            List<Opcion> menu = cmenu.CargaMenuTI();
            tvwMenu.BeginUpdate();
            tvwMenu.Nodes.Clear();
            
            int indiceP = 0;
            //TreeNode treeNode = new TreeNode(Text = "Principal",);
            foreach (Opcion item in menu)
            {
                if (item.nivel == 1)
                {
                    tvwMenu.Nodes.Add(item.Name);
                    indiceP++;
                }
                else
                {
                    tvwMenu.Nodes[indiceP-1].Nodes.Add(item.Name);
                }
            }
            tvwMenu.EndUpdate();
        }
        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (MenuVertical.Width == 250)
            {
                MenuVertical.Width = 70;
            }
            else
                MenuVertical.Width = 250;
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconmaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            iconrestaurar.Visible = true;
            iconmaximizar.Visible = false;
        }

        private void iconrestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            iconrestaurar.Visible = false;
            iconmaximizar.Visible = true;
        }

        private void iconminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void AbrirFormEnPanel(object Formhijo)
        {
            if (this.pnlPrincipal.Controls.Count > 0)
                this.pnlPrincipal.Controls.RemoveAt(0);
            Form fh = Formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.pnlPrincipal.Controls.Add(fh);
            this.pnlPrincipal.Tag = fh;
            fh.Show();
        }

        private void btnprod_Click(object sender, EventArgs e)
        {
            //AbrirFormEnPanel(new Productos());
            //if (SubmenuConexion.Visible)
            //{ SubmenuConexion.Visible = false; }
            //else
            //{ SubmenuConexion.Visible = true; }
        }

        private void btnlogoInicio_Click(object sender, EventArgs e)
        {
            //AbrirFormEnPanel(new InicioResumen());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnlogoInicio_Click(null, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (subMenuEstadistica.Visible)
            //{ subMenuEstadistica.Visible = false; }
            //else
            //{ subMenuEstadistica.Visible = true; }
        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {
                this.LsGrabarLog();
            }
            catch (Exception ex)
            {
                throw;
            }
            this.Close();
            Application.Exit();
        }
        private void LsGrabarLog()
        {
            //ConfigBD configBd = ReadConfig.GetConfigBD();
            //try
            //{

            //    new RegisterLog("data source = " + configBd.SQLConsolidado + "; initial catalog = CAIConsolidado; user id = TransCAI; password =; connection timeout = 15").RegisterLogBD("NMDManagement", this.lblusuario.Text, "", this.logDTOs);
            //    this.LvBolLogGrabado = true;
            //    MessageBox.Show("Log guardado", "Aviso", MessageBoxButtons.OK);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error al grabar log. Error:" + ex.Message  , "Aviso", MessageBoxButtons.OK,MessageBoxIcon.Error);
            //    this.LvBolLogGrabado = false;
            //    throw;
            //}
        }

        private void tvwMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text == "Liberar Bandeja")
            {
                this.LsLlamadaAFuncion(1);
            }
            else if (e.Node.Text == "Cerrar Bandejas")
            {
                this.LsLlamadaAFuncion(2);
            }
            else if (e.Node.Text == "Abrir y Leer Id Bandejas")
            {
                this.LsLlamadaAFuncion(3);
            }
            else if (e.Node.Text == "Reset")
                this.LsLlamadaAFuncion(10);
            else if (e.Node.Text == "Reset Lento")
                this.LsLlamadaAFuncion(11);
            else if (e.Node.Text == "Read Información ID")
                this.LsLlamadaAFuncion(13);
            else if (e.Node.Text == "Entrega")
            {
                this.Refresh();
                FrmCashDelivery frm = new FrmCashDelivery();
                frm.Owner = this;
                frm.ShowDialog(this);
            }
            else if (e.Node.Text == "Tabla de Estados")
                this.LsLlamadaAFuncion(4);
            else if (e.Node.Text == "Contadores")
                this.LsLlamadaAFuncion(5);
            else if (e.Node.Text == "Verificar Estado NMD")
            {
                FrmNMDStatus frm = new FrmNMDStatus();
                frm.TopMost = true;
                frm.Owner = (Form)this;
                frm.ShowDialog(this);
            }
            else if (e.Node.Text == "Datos de Autocomprobación")
            {
                FrmSelfTestData frmSelfTestData = new FrmSelfTestData();
                frmSelfTestData.TopMost = true;
                frmSelfTestData.Owner = (Form)this;
                int num = (int)frmSelfTestData.ShowDialog((IWin32Window)this);
            }
            else if (e.Node.Text == "Abrir Shutter")
                this.LsLlamadaAFuncion(7);
            else if (e.Node.Text == "Cerrar Shutter")
                this.LsLlamadaAFuncion(8);
            else if (e.Node.Text == "Emulador Shutter")
            {
                if (globalConfigATM.configDispenserStatus.Tipo == "NMD100")
                {
                    this.Refresh();
                    FrmEmulatorShutter frm = new FrmEmulatorShutter();
                    frm.Show(Owner);
                }
            }
            else if (e.Node.Text == "Limpiaza NT y NFs")
            {
                this.Refresh();
                Maintenance.frmCleanNSNF frmClean = new Maintenance.frmCleanNSNF();
                frmClean.TopMost = true;
                frmClean.Owner = (Form)this;
                frmClean.ShowDialog(this);
            }
            else if (e.Node.Text == "Bandejas")
            {
                FrmCassettes frmCassettes = new FrmCassettes();
                new FrmCassettes().Show();
                frmCassettes.TopMost = true;
                frmCassettes.Owner = (Form)this;
                frmCassettes.ShowDialog(this);
            }
            else if (e.Node.Text == "Serie NMD")
                this.LsLlamadaAFuncion(14);
            else if (e.Node.Text == "Habilitar Detección de Bandejas")
                this.LsLlamadaAFuncion(16);
            else if (e.Node.Text == "Habilitar RESET Lento")
                this.LsLlamadaAFuncion(17);
            else if (e.Node.Text == "Configurar ATM")
            {
                Administration.frmConfigWizard frmConfigATM = new Administration.frmConfigWizard( token );
                frmConfigATM.TopMost = true;
                frmConfigATM.Owner = (Form)this;
                frmConfigATM.ShowDialog(this);
            }
            else if(e.Node.Text=="Salir")
            {
                //this.Close();
                Application.Exit();
            }
        }


        [STAThread]
        private void LsLlamadaAFuncion(int qProceso)
        {
            //ProviderComunication.ConfigCOM tipoDispensador = ProviderComunication.Configuration;
            //int nb = tipoDispensador.NumBandejas;
            string tipoDispensador = "NMD100";
            services servicios = new services();
            loggerATM.PsRegisterLogger("CallFunctionNMD", "Ingreso a funcion de llamada a clase de dispensador para el proceso " + qProceso.ToString());
            switch (qProceso)
            {
                case 1:
                    loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para cerrar bandejas");
                    //StatusDispenser statusDispenser1 = ExecutorCommand.ejecutar(Comando.CerrarBandejas);
                    //if (statusDispenser1.state == ResponseDispensador.Exito)
                    //{
                    //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + statusDispenser1.ResponseOriginal);
                    //    MessageBox.Show("Bandejas liberadas.\n" + statusDispenser1.ResponseOriginal, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //    break;
                    //}
                    //loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + statusDispenser1.Description);
                    //MessageBox.Show("No se liberaron bandejas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    ResponseReleaseCassette responseReleaseCassette = servicios.ReleaseCassette();
                    if (responseReleaseCassette.LiberarBandejaResult.State == ResponseType.Success)
                    {
                        loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + responseReleaseCassette.LiberarBandejaResult.ListEntities[0].Result.ResponseOriginal);
                        MessageBox.Show("Bandejas liberadas.\n" + responseReleaseCassette.LiberarBandejaResult.ListEntities[0].Result.ResponseOriginal, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + responseReleaseCassette.LiberarBandejaResult.Message);
                    MessageBox.Show("No se liberaron bandejas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                case 2:
                    loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para abrir bandejas");
                    //StatusDispenser statusDispenser2 = ExecutorCommand.ejecutar(Comando.AbrirBandejas);
                    //if (statusDispenser2.state == ResponseDispensador.Exito)
                    //{
                    //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + statusDispenser2.ResponseOriginal);
                    //    MessageBox.Show("Bandejas cerradas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //    break;
                    //}
                    //loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + statusDispenser2.Description);
                    //MessageBox.Show("No se cerraron bandejas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    ResponseCloseCassette responseCloseCassette = servicios.CloseCassette();
                    if (responseCloseCassette.AsegurarBandejaResult.State == ResponseType.Success || responseCloseCassette.AsegurarBandejaResult.State == ResponseType.Warning)
                    {
                        loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + responseCloseCassette.AsegurarBandejaResult.ListEntities[0].Result.ResponseOriginal);
                        MessageBox.Show("Bandejas cerradas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + responseCloseCassette.AsegurarBandejaResult.ListEntities[0].Result.ResponseOriginal);
                    MessageBox.Show("No se cerraron bandejas\nError:" + responseCloseCassette.AsegurarBandejaResult.ListEntities[0].Result.ResponseOriginal, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                case 3:
                    List<cIdBandejas> cIdBandejasList = new List<cIdBandejas>();
                    loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para leer ID de bandejas");

                    ResponseReadIdCassette responseReadIdCassette = servicios.ReadIdCassette();
                    if (responseReadIdCassette == null)
                    {
                        loggerATM.PsRegisterLogger("CallFunctionNMD", "respueta vacia de dispensador");
                        break;
                    }
                    if (responseReadIdCassette.VerificaBanejasResult.State == ResponseType.Success || responseReadIdCassette.VerificaBanejasResult.State == ResponseType.Warning)
                    {
                        loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + responseReadIdCassette.VerificaBanejasResult.ListEntities[1].Result.ResponseOriginal);
                        int numB = 0;
                        foreach (CassetteStatus cassette in responseReadIdCassette.VerificaBanejasResult.ListEntities[1].Result.Cassettes)
                        {
                            cIdBandejas cIdBandejas = new cIdBandejas();
                            cIdBandejas.numero = numB;
                            int values = 0;
                            foreach (CompositePartResponse keyValuePair in cassette.PartsResponse)
                            {
                                switch (values)
                                {
                                    case 1:
                                        cIdBandejas.Estado = keyValuePair.Value.code;
                                        cIdBandejas.DesEstado = keyValuePair.Value.description;
                                        break;
                                    case 2:
                                        cIdBandejas.IdBandeja = keyValuePair.Value.code;
                                        break;
                                }
                                ++values;
                            }
                            ++numB;
                            cIdBandejasList.Add(cIdBandejas);
                        }


                        this.dbgBandejas.DataSource = (object)cIdBandejasList;
                        this.pnldBandeja.Visible = true;
                    }
                    else
                        loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + responseReadIdCassette.VerificaBanejasResult.Message);
                    break;
                case 4:
                    List<cStatusTable> cStatusTableList1 = new List<cStatusTable>();
                    loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para leer estado de tablas");

                    ResponseStateTable responseStateTable = servicios.StateTable();
                    if (responseStateTable.TablaDeEstadoResult.State == ResponseType.Success)
                    {
                        loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + responseStateTable.TablaDeEstadoResult.ListEntities[0].Result.ResponseOriginal);
                        string str = responseStateTable.TablaDeEstadoResult.ListEntities[0].Result.ResponseOriginal.Substring(2, responseStateTable.TablaDeEstadoResult.ListEntities[0].Result.ResponseOriginal.Length - 2);
                        for (int index = 0; index <= str.Length / 5 - 1; ++index)
                        {
                            cStatusTable cStatusTable = new cStatusTable();
                            cStatusTable.status = str.Substring(index * 5, 2);
                            cStatusTable.quantity = Convert.ToInt32(str.Substring(index * 5 + 2, 3));
                            string LvStrState = Convert.ToChar(Convert.ToInt32(cStatusTable.status, 16)).ToString();

                            int decValue = int.Parse(cStatusTable.status, System.Globalization.NumberStyles.HexNumber);
                            char cValue = Convert.ToChar(decValue);
                            cStatusTable.statusDescription = ReadAdapterResponse.GetConfigResponse(globalConfigATM.configDispenserStatus.Tipo, cValue.ToString()).concreteResponse;
                            cStatusTableList1.Add(cStatusTable);
                        }
                        this.tdgEstadoTabla.DataSource = (object)cStatusTableList1;
                        this.pnlEstadoTable.Visible = true;
                        break;
                    }
                    //StatusDispenser statusDispenser4 = ExecutorCommand.ejecutar(Comando.StatusCodeTable);
                    //if (statusDispenser4.state == ResponseDispensador.Exito)
                    //{
                    //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + statusDispenser4.ResponseOriginal);
                    //    string str = statusDispenser4.ResponseOriginal.Substring(2, statusDispenser4.ResponseOriginal.Length - 2);
                    //    for (int index = 0; index <= str.Length / 5 - 1; ++index)
                    //    {
                    //        cStatusTable cStatusTable = new cStatusTable();
                    //        cStatusTable.status = str.Substring(index * 5, 2);
                    //        cStatusTable.quantity = Convert.ToInt32(str.Substring(index * 5 + 2, 3));
                    //        string LvStrState = Convert.ToChar(Convert.ToInt32(cStatusTable.status, 16)).ToString();
                    //        cStatusTable.statusDescription = AdapterResponse.LexiconResponse.Any<ResponseComplex>((Func<ResponseComplex, bool>)(x => x.code.Equals(LvStrState))) ? AdapterResponse.LexiconResponse.First<ResponseComplex>((Func<ResponseComplex, bool>)(x => x.code.Equals(LvStrState))).description : "";
                    //        cStatusTableList1.Add(cStatusTable);
                    //    }
                    //    this.tdgEstadoTabla.DataSource = (object)cStatusTableList1;
                    //    this.pnlEstadoTable.Visible = true;
                    //    break;
                    //}
                    loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + responseStateTable.TablaDeEstadoResult.Message);
                    MessageBox.Show("No se cerraron bandejas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                case 5:
                    try
                    {
                        List<cStatusTable> cStatusTableList2 = new List<cStatusTable>();
                        loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para leer CounterNote303");
                        //StatusDispenser statusDispenser5 = ExecutorCommand.ejecutar(Comando.CounterNotes303);
                        ResponseStateTableGrl stateTableGrl = servicios.StateTableGnl();
                        if (stateTableGrl.TablaDeEstadosGrlResult.State == ResponseType.Success )
                        {
                            int index = 0;
                            foreach(Trace  trace in stateTableGrl.TablaDeEstadosGrlResult.ListEntities)
                            {
                                loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador("+ trace.IdCommand  +"). Respuesta:" + trace.Result.ResponseOriginal );
                                switch(index)
                                {
                                    case 0:
                                        cStatusTableList2.Add(new cStatusTable()
                                        {
                                            status = "303",
                                            statusDescription = "Total number of notes delivered",
                                            quantity = Convert.ToInt32(trace.Result.ResponseOriginal.Substring(2, 10))

                                        });
                                        break;
                                    case 1://talNotesRejected':
                                        cStatusTableList2.Add(new cStatusTable()
                                        {
                                            status = "304",
                                            statusDescription = "Total number of notes rejected",
                                            quantity = Convert.ToInt32(trace.Result.ResponseOriginal.Substring(2, 10))

                                        });
                                        break;
                                    case 2://talNotesRejected':
                                        cStatusTableList2.Add(new cStatusTable()
                                        {
                                            status = "305",
                                            statusDescription = "Total number of bundle rejects",
                                            quantity = Convert.ToInt32(trace.Result.ResponseOriginal.Substring(2, 10))

                                        });
                                        break;
                                    case 3://talNotesRejected':
                                        cStatusTableList2.Add(new cStatusTable()
                                        {
                                            status = "308",
                                            statusDescription = "Total number of notes singel rejected",
                                            quantity = Convert.ToInt32(trace.Result.ResponseOriginal.Substring(2, 10))

                                        });
                                        break;
                                    case 4://talNotesRejected':
                                        cStatusTableList2.Add(new cStatusTable()
                                        {
                                            status = "309",
                                            statusDescription = "Total number of notes bundle rejected",
                                            quantity = Convert.ToInt32(trace.Result.ResponseOriginal.Substring(2, 10))

                                        });
                                        break;
                                    case 5://talNotesRejected':
                                        cStatusTableList2.Add(new cStatusTable()
                                        {
                                            status = "320",
                                            statusDescription = "Total number of transaction bundles",
                                            quantity = Convert.ToInt32(trace.Result.ResponseOriginal.Substring(2, 10))

                                        });
                                        break;
                                    case 6://talNotesRejected':
                                        cStatusTableList2.Add(new cStatusTable()
                                        {
                                            status = "390",
                                            statusDescription = "Total number of bundle retracts",
                                            quantity = Convert.ToInt32(trace.Result.ResponseOriginal.Substring(2, 10))

                                        });
                                        break;
                                    case 7://talNotesRejected':
                                        cStatusTableList2.Add(new cStatusTable()
                                        {
                                            status = "318",
                                            statusDescription = "Total number of notes delivered in HPF mode",
                                            quantity = Convert.ToInt32(trace.Result.ResponseOriginal.Substring(2, 10))

                                        });
                                        break;
                                    case 8://talNotesRejected':
                                        cStatusTableList2.Add(new cStatusTable()
                                        {
                                            status = "319",
                                            statusDescription = "Total number of notes rejected in HPF mode",
                                            quantity = Convert.ToInt32(trace.Result.ResponseOriginal.Substring(2, 10))

                                        });
                                        break;
                                    case 9://talNotesRejected':
                                        cStatusTableList2.Add(new cStatusTable()
                                        {
                                            status = "330",
                                            statusDescription = "Total number of notes delivered (Life Long)",
                                            quantity = Convert.ToInt32(trace.Result.ResponseOriginal.Substring(2, 10))

                                        });
                                        break;
                                    case 10://talNotesRejected':
                                        cStatusTableList2.Add(new cStatusTable()
                                        {
                                            status = "331",
                                            statusDescription = "Total number of notes rejected (Life Long)",
                                            quantity = Convert.ToInt32(trace.Result.ResponseOriginal.Substring(2, 10))

                                        });
                                        break;
                                    case 11://talNotesRejected':
                                        cStatusTableList2.Add(new cStatusTable()
                                        {
                                            status = "332",
                                            statusDescription = "Total number of bundle rejects (Life Long)",
                                            quantity = Convert.ToInt32(trace.Result.ResponseOriginal.Substring(2, 10))

                                        });
                                        break;
                                    case 12://talNotesRejected':
                                        cStatusTableList2.Add(new cStatusTable()
                                        {
                                            status = "333",
                                            statusDescription = "Total number of transaction bundles(Life Long)",
                                            quantity = Convert.ToInt32(trace.Result.ResponseOriginal.Substring(2, 10))

                                        });
                                        break;
                                    case 13://talNotesRejected':
                                        cStatusTableList2.Add(new cStatusTable()
                                        {
                                            status = "334",
                                            statusDescription = "Total number of bundle retracts (Life Long)",
                                            quantity = Convert.ToInt32(trace.Result.ResponseOriginal.Substring(2, 10))

                                        });
                                        break;
                                }

                                index++;
                            }    
                            //loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + statusDispenser5.ResponseOriginal);
                            //cStatusTableList2.Add(new cStatusTable()
                            //{
                            //    status = "303",
                            //    statusDescription = "Total number of notes delivered",
                            //    quantity = Convert.ToInt32(statusDispenser5.ResponseOriginal.Substring(2, 10))
                            //});
                        }
                        else
                            loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + stateTableGrl.TablaDeEstadosGrlResult.Message);
                        //loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para leer CounterNote304");
                        //StatusDispenser statusDispenser6 = ExecutorCommand.ejecutar(Comando.CounterNotes304);
                        //if (statusDispenser6.state == ResponseDispensador.Exito)
                        //{
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + statusDispenser6.ResponseOriginal);
                        //    cStatusTableList2.Add(new cStatusTable()
                        //    {
                        //        status = "304",
                        //        statusDescription = "Total number of notes rejected",
                        //        quantity = Convert.ToInt32(statusDispenser6.ResponseOriginal.Substring(2, 10))
                        //    });
                        //}
                        //else
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + statusDispenser6.Description);
                        //StatusDispenser statusDispenser7 = ExecutorCommand.ejecutar(Comando.CounterNotes305);
                        //if (statusDispenser7.state == ResponseDispensador.Exito)
                        //{
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + statusDispenser7.ResponseOriginal);
                        //    cStatusTableList2.Add(new cStatusTable()
                        //    {
                        //        status = "305",
                        //        statusDescription = "Total number of bundle rejects",
                        //        quantity = Convert.ToInt32(statusDispenser7.ResponseOriginal.Substring(2, 10))
                        //    });
                        //}
                        //else
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + statusDispenser7.Description);
                        //loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para leer CounterNote308");
                        //StatusDispenser statusDispenser8 = ExecutorCommand.ejecutar(Comando.CounterNotes308);
                        //if (statusDispenser8.state == ResponseDispensador.Exito)
                        //{
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + statusDispenser8.ResponseOriginal);
                        //    cStatusTableList2.Add(new cStatusTable()
                        //    {
                        //        status = "308",
                        //        statusDescription = "Total number of notes singel rejected",
                        //        quantity = Convert.ToInt32(statusDispenser8.ResponseOriginal.Substring(2, 10))
                        //    });
                        //}
                        //else
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + statusDispenser8.Description);
                        //loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para leer CounterNote309");
                        //StatusDispenser statusDispenser9 = ExecutorCommand.ejecutar(Comando.CounterNotes309);
                        //if (statusDispenser9.state == ResponseDispensador.Exito)
                        //{
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + statusDispenser9.ResponseOriginal);
                        //    cStatusTableList2.Add(new cStatusTable()
                        //    {
                        //        status = "309",
                        //        statusDescription = "Total number of notes bundle rejected",
                        //        quantity = Convert.ToInt32(statusDispenser9.ResponseOriginal.Substring(2, 10))
                        //    });
                        //}
                        //else
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + statusDispenser9.Description);
                        //loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para leer CounterNote320");
                        //StatusDispenser statusDispenser10 = ExecutorCommand.ejecutar(Comando.CounterNotes320);
                        //if (statusDispenser10.state == ResponseDispensador.Exito)
                        //{
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + statusDispenser10.ResponseOriginal);
                        //    cStatusTableList2.Add(new cStatusTable()
                        //    {
                        //        status = "320",
                        //        statusDescription = "Total number of transaction bundles",
                        //        quantity = Convert.ToInt32(statusDispenser10.ResponseOriginal.Substring(2, 10))
                        //    });
                        //}
                        //else
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + statusDispenser10.Description);
                        //loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para leer CounterNote390");
                        //StatusDispenser statusDispenser11 = ExecutorCommand.ejecutar(Comando.CounterNotes390);
                        //if (statusDispenser11.state == ResponseDispensador.Exito)
                        //{
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + statusDispenser11.ResponseOriginal);
                        //    cStatusTableList2.Add(new cStatusTable()
                        //    {
                        //        status = "390",
                        //        statusDescription = "Total number of bundle retracts",
                        //        quantity = Convert.ToInt32(statusDispenser11.ResponseOriginal.Substring(2, 10))
                        //    });
                        //}
                        //else
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + statusDispenser11.Description);
                        //loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para leer CounterNote318");
                        //StatusDispenser statusDispenser12 = ExecutorCommand.ejecutar(Comando.CounterNotes318);
                        //if (statusDispenser12.state == ResponseDispensador.Exito)
                        //{
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + statusDispenser12.ResponseOriginal);
                        //    cStatusTableList2.Add(new cStatusTable()
                        //    {
                        //        status = "318",
                        //        statusDescription = "Total number of notes delivered in HPF mode",
                        //        quantity = Convert.ToInt32(statusDispenser12.ResponseOriginal.Substring(2, 10))
                        //    });
                        //}
                        //else
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + statusDispenser12.Description);
                        //loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para leer CounterNote319");
                        //StatusDispenser statusDispenser13 = ExecutorCommand.ejecutar(Comando.CounterNotes319);
                        //if (statusDispenser13.state == ResponseDispensador.Exito)
                        //{
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + statusDispenser13.ResponseOriginal);
                        //    cStatusTableList2.Add(new cStatusTable()
                        //    {
                        //        status = "319",
                        //        statusDescription = "Total number of notes rejected in HPF mode",
                        //        quantity = Convert.ToInt32(statusDispenser13.ResponseOriginal.Substring(2, 10))
                        //    });
                        //}
                        //else
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + statusDispenser13.Description);
                        //loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para leer CounterNote330");
                        //StatusDispenser statusDispenser14 = ExecutorCommand.ejecutar(Comando.CounterNotes330);
                        //if (statusDispenser14.state == ResponseDispensador.Exito)
                        //{
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + statusDispenser14.ResponseOriginal);
                        //    cStatusTableList2.Add(new cStatusTable()
                        //    {
                        //        status = "330",
                        //        statusDescription = "Total number of notes delivered (Life Long)",
                        //        quantity = Convert.ToInt32(statusDispenser14.ResponseOriginal.Substring(2, 10))
                        //    });
                        //}
                        //else
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + statusDispenser14.Description);
                        //loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para leer CounterNote331");
                        //StatusDispenser statusDispenser15 = ExecutorCommand.ejecutar(Comando.CounterNotes331);
                        //if (statusDispenser15.state == ResponseDispensador.Exito)
                        //{
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + statusDispenser15.ResponseOriginal);
                        //    cStatusTableList2.Add(new cStatusTable()
                        //    {
                        //        status = "331",
                        //        statusDescription = "Total number of notes rejected (Life Long)",
                        //        quantity = Convert.ToInt32(statusDispenser15.ResponseOriginal.Substring(2, 10))
                        //    });
                        //}
                        //else
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + statusDispenser15.Description);
                        //loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para leer CounterNote332");
                        //StatusDispenser statusDispenser16 = ExecutorCommand.ejecutar(Comando.CounterNotes332);
                        //if (statusDispenser16.state == ResponseDispensador.Exito)
                        //{
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + statusDispenser16.ResponseOriginal);
                        //    cStatusTableList2.Add(new cStatusTable()
                        //    {
                        //        status = "332",
                        //        statusDescription = "Total number of bundle rejects (Life Long)",
                        //        quantity = Convert.ToInt32(statusDispenser16.ResponseOriginal.Substring(2, 10))
                        //    });
                        //}
                        //else
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + statusDispenser16.Description);
                        //loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para leer CounterNote333");
                        //StatusDispenser statusDispenser17 = ExecutorCommand.ejecutar(Comando.CounterNotes333);
                        //if (statusDispenser17.state == ResponseDispensador.Exito)
                        //{
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + statusDispenser17.ResponseOriginal);
                        //    cStatusTableList2.Add(new cStatusTable()
                        //    {
                        //        status = "333",
                        //        statusDescription = "Total number of transaction bundles(Life Long)",
                        //        quantity = Convert.ToInt32(statusDispenser17.ResponseOriginal.Substring(2, 10))
                        //    });
                        //}
                        //else
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + statusDispenser17.Description);
                        //loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para leer CounterNote334");
                        //StatusDispenser statusDispenser18 = ExecutorCommand.ejecutar(Comando.CounterNotes334);
                        //if (statusDispenser18.state == ResponseDispensador.Exito)
                        //{
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + statusDispenser18.ResponseOriginal);
                        //    cStatusTableList2.Add(new cStatusTable()
                        //    {
                        //        status = "334",
                        //        statusDescription = "Total number of bundle retracts (Life Long)",
                        //        quantity = Convert.ToInt32(statusDispenser18.ResponseOriginal.Substring(2, 10))
                        //    });
                        //}
                        //else
                        //    loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + statusDispenser18.Description);
                        this.tdgEstadoTabla.DataSource = (object)cStatusTableList2;
                        this.pnlEstadoTable.Visible = true;
                        break;
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                case 7:
                    loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para abrir shutter");

                    if (globalConfigATM.configDispenserStatus.Tipo == "NMD100")
                    {
                        //StatusDispenser statusDispenser19 = ExecutorCommand.ejecutar(Comando.AbrirShutter);
                        ResponseOpenShutter responseOpenShutter = servicios.OpenShutter();
                        if (responseOpenShutter.AbrirShutterResult.State == ResponseType.Success)
                        //if (statusDispenser19.state == ResponseDispensador.Exito)
                        {
                            loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + responseOpenShutter.AbrirShutterResult.ListEntities[0].Result.ResponseOriginal);
                            MessageBox.Show("Shutter abierto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                        loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + responseOpenShutter.AbrirShutterResult.ListEntities[0].Result.ResponseOriginal);
                        MessageBox.Show("Error:" + responseOpenShutter.AbrirShutterResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                    break;
                case 8:
                    loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para cerrar shutter");

                    if (globalConfigATM.configDispenserStatus.Tipo == "NMD100")
                    {
                        //StatusDispenser statusDispenser20 = ExecutorCommand.ejecutar(Comando.CerrarShutter);
                        ResponseCloseShutter responseCloseShutter = servicios.CloseShutter();
                        if (responseCloseShutter.CerrarShutterResult.State == ResponseType.Success)
                        //if (statusDispenser20.state == ResponseDispensador.Exito)
                        {
                            loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + responseCloseShutter.CerrarShutterResult.ListEntities[1].Result.ResponseOriginal);
                            MessageBox.Show("Shutter cerrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                        loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + responseCloseShutter.CerrarShutterResult.ListEntities[0].Result.ResponseOriginal);
                        MessageBox.Show("Error:" + responseCloseShutter.CerrarShutterResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    break;
                case 9:
                    //loggerATM.PsRegisterLogger( "CallFunctionNMD", "Llamada a dispensador para emular shutter");
                    //StatusDispenser statusDispenser21 = ExecutorCommand.ejecutar(Comando.ReadEmulatorShutter);
                    //if (statusDispenser21.state == ResponseDispensador.Exito)
                    //{
                    //    loggerATM.PsRegisterLogger( "CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + statusDispenser21.ResponseOriginal);
                    //    MessageBox.Show("Shutter cerrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //    break;
                    //}
                    //loggerATM.PsRegisterLogger( "CallFunctionNMD", "Error en respuesta. Error:" + statusDispenser21.Description);
                    //MessageBox.Show("Error:" + statusDispenser21.Description, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    FrmEmulatorShutter frm = new FrmEmulatorShutter();
                    frm.Show(Owner);
                    break;
                case 10:
                    try
                    {
                        loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para RESET");

                        ResponseReset responseReset = servicios.Reset();


                        //StatusDispenser statusDispenser22 = ExecutorCommand.ejecutar(Comando.Reset);
                        loggerATM.PsRegisterLogger("CallFunctionNMD", "Luego de RESET");
                        if (responseReset.ResetResult.State == ResponseType.Success)
                        {

                            loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + responseReset.ResetResult.Message);
                            MessageBox.Show("Reset realizado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                        loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + responseReset.ResetResult.Message);
                        MessageBox.Show("Error:" + responseReset.ResetResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    catch (Exception Error)
                    {
                        MessageBox.Show(Error.Message);
                        //throw;
                    }

                    break;
                case 11:
                    loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para RESET lento");
                    ResponseResetLento response = servicios.ResetLento();
                    //StatusDispenser statusDispenser23 = ExecutorCommand.ejecutar(Comando.ResetLento);
                    if (response.ResetLentoResult.State == ResponseType.Success)
                    {
                        loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + response.ResetLentoResult.ListEntities[1].Result.ResponseOriginal);
                        MessageBox.Show("Reset realizado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + response.ResetLentoResult.Message);
                    MessageBox.Show("Error:" + response.ResetLentoResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                case 12:
                    loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para borrar tabla");
                    //StatusDispenser statusDispenser24 = ExecutorCommand.ejecutar(Comando.ClearTable);
                    ResponseClearTable responseCT = servicios.ClearTable();
                    if (responseCT.BorrarTablaResult.State == ResponseType.Success)
                    {
                        loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + responseCT.BorrarTablaResult.ListEntities[0].Result.ResponseOriginal);
                        MessageBox.Show("Borrado realizado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    }
                    loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + responseCT.BorrarTablaResult.Message);
                    MessageBox.Show("Error:" + responseCT.BorrarTablaResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                case 13:
                    loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para leer ID de PROGRAMA");
                    //StatusDispenser statusDispenser25 = ExecutorCommand.ejecutar(Comando.ProgramIDBlock);
                    ResponseReadIdBlock responseReadId = servicios.ReadIdBlock();
                    if (responseReadId.LeeIdBlockResult.State == ResponseType.Success)
                    {
                        loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + responseReadId.LeeIdBlockResult.ListEntities[0].Result.ResponseOriginal);
                        string textMessage = "";
                        int index = 2;
                        while (index + 4 < responseReadId.LeeIdBlockResult.ListEntities[0].Result.ResponseOriginal.Length)
                        {
                            string subText = responseReadId.LeeIdBlockResult.ListEntities[0].Result.ResponseOriginal.Substring(index, 3) + ":" + responseReadId.LeeIdBlockResult.ListEntities[0].Result.ResponseOriginal.Substring(index + 3, 4) + "-" + responseReadId.LeeIdBlockResult.ListEntities[0].Result.ResponseOriginal.Substring(index + 7, 2) + "." + responseReadId.LeeIdBlockResult.ListEntities[0].Result.ResponseOriginal.Substring(index + 9, 2) + "\n";
                            textMessage = textMessage + subText;
                            index += 11;
                        }

                        MessageBox.Show(textMessage, "Program Id's", MessageBoxButtons.OK, MessageBoxIcon.None);
                        break;
                    }
                    loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + responseReadId.LeeIdBlockResult.Message);
                    MessageBox.Show("Error:" + responseReadId.LeeIdBlockResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                case 14:
                    loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para leer numero de serie");
                    //StatusDispenser statusDispenser26 = ExecutorCommand.ejecutar(Comando.NumberSerialNMD);
                    ResponseReadSerieNMD responseReadSerieNMD = servicios.ReadSerieNMD();

                    if (responseReadSerieNMD.LeeNumeroSerieNMDResult.State == ResponseType.Success)
                    {
                        loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + responseReadSerieNMD.LeeNumeroSerieNMDResult.ListEntities[0].Result.ResponseOriginal);
                        this.txtNumSerial.Text = responseReadSerieNMD.LeeNumeroSerieNMDResult.ListEntities[0].Result.ResponseOriginal.Substring(5, 14);
                        this.pnlIDNMD.Visible = true;
                        break;
                    }
                    loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + responseReadSerieNMD.LeeNumeroSerieNMDResult.Message);
                    MessageBox.Show("Error:" + responseReadSerieNMD.LeeNumeroSerieNMDResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                case 15:
                    loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para escribir numero de serie");
                    //StatusDispenser statusDispenser27 = ExecutorCommand.ejecutar(Comando.WriteNumberSerialNMD, "NMD" + this.txtNumSerial.Text);
                    ResponseUpdateSerieNMD responseUpdateSerieNMD = servicios.UpdateSerieNMD(txtNumSerial.Text);

                    if (responseUpdateSerieNMD.ActualizaNumeroSerieNMDResult.State == ResponseType.Success)
                    {
                        loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + responseUpdateSerieNMD.ActualizaNumeroSerieNMDResult.ListEntities[0].Result.ResponseOriginal);
                        MessageBox.Show("Registrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + responseUpdateSerieNMD.ActualizaNumeroSerieNMDResult.Message);
                        MessageBox.Show("Error:" + responseUpdateSerieNMD.ActualizaNumeroSerieNMDResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    break;
                case 16://habilita deteccion de bandeja
                    loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para escribir numero de serie");
                    //StatusDispenser statusDispenser28 = ExecutorCommand.ejecutar(Comando.EnabledCassette, "");
                    ResponseEnableCassettes responseEnableCassettes = servicios.EnableCassettes();

                    if (responseEnableCassettes.HabilitaDeteccionDeBandejaResult.State == ResponseType.Success)
                    {
                        loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + responseEnableCassettes.HabilitaDeteccionDeBandejaResult.ListEntities[0].Result.ResponseOriginal);
                        MessageBox.Show("Actualizado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + responseEnableCassettes.HabilitaDeteccionDeBandejaResult.Message);
                        MessageBox.Show("Error:" + responseEnableCassettes.HabilitaDeteccionDeBandejaResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    break;
                case 17://habilita reset lento
                    if (globalConfigATM.configDispenserStatus.Tipo == "NMD50")
                    {
                        loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para escribir numero de serie");
                        //StatusDispenser statusDispenser29 = ExecutorCommand.ejecutar(Comando.EnabledCheckNotes, "");
                        ResponseEnabledCheckNotes responseEnabledCheckNotes = servicios.EnableCheckNotes();
                        if (responseEnabledCheckNotes.HabilitaValidadorDeEntregaResult.State == ResponseType.Success)
                        {
                            loggerATM.PsRegisterLogger("CallFunctionNMD", "Respuesta correcta de dispensador. Respuesta:" + responseEnabledCheckNotes.HabilitaValidadorDeEntregaResult.ListEntities[1].Result.ResponseOriginal);
                            MessageBox.Show("Actualizado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                        {
                            loggerATM.PsRegisterLogger("CallFunctionNMD", "Error en respuesta. Error:" + responseEnabledCheckNotes.HabilitaValidadorDeEntregaResult.Message);
                            MessageBox.Show("Error:" + responseEnabledCheckNotes.HabilitaValidadorDeEntregaResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Esta opción solo es válidado para dispensador NMD50", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    break;
            }
        }

        private void btnCerrarIdBandejas_Click(object sender, EventArgs e) => this.pnldBandeja.Visible = false;

        //private void LvGrabarLog(eTypes pType , string pModulo, string pMensaje)
        //{
        //    string path = Application.StartupPath +  "\\LogNMD.txt";
        //    string Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")+ "|" + pModulo +"|"+ pMensaje ;
        //    using (StreamWriter writetext = new StreamWriter(path))
        //    {
        //        writetext.WriteLine(Text);
        //    }
        //}
        private void btnEstadoTabla_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    this.subMenuEstadistica.Visible = false;
            //    this.LsLlamadaAFuncion(4);
            //}
            //catch (Exception ex)
            //{
            //    int num = (int)MessageBox.Show("Error:" + ex.Message);
            //}
        }

        private void btnCerrarTablaEstado_Click(object sender, EventArgs e) => this.pnlEstadoTable.Visible = false;

        private void btnContadore_Click(object sender, EventArgs e)
        {
            //this.pnlEstadoTable.Visible = false;
            //this.subMenuEstadistica.Visible = false;
            //this.LsLlamadaAFuncion(5);
        }

        private void btmVerifEstadoNMD_Click(object sender, EventArgs e)
        {
            //this.subMenuEstadistica.Visible = false;
            ////MessageBox.Show("En desarrollo...");
            //FrmNMDStatus frm = new FrmNMDStatus();
            //frm.TopMost = true;
            //frm.Owner = (Form)this;
            //frm.ShowDialog(this);
        }
        private void btnAutoTest_Click(object sender, EventArgs e)
        {
            //this.pnlEstadoTable.Visible = false;
            //this.subMenuEstadistica.Visible = false;
            //FrmSelfTestData frmSelfTestData = new FrmSelfTestData();
            //frmSelfTestData.TopMost = true;
            //frmSelfTestData.Owner = (Form)this;
            //int num = (int)frmSelfTestData.ShowDialog((IWin32Window)this);
        }
        private void btnAbrirShutter_Click(object sender, EventArgs e)
        {
            //this.subMenuServicios.Visible = false;
            //this.LsLlamadaAFuncion(7);
        }
        //private void btnBandejas_Click(object sender, EventArgs e) => new FrmCassettes().Show();

        private void btnServicios_Click(object sender, EventArgs e)
        {
            //if (this.subMenuServicios.Visible)
            //    this.subMenuServicios.Visible = false;
            //else
            //    this.subMenuServicios.Visible = true;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //this.SubmenuConexion.Visible = false;
            //this.LsLlamadaAFuncion(10);
        }

        private void btnReset0_Click(object sender, EventArgs e)
        {
            //this.SubmenuConexion.Visible = false;
            //this.LsLlamadaAFuncion(11);
        }

        private void btnBorrarTabla_Click(object sender, EventArgs e) => this.LsLlamadaAFuncion(12);

        private void btnRefrescar_Click(object sender, EventArgs e) => this.LsLlamadaAFuncion(4);

        private void btnReadInfo_Click(object sender, EventArgs e)
        {
            //this.SubmenuConexion.Visible = false;
            //this.LsLlamadaAFuncion(13);
        }

        private void btnSerial_Click(object sender, EventArgs e) => this.LsLlamadaAFuncion(14);

        private void btnCerrarIDNMD_Click(object sender, EventArgs e) => this.pnlIDNMD.Visible = false;

        private void btnGrabarIDNMD_Click(object sender, EventArgs e)
        {
            if (this.txtNumSerial.Text.Length < 14)
                this.txtNumSerial.Text = new string('0', 14 - this.txtNumSerial.Text.Length) + this.txtNumSerial.Text;
            this.LsLlamadaAFuncion(15);
        }

        private void pnlIDNMD_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBandeja2_Click(object sender, EventArgs e) => new FrmCassettes().Show();

        private void btnEmularShutter_Click(object sender, EventArgs e)
        {
            //this.subMenuServicios.Visible = false;
            
            //if (globalConfigATM.configDispenserStatus.Tipo == "NMD100")
            //{
            //    this.subMenuServicios.Visible = false;
            //    this.Refresh();
            //    FrmEmulatorShutter frm = new FrmEmulatorShutter();
            //    frm.Show(Owner);
            //}
        }

        private void UpdateConfigCOM()
        {
            AccedeBD accede = new AccedeBD();
            XmlDocument document = new XmlDocument();
            string nameXml = Application.StartupPath + "\\Config\\ConfigCOM.xml";
            document.Load(nameXml);

            try
            {
                XmlNodeList aNodesPort = document.SelectNodes("/ConfigCOM/Portname");

                foreach (XmlNode node in aNodesPort)
                {
                    if (node.LocalName == "Portname")
                    {
                        node.InnerText = globalConfigATM.configDispenserCOM.Portname;
                    }
                }

                XmlNodeList aNodesRTime = document.SelectNodes("/ConfigCOM/ReadTimeout");

                foreach (XmlNode node in aNodesRTime)
                {
                    if (node.LocalName == "ReadTimeout")
                    {
                        node.InnerText = globalConfigATM.configDispenserCOM.ReadTimeout.ToString();
                    }
                }

                XmlNodeList aNodesWTime = document.SelectNodes("/ConfigCOM/WriteTimeout");
                foreach (XmlNode node in aNodesWTime)
                {
                    if (node.LocalName == "WriteTimeout")
                    {
                        node.InnerText = globalConfigATM.configDispenserCOM.WriteTimeout.ToString();
                    }
                }
                XmlNodeList aNodesBaudio = document.SelectNodes("/ConfigCOM/Baudios");
                foreach (XmlNode node in aNodesBaudio)
                {
                    if (node.LocalName == "Baudios")
                    {
                        node.InnerText = globalConfigATM.configDispenserCOM.Baudios.ToString();
                    }
                }

                XmlNodeList aNodesParidad = document.SelectNodes("/ConfigCOM/Paridad");
                foreach (XmlNode node in aNodesParidad)
                {
                    if (node.LocalName == "Paridad")
                    {
                        node.InnerText = globalConfigATM.configDispenserCOM.Paridad.ToString();
                    }
                }

                XmlNodeList aNodesData = document.SelectNodes("/ConfigCOM/Data");
                foreach (XmlNode node in aNodesData)
                {
                    if (node.LocalName == "Data")
                    {
                        node.InnerText = globalConfigATM.configDispenserCOM.Data.ToString();
                    }
                }

                XmlNodeList aNodesTipo = document.SelectNodes("/ConfigCOM/Tipo");
                foreach (XmlNode node in aNodesTipo)
                {
                    if (node.LocalName == "Tipo")
                    {
                        node.InnerText = globalConfigATM.configDispenserStatus.Tipo.ToString();
                    }
                }

                XmlNodeList aNodesBandeja = document.SelectNodes("/ConfigCOM/NumBandejas");
                foreach (XmlNode node in aNodesBandeja)
                {
                    if (node.LocalName == "NumBandejas")
                    {
                        node.InnerText = globalConfigATM.configDispenserStatus.NumBandejas.ToString();
                    }
                }
                document.Save(nameXml);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al actualizar configuración de Dispensador. Error:" + error.Message + "\n\nVerifique la configuración de dispensador(ConfigCOM.xml)");
            }


        }

        private void btnEntrega_Click(object sender, EventArgs e)
        {
            //this.SubmenuConexion.Visible = false;
            //this.Refresh();
            //FrmCashDelivery frm = new FrmCashDelivery();
            //frm.Owner = this;
            //frm.ShowDialog(this);
        }

        private void btnHabBandejas_Click(object sender, EventArgs e)
        {
            this.LsLlamadaAFuncion(16);
        }

        private void btnHabResetLento_Click(object sender, EventArgs e)
        {

            this.LsLlamadaAFuncion(17);
        }

        private void btnCerrarShutter_Click(object sender, EventArgs e)
        {
            //this.subMenuServicios.Visible = false;
            //this.LsLlamadaAFuncion(8);
        }

        private void btnCleanNF_Click(object sender, EventArgs e)
        {
            //this.subMenuServicios.Visible=false;
            //this.Refresh();
            //Maintenance.frmCleanNSNF frmClean = new Maintenance.frmCleanNSNF();
            //frmClean.TopMost = true;
            //frmClean.Owner =(Form) this;
            //frmClean.ShowDialog(this);
        }

        private void FrmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {           
            try
            {
                services servicios = new services();

                ResponseValidateFingerClose responseClose = servicios.ValidateUserAccessClose(idAccess, token);
                if (responseClose.ATMAccessHistoryCloseResult.State == ResponseType.Success)
                {
                    loggerATM.PsRegisterLogger("FrmClosed", "Se registro la salida de la aplicación de manera correcta");
                }
                else
                {
                    loggerATM.PsRegisterLogger("FrmClosed", "Error al registrar salida. Error:" + responseClose.ATMAccessHistoryCloseResult.Message);
                    MessageBox.Show("Error al cerrar la aplicación. \nIdAccess:" + idAccess.ToString() + "\nError:" + responseClose.ATMAccessHistoryCloseResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al cerrar la aplicación. Error:" + error.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }
    }
    public class IdBandejas
    {
        public int nummero { get; set; }
        public string IdBandeja { get; set; }
        public string Estado { get; set; }
        public string DesEstado { get; set; }
    }

   
}
