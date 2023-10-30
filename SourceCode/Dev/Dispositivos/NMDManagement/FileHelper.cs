using Newtonsoft.Json;
using RegisterLogger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NMDManagement
{
    internal class FileHelper
    {
        public static void updateStateATM(GlobalConfigATM globalConfigATM)
        {
            try
            {
                File.Delete(@"C:\HLA\ATM.json");
                Thread.Sleep(1000);
                
                string jsonText = JsonConvert.SerializeObject(globalConfigATM);
                loggerATM.PsRegisterLogger("updateStateATM", jsonText);
                //File.Write(@"c:\hla\ATM.json", jsonText);
                using (StreamWriter streamWriter = new StreamWriter(@"C:\HLA\ATM.json", append: false))
                {
                    streamWriter.WriteLine(jsonText);
                }


            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}
