using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NPM_GUI
{
    public class NpmHelper
    {
        public const string NPM_START = "npm start";
        public const string NPM_ELCTRON = "npm run start:electron";
        public const string NPM_GENERATE_COMPONENT = "ng generate component ";
        public const string NPM_GENERATE_SERVICE = "ng generate service ";
        private List<Process> IdProcess = new List<Process>();
        private Dictionary<Process, TextBox> salidas = new Dictionary<Process, TextBox>();
        public NpmHelper()
        {

        }
        private void execProcess(string comandString, TextBox txt)
        {
            try
            {
                Process p = new Process();
                // set start info
                p.StartInfo = new ProcessStartInfo("cmd.exe")
                {
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    WorkingDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath)
                };
                // event handlers for output & error
                p.OutputDataReceived += p_OutputDataReceived;
                p.ErrorDataReceived += p_ErrorDataReceived;

                // start process
                p.Start();
                IdProcess.Add(p);
                salidas.Add(p, txt);
                // send command to its input
                p.StandardInput.Write(comandString + p.StandardInput.NewLine);
                //wait
                //p.WaitForExit();
                txt.Text = $"{comandString} sucess";
            }
            catch (Exception ex)
            {
                txt.Text = $"{comandString} error:";
                txt.Text = ex.Message;
            }

        }
        void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Process p = sender as Process;
            if (p == null)
                return;
            salidas[p].Text += e.Data;
        }

        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Process p = sender as Process;
            if (p == null)
                return;
            salidas[p].Text += e.Data;
        }
        public void StartDev(TextBox txtSeve, TextBox txtElectron)
        {
            //run serve
            Task.Run(() =>
             {
                 execProcess(NPM_START, txtSeve);
             });
            //run electron
            Task.Run(() =>
             {
                 execProcess(NPM_ELCTRON, txtElectron);
             });
        }

        public void GenerarComponente(string nombreComponente, TextBox txtGenerate)
        {
            //run serve
            Task.Run(() =>
            {
                execProcess(NPM_GENERATE_COMPONENT + nombreComponente, txtGenerate);
            });

        }

        public void GenerarServicio(string nombreComponente, TextBox txtGenerate)
        {
            //run serve
            Task.Run(() =>
            {
                execProcess(NPM_GENERATE_SERVICE + nombreComponente, txtGenerate);
            });

        }

        public void StartOnlyElectron(TextBox txtGenerate)
        {
            //run serve
            Task.Run(() =>
            {
                execProcess(NPM_ELCTRON, txtGenerate);
            });

        }

        public void EndProcess()
        {
            IdProcess.ForEach(x =>
            {
                try
                {
                    x.Kill();
                }
                catch
                {
                    ;
                }

            });
            IdProcess = new List<Process>();
            salidas = new Dictionary<Process, TextBox>();
        }
    }
}
