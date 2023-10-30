using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActualizaCAI
{
    internal class cBD
    {
        public bool ActualizaEnBD(string pScript)
        {
            try
            {
                string sCnn = "data source =(local); initial catalog = CAI; user id = Admin; password =cajeros2008";
                using (SqlConnection connection = new SqlConnection(sCnn))
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandTimeout = 0;
                    command.CommandText = pScript;
                    command.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error:" + error.Message + "/n/n" + "script:" + pScript);
                return false;
            }
        }

        public void BorraHangarService()
        {
            try
            {
                Process[] proc= Process.GetProcessesByName("Hangar.Desktop");
                if (proc.Length > 0)
                {
                    proc[0].Kill();
                }
                string path = @"C:\HangarServices";
                DirectoryInfo di = new DirectoryInfo(path);
                DirectoryInfo[] subDirectories = di.GetDirectories();
                foreach (DirectoryInfo subDirectory in subDirectories)
                {
                    subDirectory.Delete(true);
                }

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
        }
    }
}
