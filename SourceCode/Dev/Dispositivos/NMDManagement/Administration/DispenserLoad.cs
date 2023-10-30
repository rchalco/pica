
using RegisterLogger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NMDManagement.Administration
{
    
    public partial class DispenserLoad : Form
    {
        string proccessToken = string.Empty ;
        long IdSolicitation = 0 ;
        long IdPerson=0 ;
        string CI = "";
        string LoginUser = "";
        List<BalanceData> balanceDatas;
        ResultConfgATM globalConfigATM=new ResultConfgATM();
        ResultConfgATM globalConfigATMBackup = new ResultConfgATM();
        public DispenserLoad(string pToken, long pIdPerson, string pCI , string pUser)
        {
            InitializeComponent();
            proccessToken= pToken;
            IdPerson = pIdPerson;
            CI = pCI;
            LoginUser = pUser;
        }

        private void DispenserLoad_Load(object sender, EventArgs e)
        {
            services servicios = new services();
            //string jsonContent = File.ReadAllText(@"c:\hla\ATM.json");
            globalConfigATM = servicios.GetHotState();

            //globalConfigATM = Newtonsoft.Json.JsonConvert.DeserializeObject<GlobalConfigATM>(jsonContent);
            globalConfigATMBackup = globalConfigATM ;// Newtonsoft.Json.JsonConvert.DeserializeObject<GlobalConfigATM>(jsonContent);

            bool adiciono = false;
            balanceDatas = new List<BalanceData >();
            services service=new services();
            balanceDatas = new List<BalanceData>();
            ATMLoadSolicitationPending recordLoad = service.ATMLoadSolicitationGetPending(globalConfigATM.GetHotStateResult.Object.Id , proccessToken );
            if(recordLoad.ATMLoadSolicitationGetPendingForATMResult.State  == ResponseType.Success  )
            {
              
                List<ListEntity> records = recordLoad.ATMLoadSolicitationGetPendingForATMResult.ListEntities.OrderBy(record =>record.CoinageValue ).ToList();


                foreach(Cassette  cassette  in globalConfigATM.GetHotStateResult.Object.configDispenserStatus.Cassettes )
                {
                    adiciono = false;
                    foreach (ListEntity record in records)
                    {
                        
                        if (cassette.CurrencyCourt == record.CoinageValue)
                        {
                            balanceDatas.Add(new BalanceData { Denomination = record.CoinageValue, IdCount = 1, IdMoney = 1, NroCassette = cassette.Sequence, RegisterBalance = record.Quantity.ToString() });
                            IdSolicitation = record.IdATMLoadSolicitation;
                            cassette.TotalQuantity = cassette.TotalQuantity + record.Quantity;
                            adiciono = true;
                            break;
                        }
                    }
                    if(adiciono ==false)
                    {
                        balanceDatas.Add(new BalanceData { Denomination = cassette.CurrencyCourt  , IdCount = 1, IdMoney = 1, NroCassette = cassette.Sequence, RegisterBalance = "0"});
                    }
                }
                
                dgvBandejas.DataSource = balanceDatas;
                ResponseReleaseCassette opened = service.ReleaseCassette();
                IdSolicitation = recordLoad.ATMLoadSolicitationGetPendingForATMResult.ListEntities[0].IdATMLoadSolicitation;
                if (opened.LiberarBandejaResult.State == ResponseType.Success)
                {
                    MessageBox.Show("Bandejas liberadas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al liberar las bandejas\n Error:" + opened.LiberarBandejaResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }   
            else
            {
                MessageBox.Show("Estado:" + recordLoad.ATMLoadSolicitationGetPendingForATMResult.State.ToString() + "\nDescripción:" + recordLoad.ATMLoadSolicitationGetPendingForATMResult.Message , "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            decimal mount = 0;
   

            loggerATM.PsRegisterLogger("Verif", "ingresp a validar carga");
            services service = new services();
            if ( MessageBox.Show("Adicionó los billetes asignados y colocó en las bandejas en el lugar correspondiente?", "Pregunta",MessageBoxButtons.YesNo , MessageBoxIcon.Question  )== DialogResult.Yes)
            {
                try
                {
                    ConfigurationATM configurationATM = service.GetATM(proccessToken, globalConfigATM.GetHotStateResult.Object.Id);

                    //if (configurationATM.GetATMByIdResult.State != ResponseType.Success)
                    //{
                    //    MessageBox.Show("Error al obtener configuración de ATM. Error:" + configurationATM.GetATMByIdResult.Message);
                    //    return;
                    //}

                    //loggerATM.PsRegisterLogger("Verif", "Antes de actualizar tabla de ATM");
                    //foreach (BalanceData balanceData in balanceDatas)
                    //{
                    //    loggerATM.PsRegisterLogger("Verif", "Se buscar " + balanceData.Denomination.ToString());
                    //    foreach (NMDManagement.ATMConfig.Cassette cassette1 in configurationATM.GetATMByIdResult.Object.Configuration.configDispenserStatus.Cassettes)
                    //    {
                    //        if (cassette1.CurrencyCourt == balanceData.Denomination)
                    //        {
                    //            cassette1.TotalQuantity = cassette1.TotalQuantity + Convert.ToInt32(balanceData.RegisterBalance);
                    //        }
                    //    }
                    //}
                    //loggerATM.PsRegisterLogger("Verif", "Llamada a UpdateATM");
                    //ResultUpdateATM resultUpdateATM = service.UpadateATM(configurationATM.GetATMByIdResult.Object, proccessToken);

                    loggerATM.PsRegisterLogger("Verif", "Cerrar bandejas");
                    ResponseCloseCassette respCerrar = service.CloseCassette();
                    //RestultIniATM resp = service.InitATM(proccessToken);
                    //loggerATM.PsRegisterLogger("Verif", "Respuesta cerrar:" + resp.ToString());

                    if (respCerrar.AsegurarBandejaResult.State == ResponseType.Success || (respCerrar.AsegurarBandejaResult.State == ResponseType.Warning && respCerrar.AsegurarBandejaResult.Message.Contains("Low Level") ))
                        {
                        loggerATM.PsRegisterLogger("Verif", "Inicializa  dispensador");
                        //RestultIniATM respIni = service.InitATM(proccessToken);
                        ResponseInit respIni = service.initializa_dispenser();
                        loggerATM.PsRegisterLogger("Verif", "Respuesta Inicializa");
                        if (respIni.InicializarResult.State == ResponseType.Success || respIni.InicializarResult.State == ResponseType.Warning )
                        {
                            //actualiza json saldos en json para entrega, en especial para primera carga
                            //FileHelper.updateStateATM(globalConfigATM);
                            //
                            foreach (BalanceData balanceData in balanceDatas)
                            {
                                loggerATM.PsRegisterLogger("Verif", "Se entregará " + balanceData.Denomination.ToString());
                                ResponseDeliveryForce  entrega = service.DeliveryCashForce (Convert.ToInt32(balanceData.Denomination));
                                if (entrega.DispensarForceResult.State == ResponseType.Success || entrega.DispensarForceResult.State == ResponseType.Warning)
                                {
                                    if (MessageBox.Show("Se entregó un billete de " + balanceData.Denomination + "?", "pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        //seguiente
                                        loggerATM.PsRegisterLogger("Verif", "Respuesta correcta a consulta de entrega");
                                        mount = mount + balanceData.Denomination;
                                        
                                    }
                                    else
                                    {
                                        loggerATM.PsRegisterLogger("Verif", "Error de entrega:" + entrega.DispensarForceResult.Message);
                                        MessageBox.Show("Error en entrega de prueba.\n" + entrega.DispensarForceResult.Message + "\n\nTodos los billetes expulsados por el dispensador deben ser devueltos a las bandejas correspondientes.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        //                                        FileHelper.updateStateATM(globalConfigATMBackup);
                                        this.Close();
                                        return;
                                    }
                                }
                                else
                                {
                                    loggerATM.PsRegisterLogger("Verif", "Error de entrega:" + entrega.DispensarForceResult.Message);
                                    MessageBox.Show("Error en entrega de prueba.\n" + entrega.DispensarForceResult.Message + "\n\nTodos los billetes expulsados por el dispensador deben ser devueltos a las bandejas correspondientes.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    //FileHelper.updateStateATM(globalConfigATMBackup);
                                    this.Close();
                                    return;
                                }
                            }
                            loggerATM.PsRegisterLogger("Verif", "Llamada a habilitar carga");
                            ATMLoadSolicitationConfirmProcess resVerif = service.ATMLoadSolicitationConfirmProcess(IdSolicitation, IdPerson, proccessToken);
                            if (resVerif.ATMLoadSolicitationConfirmProcessResult.State == ResponseType.Success)
                            {
                                ResultConfgATM globalConfigATM = service.GetHotState();
                                clPrint clprint = new clPrint();
                                ParameterTicket ticket = new ParameterTicket();
                                ticket.objParameters = new ParameterTicket.Param();
                                ticket.objParameters.IdcTranssactionType = 9190;
                                ticket.objParameters.IdMoney = 1;
                                ticket.objParameters.IdOffice = globalConfigATM.GetHotStateResult.Object.IdOffice;
                                ticket.objParameters.IdATM = globalConfigATM.GetHotStateResult.Object.Id;
                                ticket.objParameters.NameATM = globalConfigATM.GetHotStateResult.Object.Name;
                                ticket.objParameters.IdOffice = globalConfigATM.GetHotStateResult.Object.IdOffice;
                                ticket.objParameters.Observation = textBox1.Text;
                                ticket.objParameters.IdentityNumber = CI;
                                ticket.objParameters.AmountOperation = mount;
                                ticket.objParameters.DescriptionATM = globalConfigATM.GetHotStateResult.Object.Name;
                                ticket.objParameters.NumberOperation = 0;
                                ticket.objParameters.NewConfiguration = "";
                                ticket.objParameters.TraceDevice = "";

                                RecuestRegisterTicket recuestRegisterTicket = service.RegisterTicket(ticket, proccessToken);
                                if (recuestRegisterTicket.CreateTicketForOperationExternalServiceResult.State != ResponseType.Success)
                                {
                                    MessageBox.Show("Error al registrar retoma.\nError: " + recuestRegisterTicket.CreateTicketForOperationExternalServiceResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    recuestRegisterTicket.CreateTicketForOperationExternalServiceResult.Object = String.Empty ;
                                }
                                clprint.PrintLoadDispenser(balanceDatas, IdSolicitation, recuestRegisterTicket.CreateTicketForOperationExternalServiceResult.Object, mount, LoginUser);

                                RestultIniATM restultIniATM =service.InitATM(proccessToken );

                                MessageBox.Show("Entregas correctas y se registró la carga.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Error al registrar la confirmación.\nError:" + resVerif.ATMLoadSolicitationConfirmProcessResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Error al inicializar bandejas.\n" + respIni.InicializarResult.Message + "\nEstado:" + respIni.InicializarResult.State.ToString(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Error al cerrar bandejas.\n" + respCerrar.AsegurarBandejaResult.Message + "\nEstado:" + respCerrar.AsegurarBandejaResult.State.ToString(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error al registrar carga. Error:" + error.Message);
                    throw;
                }
                

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
