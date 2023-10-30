using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NPM_GUI
{
    public partial class Form1 : Form
    {
        NpmHelper npmHelperDev = new NpmHelper();
        NpmHelper npmHelperGenerar = new NpmHelper();
        NpmHelper npmHelperElectron = new NpmHelper();
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            txtElectron.Text = txtServe.Text = string.Empty;
            npmHelperDev.StartDev(txtServe, txtElectron);
        }

        private void btnFinDev_Click(object sender, EventArgs e)
        {
            npmHelperDev.EndProcess();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            txtGenerar.Text = string.Empty;
            if (!string.IsNullOrEmpty(txtNombreComponente.Text))
            {
                npmHelperGenerar.GenerarComponente(txtNombreComponente.Text, txtGenerar);
            }
            else
            {
                MessageBox.Show("debe ingresar el nombre del componente a crear!");
            }
        }

        private void bntElectron_Click(object sender, EventArgs e)
        {
            txtOblyElectron.Text = string.Empty;
            npmHelperElectron.StartOnlyElectron(txtOblyElectron);
        }

        private void btnFinProd_Click(object sender, EventArgs e)
        {
            npmHelperElectron.EndProcess();
        }

        private void btnServicio_Click(object sender, EventArgs e)
        {
            txtServicioResul.Text = string.Empty;
            if (!string.IsNullOrEmpty(txtServicio.Text))
            {
                npmHelperGenerar.GenerarServicio(txtServicio.Text, txtServicioResul);
            }
            else
            {
                MessageBox.Show("debe ingresar el nombre del servicio a crear!");
            }
        }
    }
}
