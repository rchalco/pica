using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using LCS;
using System.Windows.Forms;

namespace IniciaCAI
{
  internal class Archivos : IDisposable 
  {
    private IntPtr m_intHwnd;

    private const string VersionActual = "[v5.03.0003]";
    private const string FNotFound = "Bootnnova- El archivo ${FileN} [v${VFileR}] no existe y es necesario para el correcto funcionamiento de CAI " + VersionActual;
    private const string FWrongVer = "Bootnnova- Version incorrecta del archivo ${File} [v${VFile}] [...${VMD51}] requiere ${FileR} [v${VRFile}] [...${VMD52}] para el correcto funcionamiento de CAI " + VersionActual;

    private string _system = Environment.GetFolderPath(Environment.SpecialFolder.System);

    public enum enumEstados : int
    {
      e_log = 0,
      e_error = 1,
      e_otro = 2
    };

    #region "Constructor"
    //public Archivos(IntPtr pHwnd)
    //{
    //  this.Hwnd = pHwnd;
    //}
        public Archivos()
        {
            
        }
        #endregion

        #region "Escribir"
        internal void Escribir(string cadena, enumEstados Estado)
    {
        string msg = "(" + System.DateTime.Now.ToString("HH:mm:ss").ToString() + ")" + cadena ;
        try
        {
            TextWriter tw = new StreamWriter(@"C:\LogBootnnova"+ System.DateTime.Now.ToString("yyyyMMdd") +".txt", true);
            tw.WriteLine(
              (int)Estado == 0 ? @"--> ...\" + msg : ((int)Estado == 1 ? @"=!= ...\" + msg : @"" + msg)
                        );
            tw.Close();
        }
        catch (Exception error)
        {
                MessageBox.Show(error.Message, "Error al escribir log", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
    }
    #endregion

    #region "InfoF"
    internal void InfoF(List<string> pListCompilacion,
                               string sFile)
    {
      string sRess = string.Empty;
      string sVersion = string.Empty;
      string strMensaje = FNotFound.Replace("${FileN}", pListCompilacion[0]);
             strMensaje = strMensaje.Replace("${VFileR}", pListCompilacion[2]);
      List<string> LDatosFile = new List<string>();

      using (NSIS nObject = new NSIS(this.Hwnd))
      {
        try
        {
          FileInfo TheFile = new FileInfo(sFile);
          FileVersionInfo myFile = FileVersionInfo.GetVersionInfo(sFile);

          if (TheFile.Exists)
          {
            LDatosFile.Add(sFile);
            LDatosFile.Add(cMD5.MD5DeArchivo(sFile));
            if (sFile.Replace(_system, "") == "\\Matrix.dll" |
                sFile.Replace(_system, "") == "\\W32gpkv2.dll")
            {
              sVersion = myFile.ProductVersion;
            }
            else
            {
              sVersion = myFile.FileVersion;
            }
                
            LDatosFile.Add(sVersion == null ? "" : sVersion);   
            if (LDatosFile[2] == null) { LDatosFile[2] = ""; }

            LDatosFile.Add(TheFile.LastWriteTime.ToString());
            LDatosFile.Add(TheFile.Length.ToString());

                FComparacion(LDatosFile, pListCompilacion);
          }
          else
          {
            nObject.DetailPrint(strMensaje);
          }
        }
        catch (Exception e)
        {
          nObject.DetailPrint(strMensaje);
          nObject.DetailPrint(e.Message);
        }
      }
    }
    #endregion

    #region "FComparacion"
    private void FComparacion(List<string> pListDisco, 
                              List<string> pListCompilacion)
    {
      string _FileName = pListCompilacion[0];
      
       using(NDebug nObj = new NDebug(this.Hwnd))
       {
        try
        {
          string strVersion = string.Empty;

//              if((pListDisco[1].ToUpper() != pListCompilacion[1].ToUpper()) | (pListDisco[2] != pListCompilacion[2]))

            //VERSION Y TAMAÑO DEL ARCHIVO
             if((pListDisco[2] != pListCompilacion[2]) | pListDisco[4] != pListCompilacion[3])
              {
                string strMensaje = FWrongVer.Replace("${File}", pListCompilacion[0]);
                       strMensaje = strMensaje.Replace("${VFile}", pListDisco[2]);
                       strMensaje = strMensaje.Replace("${FileR}", pListCompilacion[0]);
                       strMensaje = strMensaje.Replace("${VRFile}", pListCompilacion[2]);
                       strMensaje = strMensaje.Replace("${VMD51}", pListDisco[1].Substring(pListDisco[1].Length - 4, 4));
                       strMensaje = strMensaje.Replace("${VMD52}", pListCompilacion[1].Substring(pListCompilacion[1].Length - 4, 4));
                       nObj.DetailPrint(strMensaje);
              }
              else
              {
                strVersion = pListDisco[2].Replace(",", ".");
                strVersion = strVersion.Replace(" ", "");

                while (_FileName.Length < 18)
                {
                  _FileName = _FileName + @" ";

                }

                while (strVersion.Length < 11)
                {
                  strVersion = strVersion + @" ";
                }

                 Escribir(_FileName  + "[" + (strVersion.Trim().ToString() == "" ? @" " : "v" ) + strVersion + "|" + pListDisco[3] + "|..." + pListDisco[1].Substring(pListDisco[1].Length -3,3)   + "]", enumEstados.e_log);
              }
        }
	      catch(Exception e)
        {
          nObj.DetailPrint("Ocurrio el siguiente error: " + e.Message); 
        }
      }
    }
    #endregion

    #region "Destructor"
    void IDisposable.Dispose() { }
    public void Dispose() { }
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

   
  }
}