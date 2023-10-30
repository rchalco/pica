using DPUruNet;
using Prodem.Fingerprint.FingerPrintControl.Domain;
using Prodem.Fingerprint.FingerPrintControl.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace Prodem.Fingerprint.FingerPrintControl.FingerEnroll
{
    public partial class EnrollOneFingerControl : UserControl
    {

        private int nCaptured = 0;
        private const int DPFJ_PROBABILITY_ONE = 0x7fffffff;

        public Action<string, byte[]> ReleaseResul { get; set; }
        public TypeEnroll typeEnroll { get; set; }
        private List<Fmd> lEnrrollResul = new List<Fmd>();
        public byte[] LastImageFinger { get; set; }

        private List<Fmd> ColFingerCapture { get; set; }

        private ImageConverter converterImg = new ImageConverter();
        public Panel[] PanelNumber { get; set; }

        private Constants.Formats.Fmd FormatFid = Constants.Formats.Fmd.DP_PRE_REGISTRATION;
        private Constants.Formats.Fmd FormatFmd = Constants.Formats.Fmd.DP_REGISTRATION;

        public EnrollOneFingerControl(TypeEnroll _typeEnroll)
        {
            InitializeComponent();
            typeEnroll = _typeEnroll;


        }

        private void EnrollOneFingerControl_Load(object sender, EventArgs e)
        {
            PanelNumber = new Panel[] { N1, N2, N3, N4 };
            ReaderFinger.ActionCapture = _currentReader_On_Captured;
            ReaderFinger.BreakApplicationError = false;

            PanelNumber.ToList().ForEach(x => { x.BackColor = Color.White; });
            if (typeEnroll == TypeEnroll.DP)
            {
                FormatFid = Constants.Formats.Fmd.DP_PRE_REGISTRATION;
                FormatFmd = Constants.Formats.Fmd.DP_REGISTRATION;
            }
            else if (typeEnroll == TypeEnroll.ISO)
            {
                FormatFid = Constants.Formats.Fmd.ISO;
                FormatFmd = Constants.Formats.Fmd.ISO;
            }
        }



        public void CaptureFinger()
        {
            nCaptured = 0;
            ReaderFinger.ActivateCaptureAsync();
        }


        public void Release()
        {
            ReaderFinger.ActionCapture = null;
            ReaderFinger.Release();
            Dispose();
        }

        private void _currentReader_On_Captured(CaptureResult result)
        {
            try
            {
                if (nCaptured == 0)
                {
                    ColFingerCapture = new List<Fmd>();
                    lEnrrollResul.Clear();
                }

                if (result.Data != null)
                {
                    foreach (Fid.Fiv fiv in result.Data.Views)
                    {
                        Image imgFinger = CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                        LastImageFinger = (byte[])converterImg.ConvertTo(imgFinger, typeof(byte[]));

                        pbFingerprint.Image = imgFinger;

                    }

                }

                DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(result.Data, FormatFid);

                DataResult<Fmd> resultConversionVer = typeEnroll == TypeEnroll.ISO ? FeatureExtraction.CreateFmdFromFid(result.Data, FormatFid)
                    : FeatureExtraction.CreateFmdFromFid(result.Data, Constants.Formats.Fmd.DP_VERIFICATION);

                if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    return;
                }

                ColFingerCapture.Add(resultConversionVer.Data);

                PanelNumber[nCaptured].BackColor = Color.Transparent;
                nCaptured++;
                lEnrrollResul.Add(resultConversion.Data);

                if (nCaptured >= 4)
                {
                    nCaptured = 0;
                    var resultEnrollment = DPUruNet.Enrollment.CreateEnrollmentFmd(FormatFmd, lEnrrollResul);
                    lEnrrollResul.Clear();
                    PanelNumber.ToList().ForEach(x => { x.BackColor = Color.White; });
                                        

                    CompareAllCapturesWithResult(ColFingerCapture, resultEnrollment.Data);

                    ColFingerCapture = new List<Fmd>();

                    if (ReleaseResul != null)
                    {
                        var rsulXml = resultEnrollment.Data != null ? Fmd.SerializeXml(resultEnrollment.Data) : null;
                        ReleaseResul(rsulXml, LastImageFinger);
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
                MessageBox.Show($"Imposible capturar huellas: {ex.Message}");
            }

        }

        private void CompareAllCapturesWithResult(List<Fmd> colFingersCapture, Fmd fingerResult)
        {
            if (colFingersCapture != null
                && colFingersCapture.Count > 0)
            {
                int thresholdScore = DPFJ_PROBABILITY_ONE * 1 / 100000;
                IdentifyResult identifyResult = null;
                List<Fmd> colResults = new List<Fmd>();
                colResults.Add(fingerResult);

                foreach (var fingerCapture in colFingersCapture)
                {
                    identifyResult = Comparison.Identify(fingerCapture, 0, colResults, thresholdScore, 1);

                    if (identifyResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                    {
                        throw new Exception($"Error al capturar la huella, por favor intente nuevamente.");
                    }
                }
            }
        }

        public static Bitmap CreateBitmap(byte[] bytes, int width, int height)
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
