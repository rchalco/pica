using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace IniciaCAI
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (IsExecutingApplication())
                {
                    Application.Exit();
                }
                else
                {
                    Application.Run(new frmInicioCAI());
                }
            }
            catch (Exception error)
            {

                MessageBox.Show(error.Message, "Error al iniciar MAIN", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
        }
        private static bool IsExecutingApplication()
        {
            try
            {
                Process currentProcess = Process.GetCurrentProcess();

                // Matriz de procesos
                Process[] processes = Process.GetProcesses();

                // Recorremos los procesos en ejecución
                foreach (Process p in processes)
                {
                    if (p.Id != currentProcess.Id)
                    {
                        if (p.ProcessName == currentProcess.ProcessName)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error al verificar app", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            // Proceso actual
            
        }

    }
}
