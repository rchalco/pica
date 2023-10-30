using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prodem.Fingerprint.FingerPrintControl.Domain;
using DPUruNet;
using System.Diagnostics;
using Prodem.Fingerprint.FingerPrintControl.Util;
using System.Drawing.Imaging;
using System.Threading;

namespace Prodem.Fingerprint.FingerPrintControl.FingerEnroll
{
    public partial class GetFingerControl : UserControl
    {


        public Action<List<string>> ReleaseFingerSerialize { get; set; }

        
        public GetFingerControl()
        {
            InitializeComponent();
            ReaderFinger.GetCurrentReader();
            ReaderFinger.ActionCapture = _currentReader_On_Captured;
        }

        private void GetFingerControl_Load(object sender, EventArgs e)
        {

        }

        public void ActiveCapture()
        {
            /*if(!ReaderFinger.ActivateCaptureAsync())
                MessageBox.Show("No se puede conectar con el lecto de huella, verifique que se encuentre conectado!"); */
            ReaderFinger.ActivateCaptureAsync();
        }



        private void _currentReader_On_Captured(CaptureResult result)
        {
            try
            {
                if (result.Data != null)
                {
                    foreach (Fid.Fiv fiv in result.Data.Views)
                    {
                        pbFinger.Image = CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                    }
                }

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

                if (ReleaseFingerSerialize != null)
                {
                    ReleaseFingerSerialize(lCompositeFinger);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
                MessageBox.Show("Por favor verifique si su lector de huella esta conectado", "Problemas con el lector de huella");
            }


        }

        public Bitmap CreateBitmap(byte[] bytes, int width, int height)
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

        public void Release()
        {
            ReaderFinger.ActionCapture = null;
            ReaderFinger.Release();
            Dispose();
        }
    }
}
