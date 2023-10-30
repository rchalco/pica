using Monitoreo.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monitor
{
    public partial class FrmMain : Form
    {
        Monitor.ServiceMonitoreo.ResponseQueryOfPublisherDTO2XnGHQYa resulServices;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIP.Text.Trim()))
            {
                MessageBox.Show("Debe ingrear el IP o serial para continuar");
                return;
            }

            using (ServiceMonitoreo.HandlerMonitoreoClient client = new ServiceMonitoreo.HandlerMonitoreoClient())
            {
                resulServices = client.GetPunblisherByIdentifier(txtIP.Text.Trim());
                if (resulServices.State == ServiceMonitoreo.ResponseType.Success)
                {
                    dgPublisher.DataSource = resulServices.ListEntities;
                    pnlMain.Visible = true;
                    var resulFinger = client.GetStateFingerPrint(txtIP.Text.Trim());
                    rtHuella.Text = resulFinger.Message + ":" + resulFinger.Object;
                }
                else
                {
                    MessageBox.Show("El IP o serial: " + txtIP.Text.Trim() + "No se encuentra registrado en la Central del Hangar");
                }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(cmbTipoLector.Text))
            {
                MessageBox.Show("Debe elegir un tipo de lector");
                return;
            }
            using (ServiceMonitoreo.HandlerMonitoreoClient client = new ServiceMonitoreo.HandlerMonitoreoClient())
            {
                string tipoCardReader = cmbTipoLector.Text.Equals("CREATOR - ATM") ? "1" : "3";
                var resulTarjeta = client.GetStateCardReader(txtIP.Text.Trim(), tipoCardReader);
                rtCardReader.Text = resulTarjeta.Message + ":" + resulTarjeta.Object;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbTipoLector.Text))
            {
                MessageBox.Show("Debe elegir un tipo de lector");
                return;
            }
            using (ServiceMonitoreo.HandlerMonitoreoClient client = new ServiceMonitoreo.HandlerMonitoreoClient())
            {
                string tipoCardReader = cmbTipoLector.Text.Equals("CREATOR - ATM") ? "1" : "3";
                var resulTarjeta = client.ReadCard(txtIP.Text.Trim());
                rtCardReader.Text = resulTarjeta.Message + ":" + resulTarjeta.Object;
            }
        }

        private void Expulsar(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbTipoLector.Text))
            {
                MessageBox.Show("Debe elegir un tipo de lector");
                return;
            }
            using (ServiceMonitoreo.HandlerMonitoreoClient client = new ServiceMonitoreo.HandlerMonitoreoClient())
            {
                string tipoCardReader = cmbTipoLector.Text.Equals("CREATOR - ATM") ? "1" : "3";
                var resulTarjeta = client.EjectCard(txtIP.Text.Trim());
                rtCardReader.Text = resulTarjeta.Message + ":" + resulTarjeta.Object;
            }
        }
    }
}
