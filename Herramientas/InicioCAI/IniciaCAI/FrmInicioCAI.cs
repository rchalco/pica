using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Configuration;
using System.IO;
using LCS;
using System.Threading;
using System.Data.SqlClient;
using System.Diagnostics;
using utilities;
using Microsoft.Win32;
using Microsoft.VisualBasic;
namespace IniciaCAI
{
    public partial class frmInicioCAI : Form
    {
        private string m_strDirProy5 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\ProyCajeros5\";
        private string m_ProgramFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles).ToString();
        private string m_SystemDir = Environment.GetFolderPath(Environment.SpecialFolder.System).ToString();
        private string m_strCAIVersion = "[5.03.0002]";

        private bool KeyCtrl = false;
        private KeyCapturer keyCapture;

        //void App_SessionEnding(object sender, SessionEndingCancelEventArgs e)
        //{
        //    // Ask the user if they want to allow the session to end
        //    string msg = string.Format("{0}. End session?", e.ReasonSessionEnding);
        //    MessageBoxResult result = MessageBox.Show(msg, "Session Ending", MessageBoxButton.YesNo);

        //    // End session, if specified
        //    if (result == MessageBoxResult.No)
        //    {
        //        e.Cancel = true;
        //    }
        //}
        public frmInicioCAI()
        {
            Archivos Arch = new Archivos();
            Arch.Escribir("Ingresando a FrmIncioCAI", Archivos.enumEstados.e_log);
            try
            {
                InitializeComponent();
                Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
                if (Environment.Is64BitOperatingSystem)
                {
                    m_strDirProy5 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\ProyCajeros5\";
                    m_ProgramFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles).ToString();
                    if (!m_ProgramFiles.Contains("x86"))
                    {
                        m_strDirProy5 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @" (x86)\ProyCajeros5\";
                        m_ProgramFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles).ToString() + @" (x86)\";
                    }
                }
                else
                {
                    m_strDirProy5 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\ProyCajeros5\";
                    m_ProgramFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles).ToString();
                }
            }
            catch (Exception error)
            {

                //Archivos Arch = new Archivos();
                Arch.Escribir("Error al inicializar:" + error.Message , Archivos.enumEstados.e_log);
                Arch.Dispose();
            }
            Arch.Escribir("Mostrar version", Archivos.enumEstados.e_log);
            lblVersion.Text = m_strCAIVersion;
            Arch.Escribir("Habilita eventos de teclado", Archivos.enumEstados.e_log);
            try
            {
                this.keyCapture = new KeyCapturer((KeyCapturerResult result)
                    =>
                {
                    using (Archivos Archi = new Archivos())
                    {
                        if (result.code == 27)//Tecla ESC
                        {
                            KeyCtrl = false;
                            Archi.Escribir("Presiono el boton ESC. llamando a funcion eliminar CAI", Archivos.enumEstados.e_log);
                            Ejecutar Eje = new Ejecutar(this.txtDebug.Handle);
                            Eje.KillProc("CAIv5.0");
                            this.keyCapture.stop();
                        }
                        else if (result.code == 17) //Tecla Ctrl
                        {
                            Archi.Escribir("Presiono el boton Ctrl", Archivos.enumEstados.e_log);
                            KeyCtrl = true;
                        }
                        else if (result.code == 70) //Tecla F o f
                        {
                            Archi.Escribir("Presiono el boton F", Archivos.enumEstados.e_log);
                            if (KeyCtrl)
                            {
                                Archi.Escribir("Presiono el boton ctrl+ F. eliminar CAI", Archivos.enumEstados.e_log);
                                //Ejecutar Eje = new Ejecutar(this.txtDebug.Handle);
                                //Eje.KillProc("CAIv5.0");
                                this.keyCapture.stop();
                            }
                            KeyCtrl = false;
                        }
                        else //cualquier tecla
                        {
                            Archi.Escribir("Presiono el boton otros(" + result.code.ToString() + ")", Archivos.enumEstados.e_log);
                            KeyCtrl = false;
                        }
                    }

                });
                this.keyCapture.run();
                Arch.Escribir("fin inicio", Archivos.enumEstados.e_log);
            }
            catch (Exception error)
            {
                //Archivos Arch = new Archivos();
                Arch.Escribir("Error al inicializar teclado:" + error.Message, Archivos.enumEstados.e_log);
                Arch.Dispose();

            }
            
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            // When the application is exiting, write the application data to the
            // user file and close it.
            //WriteFormDataToFile();

            try
            {
                // Ignore any errors that might occur while closing the file handle.
                //MessageBox.Show("Salir");
            }
            catch { }
        }

        private void frmInicioCAI_Shown(object sender, EventArgs e)
        {
            this.Refresh();
            //Progreso(25, "Verificando Servicios SGDB...");
            Ejecutar ejecutar = new Ejecutar(this.txtDebug.Handle);
            ejecutar.ValidaVersionCardReader();

            try
            {
                using (Archivos Arch = new Archivos())
                {
                    Arch.Escribir("******************Ingresando a BOOTNNOVA v2.1*****************", Archivos.enumEstados.e_log);

                    Arch.Escribir("Valida REGEdit", Archivos.enumEstados.e_log);
                    //ValidaRegEdit();

                    #region "Habilita conexion"
                    ConectaRed();
                    #endregion
                    #region "Ejecuta HLA"
                    /****************************************************/
                    datosParam param = ObtieneParam();
                    if ( Convert.ToDouble( param.TipoLectorTI )>100)
                    {                    
                        using (System.Diagnostics.Process process = new System.Diagnostics.Process())
                        {
                            try
                            {
                                Process[] processes = Process.GetProcessesByName("Hangar.Desktop");
                                if (processes.Length == 0)
                                {
                                    System.Diagnostics.ProcessStartInfo empaquetaShell = new ProcessStartInfo();
                                    empaquetaShell.UseShellExecute = true;
                                    empaquetaShell.FileName = "Hangar.Desktop.exe";
                                    empaquetaShell.WorkingDirectory = @"C:\HLA";
                                    empaquetaShell.Arguments = "start";
                                    empaquetaShell.WindowStyle = ProcessWindowStyle.Hidden;

                                    process.StartInfo = empaquetaShell;

                                    process.Start();
                                }

                            }
                            catch (Exception error)
                            {
                                MessageBox.Show(error.Message);
                                throw;
                            }

                        }
                    }
                    /******************************************************/
                    #endregion

                    #region "Verificar SGDB"
                    Arch.Escribir("SGBD: v" + Registro.ReadSqlVersion(), Archivos.enumEstados.e_otro);

                    using (WServicios WSer = new WServicios(this.txtDebug.Handle))
                    {
                        WSer.EstadoServicio("SQLBrowser");
                        WSer.EstadoServicio("MSSQLSERVER");
                    }
                    #endregion
                    #region "Dependencias"
                    //Progreso(40, "Verificando dependencias CAI...");
                    using (cMD5 md5 = new cMD5(this.txtDebug.Handle))
                    {
                        //md5.DatosFiles(@"C:\Bootnnova.exe");
                        //md5.DatosFiles(m_ProgramFiles + @"\ProyCajeros5\CAIv5.0.exe");
                        //md5.DatosFiles(m_ProgramFiles + @"\ProyCajeros5\ProyAdminCAI23.exe");
                        ////MessageBox.Show("ok1");
                        //md5.DatosFiles(m_ProgramFiles + @"\ProyCajeros5\DesbloqueaCAI.exe");
                        //md5.DatosFiles(m_ProgramFiles + @"\ProyCajeros5\BloqueaCAI.exe");
                        ////MessageBox.Show("ok2");
                        //md5.DatosFiles(m_ProgramFiles + @"\ProyCajeros5\ProyReinicio.exe");
                        ////cMD5.DatosFiles(m_ProgramFiles + @"\ProyCajeros5\SeteoCAIv5.0.exe", this.txtDebug.Handle);
                        ////md5.DatosFiles(m_ProgramFiles + @"\ProyCajeros5\Correo.exe");
                        ////MessageBox.Show("ok3");
                        //md5.DatosFiles(m_ProgramFiles + @"\ProyCajeros5\WinLockDll.dll");
                        //md5.DatosFiles(m_ProgramFiles + @"\CardsManager.dll");
                        ////MessageBox.Show("ok42");
                        //md5.DatosFiles(Application.StartupPath + @"\LCS.dll");
                        //md5.DatosFiles(m_SystemDir + @"\MFC42D.dll");
                        //md5.DatosFiles(m_SystemDir + @"\MFCO42D.dll");
                        ////MessageBox.Show("ok5");
                        //md5.DatosFiles(m_SystemDir + @"\MSVCRTD.dll");
                        //md5.DatosFiles(m_SystemDir + @"\RegUtils.dll");
                        //md5.DatosFiles(m_SystemDir + @"\W32GAtf.dll");
                        ////MessageBox.Show("ok6");
                        //md5.DatosFiles(m_SystemDir + @"\W32gpkv2.dll");
                        //md5.DatosFiles(m_SystemDir + @"\W32Gscript.dll");
                        ////md5.DatosFiles(m_SystemDir + @"\HCompDll.dll");
                        ////MessageBox.Show("ok7");
                        ////md5.DatosFiles(m_SystemDir + @"\IWP_SCReader.dll");
                        ////md5.DatosFiles(m_ProgramFiles + @"\ProyCajeros5\IWP_SCReader.dll");
                        ////md5.DatosFiles(m_SystemDir + @"\IWP_SCAg.dll");
                        ////md5.DatosFiles(m_ProgramFiles + @"\ProyCajeros5\IWP_SCAg.dll");  
                        ////md5.DatosFiles(m_SystemDir + @"\Matrix.dll");
                        //md5.DatosFiles(m_SystemDir + @"\VBPrinter.dll");
                        //md5.DatosFiles(m_SystemDir + @"\SMJSMon.dll");
                        ////MessageBox.Show("ok8");
                        ////md5.DatosFiles(m_SystemDir + @"\IWP_HuellaX.ocx");
                        ////md5.DatosFiles(m_SystemDir + @"\sat-sock.dll");
                        ////md5.DatosFiles(m_SystemDir + @"\SiSAT.dll");
                        ////md5.DatosFiles(m_SystemDir + @"\ServiciosCAI.dll");
                        //md5.DatosFiles(m_SystemDir + @"\VbSEmail.dll");
                        ////MessageBox.Show("ok9");
                        //md5.DatosFiles(@"C:\sleep.exe");
                        //md5.DatosFiles(@"C:\SHUTDOWN.exe");
                        //md5.DatosFiles(m_SystemDir + @"\todg6.ocx");
                    }

                    #endregion

                    //#region "Back de BD y Log"
                    //if (System.DateTime.Now.Hour == 5 & System.DateTime.Now.Minute < 10)
                    //{
                    //    ValidaConexion();
                    //    Arch.Escribir("Backups. Realizando Backup de BD", Archivos.enumEstados.e_log);
                    //    lsRealizaBackupBD();
                    //    Arch.Escribir("Backups. Realizando Backup Mensual", Archivos.enumEstados.e_log);
                    //    EmpaquetaLogMensual();
                    //    Arch.Escribir("Backups. Finalizado", Archivos.enumEstados.e_log);
                    //}

                    //#endregion

                    #region "Ejecutar CAI
                    using (Ejecutar Eje = new Ejecutar(this.txtDebug.Handle))
                    {
                        Eje.KillProc("FondoDePantalla");
                        Arch.Escribir("Minimizando Bootnnova", Archivos.enumEstados.e_log);
                        //Owner.WindowState = FormWindowState.Minimized;
                        this.WindowState = FormWindowState.Minimized;
                        Arch.Escribir("Ejecutando CAI...", Archivos.enumEstados.e_log);
                        if (!Eje.Iniciar(m_strDirProy5 + @"CAIv5.0.exe", "0123456789"))
                        {
                            Arch.Escribir("Ocurrieron problemas en la ejecución del CAI.", Archivos.enumEstados.e_error);
                        }
                        Arch.Escribir("Finalizado CAI", Archivos.enumEstados.e_log);
                        //MessageBox.Show("ok2");
                        #endregion

                        #region "Reiniciar"
                        this.WindowState = FormWindowState.Maximized;
                        //Progreso(70, "Programando reinicio...");
                        Arch.Escribir("Programando reinicio...", Archivos.enumEstados.e_log);
                        Eje.Iniciar(m_strDirProy5 + "ProyReinicio.exe", "");
                        if (!Eje.Iniciar(m_strDirProy5 + @"ProyReinicio.exe", ""))
                        {
                            Arch.Escribir("No se pudo desbloquear el CAI.", Archivos.enumEstados.e_error);
                        }
                        Arch.Escribir("finalizar aplicaciones ", Archivos.enumEstados.e_log);
                        Eje.KillProc("ProyAdminCAI23");
                        Eje.KillProc("ProyReinicio");

                        #region "Back de BD y Log"
                        if (System.DateTime.Now.Hour == 5 & System.DateTime.Now.Minute < 10)
                        {
                            ValidaConexion();
                            Arch.Escribir("Backups. Realizando Backup de BD", Archivos.enumEstados.e_log);
                            lsRealizaBackupBD();
                            Arch.Escribir("Backups. Realizando Backup Mensual", Archivos.enumEstados.e_log);
                            EmpaquetaLogMensual();
                            Arch.Escribir("Backups. Finalizado", Archivos.enumEstados.e_log);
                        }

                        #endregion

                        Arch.Escribir("Reiniciando PC...", Archivos.enumEstados.e_log);
                        if (!Eje.Iniciar(@"C:\SHUTDOWN.exe", @"/L /R /T:1 /Y /C"))
                        {
                            //objNsis.DetailPrint("No se pudo reiniciar la PC.");
                            Arch.Escribir("No se pudo reiniciar la PC.", Archivos.enumEstados.e_error);
                        }
                    }
                }
                #endregion

                Thread.Sleep(1000);

                Application.Exit();
            }

            catch (Exception error)
            {
                Archivos Arch = new Archivos();
                Arch.Escribir("Error en show. error:" + error.Message, Archivos.enumEstados.e_log);
                Ejecutar Eje = new Ejecutar(this.txtDebug.Handle);
                Eje.Iniciar(@"C:\SHUTDOWN.exe", @"/L /R /T:1 /Y /C");
                Eje.Dispose();
            }
            finally 
            {
                Archivos Arch = new Archivos();
                Arch.Escribir("Finalizo aplicación con FINALLY", Archivos.enumEstados.e_log);
                Application.Exit();
            }


        }

        //private void ValidaRegEdit()
        //{
        //    const string userRoot = "HKEY_LOCAL_MACHINE";
        //    const string subkey = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon";
        //    const string keyName = userRoot + "\\" + subkey;
        //    Archivos Arch = new Archivos();
        //    try
        //    {
        //        ///modificar reg edit
        //        ///Equipo\HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon
        //        try
        //        {
        //            /////
        //            try
        //            {
        //                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(subkey))
        //                {
        //                    if (key != null)
        //                    {
        //                        Object o = key.GetValue("Shell");
        //                        if (o.ToString()!="C:\\bootnnova.exe")
        //                        {
        //                            Registry.SetValue(keyName, "Shell", @"C:\bootnnova.exe");
        //                            Arch.Escribir("se cambio regedit de manera correcta", Archivos.enumEstados.e_log);
                                    
        //                            //do what you like with version
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)  //just for demonstration...it's always best to handle specific exceptions
        //            {
        //                //react appropriately
        //            }
        //            ///
                    
        //            //

        //        }
        //        catch (Exception error)
        //        {
        //            Arch.Escribir("Error al modificar regedit. Error:"+ error.Message , Archivos.enumEstados.e_error);
        //        }

        //        //borrar tareas programadas
        //        try
        //        {
        //            ExecuteCommand("schtasks /delete /tn InicioCAI /f");
        //            ExecuteCommand("schtasks /delete /tn " + (char)(34) + "Inicio CAI" + (char)(34) + "/f");
        //            ExecuteCommand("schtasks /delete /tn " + (char)(34) + "Inicia CAI" + (char)(34) +"/ f");
        //        }
        //        catch (Exception error)
        //        {

        //            Arch.Escribir("Error al modificar regedit. Error:" + error.Message, Archivos.enumEstados.e_error);
        //        }
        //    }
        //    catch (Exception error)
        //    {

        //        Arch.Escribir("Error al modificar regedit2. Error:" + error.Message, Archivos.enumEstados.e_error);
        //    }
        //    Arch.Dispose();
        //}
        private void ConectaRed()
        {

            Archivos Arch = new Archivos();
            string ejecuar = "";
            try
            {

                Arch.Escribir("Obteniendo parametros", Archivos.enumEstados.e_log);
                string servidor = GetEntradaConfig("IPServidor");
                string usuario = GetEntradaConfig("Usuario");
                string clave = GetEntradaConfig("Clave");

                //if (servidor != "")
                //{
                    ejecuar = "net use \\" + servidor + " /user:" + usuario + " " + clave;
                    Arch.Escribir("Llamada a funcion CMD", Archivos.enumEstados.e_log);
                    ejecutaCMD(ejecuar, usuario, clave);
                    Arch.Escribir("Luego de llamada a CMD", Archivos.enumEstados.e_log);
                //}
                
            }
            catch (Exception error)
            {
                Arch.Escribir("Error RED:" + error.Message, Archivos.enumEstados.e_error);
                throw;
            }

        }
        private static string GetEntradaConfig(string Key)
        {
            string s = ConfigurationManager.AppSettings[Key];
            if (!string.IsNullOrEmpty(s))
            {
                return s;
            }
            else
            {
                return "";
            }
        }
        private void ejecutaCMD(string pCmd, string pUsuario, string pClave)
        {
            Archivos Arch = new Archivos();
            try
            {
                //System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo();
                System.Diagnostics.Process p = new System.Diagnostics.Process();

                // Indicamos que la salida del proceso se redireccione en un Stream
                //procStartInfo.FileName = Environment.ExpandEnvironmentVariables("%SystemRoot%") + @"\System32\cmd.exe";
                p.StartInfo.FileName = Environment.ExpandEnvironmentVariables("%SystemRoot%") + @"\System32\cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                //p.StartInfo.UserName = pUsuario;

                //System.Security.SecureString passs = new System.Security.SecureString();
                //for (int i = 0; i < pClave.Length; i++)
                //{
                //    passs.AppendChar(pClave[i]);
                //}

                //p.StartInfo.Password = passs;
                p.Start();

                if (pCmd != "")
                {
                    Arch.Escribir("Ejecutar NED USER", Archivos.enumEstados.e_log);
                    p.StandardInput.WriteLine(pCmd);
                    Thread.Sleep(1000);
                }
                if (File.Exists("C:\\ProdemBKP.lnk"))
                {
                    Arch.Escribir("Ejecuar bat", Archivos.enumEstados.e_log);
                    p.StandardInput.WriteLine("C:\\ProdemBKP.lnk");
                }
                p.StandardInput.AutoFlush = true;

                //p.StandardInput.WriteLine(pCmd);
                Thread.Sleep(2000);
                //string salida = p.StandardOutput.ReadToEnd();

                //p.WaitForExit();

                p.Close();
                Thread.Sleep(1000);
            }
            catch (Exception error)
            {

                Arch.Escribir("Error al activar conexion. Error:" + error.Message, Archivos.enumEstados.e_error);

            }

        }

        private void ValidaConexion()
        {
            bool conecto = false;
            Archivos Arch = new Archivos();
            Arch.Escribir("Ingreso a validar conexion", Archivos.enumEstados.e_log);
            do
            {
                Arch.Escribir("do while", Archivos.enumEstados.e_log);
                Thread.Sleep(2000);
                try
                {
                    string conexion = @"data source=(local); initial catalog =CAI; user id = Admin; password = cajeros2008;";
                    using (SqlConnection connection = new SqlConnection(conexion))
                    {
                        Arch.Escribir("conectando", Archivos.enumEstados.e_log);
                        connection.Open();
                        if (connection.State == ConnectionState.Open)
                        {
                            Arch.Escribir("Conectado", Archivos.enumEstados.e_log);
                            conecto = true;
                        }
                        else
                        {
                            Arch.Escribir("Conectando", Archivos.enumEstados.e_log);
                            conecto = false;
                        }
                    }
                }
                catch (Exception error)
                {
                    Arch.Escribir(error.Message, Archivos.enumEstados.e_error);
                    conecto = false;
                }

            }
            while (!conecto);
            Arch.Escribir("Saliendo de conexion", Archivos.enumEstados.e_log);
        }
        private void EmpaquetaLogMensual()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            //NDebug objNsis = new NDebug(this.txtDebug.Handle);
            string DirProyCajeros = "";
            DateTime fechaLog = System.DateTime.Now;
            fechaLog = fechaLog.AddDays(-1);
            datosParam param = ObtieneParam();

            if (Environment.Is64BitOperatingSystem)
            {
                DirProyCajeros = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\ProyCajeros5\LogCAI\";
            }
            else
            {
                DirProyCajeros = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\ProyCajeros5\LogCAI\";
            }
            if (System.DateTime.Now.Day == 1)
            {
                string[] LogCAI = Directory.GetFiles(DirProyCajeros, "LOG" + param.NroCAIAgencia + "_" + fechaLog.ToString("yyyyMM") + "*.log");
                string[] LogMFnet = Directory.GetFiles(DirProyCajeros, "LogMFNet" + fechaLog.ToString("yyyyMM") + "*.txt");

                foreach (string a in LogCAI)
                {
                    string archivo = a.Substring(DirProyCajeros.Length);
                    System.IO.File.Copy(System.IO.Path.Combine(DirProyCajeros, archivo), System.IO.Path.Combine(@"C:\logCAI\", archivo));
                }

                foreach (string a in LogMFnet)
                {
                    string archivo = a.Substring(DirProyCajeros.Length);
                    System.IO.File.Copy(System.IO.Path.Combine(DirProyCajeros, archivo), System.IO.Path.Combine(@"C:\logCAI\", archivo));
                }


                string LvStrNomArchivoZip = @"c:\LogCAI\LogCAI" + param.NroCAIAgencia + "_" + fechaLog.ToString("yyyyMM");
                string LvStrNomArchivoZipA = "LogCAI" + param.NroCAIAgencia + "_" + fechaLog.ToString("yyyyMM");
                //empaquetar con 7zip

                //objNsis.DetailPrint("Empaquetar log");
                System.Diagnostics.ProcessStartInfo empaquetaShell = new System.Diagnostics.ProcessStartInfo();
                empaquetaShell.UseShellExecute = true;
                empaquetaShell.FileName = "7za.exe";
                empaquetaShell.WorkingDirectory = @"C:\LogCAI";
                empaquetaShell.Arguments = "a -sdel " + LvStrNomArchivoZip + " LOG*.*";

                process = new Process();
                process.StartInfo = empaquetaShell;
                process.Start();
                process.WaitForExit();

                string origen = System.IO.Path.Combine(@"C:\LogCAI", LvStrNomArchivoZipA + ".7z");
                string destino = System.IO.Path.Combine(param.dirBackup , LvStrNomArchivoZipA + ".7z");

                System.IO.File.Move(origen, destino);

            }
        }

        private datosParam ObtieneParam()
        {
            datosParam param = new datosParam();

            using (Archivos Arch = new Archivos())
            {
                try
                {
                    Arch.Escribir("ingresando a obtener parametro", Archivos.enumEstados.e_log);

                    string consulta = "select NroCajeroAgencia , RutaBackUPBD, CorreoDestino,RutaBackUPTrans,PuertoSC  from CJParametros ";
                    string conexion = @"data source=(local); initial catalog =CAI; user id = Admin; password = cajeros2008;Connection Timeout=180";
                    using (SqlConnection connection = new SqlConnection(conexion))
                    {
                        Arch.Escribir("conectando", Archivos.enumEstados.e_log);
                        connection.Open();
                        Arch.Escribir("Conectado", Archivos.enumEstados.e_log);
                        SqlCommand command = new SqlCommand(consulta, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        Arch.Escribir("leer", Archivos.enumEstados.e_log);

                        while (reader.HasRows)
                        {
                            Arch.Escribir("while hasrows", Archivos.enumEstados.e_log);
                            while (reader.Read())
                            {
                                Arch.Escribir("read", Archivos.enumEstados.e_log);
                                param.NroCAIAgencia = reader[0].ToString();
                                param.dirBackup = reader[1].ToString();
                                param.correo = reader[2].ToString();
                                param.BackUpTranac = reader[3].ToString();
                                param.TipoLectorTI= reader[4].ToString();
                                reader.NextResult();
                            }
                        }
                        Arch.Escribir("close", Archivos.enumEstados.e_log);
                        reader.Close();

                    }
                }
                catch (Exception error)
                {
                    Arch.Escribir(error.Message, Archivos.enumEstados.e_error);
                    param = null;
                }
                finally
                {
                    Arch.Escribir("Saliendo de obtener PARAM", Archivos.enumEstados.e_log);
                }

            }


            return param;
        }

        static void CopiarDesdeCMD(string pCmd, string pOrigen, string pDestino)
        {
            string cmdcopy = "COPY " + pOrigen + " " + pDestino;
            //Archivos Arch = new Archivos(this.txtDebug.Handle);
            try
            {

                System.Diagnostics.Process p = new Process();

                p.StartInfo.FileName = Environment.ExpandEnvironmentVariables("%SystemRoot%") + @"\System32\cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;

                p.Start();

                p.StandardInput.WriteLine(pCmd);

                Thread.Sleep(1000);

                p.StandardInput.WriteLine(cmdcopy);

                p.StandardInput.AutoFlush = true;

                //p.StandardInput.WriteLine(pCmd);
                Thread.Sleep(2000);
                //string salida = p.StandardOutput.ReadToEnd();

                //p.WaitForExit();

                p.Close();
                Thread.Sleep(5000);
            }
            catch (Exception error)
            {
                throw;
                //Arch.Escribir("Error al activar conexion. Error:" + error.Message, Archivos.enumEstados.e_error);

            }

        }

        private void LsEnviarCorreo(string pMensaje)
        {
            string conexion = @"data source=(local); initial catalog =CAI; user id = Admin; password = cajeros2008;";
            datosParam param;
            param = ObtieneParam();
            string lvCadquey = "INSERT INTO TcjCorreosPendientes (NroTransCAI,EmailDestino,Mensaje,Procesado,Ambito,ConductoRegular,FechaReg,Asunto,Adjunto,Dispositivo,Error,CorreoParaSMS,Contador) " +
                            "VALUES('','" + param.correo + "','" + pMensaje + "',0,0,1,getdate(),'BOOTNNOVA','','BOOTNNOVA','','',0)";
            using (SqlConnection cnn = new SqlConnection(conexion))
            {
                try
                {
                    cnn.Open();
                    SqlCommand cmm = new SqlCommand(lvCadquey, cnn);
                    cmm.ExecuteNonQuery();
                }
                catch (Exception error)
                {
                    Archivos Arch = new Archivos();
                    Arch.Escribir("Error al enviar Correo. Error:" + error.Message, Archivos.enumEstados.e_error);

                }
            }
        }
        public static bool LfBoolVerificaRutaDeRed(string path)
        {
            if (string.IsNullOrEmpty(path)) return false;

            string pathRoot = Path.GetPathRoot(path);

            if (string.IsNullOrEmpty(pathRoot)) return false;

            System.Diagnostics.ProcessStartInfo pinfo = new ProcessStartInfo("net" , "use");
            pinfo.CreateNoWindow = true;
            pinfo.RedirectStandardOutput = true;
            pinfo.UseShellExecute = false;
            string output;
            using (Process p = Process.Start(pinfo))
            {
                output = p.StandardOutput.ReadToEnd();
            }
            foreach (string line in output.Split('\n'))
            {
                if (line.Contains(pathRoot) && line.Contains("OK"))
                {
                    return true; // shareIsProbablyConnected 
                }
            }
            return false;
        }
        private void lsRealizaBackupBD()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();


            using (Archivos Arch = new Archivos())
            {
                try
                {
                    Arch.Escribir("RealizarBack: Iniciar backup", Archivos.enumEstados.e_log);
                    //objNsis.DetailPrint("Ingresando backup de BD");
                    Arch.Escribir("Llamada a obtener parametros", Archivos.enumEstados.e_log);
                    //objNsis.DetailPrint("Obtiene parametros");

                    datosParam param = ObtieneParam();
                    if (param == null)
                    {
                        Arch.Escribir("parametros NULL", Archivos.enumEstados.e_error);
                        return;
                    }
                    else
                    {
                        Arch.Escribir("parametros obtenidos", Archivos.enumEstados.e_log);
                    }

                    bool acceso = LfBoolVerificaRutaDeRed(param.dirBackup );
                    if (acceso)
                    {
                        Arch.Escribir("Ruta de acceso valido", Archivos.enumEstados.e_log);
                    }
                    else
                    {
                        Arch.Escribir("Sin acceso. Ruta:" + param.dirBackup , Archivos.enumEstados.e_log);
                    }
                    //objNsis.DetailPrint("parametros obtenidos");
                    //OpenFileDialog Directory;
                    //Directory.

                    //objNsis.DetailPrint("Validar ruta LogCAI");
                    Arch.Escribir("nombre a backup", Archivos.enumEstados.e_log);
                    string LvStrNomArchivoBackup = @"c:\LogCAI\BDCAI" + param.NroCAIAgencia + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".bak";
                    Arch.Escribir(LvStrNomArchivoBackup, Archivos.enumEstados.e_log);
                    //poner cursor de relojito mintras respalda
                    Cursor.Current = Cursors.WaitCursor;
                    //objNsis.DetailPrint("Verificar Ruta c:\\logcai");
                    Arch.Escribir("Validar ruca logcai", Archivos.enumEstados.e_log);

                    if (Directory.Exists(@"c:\LogCAI"))
                    {
                        Arch.Escribir("existe ruta....valida archivo", Archivos.enumEstados.e_log);
                        //objNsis.DetailPrint("Existe ruta. Validar archivo");
                        if (File.Exists(LvStrNomArchivoBackup))
                        {
                            Arch.Escribir("borrar backup", Archivos.enumEstados.e_log);
                            //objNsis.DetailPrint("Borrar backup");
                            File.Delete(LvStrNomArchivoBackup);
                        }
                    }
                    else
                    {
                        Arch.Escribir("crear ruta", Archivos.enumEstados.e_log);
                        //objNsis.DetailPrint("Crear Ruta LogCAI");
                        Directory.CreateDirectory(@"c:\LogCAI");
                    }
                    Arch.Escribir("ruta validada", Archivos.enumEstados.e_log);
                    try
                    {
                        //objNsis.DetailPrint("Conectado a SQL");
                        //esto puede ser un método aparte de conexion a la base de datos-----------
                        Arch.Escribir("formando cadena", Archivos.enumEstados.e_log);
                        SqlConnectionStringBuilder cadConectMaster = new SqlConnectionStringBuilder();
                        cadConectMaster.DataSource = ".";
                        cadConectMaster.InitialCatalog = "master";
                        cadConectMaster.UserID = "sa";
                        cadConectMaster.Password = "cai2008"; //////////CAMBIAR A CAI2008
                        cadConectMaster.ApplicationName = "bootnnova";

                        SqlConnection connect;
                        //string con = "data source=(local); initial catalog =master; user id = sa; password = cai2008;Connection Timeout=60;";
                        //string con = "data source=(local); initial catalog =Master; user id = sa; password = cajeros2008;Connection Timeout=120;";
                        //connect = new SqlConnection(con);
                        connect = new SqlConnection(cadConectMaster.ConnectionString);
                        connect.Open();
                        //////Arch.Escribir("master conectada", Archivos.enumEstados.e_log);
                        ////////objNsis.DetailPrint("Conectado a BD master");
                        //////Arch.Escribir("ruta validada", Archivos.enumEstados.e_log);
                        //////con = "create table tPruebaMaster(Indice bigint not null default(0),	descripcion varchar(max) not null default(''))";

                        //////SqlCommand command;

                        //////command = new SqlCommand(con, connect);
                        //////command.CommandTimeout = 600;
                        //////command.ExecuteNonQuery();
                        //////Arch.Escribir("tabla creada", Archivos.enumEstados.e_log);
                        //-------------------------------------------------------------------------
                        string cadBackup = @"BACKUP DATABASE [CAI] TO DISK = N'" + LvStrNomArchivoBackup + "' WITH NOFORMAT, NOINIT,  NAME = N'CAI-Full Database Backup', SKIP, NOREWIND, NOUNLOAD, NO_CHECKSUM";
                        //esto puede ser un método aparte para ejecutar comandos SQL---------------

                        Arch.Escribir("comando de backup", Archivos.enumEstados.e_log);
                        SqlCommand command = new SqlCommand(cadBackup, connect);
                        command.CommandTimeout = 600;
                        command.ExecuteNonQuery();
                        Arch.Escribir("backup realizado", Archivos.enumEstados.e_log);
                        //-------------------------------------------------------------------------

                        connect.Close();


                        Thread.Sleep(10000);
                    }
                    catch (Exception error)
                    {

                        Arch.Escribir("RealizarBack: Error BackBD" + error.Message, Archivos.enumEstados.e_error);
                        //throw;
                        LsEnviarCorreo(error.Message);
                        //return;
                    }
                    Arch.Escribir("listo para empaquetar", Archivos.enumEstados.e_log);
                    try
                    {

                        Arch.Escribir("Empaquetando BD", Archivos.enumEstados.e_log);
                        string LvStrClave = "C" + param.NroCAIAgencia.Substring(0, 1) + "@" + param.NroCAIAgencia.Substring(1, 1) + "i" + param.NroCAIAgencia.Substring(2);
                        //string LvStrClave = "2008";

                        string LvStrNomArchivoZip = @"c:\LogCAI\BDCAI" + param.NroCAIAgencia + "_" + DateTime.Now.ToString("yyyyMMddHHmm");
                        string LvStrNomArchivoZipA = "BDCAI" + param.NroCAIAgencia + "_" + DateTime.Now.ToString("yyyyMMddHHmm");
                        //empaquetar con 7zip
                        string LvStrEmpaquetar = @"C:\LogCAI\7za.exe a -sdel -p " + LvStrClave + " " + LvStrNomArchivoZip + " " + LvStrNomArchivoBackup;

                        Arch.Escribir(LvStrEmpaquetar, Archivos.enumEstados.e_log);

                        System.Diagnostics.ProcessStartInfo empaquetaShell = new System.Diagnostics.ProcessStartInfo();
                        empaquetaShell.UseShellExecute = true;
                        empaquetaShell.FileName = "7za.exe";
                        empaquetaShell.WorkingDirectory = @"C:\LogCAI";
                        empaquetaShell.Arguments = "a -sdel -pbdCAI " + LvStrNomArchivoZip + " " + LvStrNomArchivoBackup;
                        empaquetaShell.WindowStyle = ProcessWindowStyle.Hidden;
                        process.StartInfo = empaquetaShell;

                        process.Start();
                        process.WaitForExit();

                        Arch.Escribir("Empaquetado BD", Archivos.enumEstados.e_log);
                        Thread.Sleep(5000);

                        //copiar
                        Arch.Escribir("copiar....BD", Archivos.enumEstados.e_log);
                        string origen = System.IO.Path.Combine(@"C:\LogCAI", LvStrNomArchivoZipA + ".7z");
                        string destino = System.IO.Path.Combine(param.dirBackup, LvStrNomArchivoZipA + ".7z");

                        Arch.Escribir("Origen:" + origen, Archivos.enumEstados.e_log);
                        Arch.Escribir("Destino:" + destino, Archivos.enumEstados.e_log);
                        bool copiado = true;
                        try
                        {
                            System.IO.File.Move(origen, destino);

                            Arch.Escribir("Copiado BD. con IO", Archivos.enumEstados.e_log);
                            Thread.Sleep(50000);
                        }
                        catch (Exception Error)
                        {
                            Arch.Escribir("Error copia BD. Error:" + Error, Archivos.enumEstados.e_error);
                            LsEnviarCorreo("Error copia BD. Error:" + Error.Message);
                            copiado = false;

                        }

                        if (!copiado)
                        {
                            Arch.Escribir("Copiar con CMD", Archivos.enumEstados.e_log);
                            string servidor = GetEntradaConfig("IPServidor");
                            string usuario = GetEntradaConfig("Usuario");
                            string clave = GetEntradaConfig("Clave");

                            string ejecuar = "net use \\" + servidor + " /user:" + usuario + " " + clave;
                            Arch.Escribir("Copiar con CMD", Archivos.enumEstados.e_log);
                            CopiarDesdeCMD(ejecuar, origen, destino);
                            Arch.Escribir("Copiado con CMD", Archivos.enumEstados.e_log);
                        }
                    }
                    catch (Exception error)
                    {

                        Arch.Escribir("RealizarBack: Error CopiaBD" + error.Message, Archivos.enumEstados.e_error);
                        LsEnviarCorreo("Error al Empaquetar. Error:" + error.Message);
                        //return;
                    }
                    //Copiar archivos log
                    Arch.Escribir("Inciando copiar log", Archivos.enumEstados.e_log);
                    try
                    {

                        string DirProyCajeros = "";
                        DateTime fechaLog = System.DateTime.Now;

                        fechaLog = fechaLog.AddDays(-1);

                        if (Environment.Is64BitOperatingSystem)
                        {
                            DirProyCajeros = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\ProyCajeros5\LogCAI\";
                        }
                        else
                        {
                            DirProyCajeros = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\ProyCajeros5\LogCAI\";
                        }
                        string LogCAI = "LOG" + param.NroCAIAgencia + "_" + fechaLog.ToString("yyyyMMdd") + ".log";
                        string LogMFnet = "LogMFNet" + fechaLog.ToString("yyyyMMdd") + ".txt";

                        System.IO.File.Copy(System.IO.Path.Combine(DirProyCajeros, LogCAI), System.IO.Path.Combine(@"C:\logCAI\", LogCAI),true );
                        System.IO.File.Copy(System.IO.Path.Combine(DirProyCajeros, LogMFnet), System.IO.Path.Combine(@"C:\logCAI\", LogMFnet),true);

                        Thread.Sleep(5000);

                        string LvStrNomArchivoZip = @"c:\LogCAI\LogCAI" + param.NroCAIAgencia + "_" + fechaLog.ToString("yyyyMMdd");
                        string LvStrNomArchivoZipA = "LogCAI" + param.NroCAIAgencia + "_" + fechaLog.ToString("yyyyMMdd");
                        //empaquetar con 7zip


                        System.Diagnostics.ProcessStartInfo empaquetaShell = new System.Diagnostics.ProcessStartInfo();
                        empaquetaShell.UseShellExecute = true;
                        empaquetaShell.FileName = "7za.exe";
                        empaquetaShell.WorkingDirectory = @"C:\LogCAI";
                        empaquetaShell.Arguments = "a -sdel " + LvStrNomArchivoZip + " LOG*.*";
                        empaquetaShell.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        process = new System.Diagnostics.Process();
                        process.StartInfo = empaquetaShell;
                        process.Start();
                        process.WaitForExit();


                        Thread.Sleep(5000);

                        //copiar
                        Arch.Escribir("Copiar log con IO", Archivos.enumEstados.e_log);
                        string origen = System.IO.Path.Combine(@"C:\LogCAI", LvStrNomArchivoZipA + ".7z");
                        string destino = System.IO.Path.Combine(param.dirBackup, LvStrNomArchivoZipA + ".7z");

                        Arch.Escribir("Origen:" + origen, Archivos.enumEstados.e_log);
                        Arch.Escribir("Destino:" + destino, Archivos.enumEstados.e_log);

                        bool copiado = true;
                        try
                        {
                            Arch.Escribir("Copiando log con IO", Archivos.enumEstados.e_log);
                            
                            System.IO.File.Move(origen, destino);
                        }
                        catch (Exception error)
                        {
                            Arch.Escribir("Error al copiar log con IO", Archivos.enumEstados.e_log);
                            LsEnviarCorreo("Error al copiar log. Error:" + error.Message);
                            copiado = false;

                        }
                        if (!copiado)
                        {
                            Arch.Escribir("Copiar log con CMD", Archivos.enumEstados.e_log);
                            string servidor = GetEntradaConfig("IPServidor");
                            string usuario = GetEntradaConfig("Usuario");
                            string clave = GetEntradaConfig("Clave");

                            string ejecuar = "net use \\" + servidor + " /user:" + usuario + " " + clave;
                            Arch.Escribir("Copiando log con CMD", Archivos.enumEstados.e_log);
                            CopiarDesdeCMD(ejecuar, origen, destino);
                            Arch.Escribir("Copiado log con CMD", Archivos.enumEstados.e_log);
                        }

                        Thread.Sleep(5000);
                    }
                    catch (Exception error)
                    {
                        Arch.Escribir("RealizarBack: Error CopiaLOG" + error.Message, Archivos.enumEstados.e_error);
                        LsEnviarCorreo("Error al copiar log a carpeta log. error:" + error.Message);
                    }

                }
                catch (Exception error)
                {
                    Arch.Escribir("RealizarBack: Error Grl:" + error.Message, Archivos.enumEstados.e_error);
                    //throw;
                }
                Arch.Escribir("SALIR copia de seguridad", Archivos.enumEstados.e_log);

            }
        }

        private static bool IsExecutingApplication()
        {
            // Proceso actual
            Process currentProcess = Process.GetCurrentProcess();

            // Matriz de procesos
            Process[] processes = Process.GetProcesses();

            // Recorremos los procesos en ejecución
            foreach (Process p in processes)
            {
                if (p.Id != currentProcess.Id)
                {
                    if (p.ProcessName == currentProcess.ProcessName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static void ExecuteCommand(string _Command)
        {
            //Indicamos que deseamos inicializar el proceso cmd.exe junto a un comando de arranque. 
            //(/C, le indicamos al proceso cmd que deseamos que cuando termine la tarea asignada se cierre el proceso).
            //Para mas informacion consulte la ayuda de la consola con cmd.exe /? 
            System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + _Command);
            // Indicamos que la salida del proceso se redireccione en un Stream
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            //Indica que el proceso no despliegue una pantalla negra (El proceso se ejecuta en background)
            procStartInfo.CreateNoWindow = false;
            //Inicializa el proceso
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            //Consigue la salida de la Consola(Stream) y devuelve una cadena de texto
            string result= proc.StandardOutput.ReadToEnd();
            //Muestra en pantalla la salida del Comando
            //Console.WriteLine(result);
            //return result;
        }
    }
    class datosParam
    {
        public string NroCAIAgencia { get; set; }
        public string dirBackup { get; set; }
        public string correo { get; set; }
        public string BackUpTranac { get; set; }
        public string TipoLectorTI { get; set; }
    }
}
