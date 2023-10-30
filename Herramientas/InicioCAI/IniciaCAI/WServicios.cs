using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceProcess;
using System.Threading;

namespace IniciaCAI
{
  internal class WServicios : IDisposable
  {
    private IntPtr m_intHwnd;

    #region "Constructor"
    public WServicios(IntPtr pHwnd)
    {
      this.Hwnd = pHwnd;
    }
    #endregion

    #region "EstadoServicio"
    internal void EstadoServicio(string Nombre)
    {
      using (ServiceController WSPorNombre = new ServiceController())
      {
        WSPorNombre.ServiceName = Nombre;

        using (Archivos Arch = new Archivos())
        {
          while (WSPorNombre.Status != ServiceControllerStatus.Running)
          {
            Arch.Escribir("Servicio: " + Nombre + " [" + WSPorNombre.Status.ToString() + "] " + DateTime.Now.ToString(), Archivos.enumEstados.e_error);
            Thread.Sleep(5000);
            WSPorNombre.Refresh();
          }
          Arch.Escribir("Servicio: " + Nombre + " activo [" + WSPorNombre.Status.ToString() + "] " + DateTime.Now.ToString(), Archivos.enumEstados.e_log);
        }
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

    #region "Destructor"
    void IDisposable.Dispose() { }
    public void Dispose() { }
    #endregion
  }
}