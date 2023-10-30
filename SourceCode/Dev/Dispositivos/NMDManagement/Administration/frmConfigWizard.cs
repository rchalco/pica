using RegisterLogger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NMDManagement.Administration
{
    public partial class frmConfigWizard : Form
    {

        string Token = "";
        
        ConfigurationATM configurationATM;
        public frmConfigWizard(string pToken)
        {
            Token = pToken;
            InitializeComponent();
        }

        private void butFindATM_Click(object sender, EventArgs e)
        {
            int idATM = 0;
            try
            {
                idATM = Convert.ToInt32(cmbATMs.SelectedValue );
                services service = new services();

               
                Cursor.Current = Cursors.WaitCursor;
                configurationATM = service.GetATM(Token, idATM);
                if(configurationATM.GetATMByIdResult.State ==ResponseType.Success)
                {

                    lblNameATM.Text = configurationATM.GetATMByIdResult.Object.Profile.Code;
                    lblDescription.Text = configurationATM.GetATMByIdResult.Object.Name ;
                    lblIP.Text = configurationATM.GetATMByIdResult.Object.IP;
                    lblIdOffice.Text = configurationATM.GetATMByIdResult.Object.IdOffice.ToString();
                    lblNCassettes.Text = configurationATM.GetATMByIdResult.Object.Configuration.configDispenserStatus.Cassettes.Count.ToString();
                    dgvCassette.DataSource = configurationATM.GetATMByIdResult.Object.Configuration.configDispenserStatus.Cassettes;

                    string[] puertos = SerialPort.GetPortNames();
                    foreach(string puerto in puertos)
                    {
                        cbxPorts.Items.Add(puerto);
                        cmbPuertoReceptor.Items.Add(puerto);
                    }
                    groupBox1.Visible = true;
                }
                else
                {
                    MessageBox.Show("No existe ATM para el ID digitado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                Cursor.Current = Cursors.Default;
                
            }
            catch (Exception err)
            {

                MessageBox.Show("Error en la búsqueda de ATM\nError:" + err.Message,"Aviso",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void wizardControl1_Finished(object sender, EventArgs e)
        {
            
        }

        private void wizardPage1_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            //validar
            if (groupBox1.Visible)
            { 
                if(cbxPorts.Text=="")
                {
                    MessageBox.Show("Selecciones puerto COM para el dispensador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Cancel= true;
                }
                if(cmbTypeDispenser.Text =="")
                {
                    MessageBox.Show("Selecciones el tipo el dispensador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Cancel = true;
                }
            }
            else
            {
                MessageBox.Show("Realizar la búsqueda de un ATM validado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }
        }

        private void wizardPage2_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if (cmbCardReader.Text=="")
            {
                MessageBox.Show("Selecciones tipo de lector de tarjeta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }
            try
            {
                loggerATM.PsRegisterLogger("wizardPage2", "Ingresando a generar archivo json");
                GlobalConfigATM globalConfigATM = new GlobalConfigATM();
                globalConfigATM.Name = configurationATM.GetATMByIdResult.Object.Name;
                globalConfigATM.Id = configurationATM.GetATMByIdResult.Object.IdATM;
                globalConfigATM.IdOffice = configurationATM.GetATMByIdResult.Object.IdOffice;
                //CARDREADER
                globalConfigATM.configCardReader = new GlobalConfigATM.ConfigCardReader();
                globalConfigATM.configCardReader.NameCarderReader = cmbCardReader.Text;
                globalConfigATM.configCardReader.IdTypeCardReader = cmbCardReader.Text == "CREATOR" ? 1 : 3;
                globalConfigATM.configCardReader.SerialNumber = "";
                globalConfigATM.configCardReader.HasCarReader = rbActCardReader.Checked;
                //port dispenser
                globalConfigATM.configDispenserCOM = new GlobalConfigATM.ConfigDispenserCOM();
                globalConfigATM.configDispenserCOM.Paridad = 2;
                globalConfigATM.configDispenserCOM.ReadTimeout = 180000;
                globalConfigATM.configDispenserCOM.WriteTimeout = 180000;
                globalConfigATM.configDispenserCOM.Baudios = 9600;
                globalConfigATM.configDispenserCOM.SerialNumber = "";
                globalConfigATM.configDispenserCOM.Data = 7;
                globalConfigATM.configDispenserCOM.HasDispenser = true;
                globalConfigATM.configDispenserCOM.Portname = cbxPorts.Text;

                loggerATM.PsRegisterLogger("wizardPage2", "PuertoCOM=" + globalConfigATM.configDispenserCOM.Portname);
                configurationATM.GetATMByIdResult.Object.Configuration.configDispenserCOM.HasDispenser = true;
                configurationATM.GetATMByIdResult.Object.Configuration.configDispenserCOM.Portname = cbxPorts.Text;
                configurationATM.GetATMByIdResult.Object.Configuration.configDispenserStatus.HasDispenser = true;
                configurationATM.GetATMByIdResult.Object.Configuration.configDispenserStatus.Tipo=cmbTypeDispenser.Text;

                configurationATM.GetATMByIdResult.Object.Configuration.configCardReader.HasCarReader= rbActCardReader.Checked;
                configurationATM.GetATMByIdResult.Object.Configuration.configCardReader.NameCarderReader = cmbCardReader.Text;
                configurationATM.GetATMByIdResult.Object.Configuration.configCardReader.IdTypeCardReader= cmbCardReader.Text == "CREATOR" ? 1 : 3;

                configurationATM.GetATMByIdResult.Object.Configuration.configReceptorCOM = new ATMConfig.ConfigReceptorCOM();
                configurationATM.GetATMByIdResult.Object.Configuration.configReceptorCOM.Portname = cmbPuertoReceptor.Text;
                configurationATM.GetATMByIdResult.Object.Configuration.configReceptorCOM.ReadTimeout = 30;
                configurationATM.GetATMByIdResult.Object.Configuration.configReceptorCOM.Model = lblTipoReceptor.Text;
                configurationATM.GetATMByIdResult.Object.Configuration.configReceptorCOM.HasReceptor = rbActReceptor.Checked ;
                configurationATM.GetATMByIdResult.Object.Configuration.configReceptorCOM.SerialNumber = "";

                //cassettes
                globalConfigATM.configDispenserStatus = new GlobalConfigATM.ConfigDispenserStatus();
                globalConfigATM.configDispenserStatus.NumBandejas = configurationATM.GetATMByIdResult.Object.Configuration.configDispenserStatus.NumBandejas;
                globalConfigATM.configDispenserStatus.MaxQuantityBill = configurationATM.GetATMByIdResult.Object.Configuration.configDispenserStatus.MaxQuantityBill;
                globalConfigATM.configDispenserStatus.HasDispenser = true;
                globalConfigATM.configDispenserStatus.Tipo = cmbTypeDispenser.Text;
                loggerATM.PsRegisterLogger("wizardPage2", "Tipo dispensador" + globalConfigATM.configDispenserStatus.Tipo);
                globalConfigATM.configDispenserStatus.Cassettes = new List<Cassette>();
                foreach (NMDManagement.ATMConfig.Cassette cassette in configurationATM.GetATMByIdResult.Object.Configuration.configDispenserStatus.Cassettes)
                {
                    if (cassette.CurrencyCourt != 0)
                    {
                        globalConfigATM.configDispenserStatus.Cassettes.Add(new Cassette { CurrencyCourt = cassette.CurrencyCourt, Id = cassette.Id, MinQuantity = cassette.MinQuantity, Sequence = cassette.Sequence, Status = cassette.Status, TotalQuantity = cassette.TotalQuantity });
                    }
                }

                ///fingerprint
                globalConfigATM.configFingerPrintReader = new GlobalConfigATM.ConfigFingerPrintReader();
                globalConfigATM.configFingerPrintReader.IdTypeFingerPrint = 1;
                globalConfigATM.configFingerPrintReader.NameFingerPrint = "DIGITAL PERSONA";
                globalConfigATM.configFingerPrintReader.SerialNumber = "";
                globalConfigATM.configFingerPrintReader.HasFingerPrint = rbActFinger.Checked;
                //Configurar receptor
                globalConfigATM.configReceptorCOM  = new GlobalConfigATM.ConfigReceptor();
                globalConfigATM.configReceptorCOM.HasReceptor= rbActReceptor.Checked;
                globalConfigATM.configReceptorCOM.ReadTimeout = 30;
                globalConfigATM.configReceptorCOM.SerialNumber = "";
                globalConfigATM.configReceptorCOM.Model=lblTipoReceptor.Text;
                globalConfigATM.configReceptorCOM.Portname=cmbPuertoReceptor.Text;

                services service = new services();
                
                ResultUpdateATM resultUpdateATM = service.UpadateATM(configurationATM.GetATMByIdResult.Object , Token);
                
                FileHelper.updateStateATM(globalConfigATM);

                RestultIniATM restultIniATM = service.InitATM(Token);

                loggerATM.PsRegisterLogger("wizardPage2", "Actualizado");
                MessageBox.Show("Se creo archivo JSON con los datos configurados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void frmConfigWizard_Load(object sender, EventArgs e)
        {
            services service = new services();
            AllATMResult allATMResult = service.GetAllATM(Token);
            GetRedData getRedData = new GetRedData();
            string ipLocal = getRedData.GetIP2();

            
            cmbATMs.DataSource = allATMResult.GetAllATMResult.ListEntities.Where(x=>x.IP==ipLocal).ToList();    
            cmbATMs.DisplayMember = "name";
            cmbATMs.ValueMember = "IdATM";
            
         
         }

        private void wizardControl1_Cancelling(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de cancelar la configuración?","Aviso",MessageBoxButtons.YesNo,MessageBoxIcon.Question )==DialogResult.Yes)
            {
                this.Close();
            }
            
        }
    }
}
