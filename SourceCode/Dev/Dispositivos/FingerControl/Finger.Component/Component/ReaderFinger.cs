using DPUruNet;
using Finger.Component.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Finger.Component.Component
{
    public static class ReaderFinger
    {

        private static Reader _currentReader = null;
        public static bool BreakApplicationError { get; set; } = true;
        public static Action<CaptureResult> ActionCapture { get; set; }


        public static Reader GetCurrentReader()
        {
            ManageStatus();
            return _currentReader;
        }

        public static bool TestConnect()
        {
            try
            {
                OpenReader();
                Release();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
                return false;
            }

        }

        public static bool OpenReader()
        {
            _currentReader = null;
            if (ReaderCollection.GetReaders().Count > 0)
            {
                _currentReader = ReaderCollection.GetReaders()[0];
            }
            else
            {
                throw new Exception($"No existe lectoras de huellas disponibles o conectados");
            }

            if (_currentReader == null)
            {
                return false;
            }
            var resul = _currentReader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);
            if (resul != Constants.ResultCode.DP_SUCCESS)
            {
                throw new Exception($"No se puede conectar con el lector de huella. Error {resul.ToString()}");
            }
            _currentReader.On_Captured += _currentReader_On_Captured;
            return true;

        }

        public static void ManageStatus()
        {
            if (_currentReader == null)
            {
                if (!OpenReader())
                {
                    throw new Exception($"Imposible conectarse al lector de huella");
                }

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
            ManageStatus();
            var captureResult = ReaderFinger.GetCurrentReader().CaptureAsync(Constants.Formats.Fid.ISO, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, _currentReader.Capabilities.Resolutions[0]);
        }

        public static void Release()
        {
            if (_currentReader != null)
            {
                _currentReader.CancelCapture();
                _currentReader.Dispose();
                _currentReader = null;
            }
        }
    }
}
