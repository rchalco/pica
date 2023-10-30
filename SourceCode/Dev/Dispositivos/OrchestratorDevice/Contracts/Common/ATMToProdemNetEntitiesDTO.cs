using Foundation.Stone.Application.Wrapper;
using OrchestratorDevice.Contracts.Base;
using OrchestratorDevice.Contracts.LoanFlow;
using OrchestratorDevice.Contracts.SavingAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Contracts.Common
{

    [DataContract]
    public class RequestGetPersonToProdemNet : BaseRequest
    {
        [DataMember]
        public RequestPersonToProdemNet ObjParameter { get; set; }
    }

    public class RequestPersonToProdemNet
    {
        [DataMember]
        public string IdentityCardNumber { get; set; }
    }

    [DataContract]
    public class ResponseGetPersonToProdemNet
    {
        [DataMember]
        public ResponseObject<WEBPersonClientATMData> GetWebClientDataForATMResult { get; set; }
    }



    [DataContract]
    public class WEBPersonClientATMData
    {
        [DataMember]
        public long IdWebPersonClient { get; set; }
        [DataMember]
        public long IdcState { get; set; }
    }


    [DataContract]
    public class ResponseProdemNetActivationProcessByCI
    {
        [DataMember]
        public ResponseObject<string> ProdemNetActivationProcessByCIResult { get; set; }
    }

    [DataContract]
    public class ResponseWebPersonDeviceWithActivationPending
    {
        [DataMember]
        public ResponseQuery<ItemForATM> GetWebPersonDeviceWithActivationPendingForATMResult { get; set; }
    }

    [DataContract]
    public class ResponseWebPersonDevice
    {
        [DataMember]
        public ResponseQuery<ItemForATM> GetWebPersonDeviceForATMResult { get; set; }
    }

    [DataContract]
    public class ItemForATM
    {
        [DataMember]
        public virtual long IdItem { get; set; }

        [DataMember]
        public virtual string Description { get; set; }

        [DataMember]
        public virtual bool WithKey { get; set; }

        [DataMember]
        public virtual string RegisterDate { get; set; }
    }


    [DataContract]
    public class ResponseInfoDeviceActivation
    {
        [DataMember]
        public ResponseObject<InfoDeviceActivation> GetWebPersonDeviceActivationCodeFromAtmResult { get; set; }
    }

    [DataContract]
    public class InfoDeviceActivation
    {
        [DataMember]
        public string DeviceDescription { get; set; }

        [DataMember]
        public string ActivationCode { get; set; }

        [DataMember]
        public string QRCodeImage { get; set; }
    }

    [DataContract]
    public class ResponseInfoDeviceInactivation
    {
        [DataMember]
        public ResponseObject<string> WebPersonDeviceInactiveFromATMResult { get; set; }
    }


    [DataContract]
    public class ResponseResetPIN
    {
        [DataMember]
        public Response ATMResetProdemKeyPINResult { get; set; }
    }


    [DataContract]

    public class NewProdemNetCustomer
    {
        [DataMember]
        public string IdPerson { get; set; }

        [DataMember]
        public string IdentityCardNumber { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public List<string> ColPhones { get; set; }
    }


    [DataContract]
    public class ResponseAtmGetNewCustomerData
    {
        [DataMember]
        public ResponseObject<NewProdemNetCustomer> AtmGetNewCustomerDataByCIResult { get; set; }
    }

    [DataContract]
    public class ResponseNewUserSolicitation
    {
        [DataMember]
        public ResponseObject<string> CreateExternalWebPersonClientFromATMResult { get; set; }
    }
}
