using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using LCS;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml;

namespace IniciaCAI
{
    public class Book
    {
        public String title;
    }
    internal class Ejecutar : IDisposable 
  {
    private IntPtr m_intHwnd;

    #region "Contructor"
    public Ejecutar(IntPtr pHwnd)
    {
      this.Hwnd = pHwnd;
    }
    #endregion
        internal void ValidaVersionCardReader()
        {
            string version = string.Empty;
            string versionSQL = string.Empty;

            //KillProc("Hangar.Desktop.exe");

            //string path = @"C:\HangarServices\";

            //Directory.Delete(path, true);

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"C:\HangarServices\RuntimeCardReader\RuntimeCardReader.dll.config");
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    //String id = node.Attributes["id"].Value;
                    //Console.WriteLine("id: " + id);
                    if (node.HasChildNodes)
                    {
                        for (int i = 0; i < node.ChildNodes.Count; i++)
                        {
                            Console.WriteLine(node.ChildNodes[i].Name + " : " + node.ChildNodes[i].InnerText);
                        }
                    }
                }

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                throw;
            }
            

            //read version in BD
            string conexion = @"data source=(local); initial catalog =CAI; user id = Admin; password = cajeros2008;";
            using (SqlConnection connection = new SqlConnection(conexion))
            {
                connection.Open();
                string consulta = "SELECT valor from tCjParametros where IdParametro=66";
                SqlCommand command =
               new SqlCommand(consulta, connection);
                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    versionSQL = reader.GetString(0);
                }
            }
        }
    #region "Iniciar"
    internal bool Iniciar(string AppName, string pParam)
    {
        bool salida = true;
        string ValorDevuelto = string.Empty ;
        Archivos Arch = new Archivos();
        Arch.Escribir("Iniciar", Archivos.enumEstados.e_log);
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(AppName, pParam);
                Arch.Escribir("Iniciar", Archivos.enumEstados.e_log);
                psi.RedirectStandardOutput = true;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.UseShellExecute = false;
                Process listFiles;

                Arch.Escribir("Star", Archivos.enumEstados.e_log);
                listFiles = Process.Start(psi);

                StreamReader myOutput = listFiles.StandardOutput;
                listFiles.WaitForExit();
                if (listFiles.HasExited)
                {
                    ValorDevuelto = myOutput.ReadToEnd();
                    Arch.Escribir("Respuesta salida:" + ValorDevuelto, Archivos.enumEstados.e_log);
                }
                else
                {
                    Arch.Escribir("Respuesta con error:" + myOutput.ToString(), Archivos.enumEstados.e_log);
                }
            }
            catch (Exception e)
            {
                ValorDevuelto = e.Message + "(" + AppName + ")";
                Arch.Escribir("Error:" + ValorDevuelto, Archivos.enumEstados.e_error );
                salida = false;
            }
            finally
            {
                Arch.Dispose();
            }
            return salida;
        }
    #endregion

    #region "KillProc"
    public void KillProc(string processname)
    {
      Process[] aProc = Process.GetProcessesByName(processname);
      foreach(Process sProc in aProc)
      {
        sProc.Kill();
      }
    }
    #endregion

    #region "Propiedades"
    private IntPtr Hwnd
    {
      get
      {
        return m_intHwnd;
      }
      set
      {
        m_intHwnd = value;
      }
    }
    #endregion

    #region "Destrutores"
    void IDisposable.Dispose() { }
    public void Dispose() { }
#endregion 
  }
}
