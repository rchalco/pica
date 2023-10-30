using OrchestratorDevice.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Contracts.SavingAccount
{
    [DataContract]
    public class RequestAuthentication : BaseRequest
    {
        [DataMember]
        public Credential credential { get; set; }
    }
    [DataContract]
    public class Credential
    {
        [DataMember]
        public string User { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int Channel { get; set; }
        [DataMember]
        public List<AditionalItem> AditionalItems { get; set; }
    }
    [DataContract]
    public class AditionalItem
    {
        [DataMember]
        public string GroupName { get; set; }
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public string Value { get; set; }
    }
}
