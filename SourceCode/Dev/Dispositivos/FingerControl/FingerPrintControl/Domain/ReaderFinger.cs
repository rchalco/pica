using DPUruNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Prodem.Fingerprint.FingerPrintControl.Domain
{
    public static class ReaderFinger
    {
        //private static bool isOpenReader = false;
        private static Reader _currentReader = ReaderCollection.GetReaders()[0];
        public static bool BreakApplicationError { get; set; } = true;
        public static Action<CaptureResult> ActionCapture { get; set; }


        public static Reader GetCurrentReader()
        {
            GetStatus();
            return _currentReader;
        }

        public static Reader TestConnect()
        {
            OpenReader();
            return _currentReader;
        }

        public static void OpenReader()
        {
            _currentReader = ReaderCollection.GetReaders()[0];
            if (_currentReader == null)
            {
                if (BreakApplicationError)
                {
                    Application.Exit();
                }
                else
                {
                    throw new Exception("No existe lectores de huella conectados!");
                }

            }
            var resul = _currentReader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE );
            if (resul != Constants.ResultCode.DP_SUCCESS)
            {
                throw new Exception($"No se puede conectar con el dispositivo. Error {resul.ToString()}");
            }
            _currentReader.On_Captured += _currentReader_On_Captured;
            //isOpenReader = true;
        }

        public static void GetStatus()
        {
            if (_currentReader == null)
            {
                OpenReader();
            }
            var result = _currentReader.GetStatus();
            if (_currentReader.Status == null)
                OpenReader();
            else if (_currentReader.Status.Status == Constants.ReaderStatuses.DP_STATUS_BUSY)
                Thread.Sleep(500);
            else if (_currentReader.Status.Status == Constants.ReaderStatuses.DP_STATUS_NEED_CALIBRATION)
                _currentReader.Calibrate();
            else if (_currentReader.Status.Status != Constants.ReaderStatuses.DP_STATUS_READY)
            {
                Reset();
                throw new Exception("Error en el dispositivo Reader Status - " + _currentReader.Status.Status.ToString());
            }


        }

        public static void Reset()
        {
            _currentReader.Reset();

        }

        private static void _currentReader_On_Captured(CaptureResult result)
        {
            if (ActionCapture != null)
                ActionCapture(result);
        }

        public static void ActivateCaptureAsync()
        {
            GetStatus();
            var captureResult = ReaderFinger.GetCurrentReader().CaptureAsync(Constants.Formats.Fid.ISO, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, _currentReader.Capabilities.Resolutions[0]);
        }

        public static void Release()
        {
            if (_currentReader != null)
            {
                _currentReader.CancelCapture();

                // Dispose of reader handle and unhook reader events.
                _currentReader.Dispose();
                _currentReader = null;
            }
        }
    }
}
