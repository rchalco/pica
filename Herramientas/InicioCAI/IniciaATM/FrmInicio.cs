using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IniciaATM
{
    public partial class frmInicioATM : Form
    {
        public frmInicioATM()
        {
            InitializeComponent();
        }

        private void frmInicioATM_Shown(object sender, EventArgs e)
        {
            this.Refresh();
            //inicioa Hangar
            using (System.Diagnostics.Process process = new System.Diagnostics.Process())
            {
                try
                {
                    Process[] processes = Process.GetProcessesByName("Hangar.Desktop");
                    if (processes.Length == 0)
                    {
                        System.Diagnostics.ProcessStartInfo empaquetaShell = new ProcessStartInfo();
                        empaquetaShell.UseShellExecute = true;
                        empaquetaShell.FileName = "Hangar.Desktop.exe";
                        empaquetaShell.WorkingDirectory = @"C:\HLA";
                        empaquetaShell.Arguments = "start";
                        empaquetaShell.WindowStyle = ProcessWindowStyle.Hidden;

                        process.StartInfo = empaquetaShell;

                        process.Start();
                    }

                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                    throw;
                }

            }
            
            //inicia ATM
            Thread.Sleep(15000);
            this.WindowState = FormWindowState.Minimized;
            cEjecutar ejecutar = new cEjecutar();
            ejecutar.Iniciar(@"C:\electron-atm\electron", @"..\atm\index.html");
            this.WindowState = FormWindowState.Maximized ;
            ejecutar.Iniciar(@"C:\SHUTDOWN.exe", @"/L /R /T:1 /Y /C");
        }
    }
}
