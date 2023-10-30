using System;
using System.Collections.Generic;
using System.Text;
using LCS;

namespace IniciaCAI 
{
  internal class NDebug :IDisposable 
  {
    private IntPtr m_intHwnd;

    #region "Constructor"
    public NDebug(IntPtr pHwnd)
    {
      this.Hwnd = pHwnd;
    }
    #endregion

    #region "DetailPrint"
    internal void DetailPrint(string entry)
    {
        using (NSIS nsisDebug = new NSIS(Hwnd, true))
        {
            try
            {
                nsisDebug.DetailPrint(entry);
            }
            catch (Exception error)
            { }
        }
    }
    #endregion

    #region "Propiedades"
    internal IntPtr Hwnd
    {
      private get
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