using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DPUruNet;
using System.Threading;
using Prodem.Fingerprint.FingerPrintControl.Domain;

namespace Prodem.Fingerprint.FingerPrintControl.FingerEnroll
{
    public partial class CompareFinger : UserControl
    {

        public Fmd[] FIngersToCompare { get; set; }
        private const int DPFJ_PROBABILITY_ONE = 0x7fffffff;

        public Action<bool> ReleaseCompare { get; set; }
        
        private Constants.Formats.Fmd FormatFmd;
      


        public CompareFinger(Fmd[] _FIngerToCompare, TypeEnroll _TypeEnroll)
        {
            FIngersToCompare = _FIngerToCompare;
            FormatFmd = _TypeEnroll == TypeEnroll.DP ? Constants.Formats.Fmd.DP_VERIFICATION : Constants.Formats.Fmd.ISO;
            InitializeComponent();
        }

        private void CompareFinger_Load(object sender, EventArgs e)
        {
            ReaderFinger.ActionCapture = _currentReader_On_Captured;
        }

        public void ComparerFingerAction()
        {
            ReaderFinger.ActivateCaptureAsync();
        }


        private void _currentReader_On_Captured(CaptureResult result)
        {
            if (result.Data != null)
            {
                foreach (Fid.Fiv fiv in result.Data.Views)
                {
                    pbFinger.Image = FigersWrapper.CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                }

            }
            DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(result.Data, FormatFmd);

            if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
            {
                throw new Exception($"No se puede capturar la huella error: {resultConversion.ResultCode.ToString()}");
            }
            int thresholdScore = DPFJ_PROBABILITY_ONE * 1 / 100000;
            IdentifyResult identifyResult = Comparison.Identify(resultConversion.Data, 0, FIngersToCompare, thresholdScore, 2);

            if (identifyResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                throw new Exception($"Imposible comparar huellas error: {identifyResult.ResultCode.ToString()}");

            if (ReleaseCompare != null)
            {
                ReleaseCompare((identifyResult.Indexes.Length >= 1));
            }


        }


    }
}
