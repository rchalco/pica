using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace FondoDePantalla
{
    public partial class fondo : Form
    {
        globalKeyboardHook gkh = new globalKeyboardHook();
        private System.Timers.Timer aTimer= new System.Timers.Timer();
        public fondo()
        {
            InitializeComponent();
        }

        private void fondo_Load(object sender, EventArgs e)
        {
            try
            {
                gkh.HookedKeys.Add(Keys.F6);
                gkh.HookedKeys.Add(Keys.F7);
                gkh.HookedKeys.Add(Keys.F12);
                gkh.KeyDown += new KeyEventHandler(gkh_KeyPress);

            }
            catch (Exception error)
            {
                MessageBox.Show("Error al activar hotkeys. Error:" + error.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
          
        }

        private  void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //MessageBox.Show("se minimizará");
            this.WindowState = FormWindowState.Minimized;
            aTimer.Enabled = false;

        }
        private void gkh_KeyPress(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode.ToString() == "F6")// ejecutar NMDManagement
                {
                    ProcessStartInfo info = new ProcessStartInfo();
                    info.UseShellExecute = true;
                    info.FileName = "NMDManagement.exe";
                    info.WorkingDirectory = "C:\\NMDManagement";

                    Process.Start(info);
                }
                else if (e.KeyCode.ToString() == "F7")//reiniciar equipo
                {
                    ProcessStartInfo info = new ProcessStartInfo();
                    info.UseShellExecute = true;
                    info.FileName = "SHUTDOWN.exe";
                    info.WorkingDirectory = "C:";
                    info.Arguments = @"/L /R /T:1 /Y /C";

                    Process.Start(info);
                }
                else if (e.KeyCode.ToString() == "F12")//mostrar pantalla de panel
                {
                    MessageBox.Show("F12", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al correr NMDManagement.exe. Error:" + error.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void fondo_Load_1(object sender, EventArgs e)
        {
            try
            {
                gkh.HookedKeys.Add(Keys.F6);
                gkh.HookedKeys.Add(Keys.F7);
                gkh.HookedKeys.Add(Keys.F12);
                gkh.KeyDown += new KeyEventHandler(gkh_KeyPress);

            }
            catch (Exception error)
            {
                MessageBox.Show("Error al activar hotkeys. Error:" + error.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            try
            {
                aTimer = new System.Timers.Timer();
                aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                aTimer.Interval = 60000;
                aTimer.Enabled = true;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al activar Timer. Error:" + error.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
    }
}
