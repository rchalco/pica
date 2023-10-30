using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace HostFingerControl
{
    static class Program
    {

        public static bool processing = false;
        public static DateTime IniProcess { get; set; }

        public static MainForm frmMain = null;
        public static int timeoutCloseForm = Convert.ToInt32(ConfigurationManager.AppSettings["timeoutCloseForm"]);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);
            frmMain = new MainForm();
            Hosting.Host.Start();
            Application.Run();
        }

        [STAThread]
        public static void InitFrmGetOneFinger(Action<List<string>> ReleaseMethod)
        {
            processing = true;
            if (frmMain == null)
            {
                frmMain = new MainForm();
            }
            frmMain.ReleaseFingerSerialize = ReleaseMethod;
            frmMain.mode = MainForm.Mode.GetFinger;
            frmMain.WindowState = FormWindowState.Normal;
            frmMain.StartPosition = FormStartPosition.CenterScreen;
            frmMain.Show();
            frmMain.BringToFront();

            frmMain.TopMost = true;
            //frmMain.SuspendLayout();
            frmMain.IniContainer();
            IniProcess = DateTime.Now;
        }


        [STAThread]
        public static void InitFrmEnroll(Action<string, byte[]> ReleaseMethod)
        {
            processing = true;
            if (frmMain == null)
            {
                frmMain = new MainForm();
            }
            frmMain.ReleaseFingerEnroll = ReleaseMethod;
            frmMain.mode = MainForm.Mode.Enroll;
            frmMain.WindowState = FormWindowState.Normal;
            frmMain.StartPosition = FormStartPosition.CenterScreen;
            frmMain.Show();
            frmMain.BringToFront();
            frmMain.TopMost = true;
            frmMain.IniContainer();            
            IniProcess = DateTime.Now;
        }


        [STAThread]
        public static void CloseFrm()
        {
            frmMain?.Release();
        }

       
    }
}
