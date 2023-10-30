using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;

namespace IniciaCAI
{
    internal class cMD5 : IDisposable
    {
        private IntPtr m_intHwnd;

        #region "Constructor"
        public cMD5(IntPtr pHwnd)
        {
            this.Hwnd = pHwnd;
        }
        #endregion

        #region "MD5DeArchivo"
        internal static string MD5DeArchivo(string fileName)
        {
            string Md5Ress = string.Empty;
            try
            {
                MD5 md5 = MD5.Create();
                using (FileStream stream = File.OpenRead(fileName))
                {
                    byte[] checksum = md5.ComputeHash(stream);
                    return (BitConverter.ToString(checksum).Replace("-", string.Empty));
                }
            }
            catch (Exception e)
            {
                Md5Ress = e.Message + "[" + fileName + "]";
            }
            return Md5Ress;
        }
        #endregion

        #region "DatosFiles"
        internal void DatosFiles(string sApp)
        {
            string _FileName;
            if (Environment.Is64BitOperatingSystem)
            {
                _FileName = sApp.Replace(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @" (x86)\ProyCajeros5\", "");
            }
            else
            {
                _FileName = sApp.Replace(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\ProyCajeros5\", "");
                
            }
            _FileName = _FileName.Replace(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\", "");
            _FileName = _FileName.Replace(Environment.CurrentDirectory + @"\", "");
            _FileName = _FileName.Replace(@"C:\\", "");
            //using (NDebug nObject = new NDebug(this.Hwnd))
            //{
                try
                {
                    using (Archivos Arch = new Archivos())
                    {
                        switch (_FileName)
                        {
                                
                            case "CAIv5.0.exe":
                                Arch.InfoF(new List<string> { _FileName, "DF57FFDC3CDA376E755991B7E9B2A05D", "5.02.0056", "18386944" }, sApp);
                                //Arch.InfoF(new List<string> { _FileName, "F3ADD7A1B933CD28069B44B0607AC059", "5.02.0009", "4886528" }, sApp);
                                break;
                            case "ProyAdminCAI23.exe":
                                Arch.InfoF(new List<string> { _FileName, "B0D64B168CEB4E1265A2B800EC6CC2E3", "5.02.0056", "4370432" }, sApp); //Patchv1.0
                                //Arch.InfoF(new List<string> { _FileName, "ABFE06258704764E8A32AA38C52A673E", "5.01", "3760128" }, sApp); //Patchv1.0
                                break;
                            //case "DesbloqueaCAI.exe":
                                Arch.InfoF(new List<string> { _FileName, "5881160A69737B3F228F2D82B202D260", "1.3.0.0", "12288" }, sApp);
                                break;
                            case "BloqueaCAI.exe":
                                //Arch.InfoF(new List<string> { _FileName, "8F55890673C184BC543809113C8969D1", "1.3.0.0", "12288" }, sApp);
                                break;
                            case "ProyReinicio.exe":
                                Arch.InfoF(new List<string> { _FileName, "f1fcd8be79305c5d668b366b95c88c50", "1.00.0002", "20480" }, sApp);
                                break;
                            //case "SeteoCAIv5.0.exe": 
                            //Files.InfoF(new List<string>{_FileName, "abda24a874117f2df0f7e83b55736fc1", "1.28.00.00", "31/10/2007 10:10:49"}, sApp, nObject);
                            //break;
                            case "LCS.dll":
                            case "C:\\LCS.dll":
                                Arch.InfoF(new List<string> { _FileName, "DF57FFDC3CDA376E755991B7E9B2A05D", "1.1.0.0", "19968" }, sApp);
                                break;
                            case "Correo.exe":
                                //Arch.InfoF(new List<string> { _FileName, "CFAA13D1CC8B56CC92682D3B4264A970", "1.00.0002","28672"}, sApp);
                                break;
                            case "CardsManager.dll":
                                //Arch.InfoF(new List<string> { _FileName, "4f64a40a43800ab9f2e7fdf320833b7a", "", "49152" }, sApp);
                                break;
                            case "MFC42D.dll":
                               // Arch.InfoF(new List<string> { _FileName, "fd511a15680ce3aec1383db552376bb5", "6.00.8447.0", "929844" }, sApp);
                                break;
                            case "MFCO42D.dll":
                                //Arch.InfoF(new List<string> { _FileName, "64272a9871973b570b1f265a982d827d", "6.00.8447.0", "798773" }, sApp);
                                break;
                            case "MSVCRTD.dll":
                                Arch.InfoF(new List<string> { _FileName, "283be2026dabe770ae50998ebd631937", "6.00.8447.0", "401484" }, sApp);
                                break;
                            case "RegUtils.dll":
                                //Arch.InfoF(new List<string> { _FileName, "b747067e571204b99d62b6a8490e46a2", "", "53248" }, sApp);
                                break;
                            case "W32GAtf.dll":
                                //Arch.InfoF(new List<string> { _FileName, "0fe4a9e2e0f6956ec4bfbf3f9f5db455", "", "53248" }, sApp);
                                break;
                            case "W32gpkv2.dll":
                                //Arch.InfoF(new List<string> { _FileName, "b7447e92eb8b6a1ae6d3a8dcb05ddd00", "2.00.003", "122880" }, sApp);
                                break;
                            case "W32Gscript.dll":
                                Arch.InfoF(new List<string> { _FileName, "2c562f89576c00268d4e1fafea88a3d4", "", "53248" }, sApp);
                                break;
                            case "HCompDll.dll":
                                //Arch.InfoF(new List<string> { _FileName, "b33b91dcc1db0d3b197e0f7379e6d8b8", "", "49152" }, sApp);
                                break;
                            case "IWP_SCReader.dll":
                                //Arch.InfoF(new List<string> { _FileName, "E3B7B657396F1A6B8A7D4077A26FC564", "2, 0, 0, 11", "540752" }, sApp);
                                //Arch.InfoF(new List<string> { _FileName, "C43A391F9BF1C7E3D7A463A1115DD35E", "2, 0, 0, 18", "553040" }, sApp);
                                break;
                            case "IWP_SCAg.dll":
                                //Arch.InfoF(new List<string> { _FileName, "8E665392D0C7B5055A38BF5DFD2CF2DE", "1, 0, 12, 5", "110592" }, sApp);
                                break;
                            case "Matrix.dll":
                                //ch.InfoF(new List<string> { _FileName, "0BEDD6EF533233931D2E45E82E58FD6E", "2, 0, 3, 8b", "393216" }, sApp);
                                break;
                            case "VBPrinter.dll":
                                //Arch.InfoF(new List<string> { _FileName, "37db3fe4987a70bf6f96ef7d9af3182a", "", "7168" }, sApp);
                                break;
                            case "SMJSMon.dll":
                                //Arch.InfoF(new List<string> { _FileName, "894c88f0e12c32c63398592e8532f2b1", "0, 3, 0, 0", "32256" }, sApp);
                                break;
                            //case "IWP_HuellaX.ocx":
                                //Arch.InfoF(new List<string> { _FileName, "758531518FDAE28E75B91DEB6E919DBA", "2, 0, 4, 5", "2678784" }, sApp);
                                //break;
                            //case "sat-sock.dll":
                            //    Arch.InfoF(new List<string> { _FileName, "958cef63fde9f00ac776448b248e676f", "1.01.0027", "110592" }, sApp);
                            //    break;
                            //case "SiSAT.dll":
                            //    Arch.InfoF(new List<string> { _FileName, "D74A11E00768EBDA89E432D5A9DAB8EF", "1.00.0042", "200704" }, sApp);
                            //    break;
                            //case "ServiciosCAI.dll":
                            //    Arch.InfoF(new List<string> { _FileName, "C566B16408DE9D0FD2EBC1F1D2BCA39A", "1.03", "323584" }, sApp);
                            //    break;
                            //case "VbSEmail.dll":
                            //    Arch.InfoF(new List<string> { _FileName, "AD74E1534EA0FD309D6BA60F8EF87702", "3.06.0005", "139264" }, sApp);
                            //    break;
                            case "C:\\sleep.exe":
                            case "sleep.exe":
                                Arch.InfoF(new List<string> { _FileName, "1a1075e5e307f3a4b8527110a51ce827", "", "126976" }, sApp);
                                break;
                            case "SHUTDOWN.exe":
                            case "C:\\SHUTDOWN.exe":
                                Arch.InfoF(new List<string> { _FileName, "213ed15fbdbf544a3b3145299e08f6ce", "", "34576" }, sApp);
                                break;
                            case "WinLockDll.dll":
                                Arch.InfoF(new List<string> { _FileName, "b8e16a4652fd481433c9409bea9c5a9e", "1.1", "57344" }, sApp);
                                break;
                            case "todg6.ocx":
                                Arch.InfoF(new List<string> { _FileName, "B0D64B168CEB4E1265A2B800EC6CC2E3", "6.0.0240", "856560" }, sApp);
                                break;
                                 
                        }
                                 
                    }
                    
                }
                catch (Exception ex)
                {
                    //nObject.DetailPrint(ex.Message);
                }
            //}
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

        #region "Destructores"
        void IDisposable.Dispose() { }
        public void Dispose() { }
        #endregion

    }
}
