using Interop.Main.Service.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM.ControlPanel.Core
{
    public class DeamonHotKey
    {
        globalKeyboardHook gkh = new globalKeyboardHook();
        public DeamonHotKey()
        {
            gkh.HookedKeys.Add(Keys.F6);
            gkh.HookedKeys.Add(Keys.F7);
            gkh.HookedKeys.Add(Keys.F12);

            gkh.KeyDown += new KeyEventHandler(gkh_KeyPress);


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
                    MainControlPanel mainControl = new MainControlPanel();
                    mainControl.Show();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al correr NMDManagement.exe. Error:" + error.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }


    }
}
