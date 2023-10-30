using Finger.Component.Component;
using Finger.Component.Util;
using FingerCaptureService.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace FingerCaptureService.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FingerCapture" in both code and config file together.
    public class FingerCapture : IFingerCapture
    {
        public ResulFinger GetOneFinger()
        {
            ResulFinger resul = new ResulFinger();
            try
            {
                resul.State = State.Success;
                resul.Message = "Huella obtenida correctamente";

                Program.InitGetOneFinger((List<string> resulCapture, string imageBase64) =>
                {
                    if (resulCapture == null)
                    {
                        resul.State = State.Warning;
                        resul.Image = imageBase64;
                        resul.Message = "Se cancelo la lectura de huella";
                    }
                    resul.Finger = resulCapture;
                    resul.Image = imageBase64;
                    Program.processing = false;
                });

                while (Program.processing)
                {
                    if (Program.processing && Program.IniProcess.AddSeconds(Program.timeoutCloseForm) < DateTime.Now)
                    {
                        resul.State = State.Warning;
                        resul.Message = "Cancelacion por timeout";
                        Program.processing = false;
                    }
                    Thread.CurrentThread.Join(50);
                }
                if (resul.Finger != null)
                {
                    resul.State = State.Success;
                    resul.Message = "Huella obtenida correctamente";
                }
                Program.CloseProcess();

            }
            catch (Exception ex)
            {
                resul.State = State.Error;
                resul.Message = "Error el obtener la huella";
                Logger.Write(ex.Message);
            }
            return resul;
        }


        public ResulEnroll EnrollOneFinger()
        {
            throw new NotImplementedException();
        }

        public bool Init()
        {

            ReaderFinger.OpenReader();
            ReaderFinger.Release();
            return true;
        }

        public bool FinishProcess()
        {
            Program.CloseProcess();
            return true;
        }
    }
}
