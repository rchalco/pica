using DPUruNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using Prodem.Fingerprint.FingerPrintControl.Domain;

namespace Prodem.Fingerprint.FingerPrintControl.Domain
{

    public enum Finger
    {
        F1 = 1,
        F2 = 2,
        F3 = 3,
        F4 = 4,
        F5 = 5,
        F6 = 6,
        F7 = 7,
        F8 = 8,
        F9 = 9,
        F10 = 10
    }

    public enum TypeEnroll
    {
        ISO = 0,
        DP = 1
    }
    public class FingerEnroll
    {
        public Finger FingerIndex { get; set; }
        public DataResult<Fmd> ResulCapture { get; set; }
        public Boolean Activate { get; set; }
    }
    public class FigersWrapper: IDisposable
    {


        public Dictionary<RadioButton, FingerEnroll> UIFinger { get; set; }

        public PictureBox imageFinger { get; set; }

        public byte[] LastImageFinger { get; set; }

        public Panel[] PanelNumber { get; set; }
        private int nCaptured = 0;
        private ImageConverter converterImg = new ImageConverter();
        private List<Fmd> lEnrrollResul = new List<Fmd>();
        public bool Captured { get; set; }

        public bool SuccessEnroll { get; set; }

        private Constants.Formats.Fmd FormatFid = Constants.Formats.Fmd.DP_PRE_REGISTRATION;
        private Constants.Formats.Fmd FormatFmd = Constants.Formats.Fmd.DP_REGISTRATION;


        public FigersWrapper(RadioButton[] _radios, Panel[] _panelNumber, TypeEnroll typeEnroll)
        {
            Captured = false;
            //ReaderFinger.GetCurrentReader().Reset();
            ReaderFinger.ActionCapture = _currentReader_On_Captured;

            PanelNumber = _panelNumber;
            PanelNumber.ToList().ForEach(x => { x.BackColor = Color.White; });

            UIFinger = new Dictionary<RadioButton, FingerEnroll>();
            int cont = 1;
            foreach (var item in _radios)
            {
                item.CheckedChanged += ActivateCapture;
                UIFinger.Add(item, new FingerEnroll() { FingerIndex = (Finger)cont });
                cont++;
            }

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

        public void ActivateCapture(object sender, EventArgs e)
        {
            Captured = false;
            RadioButton radio = (RadioButton)sender;
            UIFinger[radio].Activate = radio.Checked;
            if (radio.Checked)
            {
                nCaptured = 0;
                ReaderFinger.ActivateCaptureAsync();
            }
        }



        private void _currentReader_On_Captured(CaptureResult result)
        {
            if (!UIFinger.Values.Any(x => x.Activate))
                return;
            FingerEnroll currentFinger = UIFinger.Values.First(x => x.Activate);

            if (nCaptured == 0) lEnrrollResul.Clear();

            if (result.Data != null)
            {
                foreach (Fid.Fiv fiv in result.Data.Views)
                {
                    Image imgFinger = CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                    LastImageFinger = (byte[])converterImg.ConvertTo(imgFinger, typeof(byte[]));

                    imageFinger.Image = imgFinger;

                }

            }
                        

            DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(result.Data, FormatFid);

            if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
            {
                //throw new Exception($"No se puede capturar la huella error: {resultConversion.ResultCode.ToString()}");
                return;
            }

            PanelNumber[nCaptured].BackColor = Color.Transparent;
            nCaptured++;
            lEnrrollResul.Add(resultConversion.Data);

            if (nCaptured >= 4)
            {
                var resultEnrollment = DPUruNet.Enrollment.CreateEnrollmentFmd(FormatFmd, lEnrrollResul);
                lEnrrollResul.Clear();
                currentFinger.Activate = false;
                PanelNumber.ToList().ForEach(x => { x.BackColor = Color.White; });
                currentFinger.ResulCapture = resultEnrollment;
                SuccessEnroll = resultEnrollment.ResultCode == Constants.ResultCode.DP_SUCCESS;

                //UIFinger.Keys.ToList().ForEach(x => x.Checked = false);
                //Util.Serialize(resultEnrollment.Data);
                //DAL.GuardarHuella(Fmd.SerializeXml(resultEnrollment.Data));

                Captured = true;
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

        public void Release()
        {
            ReaderFinger.ActionCapture = null;
            ReaderFinger.Release();
            Dispose();
        }

        public void Dispose()
        {
            
        }
    }
}
