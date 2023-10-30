using HostFingerControl.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HostFingerControl.Service
{
    [ServiceContract]
    public interface IFingerComponentService
    {
        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetOneFinger")]
        ResulFinger GetOneFinger();

        [OperationContract]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, UriTemplate = "/TestFingers")]
        ResulFinger TestFingers(long IdPerson);

        /*[OperationContract]
        ResulEnroll EnrollOneFinger();*/

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Init")]
        bool Init();

    }
}
