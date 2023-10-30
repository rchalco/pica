using Foundation.Stone.Application.Module;
using Foundation.Stone.Application.Wrapper;
using Interop.Main.Cross.Domain.FingerPrint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.Main.Service.Interface
{
    [ModuleStone(FullName = "RuntimeFingerPrint.Services.FingerPrintServices")]
    public interface IFingerPrintInterop
    {
        Response CaptureFinger(int timeOut);
        Response GetStatus();
        Response CaptureFingerForEnroll(FingerContract.TypeEnroll typeEnroll, int timeOut);
        ResponseObject<string> Enroll(FingerContract.TypeEnroll typeEnroll, List<string> fingersToEnrroll);
    }
}
