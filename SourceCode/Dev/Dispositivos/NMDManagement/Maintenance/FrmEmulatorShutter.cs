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
    public partial class FrmEmulatorShutter : Form
    {
        public FrmEmulatorShutter()
        {
            InitializeComponent();
            InitializeShutter();
        }
        private void InitializeShutter()
        {
            loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para emular shutter");
            services service =new services();
            ResponseReadStatusShutter responseReadStatusShutter = service.ReadStatusShutter();
            if(responseReadStatusShutter.LeeEstadoShutterResult.State== ResponseType.Success)
            {
                if (responseReadStatusShutter.LeeEstadoShutterResult.ListEntities[0].Result.ResponseOriginal.Substring(2,1)=="0")
                {
                    rdbNoShutter.Checked = true;
                }
                else if (responseReadStatusShutter.LeeEstadoShutterResult.ListEntities[0].Result.ResponseOriginal.Substring(2, 1) == "1")
                {
                    rdbType1.Checked = true;
                }
                else
                {
                    rdbType2.Checked = true;
                }
            }
            service = null;
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            loggerATM.PsRegisterLogger("CallFunctionNMD", "Llamada a dispensador para emular shutter");
            string param = rdbNoShutter.Checked ? "0" : rdbType1.Checked ? "1" : "2";
            //StatusDispenser statusDispenser = new StatusDispenser();// ExecutorCommand.ejecutar(Comando.WriteEmulatorShutter , param);
            services service = new services();
            ResponseWriteStatusShutter responseWriteStatusShutter = service.WriteStatusShutter(param);

                 
            if (responseWriteStatusShutter.ActualizaEstadoShutterResult.State == ResponseType.Success)
            {
                MessageBox.Show("Aplicado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error de comando.\n" + responseWriteStatusShutter.ActualizaEstadoShutterResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
