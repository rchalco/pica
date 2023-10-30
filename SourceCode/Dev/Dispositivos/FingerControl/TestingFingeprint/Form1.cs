using DPUruNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prodem.Fingerprint.FingerPrintControl.FingerEnroll;
using Prodem.Fingerprint.FingerPrintControl.Domain;
using Finger.Component.Util;
using System.ServiceModel;

namespace FingerPrintControl
{
    public partial class Form1 : Form
    {
        //private FingerEnroll.EnrollOneFingerControl ctrlEnrollOne;
        private DataResult<Fmd> resul;
        private TypeEnroll _TypeEnroll = TypeEnroll.DP;
        GetFingerControl ctrlGetOneFinger = null;
        EnrollOneFingerControl ctrlEnrollOne = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void enrollToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            EnrollControl ctrlEnroll = new EnrollControl(_TypeEnroll);
            ctrlEnroll.Dock = DockStyle.Fill;
            panel1.Controls.Add(ctrlEnroll);

        }

        private void compareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            ctrlEnrollOne = new EnrollOneFingerControl(_TypeEnroll);
            ctrlEnrollOne.Dock = DockStyle.Fill;
            ctrlEnrollOne.ReleaseResul = RecieveResul;
            panel1.Controls.Add(ctrlEnrollOne);
            ctrlEnrollOne.CaptureFinger();

        }

        private void RecieveResul(string fingerEnrollResul, byte[] img)
        {
            if (fingerEnrollResul == null)
            {
                MessageBox.Show("imposible enrolar huella");
            }
            else
            {
                resul = new DataResult<Fmd>(Constants.ResultCode.DP_SUCCESS, Fmd.DeserializeXml(fingerEnrollResul));
                MessageBox.Show("Huella Capturada :" + fingerEnrollResul);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ctrlEnrollOne.CaptureFinger();
        }

        private void compareFingerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            CompareFinger ctrlCompareFinger = new CompareFinger(new Fmd[] { resul.Data }, _TypeEnroll);
            ctrlCompareFinger.Dock = DockStyle.Fill;
            ctrlCompareFinger.ReleaseCompare = RecieveResulCompare;
            panel1.Controls.Add(ctrlCompareFinger);
            ctrlCompareFinger.ComparerFingerAction();
        }

        private void RecieveResulCompare(bool resulCompare)
        {
            MessageBox.Show($"Resultado de la comparacion: {resulCompare.ToString()}");
        }

        private void getOneFingerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            if (ctrlGetOneFinger == null)
            {
                ctrlGetOneFinger = new GetFingerControl();
            }
            ctrlGetOneFinger.Dock = DockStyle.Fill;
            ctrlGetOneFinger.ReleaseFingerSerialize = RecieveGetOneFinger;
            panel1.Controls.Add(ctrlGetOneFinger);
            ctrlGetOneFinger.ActiveCapture();
        }


        private void RecieveGetOneFinger(List<string> colFingers)
        {
            //ctrlGetOneFinger.Dispose();
            MessageBox.Show($"captura exitosa");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void elimnarComponentesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ctrlGetOneFinger?.Release();
            ctrlEnrollOne?.Release();
            panel1.Controls.Clear();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ctrlGetOneFinger?.Release();
            ctrlEnrollOne?.Release();
        }

        private void escribirLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.Write("logre escribir");
                MessageBox.Show("Mensaje escrito");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void levantarServicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ServiceHost myServiceHost = new ServiceHost(typeof(Service.FingerCapture));
            //myServiceHost.Open();
        }
    }
}
