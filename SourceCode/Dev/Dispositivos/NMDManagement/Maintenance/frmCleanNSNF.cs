using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NMDManagement.Maintenance
{
    public partial class frmCleanNSNF : Form
    {
        public frmCleanNSNF()
        {
            InitializeComponent();
        }

        private void btnLimpiarNS_Click(object sender, EventArgs e)
        {
            services service =new services();
            Cursor = Cursors.WaitCursor;
            ResultCleanNS  respSevicio = service.LImpiarNS();
            Cursor = Cursors.Default ;
            if (respSevicio.LimpiarNSRollersResult.State == ResponseType.Success)
            {
                MessageBox.Show("Comando ejecutado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al ejecutar comando.\nError:" + respSevicio.LimpiarNSRollersResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnNF1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            services service = new services();
            ResultCleanNF  respSevicio = service.LImpiarNF(1);
            Cursor = Cursors.Default;
            if (respSevicio.LimpiarNFResult .State == ResponseType.Success)
            {
                MessageBox.Show("Comando ejecutado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al ejecutar comando.\nError:" + respSevicio.LimpiarNFResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnNF2_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            services service = new services();
            ResultCleanNF respSevicio = service.LImpiarNF(2);
            Cursor = Cursors.Default ;
            if (respSevicio.LimpiarNFResult.State == ResponseType.Success)
            {
                MessageBox.Show("Comando ejecutado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al ejecutar comando.\nError:" + respSevicio.LimpiarNFResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnNF3_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            services service = new services();
            ResultCleanNF respSevicio = service.LImpiarNF(3);
            Cursor = Cursors.Default ;
            if (respSevicio.LimpiarNFResult.State == ResponseType.Success)
            {
                MessageBox.Show("Comando ejecutado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al ejecutar comando.\nError:" + respSevicio.LimpiarNFResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnNF4_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            services service = new services();
            Cursor = Cursors.Default ;
            ResultCleanNF respSevicio = service.LImpiarNF(4);
            if (respSevicio.LimpiarNFResult.State == ResponseType.Success)
            {
                MessageBox.Show("Comando ejecutado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al ejecutar comando.\nError:" + respSevicio.LimpiarNFResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
