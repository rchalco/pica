using Foundation.Stone.Application.Wrapper;
using OrchestratorDevice.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Contracts.ExternalServices
{
    [DataContract]
    public class BasicSearchData : BaseRequest
    {
        [DataMember]
        public string BaseParam { get; set; }
    }

    [DataContract]
    public class ExternalEnableServicesResult : BaseRequest
    {
        [DataMember]
        public ResponseObject<List<long>> GetEnableServicesForMobileResult { get; set; }
    }

    [DataContract]
    public class DTOBaseSearchData
    {
        [DataMember]
        public long ExternalModule { get; set; }

        [DataMember]
        public int OriginType { get; set; }

        [DataMember]
        public bool IsMobileAPP { get; set; }
    }

    [DataContract]
    public class DTOExternalServiceBaseData
    {
        [DataMember]
        public virtual string InvoiceNIT { get; set; }

        [DataMember]
        public virtual string InvoiceName { get; set; }

        [DataMember]
        public virtual string InvoiceCustomerEmail { get; set; }

        [DataMember]
        public virtual string InvoiceDocumentComplement { get; set; }

        [DataMember]
        public virtual string InvoiceDocumentType { get; set; }

        [DataMember]
        public virtual string InvoicePhone { get; set; }

        [DataMember]
        public virtual long IdExternalSystem { get; set; }

        [DataMember]
        public virtual string SearchData { get; set; }

        [DataMember]
        public virtual long IdMoney { get; set; }

        [DataMember]
        public virtual long IdUser { get; set; }

        [DataMember]
        public virtual string MFServiceCode { get; set; }

        [DataMember]
        public virtual string ExternalSystemPaymentID { get; set; }

        [DataMember]
        public virtual decimal TotalToPay { get; set; }

        [DataMember]
        public long? IdWebClient { get; set; }

        [DataMember]
        public bool IsMobile { get; set; }

        [DataMember]
        public long? IdSavingAccount { get; set; }

        [DataMember]
        public bool CreateInvoicingPendding { get; set; }

        [DataMember]
        public string CodeAuthentication { get; set; }

        [DataMember]
        public long ExternalModule { get; set; }

        [DataMember]
        public long SavingAccountOperation { get; set; }
    }

    [DataContract]
    public class DTOExternalServiceBaseResult
    {
        [DataMember]
        public virtual long IdTransaction { get; set; }

        [DataMember]
        public virtual long IdExternalMovement { get; set; }

        [DataMember]
        public string ReportString { get; set; }

        [DataMember]
        public string ReportName { get; set; }
    }
}
