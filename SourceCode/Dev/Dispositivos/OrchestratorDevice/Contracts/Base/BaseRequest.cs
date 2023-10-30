using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Contracts.Base
{
    [DataContract]
    public abstract class BaseRequest
    {
        [DataMember]
        public string Token { get; set; }
    }
}
