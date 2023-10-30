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
    public class ENDESearchData : BaseRequest
    {
        [DataMember]
        public DTOENDESearchData objSearchData { get; set; }
    }
    [DataContract]
    public class DTOENDESearchData : DTOBaseSearchData
    {
        [DataMember]
        public string CustomerCode { get; set; }

        [DataMember]
        public string VerifyCode { get; set; }

        [DataMember]
        public string SupplyCode { get; set; }
    }
    [DataContract]
    public class ENDESearchResult : BaseRequest
    {
        [DataMember]
        public ResponseObject<DTOENDESearchResult> EndeObtainOperatingDebtBalanceResult { get; set; }
    }
    [DataContract]
    public class DTOENDESearchResult
    {
        [DataMember]
        public decimal TotalToPay { get; set; }

        [DataMember]
        public string CustomerData { get; set; }

        [DataMember]
        public string CustomerAddress { get; set; }

        [DataMember]
        public List<DTOENDEDebtDetail> ColDebtDetail { get; set; }
    }

    [DataContract]
    public class DTOENDEDebtDetail
    {
        [DataMember]
        public string DebtNumber { get; set; }

        [DataMember]
        public string DebtCode { get; set; }

        [DataMember]
        public DateTime EndDebtDate { get; set; }

        [DataMember]
        public String EndDebtDateString { get; set; }

        [DataMember]
        public short DebtYear { get; set; }

        [DataMember]
        public short MonthDebt { get; set; }

        [DataMember]
        public decimal TotalElectricity { get; set; }

        [DataMember]
        public decimal TotalOtherRates { get; set; }

        [DataMember]
        public decimal TotalDebt { get; set; }

        [DataMember]
        public int OrderDebt { get; set; }

        [DataMember]
        public string DebtTextSerial { get; set; }
    }

    [DataContract]
    public class ENDEPaymentData : BaseRequest
    {
        [DataMember]
        public DTOENDEPaymentData objPaymentData { get; set; }
    }

    [DataContract]
    public class DTOENDEPaymentData : DTOExternalServiceBaseData
    {
        [DataMember]
        public string CustomerCode { get; set; }

        [DataMember]
        public string Supply { get; set; }

        [DataMember]
        public string DavCode { get; set; }

        [DataMember]
        public List<DTOENDEDebtPaymentDetail> ColPaymentsDetail { get; set; }
    }

    [DataContract]
    public class DTOENDEDebtPaymentDetail
    {
        [DataMember]
        public string DebtCode { get; set; }

        [DataMember]
        public String EndDebtDateString { get; set; }

        [DataMember]
        public decimal TotalDebt { get; set; }

        [DataMember]
        public int OrderDebt { get; set; }

        [DataMember]
        public string DebtTextSerial { get; set; }
    }

    [DataContract]
    public class EndePaymentResult : BaseRequest
    {
        [DataMember]
        public ResponseObject<DTOENDEPaymentResult> EndePaymentProcessResult { get; set; }
    }

    [DataContract]
    public class DTOENDEPaymentResult : DTOExternalServiceBaseResult
    {
        [DataMember]
        public string PaymentCodeCorrelative { get; set; }

        [DataMember]
        public string AuthenticateCode { get; set; }

        [DataMember]
        public string PaymentDate { get; set; }

        [DataMember]
        public string PaymentHourDate { get; set; }
    }
}
