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
    public partial class FrmCalibrateDispenser : Form
    {
        string Token = string.Empty;
        string CILogin=string.Empty;    
        public FrmCalibrateDispenser(string pToken, string pCILogin)
        {
            InitializeComponent();
            Token = pToken;
            CILogin = pCILogin;
        }
        private void CalibrateDispenser()
        {
            services servicios = new services();

            //GlobalConfigATM globalConfigATM = new GlobalConfigATM();
            List<BalanceData> balanceData = new List<BalanceData>();
            //string jsonContent = File.ReadAllText(@"c:\hla\ATM.json");
            //globalConfigATM = Newtonsoft.Json.JsonConvert.DeserializeObject<GlobalConfigATM>(jsonContent);

            ResultConfgATM globalConfigATM = servicios.GetHotState();
            ResponseDispensarATM delivered =new ResponseDispensarATM();
            int amountToRequest = 0;

            int total = 0;

            if (globalConfigATM.GetHotStateResult.Object.configDispenserStatus.NumBandejas  == 2)
            {
                amountToRequest = 120; // para que dispence un billete de 20 y otro de 100
            }
            else
            {
                amountToRequest = 180; //para que dispence un billete de cada banceja
            }
            Cursor.Current = Cursors.WaitCursor;
            //borrar memoria
            services service =new services();
            ResultClearNQ resultNQ = service.BorraNQ(Token);
            //entrega de efectivo
            if (resultNQ.BorrarNQResult.State== ResponseType.Success)
            {
                //var responseInitATM = service.InitATM(Token);

                delivered = service.DeliveryCashATM (amountToRequest);
                if(delivered.DispensarATMResult.State== ResponseType.Success  || delivered.DispensarATMResult.State == ResponseType.Warning)
                {
                    //foreach (ResultDelivered firstDispenser in delivered.DispensarATMResult.Object.billDelivered.FirstResult)
                    //{
                    //    total = total + (firstDispenser.CurrencyCourt * firstDispenser.Quantity);

                    //}
                    
                    int i = 0;
                    foreach(ResultDelivered deliver in delivered.DispensarATMResult.Object.billDelivered.Delivered  )
                    {
                        total += (deliver.Quantity * deliver.CurrencyCourt);
                        balanceData.Add(new BalanceData { Denomination = deliver.CurrencyCourt, IdCount = deliver.IdMoney, IdMoney = deliver.IdMoney, NroCassette = i++, RegisterBalance = deliver.Quantity.ToString() });
                    }
                    ParameterTicket ticket = new ParameterTicket();
                    ticket.objParameters = new ParameterTicket.Param();
                    ticket.objParameters.IdcTranssactionType = 9190;
                    ticket.objParameters.IdMoney = 1;
                    ticket.objParameters.IdOffice = globalConfigATM.GetHotStateResult.Object.IdOffice;
                    ticket.objParameters.IdATM = globalConfigATM.GetHotStateResult.Object.Id;
                    ticket.objParameters.NameATM = globalConfigATM.GetHotStateResult.Object.Name;
                    ticket.objParameters.IdOffice = globalConfigATM.GetHotStateResult.Object.IdOffice;
                    ticket.objParameters.Observation = "Retoma de calibrado de dispensador";
                    ticket.objParameters.IdentityNumber = CILogin;
                    ticket.objParameters.AmountOperation = total;
                    ticket.objParameters.DescriptionATM = globalConfigATM.GetHotStateResult.Object.Name;
                    ticket.objParameters.NumberOperation = 0;
                    ticket.objParameters.NewConfiguration = "";
                    ticket.objParameters.TraceDevice = "";

                    RecuestRegisterTicket recuestRegisterTicket = service.RegisterTicket(ticket, Token);
                    string numberTicked=string.Empty;
                    if (recuestRegisterTicket.CreateTicketForOperationExternalServiceResult.State != ResponseType.Success)
                    {
                        MessageBox.Show("Error al registrar retoma.\nError: " + recuestRegisterTicket.CreateTicketForOperationExternalServiceResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        numberTicked = recuestRegisterTicket.CreateTicketForOperationExternalServiceResult.Object ;
                    }
                    clPrint clPrint = new clPrint();
                    clPrint.PrintCalibrateDispenser(balanceData, 0,numberTicked  , total, CILogin);
                    MessageBox.Show("Calibración de dispensador realizado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }


            Cursor.Current = Cursors.Default;

        }

        private void btnCalibrar_Click(object sender, EventArgs e)
        {
            CalibrateDispenser();

        }
    }
}
