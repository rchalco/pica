using Foundation.Stone.Application.Wrapper;
using Interop.Main.Cross.Domain.FingerPrint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RuntimeFingerPrint.Services
{
    [ServiceContract]
    public interface IFingerPrintServices
    {
        [OperationContract]
        Response CaptureFinger(int timeOut);

        [OperationContract]
        Response GetStatus();

        [OperationContract]
        Response CaptureFingerForEnroll(FingerContract.TypeEnroll typeEnroll, int timeOut);

        [OperationContract]
        ResponseObject<string> Enroll(FingerContract.TypeEnroll typeEnroll, List<string> fingersToEnrroll);
    }
}
