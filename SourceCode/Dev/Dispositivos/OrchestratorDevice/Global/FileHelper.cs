using Interop.Main.Cross.Domain.Orchestrator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Global
{
    public class FileHelper
    {
        public static string writeEvent(string content)
        {
            string path = "c:\\tmp\\" + Foundation.Stone.CrossCuting.Util.CustomGuid.GetGuid();
            if (!Directory.Exists("c:\\tmp\\"))
            {
                Directory.CreateDirectory("c:\\tmp\\");
            }
            File.WriteAllText(path, content);
            return path;
        }

        public static void deleteEvent(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void updateStateATM(GlobalConfigATM globalConfigATM )
        {
            try
            {
                string jsonText = JsonConvert.SerializeObject(globalConfigATM);
                File.WriteAllText(@"c:\hla\ATM.json", jsonText);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
