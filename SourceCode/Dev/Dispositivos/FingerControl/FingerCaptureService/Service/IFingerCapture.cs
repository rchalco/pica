using FingerCaptureService.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace FingerCaptureService.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFingerCapture" in both code and config file together.
    [ServiceContract]
    public interface IFingerCapture
    {
        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetOneFinger")]
        ResulFinger GetOneFinger();

        [OperationContract]
        ResulEnroll EnrollOneFinger();

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Init")]
        bool Init();

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, UriTemplate = "/FinishProcess")]
        bool FinishProcess();
    }
}
