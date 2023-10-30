using RegisterLogger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NMDManagement.Administration
{
    public partial class MDIAdmin : Form
    {
        private int childFormNumber = 0;
        private string token=string.Empty;
        private long IdPerson=0;
        private string CI=string.Empty;
        private string LoginUser =string.Empty;
        public  ResultConfgATM globalConfigATM;
        int IdATM = 0;
        Int64 IdAccessATM = 0;
        public MDIAdmin(UserData userData, string pToken, string pLoginUser)
        {
            InitializeComponent();
            lblCargp.Text = userData.UserRole;
            lblusuario.Text = userData.UserPersonName ;
            this.token = pToken;
            IdPerson = userData.IdPerson;
            CI = userData.UserCI ;
            LoginUser = pLoginUser;
            services servicios = new services();
            globalConfigATM = servicios.GetHotState();
            //string jsonContent = File.ReadAllText(@"c:\hla\ATM.json");
            //globalConfigATM = Newtonsoft.Json.JsonConvert.DeserializeObject<GlobalConfigATM>(jsonContent);
            IdATM = globalConfigATM.GetHotStateResult.Object.Id;
            IdAccessATM = userData.IdATMAccess;
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

     

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void btnCarga_Click(object sender, EventArgs e)
        {
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "DispenserLoad").SingleOrDefault<Form>();
            if (existe != null)

            {
                return;
            }
            services service = new services();
            ATMLoadSolicitationPending recordLoad = service.ATMLoadSolicitationGetPending(globalConfigATM.GetHotStateResult.Object.Id , token);
            if (recordLoad.ATMLoadSolicitationGetPendingForATMResult.State == ResponseType.Success)
            {
                DispenserLoad dispenserLoad = new DispenserLoad(token, IdPerson, CI, LoginUser);
                dispenserLoad.MdiParent = this;
                dispenserLoad.Show();
            }
            else
            {
                MessageBox.Show("Estado:" + recordLoad.ATMLoadSolicitationGetPendingForATMResult.State.ToString() + "\nDescripción:" + recordLoad.ATMLoadSolicitationGetPendingForATMResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //clPrint clprint = new clPrint();
            //List<BalanceData> balanceDatas = new List<BalanceData>();
            //balanceDatas.Add(new BalanceData { Denomination = 20, IdCount = 1, IdMoney = 1, NroCassette = 1, RegisterBalance = 21});
            //balanceDatas.Add(new BalanceData { Denomination = 100, IdCount = 1, IdMoney = 1, NroCassette = 1, RegisterBalance = 12 });
            //clprint.PrintLoadDispenser(balanceDatas,123145646);
            Application.Exit();
        }

        private void butVerif_Click(object sender, EventArgs e)
        {
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "FrmCheckNMDStatus").SingleOrDefault<Form>();
            if (existe != null)
            {
                return;
            }
            FrmCheckNMDStatus frmCheckNMDStatus = new FrmCheckNMDStatus();
            frmCheckNMDStatus.MdiParent = this;
            frmCheckNMDStatus.Show();
        }

        private void btnArqueo_Click(object sender, EventArgs e)
        {
            services service=new services();
            ResultRecordForBalance recordForBalance = service.GetRecordForBalance(IdATM, token);
            if (recordForBalance.GetATMInformationToCashCountByIdATMResult.State == ResponseType.Success)
            {
                Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "FrmDispencerAccountingBalance").SingleOrDefault<Form>();
                if (existe != null)

                {
                    return;
                }
                FrmDispencerAccountingBalance frmDispencerAccountingBalance = new FrmDispencerAccountingBalance(token, LoginUser);
                frmDispencerAccountingBalance.MdiParent = this;
                frmDispencerAccountingBalance.Show();
            }
            else
            {
                MessageBox.Show("No se tiene programado realizar arqueo en el  ATM \nEstado:" + recordForBalance.GetATMInformationToCashCountByIdATMResult.State.ToString() + "\nDescripción:" + recordForBalance.GetATMInformationToCashCountByIdATMResult.Message , "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            service = null;
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
        frmConfigWizard frmConfigWizard = new frmConfigWizard(token );
            frmConfigWizard.MdiParent=this;
            frmConfigWizard.Show();
        }

        private void btnCalibrateDispenser_Click(object sender, EventArgs e)
        {
            FrmCalibrateDispenser frmCalibrateDispenser = new FrmCalibrateDispenser(token, CI);
            frmCalibrateDispenser.MdiParent = this;
            frmCalibrateDispenser.Show();
        }

        private void MDIAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                services servicios = new services();

                ResponseValidateFingerClose responseClose = servicios.ValidateUserAccessClose(IdAccessATM, token);
                if (responseClose.ATMAccessHistoryCloseResult.State == ResponseType.Success )
                {
                    loggerATM.PsRegisterLogger("FrmClosed", "Se registro la salida de la aplicación de manera correcta" );
                }
                else
                {
                    loggerATM.PsRegisterLogger("FrmClosed", "Error al registrar salida. Error:" + responseClose.ATMAccessHistoryCloseResult.Message );
                    MessageBox.Show("Error al cerrar la aplicación. \nIdAccess:"+ IdAccessATM.ToString()  +"\nError:" + responseClose.ATMAccessHistoryCloseResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al cerrar la aplicación. Error:" + error.Message, "Aviso", MessageBoxButtons.OK , MessageBoxIcon.Exclamation);
                
            }
            
        }
    }
}
