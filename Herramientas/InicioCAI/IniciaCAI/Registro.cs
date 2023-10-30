using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;   

namespace IniciaCAI
{
  internal static class Registro
  {
    #region "ReadSqlVersion"
    internal static string ReadSqlVersion()
    {
      string Valor = string.Empty ;

      //Leer la entrada en el registro
      RegistryKey RegKey = Registry.LocalMachine;
                  RegKey = RegKey.OpenSubKey("SOFTWARE\\Microsoft\\MSSQLServer\\MSSQLServer\\CurrentVersion");  

      //Se encontro la entrada?
      if (RegKey !=  null )
      {

        Valor = (string)RegKey.GetValue("CurrentVersion").ToString();
      }

      //Devolver el valor
      return Valor;
    }
    #endregion
  }
}