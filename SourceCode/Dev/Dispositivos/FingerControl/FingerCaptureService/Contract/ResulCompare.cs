using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FingerCaptureService.Contract
{
    public enum State
    {
        None = 0,
        Success = 1,
        Error = 2,
        Warning = 3
    }

    [DataContract]
    public class ResulFinger
    {
        [DataMember]
        public State State { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public List<string> Finger { get; set; }

        [DataMember]
        public string Image { get; set; }

    }

    [DataContract]
    public class ResulEnroll
    {
        [DataMember]
        public State State { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string FingerEnroll { get; set; }

        [DataMember]
        public byte[] img { get; set; }

    }
}
