using DPUruNet;
using Finger.Component.Component;
using Finger.Component.Util;
using FingerCaptureService.FormFinger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace FingerCaptureService
{
    static class Program
    {
        public static int timeoutCloseForm = Convert.ToInt32(ConfigurationManager.AppSettings["timeoutCloseForm"]);
        public static bool processing = false;
        public static Action<List<string>, string> ReleaseMethodGetOneFinger;
        public static FrmDummy frm1 = null;
        public static DateTime IniProcess { get; set; }
        internal static ServiceHost myServiceHost = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frm1 = new FrmDummy();
            try
            {
                myServiceHost = new ServiceHost(typeof(Service.FingerCapture));
                myServiceHost.Open();
            }
            catch (Exception ex)
            {
                Logger.Write("Imposible lenvantar el servicio: " + ex.Message, EventLogEntryType.Information);
                throw ex;
            }

            Logger.Write("Servicio de Captura de Huellas iniciado", EventLogEntryType.Information);
            Application.Run();
        }

        [STAThread]

        public static void InitGetOneFinger(Action<List<string>, string> ReleaseMethod)
        {
            processing = true;
            ReleaseMethodGetOneFinger = ReleaseMethod;
            ReaderFinger.ActionCapture = _currentReader_On_Captured;
            ReaderFinger.ActivateCaptureAsync();
            frm1.Show();
            frm1.WindowState = FormWindowState.Normal;
            frm1.BringToFront();
            frm1.TopLevel = true;
            frm1.Focus();
            IniProcess = DateTime.Now;
        }

        [STAThread]

        public static void CloseProcess()
        {
            processing = false;
            ReaderFinger.ActionCapture = null;
            ReaderFinger.Release();
            frm1.Hide();
        }

        [STAThread]

        private static void _currentReader_On_Captured(CaptureResult result)
        {
            try
            {
                Bitmap image = null;
                if (result.Data != null)
                {
                    foreach (Fid.Fiv fiv in result.Data.Views)
                    {
                        image = CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                    }
                }
                Bitmap newImage = new Bitmap(200, 200);
                using (Graphics gr = Graphics.FromImage(newImage))
                {
                    gr.SmoothingMode = SmoothingMode.Default;
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    gr.DrawImage(image, new Rectangle(0, 0, 200, 200));
                }

                ImageConverter converter = new ImageConverter();
                string base64String = Convert.ToBase64String((byte[])converter.ConvertTo(newImage, typeof(byte[])));

                DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(result.Data, Constants.Formats.Fmd.DP_VERIFICATION);

                if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    throw new Exception($"No se puede (formato DP) capturar la huella error: {resultConversion.ResultCode.ToString()}");
                }


                List<string> lCompositeFinger = new List<string>();
                lCompositeFinger.Add(Fmd.SerializeXml(resultConversion.Data));

                DataResult<Fmd> resultConversionISO = FeatureExtraction.CreateFmdFromFid(result.Data, Constants.Formats.Fmd.ISO);


                if (resultConversionISO.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    throw new Exception($"No se puede (formato ISO) capturar la huella error: {resultConversionISO.ResultCode.ToString()}");
                }

                lCompositeFinger.Add(Fmd.SerializeXml(resultConversionISO.Data));

                if (ReleaseMethodGetOneFinger != null)
                {
                    ReleaseMethodGetOneFinger(lCompositeFinger, base64String);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
                if (ReleaseMethodGetOneFinger != null)
                {
                    ReleaseMethodGetOneFinger(null, null);
                }
            }

        }

        private static Bitmap CreateBitmap(byte[] bytes, int width, int height)
        {
            byte[] rgbBytes = new byte[bytes.Length * 3];

            for (int i = 0; i <= bytes.Length - 1; i++)
            {
                rgbBytes[(i * 3)] = bytes[i];
                rgbBytes[(i * 3) + 1] = bytes[i];
                rgbBytes[(i * 3) + 2] = bytes[i];
            }
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            for (int i = 0; i <= bmp.Height - 1; i++)
            {
                IntPtr p = new IntPtr(data.Scan0.ToInt64() + data.Stride * i);
                System.Runtime.InteropServices.Marshal.Copy(rgbBytes, i * bmp.Width * 3, p, bmp.Width * 3);
            }

            bmp.UnlockBits(data);
            return bmp;
        }
    }
}
