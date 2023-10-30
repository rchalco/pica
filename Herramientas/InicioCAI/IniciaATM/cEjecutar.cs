using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IniciaATM
{
    internal class cEjecutar
    {
        internal bool Iniciar(string pAppName, string pParam)
        {
            bool salida = true;
            string ValorDevuelto = string.Empty;
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(pAppName, pParam);
                //Arch.Escribir("Iniciar", Archivos.enumEstados.e_log);
                psi.RedirectStandardOutput = true;
                psi.WindowStyle = ProcessWindowStyle.Maximized ;
                psi.UseShellExecute = false;
                Process listFiles;

                //Arch.Escribir("Star", Archivos.enumEstados.e_log);
                listFiles = Process.Start(psi);

                StreamReader myOutput = listFiles.StandardOutput;
                SendKeys.Send("{F11}");
                listFiles.WaitForExit();
                if (listFiles.HasExited)
                {
                    ValorDevuelto = myOutput.ReadToEnd();
                    //Arch.Escribir("Respuesta salida:" + ValorDevuelto, Archivos.enumEstados.e_log);
                    salida = true;
                }
                else
                {
                    salida = false;
                    //Arch.Escribir("Respuesta con error:" + myOutput.ToString(), Archivos.enumEstados.e_log);
                }
            }
            catch (Exception)
            {
                salida = false;
                throw;
            }
            return salida;

        }

        public void KillProc(string processname)
        {
            Process[] aProc = Process.GetProcessesByName(processname);
            foreach (Process sProc in aProc)
            {
                sProc.Kill();
            }
        }
    }
}
