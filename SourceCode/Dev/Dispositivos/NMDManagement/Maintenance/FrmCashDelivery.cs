using RegisterLogger;


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NMDManagement
{
    public partial class FrmCashDelivery : Form
    {
        public FrmCashDelivery()
        {
            InitializeComponent();
        }

        private void txtBandeja1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&(e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            ////txtBandeja1.Text = "";
            //txtBandeja2.Text = "";
            //txtBandeja3.Text = "";
            //txtBandeja4.Text = "";
        }

        private void txtBandeja2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            //txtBandeja1.Text = "";
            ////txtBandeja2.Text = "";
            //txtBandeja3.Text = "";
            //txtBandeja4.Text = "";
        }

        private void txtBandeja3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            //txtBandeja1.Text = "";
            //txtBandeja2.Text = "";
            ////txtBandeja3.Text = "";
            //txtBandeja4.Text = "";
        }

        private void txtBandeja4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            //txtBandeja1.Text = "";
            //txtBandeja2.Text = "";
            //txtBandeja3.Text = "";
            ////txtBandeja4.Text = "";
        }

        private void btnEntrega_Click(object sender, EventArgs e)
        {
            services servicios = new services();
            string entrega = "";
            if (txtBandeja1.Text != "")
            {
                entrega += "1" + (txtBandeja1.TextLength == 1 ? "00" + txtBandeja1.Text : "0" + txtBandeja1.Text);
            }

            if (txtBandeja2.Text != "")
            {
                entrega += "2" + (txtBandeja2.TextLength == 1 ? "00" + txtBandeja2.Text : "0" + txtBandeja2.Text);
            }
            if (txtBandeja3.Text != "")
            {
                entrega += "3" + (txtBandeja3.TextLength == 1 ? "00" + txtBandeja3.Text : "0" + txtBandeja3.Text);
            }
            if (txtBandeja4.Text != "")
            {
                entrega += "4" + (txtBandeja4.TextLength == 1 ? "00" + txtBandeja4.Text : "0" + txtBandeja4.Text);
            }

            ResponseDispensarCMD  responseCT = servicios.DeliveryCashCMD(entrega);
            if (responseCT.DispensarCmdResult.State == ResponseType.Success)
            {
                MessageBox.Show("Billete entregado.\n" + responseCT.DispensarCmdResult.Message+ "(" + responseCT.DispensarCmdResult.ListEntities[0].Result.ResponseOriginal + ")\n\n" + entrega , "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Billete entregado.\n" + responseCT.DispensarCmdResult.Message + "\n" + responseCT.DispensarCmdResult.ListEntities[0].Result.ResponseOriginal  + "\n\n" + entrega, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            ////if (tipoDispensador.Tipo == "NMD100")
            ////{
            ////    StatusDispenser statusDispenser2 = RuntimeDispensador.Core.ExecutorCommand.ejecutar(Comando.Liberar);
            ////    txtmsg = "Liberar billete\n"+ statusDispenser2.Description + "(" + statusDispenser2.ResponseOriginal + ")";
            ////}
            ////if (statusDispenser1.state == ResponseDispensador.Exito)
            ////{
            ////    MessageBox.Show("Billete entregado.\n" + statusDispenser1.Description  + "("+ statusDispenser1.ResponseOriginal +")\n\n" + txtmsg   , "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////}
            ////else
            ////{
            ////    MessageBox.Show("Billete entregado.\n" + statusDispenser1.Description +"\n" + statusDispenser1.ResponseOriginal +"\n\n" + txtmsg , "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            ////}
        }

        private void btnCerrarVentana_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHabilitar_Click(object sender, EventArgs e)
        {
            services servicios = new services();

            this.Cursor = Cursors.WaitCursor;
            loggerATM.PsRegisterLogger("CashDelivery", "Llamada a dispensador para leer ID de bandejas");

            //StatusDispenser statusDispenser1 = new StatusDispenser();// ExecutorCommand.ejecutar(Comando.AbrirBandejas );
            ResponseInit responseInit = servicios.initializa_dispenser();
            
            if (responseInit.InicializarResult.State == ResponseType.Success || responseInit.InicializarResult.State  == ResponseType.Warning )
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Proceso realizado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //StatusDispenser statusDispenser22 =new StatusDispenser();// ExecutorCommand.ejecutar(Comando.Reset);
                //if (statusDispenser22.state == ResponseDispensador.Exito || statusDispenser22.state == ResponseDispensador.Advertencia)
                //{
                //    StatusDispenser statusDispenser3 = new StatusDispenser();// ExecutorCommand.ejecutar(Comando.LeeIdBandeja);
                //    if (statusDispenser3 == null)
                //    {
                //        loggerATM.PsRegisterLogger("CallFunctionNMD", "respueta vacia de dispensador");

                //    }
                //    else if (statusDispenser3.state == ResponseDispensador.Exito || statusDispenser3.state == ResponseDispensador.Advertencia)
                //    {
                //        this.Cursor = Cursors.Default;
                //        MessageBox.Show("Proceso realizado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    }
                //    else
                //    {
                //        this.Cursor = Cursors.Default;
                //        MessageBox.Show("Error la leer ID de bandeja. Error:" + statusDispenser3.Description + '\n' + statusDispenser3.ResponseOriginal, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    }
            //}
                //else
                //{
                //    this.Cursor = Cursors.Default;
                //    MessageBox.Show("Error al realizar reset. Error:" + responseInit.InicializarResult.Message  + '\n' + responseInit.InicializarResult.ListEntities[0].Result.ResponseOriginal , "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            else
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error al cerrar bandejas. Error:" + responseInit.InicializarResult.Message + '\n' + responseInit.InicializarResult.ListEntities[0].Result.ResponseOriginal , "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Cursor = Cursors.Default ;
        }

        private void btnLiberaBandeja_Click(object sender, EventArgs e)
        {
            try
            {
                //StatusDispenser statusDispenser1 = new StatusDispenser();// ExecutorCommand.ejecutar(Comando.CerrarBandejas);
                services ser = new services();
                ResponseReleaseCassette responseReleaseCassette = ser.ReleaseCassette(); 
                MessageBox.Show("Bandejas liberadas.\n\n" + responseReleaseCassette.LiberarBandejaResult.Message  + "(" + responseReleaseCassette.LiberarBandejaResult.ListEntities[0].Result.ResponseOriginal  + ")");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar operación. Error:" + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}
