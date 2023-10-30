using Prodem.Fingerprint.FingerPrintControl.Domain;
using Prodem.Fingerprint.FingerPrintControl.FingerEnroll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HostFingerControl
{
    public partial class MainForm : Form
    {
        public Action<List<string>> ReleaseFingerSerialize { get; set; }
        public Action<string, byte[]> ReleaseFingerEnroll { get; set; }

        private TypeEnroll _TypeEnroll = TypeEnroll.ISO;
        private GetFingerControl ctrlGetOneFinger = null;
        private EnrollOneFingerControl ctrlEnrollOne = null;


        public Mode mode = Mode.None;
        public enum Mode
        {
            None,
            GetFinger,
            Enroll
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        public void IniContainer()
        {
            pnlContainer.Controls.Clear();
            switch (mode)
            {
                case Mode.GetFinger:

                    if (ctrlGetOneFinger == null)
                    {
                        ctrlGetOneFinger = new GetFingerControl();
                    }
                    ctrlGetOneFinger.Dock = DockStyle.Fill;
                    ctrlGetOneFinger.ReleaseFingerSerialize = RecieveGetOneFinger;
                    pnlContainer.Controls.Add(ctrlGetOneFinger);
                    ctrlGetOneFinger.ActiveCapture();
                    break;
                case Mode.Enroll:
                    pnlContainer.Controls.Clear();
                    ctrlEnrollOne = new EnrollOneFingerControl(_TypeEnroll);
                    ctrlEnrollOne.Dock = DockStyle.Fill;
                    ctrlEnrollOne.ReleaseResul = RecieveResul;
                    pnlContainer.Controls.Add(ctrlEnrollOne);
                    ctrlEnrollOne.CaptureFinger();
                    break;
                default:
                    break;
            }
            MessageBox.Show($"Presione \"Aceptar\" para continuar", "Huella", MessageBoxButtons.OK, MessageBoxIcon.Information);
            /*DialogResult result = MessageBox.Show($"¿Desea continuar con la validación por huella?", "Huella", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (!result.Equals(DialogResult.Yes))
            {
                Program.processing = false;
                ReleaseFingerEnroll?.Invoke(null, null);
                ReleaseFingerSerialize?.Invoke(null);
                this.ResumeLayout();
                Release();

            }*/
            Activate();
            Focus();
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
        }

        private void RecieveResul(string fingerEnrollResul, byte[] img)
        {
            Program.processing = false;
            //this.ResumeLayout();
            ReleaseFingerEnroll?.Invoke(fingerEnrollResul, img);

        }

        private void RecieveGetOneFinger(List<string> colFingers)
        {
            Program.processing = false;
            //this.ResumeLayout();
            ReleaseFingerSerialize?.Invoke(colFingers);

        }

        public void Release()
        {
            Hide();
            //ResumeLayout();
            pnlContainer.Controls.Clear();
            ctrlGetOneFinger?.Release();
            ctrlGetOneFinger = null;
            ctrlEnrollOne?.Release();
            ctrlEnrollOne = null;
            this.Deactivate -= new System.EventHandler(this.MainForm_Deactivate);
            GC.Collect();
            
        }


        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                Release();
            }
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            //MessageBox.Show("iiiiipichjhhjk");
            Program.processing = false;
            //this.ResumeLayout();
            Release();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.Focus();
        }

        /*protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            this.Focus();
        }*/



    }
}
