using Foundation.Stone.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using DPUruNet;
using System.Text;
using System.Threading;
using Foundation.Stone.CrossCuting.Util;
using Microsoft.Win32.TaskScheduler;
using Foundation.Stone.Business.Core;
using RuntimeFingerPrint.Global;
using Interop.Main.Cross.Domain.FingerPrint;
using Interop.Main.Service.Interface;
using Foundation.Stone.CrossCuting.StoneException.Enumerators;
using Foundation.Stone.CrossCuting.StoneException;

namespace RuntimeFingerPrint.Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single, MaxItemsInObjectGraph = 2147483647)]
    public class FingerPrintServices : BaseManager, IFingerPrintServices, IFingerPrintInterop
    {
        private static Reader currentReader;

        private Constants.Formats.Fmd EnrollFormatFid = Constants.Formats.Fmd.DP_PRE_REGISTRATION;
        private Constants.Formats.Fmd EnrollFormatFmd = Constants.Formats.Fmd.DP_REGISTRATION;

        public FingerPrintServices()
        {

        }

        private void InitReader(bool force = false)
        {
            using (TaskService taskService = new TaskService())
            {
                if (currentReader == null || force)
                {
                    if (ReaderCollection.GetReaders().Count == 0)
                    {
                        throw new Exception("No se tiene conectaodo ningun lector de huella!");
                    }
                    currentReader = ReaderCollection.GetReaders()[0];
                    currentReader.Open(Constants.CapturePriority.DP_PRIORITY_EXCLUSIVE);
                }
            }

        }


        public Response GetStatus()
        {
            InitReader();
            Response response = new Response { State = ResponseType.Success, Message = "state sucess" };

            Constants.ResultCode result = currentReader.GetStatus();

            if ((result != Constants.ResultCode.DP_SUCCESS))
            {
                currentReader.Reset();
                response.State = ResponseType.Warning;
                response.Message = $"state {result}";
            }

            if ((currentReader.Status.Status == Constants.ReaderStatuses.DP_STATUS_BUSY))
            {
                Thread.Sleep(50);
            }
            else if ((currentReader.Status.Status == Constants.ReaderStatuses.DP_STATUS_NEED_CALIBRATION))
            {
                currentReader.Calibrate();
                response.State = ResponseType.Success;
                response.Message = $"state {result}";
            }
            else if ((currentReader.Status.Status != Constants.ReaderStatuses.DP_STATUS_READY))
            {
                response.State = ResponseType.Warning;
                response.Message = $"state {currentReader.Status.Status}";
            }
            if (response.State != ResponseType.Success)
            {
                InitReader(true);
            }
            return response;
        }

        private void CloseReader()
        {
            currentReader?.StopStreaming();
            currentReader?.Dispose();
            currentReader = null;
        }

        public Response CaptureFinger(int timeOut)
        {
            Response resul = new Response { CodeBase = "", Message = "", State = ResponseType.Success };

            try
            {
                resul = GetStatus();
                if (resul.State != ResponseType.Success)
                {
                    return resul;
                }

                var OnCaptured = currentReader.Capture(Constants.Formats.Fid.ISO, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, timeOut, currentReader.Capabilities.Resolutions[0]);
                if (OnCaptured.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    resul.State = ResponseType.Warning;
                    resul.Message = $"Error en la captura: {OnCaptured.ResultCode}";
                    CloseReader();
                    return resul;
                }
                DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(OnCaptured.Data, Constants.Formats.Fmd.DP_VERIFICATION);
                if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    resul.State = ResponseType.Warning;
                    resul.Message = "Huella no capturada";
                    CloseReader();
                    return resul;
                }
                List<string> lCompositeFinger = new List<string>();
                lCompositeFinger.Add(Fmd.SerializeXml(resultConversion.Data));
                DataResult<Fmd> resultConversionISO = FeatureExtraction.CreateFmdFromFid(OnCaptured.Data, Constants.Formats.Fmd.ISO);

                if (resultConversionISO.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    resul.State = ResponseType.Warning;
                    resul.Message = "Huella no capturada";
                    CloseReader();
                    return resul;
                }

                lCompositeFinger.Add(Fmd.SerializeXml(resultConversionISO.Data));
                resul.Message = Convert.ToBase64String(lCompositeFinger.BinarySerialize());
                CloseReader();

            }
            catch (Exception ex)
            {
                //ProcessError(ex, resul, Setings.LogConfigName, null);
                ExceptionManager.ProcessError(new StoneException(ex, CodeExceptionAplication.ErrorGenerico), "FingerDevice");
                resul.State = ResponseType.Error;
                resul.Message = ex.Message;
            }

            return resul;
        }

        public Response CaptureFingerForEnroll(FingerContract.TypeEnroll typeEnroll, int timeOut)
        {
            Response resul = new Response { CodeBase = "", Message = "", State = ResponseType.Success };

            try
            {
                resul = GetStatus();
                if (resul.State != ResponseType.Success)
                {
                    return resul;
                }

                EnrollFormatFid = typeEnroll == FingerContract.TypeEnroll.ISO ? Constants.Formats.Fmd.ISO : Constants.Formats.Fmd.DP_PRE_REGISTRATION;
                var OnCaptured = currentReader.Capture(Constants.Formats.Fid.ISO, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, timeOut, currentReader.Capabilities.Resolutions[0]);
                if (OnCaptured.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    resul.State = ResponseType.Warning;
                    resul.Message = $"Error en la captura: {OnCaptured.ResultCode}";
                    CloseReader();
                    return resul;
                }
                DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(OnCaptured.Data, EnrollFormatFid);
                if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    resul.State = ResponseType.Warning;
                    resul.Message = "Huella no capturada";
                    CloseReader();
                    return resul;
                }
                resul.Message = Fmd.SerializeXml(resultConversion.Data);
                CloseReader();
            }
            catch (Exception ex)
            {
                ProcessError(ex, resul, Setings.LogConfigName, null);
            }

            return resul;
        }

        public ResponseObject<string> Enroll(FingerContract.TypeEnroll typeEnroll, List<string> fingersToEnrroll)
        {
            ResponseObject<string> resul = new ResponseObject<string> { CodeBase = "", Message = "", State = ResponseType.Success, Object = string.Empty };
            //var resulDEs = Fmd.DeserializeXml("<?xml version=\"1.0\" encoding=\"UTF - 8\"?><Fid><Bytes>Rk1SACAyMAAAAAG2AAABZQGIAMUAxQEAAABWRIBaAMrLZECeADxzY4A4AP65YkAtALuwYoDzAOy3YUBdAObLYEBgAGOhX4EEAH3TX4BpALHFX4BuAUVoXYD1AUWTXECoAFZrW4CyAP1pWUAyARWhWUBDAOG9WUCHAHeQWEBOATlyWIC0ACdwV4BAAQnPV0DMAEVmVkCGAThgVYCAANjaVIClAUYDVEDHAF3TVEDEASsfVID/AKtOU0D/ATgaU4BKASNTUoCWANfsUkDpATYgUUDXABngUYD2AFJdUYCGALbdUUC8AL63UIERANzHUEBKAKW5T4CiAKdcTkDuAErbTEDqAQo7TIDwALvHTEBgAIcjS0DQAQSiS0CBARVZS0CBAS3pS0A/AECcSoCrAIpfSkC9AOmHSICoAPYSSEDqAVcTSEBFADWVR4CxAR1uR0BmAKc1RkBbAKm7RUBuAG6mRYCjALFfRECxAWF9QkA/AQ3JQYB3AJvAQUBdAIooP0CKAKPfP0ClAOwJP0DfAQ2mP0BUAC4HPkA8AIQpPkCtARkQPYDxAPm2PYEEATwWPIARAISuOwAA</Bytes><Format>16842753</Format><Version>1.0.0</Version></Fid>");

            try
            {
                if (fingersToEnrroll == null || fingersToEnrroll.Count < 4)
                {
                    resul.Message = "Argumento fingersToEnrroll vacio, imposible continuar!";
                    resul.State = ResponseType.Warning;
                    return resul;
                }
                if (typeEnroll == FingerContract.TypeEnroll.DP)
                {
                    EnrollFormatFid = Constants.Formats.Fmd.DP_PRE_REGISTRATION;
                    EnrollFormatFmd = Constants.Formats.Fmd.DP_REGISTRATION;
                }
                else if (typeEnroll == FingerContract.TypeEnroll.ISO)
                {
                    EnrollFormatFid = Constants.Formats.Fmd.ISO;
                    EnrollFormatFmd = Constants.Formats.Fmd.ISO;
                }

                List<Fmd> lEnrrollResul = new List<Fmd>();
                fingersToEnrroll.ForEach(x =>
                {
                    lEnrrollResul.Add(Fmd.DeserializeXml(x));
                });

                var resultEnrollment = DPUruNet.Enrollment.CreateEnrollmentFmd(EnrollFormatFmd, lEnrrollResul);
                resul.Message = Convert.ToBase64String(Fmd.SerializeXml(resultEnrollment.Data).BinarySerialize());
            }
            catch (Exception ex)
            {
                //ProcessError(ex, resul, Setings.LogConfigName, null);
                ExceptionManager.ProcessError(new StoneException(ex, CodeExceptionAplication.ErrorGenerico), "FingerDevice");
                resul.State = ResponseType.Error;
                resul.Message = ex.Message;
            }
            return resul;

        }
    }
}
