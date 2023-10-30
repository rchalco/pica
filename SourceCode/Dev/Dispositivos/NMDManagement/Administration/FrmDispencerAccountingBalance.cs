using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using System.Threading;

namespace NMDManagement.Administration
{
    public partial class FrmDispencerAccountingBalance : Form
    {
        string proccessToken = string.Empty;
        List<BalanceData> balanceDatas ;
        int IdATM = 0;
        long IdATMCashCount = 0;
        string LoginUser = "";
        public FrmDispencerAccountingBalance(string pToken, string pUser)
        {
            proccessToken = pToken;
            LoginUser = pUser;
            InitializeComponent();
        }

        private void FrmDispencerAccountingBalance_Load(object sender, EventArgs e)
        {
            //string jsonContent = File.ReadAllText(@"c:\hla\ATM.json");
            //GlobalConfigATM globalConfigATM = Newtonsoft.Json.JsonConvert.DeserializeObject<GlobalConfigATM>(jsonContent);
            services servicios = new services();
            ResultConfgATM globalConfigATM = servicios.GetHotState();
            IdATM = globalConfigATM.GetHotStateResult.Object.Id;
            bool adiciono = false;
            
            balanceDatas = new List<BalanceData>();
            services service = new services();
            ResultRecordForBalance recordForBalance = service.GetRecordForBalance(IdATM , proccessToken);
            if (recordForBalance.GetATMInformationToCashCountByIdATMResult.State == ResponseType.Success)
            {
                IdATMCashCount = recordForBalance.GetATMInformationToCashCountByIdATMResult.Object.IdATMCashCount ;
                List<Coinage> coinages =  recordForBalance.GetATMInformationToCashCountByIdATMResult.Object.ColCoinage.OrderBy(coinage =>coinage.Value ).ToList();
                
                foreach (Cassette cassette in globalConfigATM.GetHotStateResult.Object.configDispenserStatus.Cassettes)
                {
                    adiciono = false;
                    foreach (Coinage coinage in coinages)
                    {
                        if (cassette.CurrencyCourt == coinage.Value)
                        {
                            balanceDatas.Add(new BalanceData { NroCassette = cassette.Sequence , IdCount = coinage.IdCoinage, Denomination = coinage.Value, RegisterBalance = "0", IdMoney = coinage.IdMoney });
                            adiciono = true;
                        }
                    }

                }
                

                dgvBandejas.DataSource = balanceDatas;
                ResponseReleaseCassette opened = service.ReleaseCassette();
                if (opened.LiberarBandejaResult.State==ResponseType.Success)
                {
                    MessageBox.Show("Bandejas liberadas","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Information );
                }
                else
                {
                    MessageBox.Show("Error al liberar las bandejas\n Error:" + opened.LiberarBandejaResult.Message , "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    return;
                    //this.Close();
                }

            }
            else
            {
                MessageBox.Show("Estado:" + recordForBalance.GetATMInformationToCashCountByIdATMResult.State.ToString() + "\nDescripción:" + recordForBalance.GetATMInformationToCashCountByIdATMResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
        }

        private void btnRegistraArqueo_Click(object sender, EventArgs e)
        {
            services service = new services();
            if (MessageBox.Show("Se registró los datos correctos?","Pregunta",MessageBoxButtons.YesNo ,MessageBoxIcon.Question )==DialogResult.Yes )
            {
                try
                {
                    ATMCashCountDTO ATMCashCount = new ATMCashCountDTO();
                    ATMCashCount.objATMCashCountDTO = new ATMCashCountDTOa();
                    ATMCashCount.objATMCashCountDTO.Observations = txtObs.Text;
                    ATMCashCount.objATMCashCountDTO.IdAtmCashCount = IdATMCashCount;
                    ATMCashCount.objATMCashCountDTO.ColAtmCashCountDetail = new List<ATMCashCountDetailDTO>();
                    foreach (BalanceData balanceData in balanceDatas)
                    {
                        ATMCashCount.objATMCashCountDTO.ColAtmCashCountDetail.Add(new ATMCashCountDetailDTO { IdCoinage = balanceData.IdCount, IdMoney = balanceData.IdMoney, Quantity = Convert.ToInt32 ( balanceData.RegisterBalance) });
                    }


                    ResponseObject responseObject = service.InsertRecordForBalance(proccessToken, ATMCashCount);

                    if (responseObject.InsertAtmCashCountDetailResult.State== ResponseType.Success )
                    {
                        clPrint clprint = new clPrint();
                        clprint.PrintBalance(balanceDatas, IdATMCashCount, LoginUser );
                        MessageBox.Show("Registro correcto");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(responseObject.InsertAtmCashCountDetailResult.Message , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                

            }
        }

        private void dgvBandejas_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            decimal num = 0;
            if (dgvBandejas.Columns[e.ColumnIndex ].HeaderText=="Arqueo")
            {
                
                if( decimal.TryParse( dgvBandejas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() , out num ))
                {
                    if (num < 0)
                    {
                        dgvBandejas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                    }
                }
                else
                {
                    dgvBandejas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                }
            }
        }

        private void dgvBandejas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            decimal num = 0;
            if (dgvBandejas.Columns[e.ColumnIndex].HeaderText == "Arqueo")
            {

                if (decimal.TryParse(dgvBandejas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out num))
                {
                    if (num < 0)
                    {
                        dgvBandejas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                    }
                }
                else
                {
                    dgvBandejas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                }
            }
        }

        private void dgvBandejas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            decimal num = 0;
            try
            {
                if (dgvBandejas.Columns[e.ColumnIndex].HeaderText == "Arqueo")
                {

                    if (decimal.TryParse(dgvBandejas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out num))
                    {
                        if (num < 0)
                        {
                            dgvBandejas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                        }
                    }
                    else
                    {
                        dgvBandejas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }
            
        }

        private void dgvBandejas_CellErrorTextChanged(object sender, DataGridViewCellEventArgs e)
        {
            decimal num = 0;
            try
            {
                if (dgvBandejas.Columns[e.ColumnIndex].HeaderText == "Arqueo")
                {

                    if (decimal.TryParse(dgvBandejas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out num))
                    {
                        if (num < 0)
                        {
                            dgvBandejas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                        }
                    }
                    else
                    {
                        dgvBandejas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                    }
                }
            }
            catch (Exception)
            {
                dgvBandejas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                //throw;
            }
        }

        private void btnCloseCassette_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Se insertó las bandejas de manera correcta?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                File.Delete(@"C:\HLA\ATM.json");
                Thread.Sleep(1000);
                services service = new services();

                RestultIniATM restultIniATM = service.InitATM(proccessToken );
                this.Close();
            }
        }
    }
    public class BalanceData
    {
        public long NroCassette { get; set; }
        public long IdCount { get; set; }
        public long IdMoney { get; set; }   
        public long Denomination { get; set; }
        
        public string RegisterBalance { get; set; }
    }
}
