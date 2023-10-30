using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HostFingerControl.Contract;
using System.Windows.Forms;
using System.Threading;
using Prodem.Fingerprint.FingerPrintControl.Util;
using Prodem.Fingerprint.FingerPrintControl.Domain;
using System.Configuration;
using Foundation.Stone.CrossCuting.Util;
//using System.Threading.Tasks;

namespace HostFingerControl.Service
{

    public class FingerComponentService : IFingerComponentService
    {

        public ResulFinger GetOneFinger()
        {
            ResulFinger resul = new ResulFinger();
            try
            {
                resul.State = State.Success;
                resul.Message = "Huella obtenida correctamente";

                Program.InitFrmGetOneFinger((List<string> resulCapture) =>
                {
                    if (resulCapture == null)
                    {
                        resul.State = State.Warning;
                        resul.Message = "Se cancelo la lectura de huella";
                    }
                    resul.Finger = resulCapture;
                });

                while (Program.processing)
                {
                    if (Program.processing && Program.IniProcess.AddSeconds(Program.timeoutCloseForm) < DateTime.Now)
                    {
                        resul.State = State.Warning;
                        resul.Message = "Cancelacion por timeout";
                        Program.processing = false;
                        Program.CloseFrm();
                    }
                    Thread.CurrentThread.Join(50);
                }
                Program.CloseFrm();

            }
            catch (Exception ex)
            {
                resul.State = State.Error;
                resul.Message = "Error el obtener la huella";
                Logger.Write(ex.Message);
            }
            return resul;
        }


        public ResulFinger TestFingers(long IdPerson)
        {
            ResulFinger resul = new ResulFinger();
            ResulFinger resulGetOneFinger = GetOneFinger();
            Data.MFQATeamEntities context = new Data.MFQATeamEntities();
            var resulDB = context.tFingerprint.Where(x => x.IdPerson == IdPerson);
            if (resulDB == null || resulDB.Count() == 0)
            {
                resul.Message = "la persona no cuenta con huellas registradas";
                resul.State = State.Warning;
                return resul;
            }
            using (ServiceCompare.FingerprintCompServiceClient client = new ServiceCompare.FingerprintCompServiceClient())
            {
                ServiceCompare.FingerprintDTO fingerprintDTO = new ServiceCompare.FingerprintDTO
                {
                    FingerIdentifier = 0,
                    FingerprintFormat = ServiceCompare.FingerprintFormat.DP,
                    FingerprintProcess = 0,
                    FingerprintSize = 0,
                    FingerprintStatus = 0,
                    ObjFingerprint = resulGetOneFinger.Finger.BinarySerialize(),
                    Origin = ServiceCompare.Origin.MFNET
                };
                List<ServiceCompare.FingerprintDTO> fingertoCompare = new List<ServiceCompare.FingerprintDTO>();
                resulDB.ToList().ForEach(x =>
                {
                    ServiceCompare.FingerprintDTO fingerprintDTOToCompare = new ServiceCompare.FingerprintDTO
                    {
                        FingerIdentifier = 0,
                        FingerprintFormat = ServiceCompare.FingerprintFormat.DP,
                        FingerprintProcess = 0,
                        FingerprintSize = 0,
                        FingerprintStatus = 0,
                        ObjFingerprint = x.CodeValue,
                        Origin = ServiceCompare.Origin.MFNET
                    };
                    fingertoCompare.Add(fingerprintDTOToCompare);
                });

                var resulService = client.FingerprintComparerValidation(fingerprintDTO, fingertoCompare.ToArray(), false);
                resul.Message = resulService.ResponseMessage;
                resul.State = State.Success;
                resul.Finger = resulGetOneFinger.Finger;
            }


            return resul;
        }

        public ResulEnroll EnrollOneFinger()
        {
            ResulEnroll resul = new ResulEnroll();
            try
            {
                Program.InitFrmEnroll((string finger, byte[] img) =>
                {
                    resul.FingerEnroll = finger;
                    resul.img = img;
                });
                while (Program.processing)
                {
                    if (Program.processing && Program.IniProcess.AddSeconds(Program.timeoutCloseForm) < DateTime.Now)
                    {
                        resul.State = State.Warning;
                        resul.Message = "Cancelacion por timeout";
                        Program.processing = false;
                        Program.CloseFrm();
                    }
                    Thread.CurrentThread.Join(50);
                }
                Program.CloseFrm();
                resul.State = State.Success;
                resul.Message = "Huella enrolada correctamente";
            }
            catch (Exception ex)
            {
                resul.State = State.Error;
                resul.Message = "Error el obtner la huella";
                Logger.Write(ex.Message);
            }
            return resul;
        }

        public bool Init()
        {

            ReaderFinger.OpenReader();
            ReaderFinger.Release();
            return true;
        }
    }
}
